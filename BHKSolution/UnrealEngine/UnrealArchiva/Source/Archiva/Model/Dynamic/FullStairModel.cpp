// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "FullStairModel.h"


// Sets default values
AFullStairModel::AFullStairModel()
{
	stepL = 30;
	stepW = 100;
	stepH = 20;

	numSteps = 10;

	foundation = 0;
}
void AFullStairModel::Create(FModelData data)
{
	model->ClearAllMeshSections();
	meshNum = 0;
	SavedMaterial = data.materials;
	//입력된 데이터 받기
	stepL = data.stepLength;
	stepW = data.stepWidth;
	stepH = data.stepHeight;

	numSteps = data.numSteps;

	foundation = data.addToFirstStep;

	if ( stepL <= 0 || stepW <= 0 || stepH <= 0 || numSteps <= 0 )
	{
		UE_LOG(MyLog, Error, TEXT("AFullStairModel::Create - Invalid Step Params"));
		return;
	}
	if(foundation > 0)
	{
		//계단 밑으로 지지대 생성
		FVector cordinate[8];
		cordinate[0] = FVector(0, stepW, -foundation);
		cordinate[1] = FVector(stepL * numSteps, stepW, -foundation);
		cordinate[2] = FVector(stepL * numSteps, stepW, 0);
		cordinate[3] = FVector(0, stepW, 0);

		cordinate[4] = FVector(0, 0, -foundation);
		cordinate[5] = FVector(stepL * numSteps, 0, -foundation);
		cordinate[6] = FVector(stepL * numSteps, 0, 0);
		cordinate[7] = FVector(0, 0, 0);

		//왼쪽 side
		CreateQuad(cordinate[4], cordinate[0], cordinate[3], cordinate[7], "side", FVector(-1, 0, 0));
		//위쪽 side
		CreateQuad(cordinate[3], cordinate[2], cordinate[6], cordinate[7], "side", FVector(0, 0, 1));
		//오른쪽 side
		CreateQuad(cordinate[1], cordinate[5], cordinate[6], cordinate[2], "side", FVector(1, 0, 0));
		//아래쪽 side
		CreateQuad(cordinate[4], cordinate[5], cordinate[1], cordinate[0], "side", FVector(0, 0, -1));
		//앞면
		CreateQuad(cordinate[0], cordinate[1], cordinate[2], cordinate[3], "side", FVector(0, 1, 0));
		//뒷면
		CreateQuad(cordinate[7], cordinate[6], cordinate[5], cordinate[4], "side", FVector(0, -1, 0));
	}

	//계단생성
	for(int i=0; i<numSteps; i++)
	{
		this->CreateStep(i);
	}
	
	ApplyMaterial(data.materials);
}
void AFullStairModel::CreateStep(int32 stepNumber)
{
	float treadL = 5;
	float treadH = 5;
	
	float l = stepL * (numSteps - stepNumber);
	float w = stepW;
	float h = stepH * stepNumber;

	FVector cord[16];
	cord[0] = FVector(0, w, h);
	cord[1] = FVector(l - treadL, w, h);
	cord[2] = FVector(l - treadL, w, h + stepH - treadH);
	cord[3] = FVector(l, w, h + stepH - treadH);
	cord[4] = FVector(l, w, h + stepH);
	cord[5] = FVector(l - stepL - treadL, w, h + stepH);
	cord[6] = FVector(0, w, h + stepH);
	cord[7] = FVector(0, w, h + stepH - treadH);

	cord[8] = FVector(0, 0, h);
	cord[9] = FVector(l - treadL, 0, h);
	cord[10] = FVector(l - treadL, 0, h + stepH - treadH);
	cord[11] = FVector(l, 0, h + stepH - treadH);
	cord[12] = FVector(l, 0, h + stepH);
	cord[13] = FVector(l - stepL - treadL, 0, h + stepH);
	cord[14] = FVector(0, 0, h + stepH);
	cord[15] = FVector(0, 0, h + stepH - treadH);

	//아래 면
	CreateQuad(cord[8], cord[9], cord[1], cord[0], "side", FVector(0, 0, -1));
	//tread 아래 면
	CreateQuad(cord[10], cord[11], cord[3], cord[2], "step", FVector(0, 0, -1));
	//뒷 면
	CreateQuad(cord[8], cord[0], cord[6], cord[14], "side", FVector(-1, 0, 0));
	//앞 면
	CreateQuad(cord[9], cord[10], cord[2], cord[1], "side", FVector(1, 0, 0));
	//tread 앞 면
	CreateQuad(cord[11], cord[12], cord[4], cord[3], "step", FVector(1, 0, 0));
	//오른쪽
	CreateQuad(cord[0], cord[1], cord[2], cord[7], "side", FVector(0, 1, 0));
	//왼쪽
	CreateQuad(cord[15], cord[10], cord[9], cord[8], "side", FVector(0, -1, 0));
	if(numSteps == (stepNumber+1)) // 마지막 단일 경우
	{
		//윗 면
		CreateQuad(cord[6], cord[4], cord[12], cord[14], "step", FVector(0, 0, 1));
		//오른쪽
		CreateQuad(cord[7], cord[3], cord[4], cord[6], "step", FVector(0, 1, 0));
		//왼쪽
		CreateQuad(cord[14], cord[12], cord[11], cord[15], "step", FVector(0, -1, 0));
	}
	else
	{
		//tread 윗면
		CreateQuad(cord[5], cord[4], cord[12], cord[13], "step", FVector(0, 0, 1));
		//side 오른쪽
		CreateQuad(cord[7], FVector(cord[5].X, w, cord[7].Z), cord[5], cord[6], "side", FVector(0, 1, 0));
		//tread 오른쪽
		CreateQuad(FVector(cord[5].X, w, cord[7].Z), cord[3], cord[4], cord[5], "step", FVector(0, 1, 0));
		//side 왼쪽
		CreateQuad(cord[14], cord[13], FVector(cord[13].X, 0, cord[15].Z), cord[15], "side", FVector(0, -1, 0));
		//tread 왼쪽
		CreateQuad(cord[13], cord[12], cord[11], FVector(cord[13].X, 0, cord[15].Z), "step", FVector(0, -1, 0));
	}

}
