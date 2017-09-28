// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/StaticModel.h"

#include "GameFramework/Actor.h"
#include "ColumnModel.generated.h"

UCLASS()
class ARCHIVA_API AColumnModel : public AStaticModel
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AColumnModel();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;
	
};
