// Fill out your copyright notice in the Description page of Project Settings.


#include "Archiva.h"
#include "HipRoofModel.h"


// Sets default values
AHipRoofModel::AHipRoofModel()
{
	roofHeight = 100;
	sideRafter = 10;
	mainRafter = 30;
	ridgeBoard = 20;
}

void AHipRoofModel::Create(FModelData data)
{
	model->ClearAllMeshSections();
	meshNum = 0;
	SavedMaterial = data.materials;
	//data값을 입력받아 저장하는 과정
	roofHeight = data.height;
	sideRafter = data.sideRafter;
	mainRafter = data.mainRafter;
	ridgeBoard = data.ridgeBoard;

	float edgeRafter = (mainRafter - ridgeBoard) / 2.0f;
	float sideHeight = 10;
	float roofsideGap = 5;

	//roofHeight = roofHeight - sideHeight - roofsideGap;

	FVector cordinate[10];
	//side part cord
	
	cordinate[0] = FVector(-roofsideGap, -roofsideGap, 0);
	cordinate[1] = FVector(mainRafter + roofsideGap, -roofsideGap, 0);
	cordinate[2] = FVector(mainRafter + roofsideGap, -roofsideGap, sideHeight);
	cordinate[3] = FVector(-roofsideGap, -roofsideGap, sideHeight);

	cordinate[4] = FVector(-roofsideGap, sideRafter + roofsideGap, 0);
	cordinate[5] = FVector(mainRafter + roofsideGap, sideRafter + roofsideGap, 0);
	cordinate[6] = FVector(mainRafter + roofsideGap, sideRafter + roofsideGap, sideHeight);
	cordinate[7] = FVector(-roofsideGap, sideRafter + roofsideGap, sideHeight);

	//지붕 바닥
	CreateQuad(cordinate[0], cordinate[1], cordinate[5], cordinate[4], "side", FVector(0, 0, -1));
	//지붕 사이드
	//왼쪽
	CreateQuad(cordinate[7], cordinate[3], cordinate[0], cordinate[4], "side", FVector(-1, 0, 0));
	//오른쪽
	CreateQuad(cordinate[2], cordinate[6], cordinate[5], cordinate[1], "side", FVector(1, 0, 0));
	//앞쪽
	CreateQuad(cordinate[3], cordinate[2], cordinate[1], cordinate[0], "side", FVector(0, 1, 0));
	//뒤쪽
	CreateQuad(cordinate[4], cordinate[5], cordinate[6], cordinate[7], "side", FVector(0, -1, 0));
	//위쪽
	CreateQuad(cordinate[7], cordinate[6], cordinate[2], cordinate[3], "side", FVector(0, 0, 1));

	//roof part cord

	roofsideGap = 1;

	cordinate[2] = FVector(mainRafter, 0, sideHeight - roofsideGap);
	cordinate[3] = FVector(0, 0, sideHeight - roofsideGap);
	cordinate[6] = FVector(mainRafter, sideRafter, sideHeight - roofsideGap);
	cordinate[7] = FVector(0, sideRafter, sideHeight - roofsideGap);
	
	cordinate[8] = FVector(edgeRafter, sideRafter / 2, roofHeight + sideHeight / 2);
	cordinate[9] = FVector(mainRafter - edgeRafter, sideRafter / 2, roofHeight + sideHeight / 2);

	//지붕 y = -1
	CreateQuad(cordinate[7], cordinate[6], cordinate[9], cordinate[8], "roof", FVector(0, -1, 0));
	//지붕 y = 1
	CreateQuad(cordinate[3], cordinate[8], cordinate[9], cordinate[2], "roof", FVector(0, 1, 0));
	//지붕 x = -1
	CreateQuad(cordinate[7], cordinate[8], cordinate[3], cordinate[7], "roof", FVector(-1, 0, 0));
	//지붕 x = 1
	CreateQuad(cordinate[2], cordinate[9], cordinate[6], cordinate[2], "roof", FVector(1, 0, 0));

	//material을 변경하는 함수
	ApplyMaterial(data.materials);
}