// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "HollowedMansardRoofModel.generated.h"

UCLASS()
class ARCHIVA_API AHollowedMansardRoofModel : public ADynamicModel
{
	GENERATED_BODY()

protected:
	float externMainRafter;
	float externSideRafter;
	float internMainRafter;
	float internSideRafter;
	float roofHeight;

public:	
	// Sets default values for this actor's properties
	AHollowedMansardRoofModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;
	
};
