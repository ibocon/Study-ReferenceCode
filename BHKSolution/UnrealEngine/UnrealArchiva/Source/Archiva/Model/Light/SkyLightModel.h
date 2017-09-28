// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/MyModel.h"

#include "GameFramework/Actor.h"
#include "SkyLightModel.generated.h"

UCLASS()
class ARCHIVA_API ASkyLightModel : public AMyModel
{
	GENERATED_BODY()
protected:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Custom|LightComp")
	USkyLightComponent* SkyLight;

public:	
	// Sets default values for this actor's properties
	ASkyLightModel();

	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;
	
};
