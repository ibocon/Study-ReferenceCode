// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/MyModel.h"

#include "GameFramework/Actor.h"
#include "PointLightModel.generated.h"

UCLASS()
class ARCHIVA_API APointLightModel : public AMyModel
{
	GENERATED_BODY()
protected:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Custom|LightComp")
	UPointLightComponent* PointLight;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Custom|LightComp")
	UStaticMeshComponent* Figure;
	
public:	
	// Sets default values for this actor's properties
	APointLightModel();

	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;
	
};
