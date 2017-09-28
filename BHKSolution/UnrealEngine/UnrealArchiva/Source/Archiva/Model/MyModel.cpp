// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "MyModel.h"


// Sets default values

AMyModel::AMyModel()
{
	PrimaryActorTick.bCanEverTick = true;
	//UE_LOG(MyLog, Log, TEXT("AMyModel::AMyModel()"));
	this->SphereComponent = CreateDefaultSubobject<USphereComponent>(TEXT("RootComponent"));
	this->RootComponent = SphereComponent;
}

void AMyModel::Create(FModelData data) {
	//UE_LOG(MyLog, Log, TEXT("AMyModel::Create(FModelData data)"));
}

