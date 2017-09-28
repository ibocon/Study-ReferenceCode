// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Archiva.h"
#include "Data/Modeldata.h"

FMaterialInst::FMaterialInst()
{
	part = "";
	path = "";
	material = nullptr;
	//target->material = Cast<UMaterialInterface>(StaticLoadObject(UMaterialInterface::StaticClass(), NULL, *target->path));
}


FHole::FHole()
{
	position = FVector2D(0, 0);
	length = 0;
	height = 0;
}

FModelData::FModelData()
{
	name = "None";
	meshType = "None";

	placement = FTransform();
	placement.SetScale3D(FVector(1));

	meshPath = "";

	size = FVector(100);

	frame = 0;

	vertices.Reset();

	holes.Reset();

	pitch = 0;
	interval = 50;
	height = 100;

	intensity = 2000.f;
	red = 1.f;
	green = 1.f;
	blue = 1.f;

	innerCone = 0.f;
	outerCone = 44.f;

	stepLength = 30;
	stepWidth = 100;
	stepHeight = 20;
	numSteps = 10;
	addToFirstStep = 0;

	CameraNumber = 0;

	sideRafter = 10;
	mainRafter = 30;
	ridgeBoard = 20;

}
