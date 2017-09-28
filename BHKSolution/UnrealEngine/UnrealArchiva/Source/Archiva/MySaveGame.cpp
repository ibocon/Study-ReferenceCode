// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "MySaveGame.h"

UMySaveGame::UMySaveGame() 
{

}

void UMySaveGame::SaveModelData(TArray<FModelData> data)
{
	for (int i=0; i<data.Num(); i++)
	{
		ModelDataList.Add(data[i]);
	}
}
