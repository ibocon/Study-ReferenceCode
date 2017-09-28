// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/SaveGame.h"
#include "MySaveGame.generated.h"

/**
 *	@class UMySaveGame
 *	아직 미완성이고, 모델데이터만 저장하는 클래스로 활용하고 있다.
 */
UCLASS()
class ARCHIVA_API UMySaveGame : public USaveGame
{
	GENERATED_BODY()
public:

	UMySaveGame();

	UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Archiva|SaveGame")
	TArray<FModelData> ModelDataList;

	UFUNCTION(BlueprintCallable, Category = "Archiva|GameMode")
	void SaveModelData(TArray<FModelData> data);
};
