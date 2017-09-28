// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "HollowStairModel.h"


// Sets default values
AHollowStairModel::AHollowStairModel()
{
 
}

void AHollowStairModel::Create(FModelData data)
{
	model->ClearAllMeshSections();
	meshNum = 0;
	SavedMaterial = data.materials;
	//입력된 데이터 받기
	stepL = data.stepLength;
	stepW = data.stepWidth;
	stepH = data.stepHeight;

	numSteps = data.numSteps;

	if (stepL <= 0 || stepW <= 0 || stepH <= 0 || numSteps <= 0)
	{
		UE_LOG(MyLog, Error, TEXT("AHollowStairModel::Create - Invalid Step Params"));
		return;
	}

	//발판 제작
	for(int i=0; i<numSteps; i++)
	{
		this->CreateStep(i);
	}

	//사이드 제작

	FVector cordS[8];
	cordS[0] = FVector(stepL * numSteps, stepW + 1, 0);
	cordS[1] = FVector(stepL * numSteps, stepW + 1, stepH);
	cordS[2] = FVector(stepL, stepW + 1, stepH * (numSteps -1));
	cordS[3] = FVector(stepL, stepW + 1, stepH * numSteps);

	cordS[4] = FVector(stepL * numSteps, -1, 0);
	cordS[5] = FVector(stepL * numSteps, -1, stepH);
	cordS[6] = FVector(stepL, -1, stepH * (numSteps - 1));
	cordS[7] = FVector(stepL, -1, stepH * numSteps);

	//오른쪽 앞면
	CreateQuad(cordS[0], cordS[1], cordS[2], cordS[3], "side", FVector(0, 1, 0));
	//오른쪽 뒷면
	CreateQuad(cordS[3], cordS[2], cordS[1], cordS[0], "side", FVector(0, -1, 0));
	//왼쪽 앞면
	CreateQuad(cordS[4], cordS[5], cordS[6], cordS[7], "side", FVector(0, 1, 0));
	//왼쪽 뒷면
	CreateQuad(cordS[7], cordS[6], cordS[5], cordS[4], "side", FVector(0, -1, 0));

	FVector cordB[8];
	cordB[0] = FVector(0, stepW + 1, stepH * (numSteps - 1));
	cordB[1] = FVector(stepL, stepW + 1, stepH * (numSteps - 1));
	cordB[2] = FVector(stepL, stepW + 1, stepH * numSteps);
	cordB[3] = FVector(0, stepW + 1, stepH * numSteps);

	cordB[4] = FVector(0, -1, stepH * (numSteps - 1));
	cordB[5] = FVector(stepL, -1, stepH * (numSteps - 1));
	cordB[6] = FVector(stepL, -1, stepH * numSteps);
	cordB[7] = FVector(0, -1, stepH * numSteps);

	//오른쪽 앞면
	CreateQuad(cordB[0], cordB[1], cordB[2], cordB[3], "side", FVector(0, 1, 0));
	//오른쪽 뒷면
	CreateQuad(cordB[3], cordB[2], cordB[1], cordB[0], "side", FVector(0, -1, 0));
	//왼쪽 앞면
	CreateQuad(cordB[4], cordB[5], cordB[6], cordB[7], "side", FVector(0, 1, 0));
	//왼쪽 뒷면
	CreateQuad(cordB[7], cordB[6], cordB[5], cordB[4], "side", FVector(0, -1, 0));

	ApplyMaterial(data.materials);
}

void AHollowStairModel::CreateStep(int32 stepNumber)
{
	float treadL = 5;
	float treadH = 5;

	float l = stepL * (numSteps - stepNumber);
	float w = stepW;
	float h = stepH * stepNumber;

	FVector cord[8];
	cord[0] = FVector(l - stepL - treadL, w, h + stepH - treadH);
	cord[1] = FVector(l, w, h + stepH - treadH);
	cord[2] = FVector(l, w, h + stepH);
	cord[3] = FVector(l - stepL - treadL, w, h + stepH);

	cord[4] = FVector(l - stepL - treadL, 0, h + stepH - treadH);
	cord[5] = FVector(l, 0, h + stepH - treadH);
	cord[6] = FVector(l, 0, h + stepH);
	cord[7] = FVector(l - stepL - treadL, 0, h + stepH);

	//왼쪽 side
	CreateQuad(cord[4], cord[0], cord[3], cord[7], "step", FVector(-1, 0, 0));
	//위쪽 side
	CreateQuad(cord[3], cord[2], cord[6], cord[7], "step", FVector(0, 0, 1));
	//오른쪽 side
	CreateQuad(cord[1], cord[5], cord[6], cord[2], "step", FVector(1, 0, 0));
	//아래쪽 side
	CreateQuad(cord[4], cord[5], cord[1], cord[0], "step", FVector(0, 0, -1));
	//앞면
	CreateQuad(cord[0], cord[1], cord[2], cord[3], "step", FVector(0, 1, 0));
	//뒷면
	CreateQuad(cord[7], cord[6], cord[5], cord[4], "step", FVector(0, -1, 0));
}
