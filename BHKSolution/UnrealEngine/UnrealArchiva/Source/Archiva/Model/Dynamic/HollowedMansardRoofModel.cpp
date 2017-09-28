// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "HollowedMansardRoofModel.h"


// Sets default values
AHollowedMansardRoofModel::AHollowedMansardRoofModel()
{
	externMainRafter = 300;
	externSideRafter = 200;
	internMainRafter = 100;
	internSideRafter = 100;
	roofHeight = 150;
}

void AHollowedMansardRoofModel::Create(FModelData data)
{
	//기본 세팅
	model->ClearAllMeshSections();
	meshNum = 0;
	SavedMaterial = data.materials;

	externMainRafter = data.mainRafter;
	externSideRafter = data.sideRafter;
	internMainRafter = data.internSideRafter;
	internSideRafter = data.internMainRafter;
	roofHeight = data.height;

	float mainInterval = (externMainRafter - internMainRafter) / 2;
	float sideInterval = (externSideRafter - internSideRafter) / 2;
	float helper = 10;

	FVector cordinate[12];

	//extern
	cordinate[0] = FVector(0,					0,					0);
	cordinate[1] = FVector(externMainRafter,	0,					0);
	cordinate[2] = FVector(externMainRafter,	externSideRafter,	0);
	cordinate[3] = FVector(0,					externSideRafter,	0);
	//intern
	cordinate[4] = FVector(mainInterval, sideInterval, helper);
	cordinate[5] = FVector(mainInterval + internMainRafter, sideInterval, helper);
	cordinate[6] = FVector(mainInterval + internMainRafter, sideInterval + internSideRafter, helper);
	cordinate[7] = FVector(mainInterval, sideInterval + internSideRafter, helper);
	//height
	cordinate[8] = FVector(mainInterval, sideInterval, roofHeight);
	cordinate[9] = FVector(mainInterval + internMainRafter, sideInterval, roofHeight);
	cordinate[10] = FVector(mainInterval + internMainRafter, sideInterval + internSideRafter, roofHeight);
	cordinate[11] = FVector(mainInterval, sideInterval + internSideRafter, roofHeight);

	//바닥
	CreateQuad(cordinate[0], cordinate[1], cordinate[2], cordinate[3], "bottom", FVector(0, 0, -1));

	//지붕
	CreateQuad(cordinate[0], cordinate[8], cordinate[9], cordinate[1], "roof", FVector(0, 1, 0));
	CreateQuad(cordinate[3], cordinate[2], cordinate[10], cordinate[11], "roof", FVector(0, -1, 0));
	CreateQuad(cordinate[3], cordinate[11], cordinate[8], cordinate[0], "roof", FVector(-1, 0, 0));
	CreateQuad(cordinate[1], cordinate[9], cordinate[10], cordinate[2], "roof", FVector(1, 0, 0));

	//내부바닥
	CreateQuad(cordinate[4], cordinate[7], cordinate[6], cordinate[5], "in", FVector(0, 0, 1));
	//내부벽
	CreateQuad(cordinate[5], cordinate[9], cordinate[8], cordinate[4], "in", FVector(0, -1, 0));
	CreateQuad(cordinate[8], cordinate[11], cordinate[7], cordinate[4], "in", FVector(-1, 0, 0));
	CreateQuad(cordinate[11], cordinate[10], cordinate[6], cordinate[7], "in", FVector(0, 1, 0));
	CreateQuad(cordinate[5], cordinate[6], cordinate[10], cordinate[9], "in", FVector(1, 0, 0));

	//material을 변경하는 함수
	ApplyMaterial(data.materials);

}
