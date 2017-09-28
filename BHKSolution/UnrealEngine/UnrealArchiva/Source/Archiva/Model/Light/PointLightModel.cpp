// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "PointLightModel.h"


// Sets default values
APointLightModel::APointLightModel()
{	
	//PointLight의 기본상태 설정
	this->SphereComponent->SetVisibility(false, true);
	this->PointLight = CreateDefaultSubobject<UPointLightComponent>(TEXT("PointLight"));
	PointLight->SetLightColor(FLinearColor(1.f, 1.f, 1.f, 1.f));
	PointLight->SetCastShadows(true);
	PointLight->CastStaticShadows = false;
	PointLight->CastDynamicShadows = true;
	PointLight->SetMobility(EComponentMobility::Stationary);
	PointLight->SetIntensity(2000.f);
	PointLight->SetupAttachment(RootComponent);
	
	this->Figure = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Figure"));
	this->Figure->SetupAttachment(RootComponent);
}

void APointLightModel::Create(FModelData data)
{
	PointLight->SetIntensity(data.intensity);
	PointLight->SetLightColor(FLinearColor(data.red, data.green, data.blue));

	if(!data.meshPath.IsEmpty()){
		ConstructorHelpers::FObjectFinder<UStaticMesh> static_mesh(*data.meshPath);
		UStaticMesh* figure_shape = static_mesh.Object;
		if (figure_shape == NULL) { UE_LOG(MyLog, Error, TEXT("Unable to load Point Light's Static Mesh")); return; }
		Figure->SetStaticMesh(figure_shape);
	}
}