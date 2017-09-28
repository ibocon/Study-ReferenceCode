// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/MyModel.h"

#include "GameFramework/Actor.h"
#include "StaticModel.generated.h"

UCLASS()
class ARCHIVA_API AStaticModel : public AMyModel
{
	GENERATED_BODY()
	
public:	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Archiva|Model")
	UStaticMeshComponent* model;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Archiva|Model")
	TArray<UMaterialInterface*> SavedMaterial;
	// Sets default values for this actor's properties
	AStaticModel();
	
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	void SaveMaterials();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	void LoadMaterials();
	
};
