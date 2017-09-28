// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Data/Modeldata.h"

#include "GameFramework/Actor.h"
#include "MyModel.generated.h"

/*
*	@class AMyModel
*	모델을 정의하는데 필요한 함수를 정의하는 제일 중요한 클래스
*/

UCLASS()
class ARCHIVA_API AMyModel : public AActor
{
	GENERATED_BODY()
	
protected:	
	/*
	*	RootComponent로서 절대좌표에 배치되고, 상대좌표의 기준이 된다.
	*/
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Archiva|Model")
	USphereComponent* SphereComponent;

public:
	// Sets default values for this actor's properties
	AMyModel();

	/**
	*	모든 Model이 포함하고 있는 함수로서, Model의 형태를 결정하는 함수
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data);
};

