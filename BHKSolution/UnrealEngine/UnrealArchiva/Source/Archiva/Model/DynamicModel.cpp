// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "DynamicModel.h"


// Sets default values
ADynamicModel::ADynamicModel()
{
	//UE_LOG(MyLog, Log, TEXT("ADynamicModel()"));
	model = CreateDefaultSubobject<UProceduralMeshComponent>(TEXT("Dynamic"));
	model->SetupAttachment(RootComponent);
	//model->AttachParent = RootComponent;
	//this->model->AttachToComponent(RootComponent, FAttachmentTransformRules::KeepWorldTransform);
	UVratio = 100;
}

void ADynamicModel::Create(FModelData data)
{
	//UE_LOG(MyLog, Log, TEXT("ADynamicModel::Create(FModelData data)"));
	model->ClearAllMeshSections();
	SavedMaterial = data.materials;

	FVector square[4];
	square[0] = FVector(0, 0, 0);
	square[1] = FVector(0, 0, data.size.Z);
	square[2] = FVector(data.size.X, 0, data.size.Z);
	square[3] = FVector(data.size.X, 0, 0);

	InitVertices(4);

	Vertices[0] = square[0];
	Vertices[1] = square[1];
	Vertices[2] = square[2];
	Vertices[3] = square[3];
	UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 0, 1, 2, 3);

	CalculateUV(4, FVector(1,0,0));
	UKismetProceduralMeshLibrary::CalculateTangentsForMesh(Vertices, Triangles, UVs, Normals, Tangents);

	ApplyMaterial(data.materials);
	model->CreateMeshSection(0, Vertices, Triangles, Normals, UVs, TArray<FColor>(), Tangents, true);
}

void ADynamicModel::InitVertices(int32 vertices)
{
	Triangles.Reset();
	Vertices.Reset();
	UVs.Reset();
	Normals.Reset();
	Tangents.Reset();

	Vertices.AddUninitialized(vertices);
	UVs.AddUninitialized(vertices);
	Normals.AddUninitialized(vertices);
	Tangents.AddUninitialized(vertices);
}

void ADynamicModel::ApplyMaterial(TArray<FMaterialInst> mats)
{
	for (int i = 0; i<mats.Num(); i++)
	{
		if (mats[i].material)
		{
			for (auto& Part : MaterialPart)
			{
				if (Part.Value.Equals(mats[i].part))
				{
					model->SetMaterial(Part.Key, mats[i].material);
				}
			}
		}
		else
		{
			UE_LOG(MyLog, Error, TEXT("ADynamicModel::ApplyMaterial - material does not exist!"))
		}
	}
}

void ADynamicModel::CalculateUV(int32 vertNum, FVector normal)
{
	if (normal.X == 1 || normal.X == -1)
	{
		for (int i = 0; i < vertNum; i++) {
			UVs[i] = FVector2D(Vertices[i].Y / UVratio, Vertices[i].Z / UVratio);
		}
	}
	else if (normal.Y == 1 || normal.Y == -1)
	{
		for (int i = 0; i < vertNum; i++) {
			UVs[i] = FVector2D(Vertices[i].X / UVratio, Vertices[i].Z / UVratio);
		}
	}
	else if (normal.Z == 1 || normal.Z == -1)
	{
		for (int i = 0; i < vertNum; i++) {
			UVs[i] = FVector2D(Vertices[i].X / UVratio, Vertices[i].Y / UVratio);
		}
	}
	else
	{
		UE_LOG(MyLog, Error, TEXT("ADynamicModel::CalculateUV - Invalid Normal Value"))
	}
}

void ADynamicModel::CreateQuad(FVector v1, FVector v2, FVector v3, FVector v4, FString part, FVector normal)
{
	InitVertices(4);

	Vertices[0] = v1;
	Vertices[1] = v2;
	Vertices[2] = v3;
	Vertices[3] = v4;
	UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 0, 1, 2, 3);

	CalculateUV(4, normal);

	UKismetProceduralMeshLibrary::CalculateTangentsForMesh(Vertices, Triangles, UVs, Normals, Tangents);

	MaterialPart.Add(meshNum, part);

	model->CreateMeshSection(meshNum++, Vertices, Triangles, Normals, UVs, TArray<FColor>(), Tangents, true);
}

void ADynamicModel::ChangeMaterial(FString partName, UMaterialInterface* path)
{
	bool isChanged = false;
	for(int i=0; i<SavedMaterial.Num(); i++)
	{
		if(SavedMaterial[i].part.Equals(partName))
		{
			SavedMaterial[i].path = path->GetPathName();
			SavedMaterial[i].material = path;
			isChanged = true;
			break;
		}
	}
	if (!isChanged) {UE_LOG(MyLog, Error, TEXT("DynamicModel::ChangeMaterial - part name does not exist!"))}

	ApplyMaterial(SavedMaterial);
}