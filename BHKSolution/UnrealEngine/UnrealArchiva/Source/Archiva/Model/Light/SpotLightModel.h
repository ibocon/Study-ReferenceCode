// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/MyModel.h"

#include "GameFramework/Actor.h"
#include "SpotLightModel.generated.h"

UCLASS()
class ARCHIVA_API ASpotLightModel : public AMyModel
{
	GENERATED_BODY()
protected:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Custom|LightComp")
	USpotLightComponent* SpotLight;
public:	
	// Sets default values for this actor's properties
	ASpotLightModel();

	UFUNCTION(BlueprintCallable, Category = "Model")
	virtual void Create(FModelData data) override;
	
};
