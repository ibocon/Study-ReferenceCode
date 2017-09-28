// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "FlatRoofModel.generated.h"

UCLASS()
class ARCHIVA_API AFlatRoofModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	FVector size;

public:
	// Sets default values for this actor's properties
	AFlatRoofModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

};