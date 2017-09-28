// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/SpectatorPawn.h"
#include "ArchivaSkyBirdPawn.generated.h"

/**
 *	@class AArchivaSkyBirdPawn
 *	ArchivaFirstPersonChara와 같은 캐릭터의 시점을 전환하는 함수
 *	플레이가 3인칭에서 캐릭터 메쉬와 카메라를 컨트롤할 수 있도록 한다.
 */
UCLASS()
class ARCHIVA_API AArchivaSkyBirdPawn : public ASpectatorPawn
{
	GENERATED_BODY()

public:

	//-----------------------f-------------

	/** Constructor */
	AArchivaSkyBirdPawn(const FObjectInitializer& ObjectInitializer);

	//------------------------------------

	/** Camera Component */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera)
	class UCameraComponent* CameraComponent;

	//------------------------------------

	/** Camera Rotation around Axis Z */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraZAnlge;

	/** Camera Height Angle */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraHeightAngle;

	/** Camera Pitch Angle Max */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraHeightAngleMax;

	/** Camera Pitch Angle Min */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraHeightAngleMin;

	/** Camera Radius From Pawn Position */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraRadius;

	/** Camera Radius Max */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraRadiusMax;

	/** Camera Radius Min */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraRadiusMin;

	/** Camera Zoom Speed */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraZoomSpeed;

	/** Camera Rotation Speed */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraRotationSpeed;

	/** Camera Movement Speed */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraMovementSpeed;

	/** Camera Scroll Boundary */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		float CameraScrollBoundary;

	/** Should the camera move? */
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		bool bCanMoveCamera;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Camera)
		bool bIsStaticCamera;

	//------------------------------------
	UPROPERTY(EditAnywhere)
		AActor* CameraOne;

	UPROPERTY(EditAnywhere)
		AActor* CameraTwo;

	UPROPERTY(EditAnywhere)
		UMaterial* Material;

	UPROPERTY(EditAnywhere)
		UMaterialInstanceDynamic* MaterialInstance;

	float TimeToNextCameraChange;
	bool IsCircleMovement;
private:

	/** Sets up player inputs
	*    @param InputComponent - Input Component
	*/
	void SetupPlayerInputComponent(class UInputComponent* InputComponent);

	//------------------------------------

public:

	/** Zooms In The Camera */
	void ZoomInByWheel();

	/** Zooms Out The Camera */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void ZoomOutByWheel();

	/** Rotate The Camera Left */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RotateLeftByWheel();

	/** Rotate The Camera Right */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RotateRightByWheel();

	/** Rotate The Camera Up */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RotateUpByWheel();

	/** Rotate The Camera Down */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RotateDownByWheel();

	//---

	/** Calculates the new Location and Rotation of The Camera */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RepositionCamera();
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RepositionCameraByAbsolute(FVector newLocation,FRotator newRotation);
	
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RepositionMoveCamera();

	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void CircleMovement();
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		FVector GetViewLocation();
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void SetViewLocation(FVector cameraLocation);
	//------------------------------------
	
private:

	// set them to +/-1 to get player input from keyboard
	float FastMoveValue;                                            // movement speed multiplier : 1 if shift unpressed, 2 is pressed
	float RotateValue;                                                // turn instead of move camera

	float MoveForwardValue;
	float MoveRightValue;
	float MoveUpValue;
	float ZoomInValue;

	float angle360;
	
	//---

public:

	/** Left or Right Shift is pressed
	* @param direcation - (1.0 for Right, -1.0 for Left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void FastMoveInput(float Direction);

	/** Left or Right Ctrl is pressed
	* @param direcation - (1.0 for Right, -1.0 for Left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void RotateInput(float Direction);
	/** Input recieved to move the camera forward
	* @param direcation - (1.0 for forward, -1.0 for backward)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void MoveCameraForwardInput(float Direction);

	/** Input recieved to move the camera right
	* @param direcation - (1.0 for right, -1.0 for left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void MoveCameraRightInput(float Direction);
	/** Input recieved to move the camera right
	* @param direcation - (1.0 for right, -1.0 for left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void MoveCameraUpInput(float Direction);

	/** Input recieved to move the camera right
	* @param direcation - (1.0 for right, -1.0 for left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void ZoomCameraInInput(float Direction);

	//---
	//---BluePrintable
	UFUNCTION(BlueprintCallable, Category = "Behavior")
		void MoveCamera(float DeltaSeconds,FString cameraName);

	UFUNCTION(BlueprintCallable, Category = "Behavior")
		void CircleCamera(float DeltaTime);

private:

	/** Moves the camera forward
	* @param direcation - (+ forward, - backward)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void MoveCameraForward(float Direction);

	/** Moves the camera right
	* @param direcation - (+ right, - left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void MoveCameraRight(float Direction);

	/** Gets the roatation of the camera with only the yaw value
	* @return - returns a rotator that is (0, yaw, 0) of the Camera
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		FRotator GetIsolatedCameraYaw();

	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		FRotator GetIsolatedCameraPitch();
	
	//---

	/** Moves the camera up/down
	* @param direcation - (+ up, - down)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void MoveCameraUp(float Direction);

	//---

	/** Zooms the camera in/out
	* @param direcation - (+ in, - out)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void ZoomCameraIn(float Direction);

	/** Turns the camera up/down
	* @param direcation - (+ up, - down)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void TurnCameraUp(float Direction);

	/** Turns the camera right/left
	* @param direcation - (+ right, - left)
	*/
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void TurnCameraRight(float Direction);
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void TurnLeftCamera(float Direction);
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		void TurnUpCamera(float Direction);
	//------------------------------------

public:

	/** Tick Function, handles keyboard inputs */
	UFUNCTION(BlueprintCallable, Category = "Archiva|SkyBirdPawn")
		virtual void Tick(float DeltaSeconds) override;

	
	//------------------------------------	
	
	
	
};
