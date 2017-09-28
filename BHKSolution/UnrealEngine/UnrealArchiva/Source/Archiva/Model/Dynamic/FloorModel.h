// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Model/DynamicModel.h"

#include "GameFramework/Actor.h"
#include "FloorModel.generated.h"

UCLASS()
class ARCHIVA_API AFloorModel : public ADynamicModel
{
	GENERATED_BODY()
protected:
	TArray<FVector> floor;

	//top, side, bottom
	
public:	
	AFloorModel();

	UFUNCTION(BlueprintCallable, Category = "Archiva|Model")
	virtual void Create(FModelData data) override;

	void Extrude(TArray<FVector> bottom, FVector m);

	/*사각형을 만들기 위해, 중심점을 기준으로 상좌 | 상우 | 하좌 | 하우 조합이 가능한지 찾는다. */
	TArray<FVector> FindPosSqud(FVector std);
	/*상하좌우 좌표를 찾는 함수*/
	FVector FindPoint(FVector std, FVector2D direction);
	/*찾은 사각형이 내부인지 확인하는 함수*/
	bool isInside(FVector mid);
};
