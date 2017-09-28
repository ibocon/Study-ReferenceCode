// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "HollowStairModel.generated.h"

UCLASS()
class ARCHIVA_API AHollowStairModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	float stepL;
	float stepW;
	float stepH;

	int32 numSteps;

	//side, step

public:	
	// Sets default values for this actor's properties
	AHollowStairModel();

	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;

	void CreateStep(int32 stepNumber);
};
