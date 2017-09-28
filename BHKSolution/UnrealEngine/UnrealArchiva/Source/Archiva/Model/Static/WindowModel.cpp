// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "WindowModel.h"


// Sets default values
AWindowModel::AWindowModel()
{
}

// Called when the game starts or when spawned
void AWindowModel::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AWindowModel::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void AWindowModel::Create(FModelData data)
{
	//UE_LOG(MyLog, Log, TEXT("AWindowModel::Create(FModelData data)"));
	if (!data.meshPath.IsEmpty())
	{
		ConstructorHelpers::FObjectFinder<UStaticMesh> static_mesh(*data.meshPath);
		UStaticMesh* window_mesh = static_mesh.Object;
		if (window_mesh == NULL) { UE_LOG(MyLog, Error, TEXT("Unable to load Window's Static Mesh")); }

		model->SetStaticMesh(window_mesh);
	}
	
}

