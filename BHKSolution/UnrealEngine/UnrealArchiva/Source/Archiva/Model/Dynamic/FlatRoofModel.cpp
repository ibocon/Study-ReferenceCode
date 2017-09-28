// Fill out your copyright notice in the Description page of Project Settings.


#include "Archiva.h"
#include "FlatRoofModel.h"


// Sets default values
AFlatRoofModel::AFlatRoofModel()
{
	//Flat Roof
	size = FVector(100, 100, 20);
}

void AFlatRoofModel::Create(FModelData data)
{
	model->ClearAllMeshSections();
	meshNum = 0;
	SavedMaterial = data.materials;

	size = data.size;

	FVector cordinate[8];

	cordinate[0] = FVector(0, 0, 0);
	cordinate[1] = FVector(size.X, 0, 0);
	cordinate[2] = FVector(size.X, 0, size.Z);
	cordinate[3] = FVector(0, 0, size.Z);

	cordinate[4] = FVector(0, size.Y, 0);
	cordinate[5] = FVector(size.X, size.Y, 0);
	cordinate[6] = FVector(size.X, size.Y, size.Z);
	cordinate[7] = FVector(0, size.Y, size.Z);
	
	//위쪽
	CreateQuad(cordinate[3], cordinate[7], cordinate[6], cordinate[2], "roof", FVector(0, 0, 1));
	//아랫쪽
	CreateQuad(cordinate[1], cordinate[5], cordinate[4], cordinate[0], "bottom", FVector(0, 0, -1));
	//왼쪽
	CreateQuad(cordinate[0], cordinate[4], cordinate[7], cordinate[3], "roof", FVector(-1, 0, 0));
	//오른쪽
	CreateQuad(cordinate[1], cordinate[2], cordinate[6], cordinate[5], "roof", FVector(1, 0, 0));
	//앞쪽
	CreateQuad(cordinate[0], cordinate[3], cordinate[2], cordinate[1], "roof", FVector(0, 1, 0));
	//뒷쪽
	CreateQuad(cordinate[5], cordinate[6], cordinate[7], cordinate[4], "roof", FVector(0, -1, 0));

	//material을 변경하는 함수
	ApplyMaterial(data.materials);
}

