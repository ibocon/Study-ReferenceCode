// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Data/Modeldata.h"

#include "ArchivaGameMode.h"
#include "Runtime/XmlParser/Public/XmlFile.h"
#include "Runtime/XmlParser/Public/XmlNode.h"

#include "ArchivaXmlReader.generated.h"

/**
*	@class UArchivaXmlReader
*	XML에 입력된 데이터를 읽고 모델에 반영하는 모듈
*/
UCLASS()
class ARCHIVA_API UArchivaXmlReader : public UObject
{
GENERATED_BODY()

public:
	//논리적인 단위의 모델을 처리하는 함수
	void CreateARoom(FXmlNode* ARoom);

	//추상적인 모델집합을 처리하는 함수
	
	void ReadStatics(FXmlNode* statics);
	void ReadWalls(FXmlNode* walls);
	void ReadWindows(FXmlNode* windows);
	void ReadFloors(FXmlNode* floors);

	//구체적인 모델을 생성하는 함수
	void CreateAStatic(FXmlNode* AStatic);
	void CreateAWall(FXmlNode* AWall);
	void CreateASlideWindow(FXmlNode* ASlidwin);
	void CreateAFixedWindow(FXmlNode* AFixedwin);
	void CreateAFloor(FXmlNode* AFloor);
	void CreateAGlassHandrail(FXmlNode* Arail);
	void CreateABarHandrail(FXmlNode* Arail);
	void CreateAPointLight(FXmlNode* APointLight);
	void CreateASkyLight(FXmlNode* ASkyLight);
	void CreateASpotLight(FXmlNode* ASpotLight);
	void CreateAFullStair(FXmlNode* AStair);
	void CreateAHollowStair(FXmlNode* AStair);
	void CreateAView(FXmlNode* AView);
	void CreateAHipRoof(FXmlNode* AHipRoof);
	void CreateAFlatRoof(FXmlNode* AFlatRoof);
	void CreateAHollowedMansardRoof(FXmlNode* HollowedMansardRoof);

	//모델끼리 공유하는 노트타입을 적용하는 함수
	//((코드의 재사용성을 높이기 위한 수단))
	void ApplyLocation(FXmlNode* loc);
	void ApplyRotation(FXmlNode* rot);
	void ApplySize(FXmlNode* sz);
	void ApplyMaterials(FXmlNode* mat);


	class ArchivaGameMode* project;
	class FXmlFile* xmlFile;
	FString path;

	FModelData data;
	FVector start;
	FVector end;

	UArchivaXmlReader(const FObjectInitializer& ObjectInitializer);
	
	/**
	*	실제로 xml파일을 읽어오는 과정
	*	@param mode 프로젝트 모드
	*	@param filepath XML 파일 위치
	*/
	UFUNCTION(BlueprintCallable, Category = "Xml")
	bool ReadXml(class ArchivaGameMode* mode, FString filepath);

};

/**
*	ModelData에 등록되어 있는 모델인지 확인하는 함수
*/
static bool IsModel(FString ElementName);

/*
*	start지점과 end지점을 활용하여, Length를 계산
*/
static float getLengthFromTwoPoints(FVector start, FVector end);

/**
*	start지점과 end지점을 활용하여, Angle을 계산
*/
static FRotator getAngleFromTwoPoints(FVector start, FVector end);