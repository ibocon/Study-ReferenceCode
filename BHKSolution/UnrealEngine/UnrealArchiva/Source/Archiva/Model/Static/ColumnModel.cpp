// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "ColumnModel.h"


// Sets default values
AColumnModel::AColumnModel()
{
}

// Called when the game starts or when spawned
void AColumnModel::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AColumnModel::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void AColumnModel::Create(FModelData data)
{
	ConstructorHelpers::FObjectFinder<UStaticMesh> static_mesh(*data.meshPath);
	UStaticMesh* column_mesh = static_mesh.Object;
	if (column_mesh == NULL) { UE_LOG(MyLog, Error, TEXT("Unable to load column's Static Mesh")); }

	model->SetStaticMesh(column_mesh);
}

