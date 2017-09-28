// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "HipRoofModel.generated.h"

UCLASS()
class ARCHIVA_API AHipRoofModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	float roofHeight;
	float sideRafter;
	float mainRafter;
	float ridgeBoard;

public:
	// Sets default values for this actor's properties
	AHipRoofModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

};
