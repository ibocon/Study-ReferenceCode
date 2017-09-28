// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "WallModel.generated.h"

UCLASS()
class ARCHIVA_API AWallModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	FVector cordOutline[8];
	FVector cordHole[8];

	float length;
	float width;
	float height;

	//in, out, side

public:

	// Sets default values for this actor's properties
	AWallModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;
	
	void MakeHole(FHole hole);
	void MakeConnection(FHole pre, FHole post);

};
