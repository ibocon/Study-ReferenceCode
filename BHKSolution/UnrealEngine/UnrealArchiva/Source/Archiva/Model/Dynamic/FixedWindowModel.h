// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "FixedWindowModel.generated.h"

UCLASS()
class ARCHIVA_API AFixedWindowModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	float length;
	float width;
	float height;

	float frame;

	FVector cordFrameOut[8];
	FVector cordFrameIn[8];

	//frame, glass

public:	
	AFixedWindowModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;
	
};
