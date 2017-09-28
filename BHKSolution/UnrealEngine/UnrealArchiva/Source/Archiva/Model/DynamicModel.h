// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/MyModel.h"

#include "ProceduralMeshComponent.h"
#include "KismetProceduralMeshLibrary.h"

#include "GameFramework/Actor.h"
#include "DynamicModel.generated.h"

UCLASS()
class ARCHIVA_API ADynamicModel : public AMyModel
{
	GENERATED_BODY()
protected:
	TArray<int32> Triangles;
	TArray<FVector> Vertices;
	TArray<FVector2D> UVs;

	TArray<FVector> Normals;
	TArray<FProcMeshTangent> Tangents;

	int32 meshNum;

	TMap<int32, FString> MaterialPart;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Archiva|Model")
	TArray<FMaterialInst> SavedMaterial;

	float UVratio;

public:	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Archiva|Model")
	UProceduralMeshComponent* model;

	ADynamicModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

	void InitVertices(int32 vetices);

	void CreateQuad(FVector v1, FVector v2, FVector v3, FVector v4, FString part, FVector normal);

	virtual void CalculateUV(int32 vertNum, FVector normal);
	
	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	void ApplyMaterial(TArray<FMaterialInst> mats);

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	void ChangeMaterial(FString partName, UMaterialInterface* path);
};
