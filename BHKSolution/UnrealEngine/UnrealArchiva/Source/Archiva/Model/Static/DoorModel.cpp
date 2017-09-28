// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "DoorModel.h"


// Sets default values
ADoorModel::ADoorModel()
{
}

// Called when the game starts or when spawned
void ADoorModel::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ADoorModel::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void ADoorModel::Create(FModelData data)
{
	ConstructorHelpers::FObjectFinder<UStaticMesh> static_mesh(*data.meshPath);
	UStaticMesh* door_mesh = static_mesh.Object;
	if (door_mesh == NULL) { UE_LOG(MyLog, Error, TEXT("Unable to load door's Static Mesh")); }

	model->SetStaticMesh(door_mesh);
}

