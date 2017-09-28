// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "SpotLightModel.h"


// Sets default values
ASpotLightModel::ASpotLightModel()
{
	this->SpotLight = CreateDefaultSubobject<USpotLightComponent>(TEXT("SpotLight"));
	SpotLight->SetLightColor(FLinearColor(1.f, 1.f, 1.f, 1.f));
	SpotLight->SetCastShadows(true);
	SpotLight->CastStaticShadows = false;
	SpotLight->CastDynamicShadows = true;
	SpotLight->SetMobility(EComponentMobility::Movable);
	SpotLight->SetIntensity(5000.f);
	SpotLight->SetupAttachment(RootComponent);

}

void ASpotLightModel::Create(FModelData data)
{
	SpotLight->SetIntensity(data.intensity);
	SpotLight->SetLightColor(FLinearColor(data.red, data.green, data.blue));
	SpotLight->InnerConeAngle = data.innerCone;
	SpotLight->OuterConeAngle = data.outerCone;
}

