// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "FloorModel.h"


// Sets default values
AFloorModel::AFloorModel()
{
}

void AFloorModel::Create(FModelData data)
{
	//UE_LOG(MyLog, Log, TEXT("AFloorModel::Create(FModelData data) - start"));
	model->ClearAllMeshSections();
	
	meshNum = 0;
	SavedMaterial = data.materials;

	floor = TArray<FVector>(data.vertices);
	//UE_LOG(MyLog, Log, TEXT("AFloorModel::Create(FModelData data) - floor num = %d"), floor.Num());

	this->Extrude(floor, data.move);

	ApplyMaterial(data.materials);

}

void AFloorModel::Extrude(TArray<FVector> bottom, FVector m)
{
	int32 vertNum = bottom.Num();
	TArray<FVector> top;
	top.AddUninitialized(vertNum);

	//top좌표를 계산
	for (int i = 0; i < vertNum; i++) {
		top[i] = FVector(bottom[i].X + m.X, bottom[i].Y + m.Y, bottom[i].Z + m.Z);
	}
	/*Create Side*/
	for (int i = 0; i < vertNum; i++) {
		//TODO: X 또는 Y값 중 어느 것이 고정되었는지 파악하여 Material을 입혀야 함.
		if(bottom[i].X == bottom[(i + 1) % vertNum].X)
		{
			CreateQuad(bottom[i], bottom[(i + 1) % vertNum], top[(i + 1) % vertNum], top[i], "side", FVector(1, 0, 0));
		}
		else if(bottom[i].Y == bottom[(i + 1) % vertNum].Y)
		{
			CreateQuad(bottom[i], bottom[(i + 1) % vertNum], top[(i + 1) % vertNum], top[i], "side", FVector(0, 1, 0));
		}
		else { UE_LOG(MyLog, Log, TEXT("AFloorModel::Extrude - calculation problem!")); }
		
	}
	/*Bottom & Top*/
	TArray<FVector> registered;
	for(int i=0; i<bottom.Num(); i++)
	{
		TArray<FVector> square = FindPosSqud(bottom[i]);
		//만약 가능한 사각형을 찾지 못했을 경우, 다음 좌표를 검토한다.
		if (square.Num() == 0 || (registered.Contains(square[0]) && registered.Contains(square[1]) && registered.Contains(square[2]) && registered.Contains(square[3]))) continue;
		//사각형이 내부에 존재하는지 확인하는 절차
		FVector mid = FVector((square[0].X+square[2].X)/2, (square[0].Y + square[2].Y) / 2, square[0].Z);
		if(isInside(mid))
		{
			//bottom생성
			CreateQuad(square[0], square[1], square[2], square[3], "bottom", FVector(0,0,-1));
			//top생성
			CreateQuad(
				FVector(square[3].X + m.X, square[3].Y + m.Y, square[3].Z + m.Z),
				FVector(square[2].X + m.X, square[2].Y + m.Y, square[2].Z + m.Z),
				FVector(square[1].X + m.X, square[1].Y + m.Y, square[1].Z + m.Z),
				FVector(square[0].X + m.X, square[0].Y + m.Y, square[0].Z + m.Z),
				"top", FVector(0,0,1));
			//생성된 사각형의 좌표를 등록하여 다시 검토하지 않도록 한다.
			registered.AddUnique(square[0]);
			registered.AddUnique(square[1]);
			registered.AddUnique(square[2]);
			registered.AddUnique(square[3]);
			/*
			UE_LOG(MyLog, Log, TEXT("AFloorModel::Extrude - registered points ["));
			for (int b=0; b<registered.Num(); b++)
			{
				UE_LOG(MyLog, Log, TEXT("AFloorModel::Extrude - %d = %f %f %f "),b , registered[i].X, registered[i].Y, registered[i].Z);
			}
			UE_LOG(MyLog, Log, TEXT("AFloorModel::Extrude - ]"));
			*/
		}
	}
}

TArray<FVector> AFloorModel::FindPosSqud(FVector std)
{
	TArray<FVector> square;

	FVector up = FindPoint(std, FVector2D(0, 1));
	FVector right = FindPoint(std, FVector2D(1, 0));
	if(!up.Equals(std) && !right.Equals(std))
	{
		square.Add(std);
		square.Add(right);
		square.Add(FVector(right.X, up.Y, std.Z));
		square.Add(up);
		/*
		UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPosSqud - ["));
		UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPosSqud -		square = %f %f %f "), square[0].X, square[0].Y, square[0].Z);
		UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPosSqud -		square = %f %f %f "), square[1].X, square[1].Y, square[1].Z);
		UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPosSqud -		square = %f %f %f "), square[2].X, square[2].Y, square[2].Z);
		UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPosSqud -		square = %f %f %f "), square[3].X, square[3].Y, square[3].Z);
		UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPosSqud -	]"));
		*/
	}

	return square;
}

FVector AFloorModel::FindPoint(FVector std, FVector2D direction)
{
	FVector found = FVector(std);
	bool changed = false;
	//상
	if(direction.Y == 1)
	{
		for(int i=0; i<floor.Num(); i++)
		{
			if((floor[i].X == std.X) && (floor[i].Y > std.Y))
			{
				if (changed && found.Y > floor[i].Y)
				{
					found = FVector(floor[i]);
				}
				else if (!changed)
				{
					found = FVector(floor[i]);
					changed = true;
				}				
			}
		}
		//UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPoint - Top = %f %f %f "), found.X, found.Y, found.Z);
	}
	//하
	/*
	else if(direction.Y == -1)
	{
		for (int i = 0; i<floor.Num(); i++)
		{
			if ((floor[i].X == std.X) && (floor[i].Y < std.Y) && (found.Y < floor[i].Y))
			{
				found = FVector(floor[i]);
			}
		}
	}
	
	//좌
	else if (direction.X == -1)
	{
		for (int i = 0; i<floor.Num(); i++)
		{
			if ((floor[i].Y == std.Y) && (floor[i].X < std.X) && (found.X < floor[i].X))
			{
				found = FVector(floor[i]);
			}
		}
	}
	*/
	//우
	else if (direction.X == 1)
	{
		for (int i = 0; i<floor.Num(); i++)
		{
			if ((floor[i].Y == std.Y) && (floor[i].X > std.X))
			{
				if(changed && floor[i].X < found.X )
				{
					found = FVector(floor[i]);
				}
				else if(!changed)
				{
					found = FVector(floor[i]);
					changed = true;
				}
					
			}
		}
		//UE_LOG(MyLog, Log, TEXT("AFloorModel::FindPoint - Right = %f %f %f "), found.X, found.Y, found.Z);
	}

	return found;
}

//TODO: 개선이 필요하다
bool AFloorModel::isInside(FVector mid)
{
	int countX = 0;

	for(int i=0; i<floor.Num(); i++)
	{
		FVector p1 = floor[i];
		FVector p2 = floor[(i + 1) % floor.Num()];

		if (p1.X != p2.X || mid.X < p1.X) continue;

		if((mid.Y < p1.Y && mid.Y > p2.Y) || (mid.Y > p1.Y && mid.Y < p2.Y))
		{
			countX++;
		}
	}

	if ((countX % 2) == 0) return false;
	else return true;

}

