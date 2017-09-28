// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "SkyLightModel.h"


// Sets default values
ASkyLightModel::ASkyLightModel()
{
	this->SphereComponent->SetVisibility(false, true);
	this->SkyLight = CreateDefaultSubobject<USkyLightComponent>(TEXT("SkyLight"));
	SkyLight->SourceType = ESkyLightSourceType::SLS_CapturedScene;
	SkyLight->SetMobility(EComponentMobility::Movable);
	SkyLight->SetLightColor(FLinearColor(1.f, 1.f, 1.f, 1.f));
	SkyLight->SetCastShadows(true);
	SkyLight->CastStaticShadows = false;
	SkyLight->CastDynamicShadows = true;
	SkyLight->IndirectLightingIntensity = 3.f;
	SkyLight->SetIntensity(2.f);
}

void ASkyLightModel::Create(FModelData data)
{
	SkyLight->SetIntensity(data.intensity);
	SkyLight->SetLightColor(FLinearColor(data.red, data.green, data.blue));
}

