// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "ArchivaCamera.h"


/**
* 매 Tick마다 카메라가 잡는 장면을 업데이트할 수 있도록, 초기화한다.
*/
AArchivaCamera::AArchivaCamera(const FObjectInitializer& ObjectInitializer) : Super(ObjectInitializer)
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	this->SceneCapture = CreateDefaultSubobject<USceneCaptureComponent2D>(TEXT("SceneCapture"));
	SceneCapture->AttachToComponent(this->GetCameraComponent(), FAttachmentTransformRules::KeepRelativeTransform);
	//SceneCapture->CaptureSource = SCS_FinalColorLDR;
	SceneCapture->DetailMode = EDetailMode::DM_High;
	
}

/**
* 카메라가 잡은 장면을 Texture로 변환시킬 수 있도록 세팅한다.
*/
// Called when the game starts or when spawned
void AArchivaCamera::BeginPlay()
{
	Super::BeginPlay();

	RenderTarget = NewObject<UTextureRenderTarget2D>();
	RenderTarget->InitAutoFormat(512, 512);
	RenderTarget->CompressionSettings = TextureCompressionSettings::TC_EditorIcon;
	RenderTarget->UpdateResource();

	SceneCapture->TextureTarget = RenderTarget;

	//AArchivaHUD* hud = (AArchivaHUD*)GetWorld()->GetFirstPlayerController()->GetHUD();
	//hud->snapShotList.Add(RenderTarget);

}


/**
* 매 Tick마다, Scene을 다시 캡쳐하여 영상과 같은 효과를 준다.
*/
void AArchivaCamera::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );
	SceneCapture->UpdateContent();
}
