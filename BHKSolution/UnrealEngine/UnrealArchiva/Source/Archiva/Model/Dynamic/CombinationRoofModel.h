// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "CombinationRoofModel.generated.h"

UCLASS()
class ARCHIVA_API ACombinationRoofModel : public ADynamicModel
{
	GENERATED_BODY()

public:
	// Sets default values for this actor's properties
	ACombinationRoofModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

};
