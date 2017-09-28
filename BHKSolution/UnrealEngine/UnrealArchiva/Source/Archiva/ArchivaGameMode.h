// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Data/Modeldata.h"
#include "Kismet/GameplayStatics.h"

#include "GameFramework/GameMode.h"
#include "ArchivaGameMode.generated.h"

/**
* @class ArchivaGameMode
* 블루프린트에서도 Model을 활용할 수 있도록, GameMode에 등록해 놓는다.
* 새로운 모델을 생성하고 나서 여기에 추가해야 블루프린트에 활용할 수 있다.
*/

UCLASS()
class ARCHIVA_API ArchivaGameMode : public AGameMode
{
	GENERATED_BODY()
public:
	/*블루프린트와 호환하기 위한 모델목록*/
	//Primitive
	TSubclassOf<class AMyModel> BP_MyModel;
	TSubclassOf<class AMyModel> BP_DynamicModel;
	TSubclassOf<class AMyModel> BP_StaticModel;

	//Basic
	TSubclassOf<class AMyModel> BP_FloorModel;
	TSubclassOf<class AMyModel> BP_WallModel;

	//Roof
	TSubclassOf<class AMyModel> BP_HipRoofModel;
	TSubclassOf<class AMyModel> BP_CombinationRoofModel;
	TSubclassOf<class AMyModel> BP_FlatRoofModel;
	TSubclassOf<class AMyModel> BP_HollowedMansardRoofModel;

	//Handrail
	TSubclassOf<class AMyModel> BP_BarHandrailModel;
	TSubclassOf<class AMyModel> BP_GlassHandrailModel;

	//Window
	TSubclassOf<class AMyModel> BP_FixedWindowModel;
	TSubclassOf<class AMyModel> BP_SlideWindowModel;

	//Stair
	TSubclassOf<class AMyModel> BP_FullStairModel;
	TSubclassOf<class AMyModel> BP_HollowStairModel;

	//Light
	TSubclassOf<class AMyModel> BP_PointLightModel;
	TSubclassOf<class AMyModel> BP_SkyLightModel;
	TSubclassOf<class AMyModel> BP_SpotLightModel;

	//ETC
	TSubclassOf<class AMyModel> BP_ColumnModel;
	TSubclassOf<class AMyModel> BP_DoorModel;
	TSubclassOf<class AMyModel> BP_WindowModel;

	//Camera
	TSubclassOf<class AArchivaCamera> BP_ArchivaCamera;

	UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Archiva|GameMode")
	TArray<AArchivaCamera*> CameraList; /**< 맵에 추가된 카메라 목록 저장*/
	UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Archiva|GameMode")
	TArray<AMyModel*> ModelList; /**< 맵에 추가된 모델 저장*/
	UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Archiva|GameMode")
	TArray<FModelData> ModelDataList; /**< 맵에 추가된 모델의 데이터 저장. 맵을 재로딩할 때나, 모델의 데이터를 수정하고 다시 로딩할 때 활용한다.*/
	UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Archiva|GameMode")
	class UArchivaXmlReader* xmlReader; /**< xml 데이터를 읽는 모듈*/
	UPROPERTY(BlueprintReadWrite, EditAnywhere, Category = "Archiva|GameMode")
	FString userDir; /**< 모델 데이터가 저장된 xml 파일 위치 */
	/**
	*	프로그램 시작 시, 제일 먼저 실행되는 함수로서 main과 동일하다.
	*	다만 런타임의 Construction 파트에서 실행된다.
	*/
	ArchivaGameMode(const FObjectInitializer& ObjectInitializer);

	/**
	*	Actor을 SpawnModel하기 위한 정보를 설정하는 Helper 함수
	*	@param name 맵에 스폰하는 모델의 이름
	*/
	FActorSpawnParameters SpawnInformation(FName name);
	/**
	*	실질적으로 Actor을 SpawnModel하는 함수다.
	*	ModelData 소스코드에 있는 모델 목록을 활용하여,
	*	SpawnModel할 모델을 선정한다.
	*	(!Construction 타임에 실행하지 않으면 오류가 날 수 있다.)
	* @param data 모델을 스폰하는데 필요한 정보를 제공하는 구조체
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|GameMode")
	void SpawnModel(FModelData data);

	/**
	*	PlayerController가 카메라를 원하는 지점에 생성할 수 있도록 하는 함수
	*	배치되는 순간, ScreenShot을 기록하고 HUD의 리스트에 업데이트 한다.
	*	카메라는 기본적으로 Pin모양을 하고 있으며, 유저가 이동하는 기준이 된다.
	* @param loc 카메라가 스폰될 위치
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|GameMode")
	void SpawnCamera(FTransform loc);

	/**
	*	PlayerController가 카메라를 부드럽게 전환하는 함수
	*	@param player 카메라를 전환하려는 플레이어
	*	@param 카메라 ID 넘버
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|GameMode")
	void ChangeCamera(APlayerController* palyer, int num);

	/**
	* 모든 모델을 특정 XML 파일로 초기화
	* @param path 초기화할 XML 파일 위치
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|GameMode")
	void ResetModel(FString path);

	/**
	* Gets all the files in a given directory.
	* @param directory The full path of the directory we want to iterate over.
	* @param fullpath Whether the returned list should be the full file paths or just the filenames.
	* @param onlyFilesStartingWith Will only return filenames starting with this string. Also applies onlyFilesEndingWith if specified.
	* @param onlyFilesEndingWith Will only return filenames ending with this string (it looks at the extension as well!). Also applies onlyFilesStartingWith if specified.
	* @return A list of files (including the extension).
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|GameMode")
	static TArray<FString> GetAllFilesInDirectory(const FString directory, const bool fullPath = true, const FString onlyFilesStartingWith = TEXT(""), const FString onlyFilesEndingWith = TEXT(""));

};
