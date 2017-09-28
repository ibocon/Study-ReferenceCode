// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "FullStairModel.generated.h"

UCLASS()
class ARCHIVA_API AFullStairModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	float stepL;
	float stepW;
	float stepH;

	int32 numSteps;

	float foundation;

	//Material을 넣기 위한 지정값
	//side, step

public:	
	// Sets default values for this actor's properties
	AFullStairModel();
	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;

	void CreateStep(int32 stepNumber);
};
