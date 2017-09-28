// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "SlideWindowModel.generated.h"

UCLASS()
class ARCHIVA_API ASlideWindowModel : public ADynamicModel
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
	// Sets default values for this actor's properties
	ASlideWindowModel();
	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;

	void MakeWindow(FVector2D pos);

};
