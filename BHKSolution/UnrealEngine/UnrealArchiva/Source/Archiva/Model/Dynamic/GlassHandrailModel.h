// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "GlassHandrailModel.generated.h"

UCLASS()
class ARCHIVA_API AGlassHandrailModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	//손잡이 부분의 속성값
	float H_length;
	float H_width;
	float H_height;
	//기둥 부분의 속성값
	float P_length;
	float P_width;
	float P_height;
	//유리 부분의 속성값
	float G_length;
	float G_width;
	float G_height;

	//기울어진 정도
	float angle;

	//Material을 넣기 위한 지정값
	//handle, pillar, glass

public:
	//기본 설정된 값이 세팅되는 함수
	AGlassHandrailModel();
	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

	void CreatePillar(FVector std);
	void CreateGlass(float startP);
	float CalcTrigX(float length);
	float CalcTrigY(float length);
};
