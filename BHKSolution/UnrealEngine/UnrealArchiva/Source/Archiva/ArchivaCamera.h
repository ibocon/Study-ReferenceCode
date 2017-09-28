// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include "ArchivaHUD.h"

#include "GameFramework/Actor.h"
#include "ArchivaCamera.generated.h"


/**
* @class AArchivaCamera
* @brief 건축물을 다각도에서 살펴볼 수 있도록 설치되는 카메라
* 
* 건축물을 다양한 각도에서 한 번에 확인하기 위해서는 월드에 카메라를 배치하여, 실시간으로 촬영한 뒤 UI에서 보여준다.
* 카메라에서 촬영된 데이터를 UI에 표현할 수 있는 2D Texture로 변환하여 내부에 저장한다.
*/

UCLASS()
class ARCHIVA_API AArchivaCamera : public ACameraActor
{
	GENERATED_BODY()
	
public:	
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Export, Category = "Archiva|Camera")
	USceneCaptureComponent2D* SceneCapture; /**< 카메라에 잡히는 영상을 저장하는 변수 */

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Export, Category = "Archiva|Camera")
	UTextureRenderTarget2D* RenderTarget; /**< 카메라에 잡힌 영상을 텍스처로 변경시켜 저장하는 변수 */

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Archiva|Camera")
	int32 CameraNumber; /**< 카메라의 순서를 저장하는 변수 */
	
	/**
	* ArchivaCamera 객체를 초기화한다. Constructor라 할 수 있다.
	* @param ObjectInitializer UE4에서 Object를 초기화하기 위해 반드시 요구한다.
	*/
	AArchivaCamera(const FObjectInitializer& ObjectInitializer);

	/**
	* 게임이 시작되었을 때 또는 객체가 생성되었을 때 실행된다.
	*/
	virtual void BeginPlay() override;
	
	/**
	* 매 Tick마다 실행된다.
	*/
	virtual void Tick( float DeltaSeconds ) override;

};
