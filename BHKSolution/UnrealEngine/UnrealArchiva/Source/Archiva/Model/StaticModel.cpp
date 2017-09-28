// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "StaticModel.h"


// Sets default values
AStaticModel::AStaticModel()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = false;
	//UE_LOG(MyLog, Log, TEXT("AStaticModel::AStaticModel()"));
	this->model = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Static"));
	this->model->SetupAttachment(RootComponent);
	//this->model->AttachToComponent(RootComponent, FAttachmentTransformRules::KeepWorldTransform);
}

// Called when the game starts or when spawned
void AStaticModel::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AStaticModel::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );
}

void AStaticModel::Create(FModelData data)
{
	//UE_LOG(MyLog, Log, TEXT("AStaticModel::Create(FModelData data)"));
	UStaticMesh* cube_shape = Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(), NULL, *data.meshPath));
	if (cube_shape == NULL) { UE_LOG(MyLog, Error, TEXT("Unable to load Cube's Static Mesh")); return; }
	model->SetStaticMesh(cube_shape);
}

void AStaticModel::SaveMaterials()
{
	SavedMaterial = model->GetMaterials();
}

void AStaticModel::LoadMaterials()
{
	for(int i=0; i<SavedMaterial.Num(); i++)
	{
		model->SetMaterial(i, SavedMaterial[i]);
	}
}
