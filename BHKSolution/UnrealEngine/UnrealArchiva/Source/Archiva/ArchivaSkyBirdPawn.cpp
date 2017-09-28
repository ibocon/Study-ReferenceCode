// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "ArchivaSkyBirdPawn.h"

AArchivaSkyBirdPawn::AArchivaSkyBirdPawn(const FObjectInitializer& ObjectInitializer)
{
	// enable Tick function
	PrimaryActorTick.bCanEverTick = true;

	// disable standard WASD movement
	bAddDefaultMovementBindings = false;

	// not needed Pitch Yaw Roll
	bUseControllerRotationPitch = false;
	bUseControllerRotationYaw = false;
	bUseControllerRotationRoll = false;

	// set defaults
	CameraRadius = 4000.0f;
	CameraRadiusMin = 10.0f;                // min height
	CameraRadiusMax = 8000.0f;                // max height

	CameraZAnlge = 180.0;                        // yaw

	CameraHeightAngle = 50.0f;                // pitch
	CameraHeightAngleMin = 0.0f;
	CameraHeightAngleMax = 89.0f;

	CameraZoomSpeed = 300.0f;                // wheel
	CameraRotationSpeed = 5.0f;            // wheel + ctrl
	CameraMovementSpeed = 100.0f*10.0f;            // in all directions

	CameraScrollBoundary = 25.0f;            // screen edge width

	bCanMoveCamera = true;
	bIsStaticCamera = false;


	// intialize the camera
	CameraComponent = ObjectInitializer.CreateDefaultSubobject<UCameraComponent>(this, TEXT("Camera"));
	CameraComponent->AttachToComponent(GetRootComponent(), FAttachmentTransformRules::KeepRelativeTransform);
	CameraComponent->bUsePawnControlRotation = false;
	GetRootComponent()->SetRelativeLocation(FVector(0.0f, 0.0f, 0.0f));
	RepositionCamera();
}

void AArchivaSkyBirdPawn::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	check(InputComponent);

	Super::SetupPlayerInputComponent(InputComponent);

	// mouse zoom
	InputComponent->BindAction("ZoomOutByWheel", IE_Pressed, this, &AArchivaSkyBirdPawn::ZoomOutByWheel);
	InputComponent->BindAction("ZoomInByWheel", IE_Pressed, this, &AArchivaSkyBirdPawn::ZoomInByWheel);

	// mouse rotate (+Ctrl or +Alt)
	InputComponent->BindAction("RotateLeftByWheel", IE_Pressed, this, &AArchivaSkyBirdPawn::RotateLeftByWheel);
	InputComponent->BindAction("RotateRightByWheel", IE_Pressed, this, &AArchivaSkyBirdPawn::RotateRightByWheel);
	InputComponent->BindAction("RotateUpByWheel", IE_Pressed, this, &AArchivaSkyBirdPawn::RotateUpByWheel);
	InputComponent->BindAction("RotateDownByWheel", IE_Pressed, this, &AArchivaSkyBirdPawn::RotateDownByWheel);

	// keyboard move (WASD, Home/End)
	InputComponent->BindAxis("MoveForward", this, &AArchivaSkyBirdPawn::MoveCameraForwardInput);
	InputComponent->BindAxis("MoveRight", this, &AArchivaSkyBirdPawn::MoveCameraRightInput);
	InputComponent->BindAxis("MoveUp", this, &AArchivaSkyBirdPawn::MoveCameraUpInput);
	InputComponent->BindAxis("ZoomIn", this, &AArchivaSkyBirdPawn::ZoomCameraInInput);

	InputComponent->BindAxis("TurnLeftCamera", this, &AArchivaSkyBirdPawn::TurnLeftCamera);
	InputComponent->BindAxis("TurnUpCamera", this, &AArchivaSkyBirdPawn::TurnUpCamera);

	

	// double speed (WASD +Shift)
	InputComponent->BindAxis("FastMove", this, &AArchivaSkyBirdPawn::FastMoveInput);
	// yaw and pitch (WASD +Ctrl)
	InputComponent->BindAxis("Rotate", this, &AArchivaSkyBirdPawn::RotateInput);
}


//////////////////////////////////////////////////////////////////
void AArchivaSkyBirdPawn::ZoomInByWheel()
{
	if (!bCanMoveCamera)    return;

	CameraRadius -= CameraZoomSpeed * FastMoveValue;
	CameraRadius = FMath::Clamp(CameraRadius, CameraRadiusMin, CameraRadiusMax);

	RepositionCamera();
}


void AArchivaSkyBirdPawn::ZoomOutByWheel()
{
	if (!bCanMoveCamera)    return;

	CameraRadius += CameraZoomSpeed * FastMoveValue;
	CameraRadius = FMath::Clamp(CameraRadius, CameraRadiusMin, CameraRadiusMax);

	RepositionCamera();
}


void AArchivaSkyBirdPawn::RotateLeftByWheel()
{
	if (!bCanMoveCamera)    return;

	CameraZAnlge -= CameraRotationSpeed * FastMoveValue;

	RepositionCamera();
}


void AArchivaSkyBirdPawn::RotateRightByWheel()
{
	if (!bCanMoveCamera)    return;

	CameraZAnlge += CameraRotationSpeed * FastMoveValue;

	RepositionCamera();
}


void AArchivaSkyBirdPawn::RotateUpByWheel()
{
	if (!bCanMoveCamera)    return;

	CameraHeightAngle += CameraRotationSpeed * FastMoveValue;
	CameraHeightAngle = FMath::Clamp(CameraHeightAngle, CameraHeightAngleMin, CameraHeightAngleMax);

	RepositionCamera();
}


void AArchivaSkyBirdPawn::RotateDownByWheel()
{
	if (!bCanMoveCamera)    return;

	CameraHeightAngle -= CameraRotationSpeed * FastMoveValue;
	CameraHeightAngle = FMath::Clamp(CameraHeightAngle, CameraHeightAngleMin, CameraHeightAngleMax);

	RepositionCamera();
}
void AArchivaSkyBirdPawn::FastMoveInput(float Direction)
{
	if (!bCanMoveCamera)    return;

	// left or right does not matter, to set double speed in any direction
	FastMoveValue = FMath::Abs(Direction) * 2.0f;

	// used as multiplier so must be 1 if not pressed
	if (FastMoveValue == 0.0f)
	{
		FastMoveValue = 1.0f;
	}
}


void AArchivaSkyBirdPawn::RotateInput(float Direction)
{
	if (!bCanMoveCamera)    return;

	// left or right does not matter
	RotateValue = FMath::Abs(Direction);
}

void AArchivaSkyBirdPawn::MoveCameraForwardInput(float Direction)
{
	if (!bCanMoveCamera)    return;

	MoveForwardValue = Direction;
}


void AArchivaSkyBirdPawn::MoveCameraRightInput(float Direction)
{
	if (!bCanMoveCamera)    return;

	MoveRightValue = Direction;
}

void AArchivaSkyBirdPawn::MoveCameraUpInput(float Direction)
{
	if (!bCanMoveCamera)    return;

	MoveUpValue = Direction;
}


void AArchivaSkyBirdPawn::ZoomCameraInInput(float Direction)
{
	if (!bCanMoveCamera)    return;

	ZoomInValue = Direction / 10;
}


void AArchivaSkyBirdPawn::RepositionCamera()
{
	FVector newLocation(0.0f, 0.0f, 0.0f);
	FRotator newRotation(0.0f, 0.0f, 0.0f);

	float sinCameraZAngle = FMath::Sin(FMath::DegreesToRadians(CameraZAnlge));
	float cosCameraZAngle = FMath::Cos(FMath::DegreesToRadians(CameraZAnlge));

	float sinCameraHeightAngle = FMath::Sin(FMath::DegreesToRadians(CameraHeightAngle));
	float cosCameraHeightAngle = FMath::Cos(FMath::DegreesToRadians(CameraHeightAngle));

	newLocation.X = cosCameraZAngle * cosCameraHeightAngle * CameraRadius;
	newLocation.Y = sinCameraZAngle * cosCameraHeightAngle * CameraRadius;
	newLocation.Z = sinCameraHeightAngle * CameraRadius;

	newRotation = (FVector(0.0f, 0.0f, 0.0f) - newLocation).Rotation();
	// new camera location and rotation
	CameraComponent->SetRelativeLocation(newLocation);
	CameraComponent->SetRelativeRotation(newRotation);
}

void AArchivaSkyBirdPawn::RepositionCameraByAbsolute(FVector newLocation,
	FRotator newRotation)
{
	// new camera location and rotation
	CameraComponent->SetRelativeLocation(newLocation);
	CameraComponent->SetRelativeRotation(newRotation);
}
void AArchivaSkyBirdPawn::MoveCamera(float DeltaTime, FString cameraName)
{
	CameraZAnlge = 135.0f;
	CameraRadius = 4000.0f;
	CameraHeightAngle = 20.0f;
	RepositionMoveCamera();
}
void AArchivaSkyBirdPawn::RepositionMoveCamera()
{
	FVector newLocation(0.0f, 0.0f, 0.0f);
	FRotator newRotation(0.0f, 0.0f, 0.0f);

	float sinCameraZAngle = FMath::Sin(FMath::DegreesToRadians(CameraZAnlge));
	float cosCameraZAngle = FMath::Cos(FMath::DegreesToRadians(CameraZAnlge));

	float sinCameraHeightAngle = FMath::Sin(FMath::DegreesToRadians(CameraHeightAngle));
	float cosCameraHeightAngle = FMath::Cos(FMath::DegreesToRadians(CameraHeightAngle));

	newLocation.X = cosCameraZAngle * cosCameraHeightAngle * CameraRadius;
	newLocation.Y = sinCameraZAngle * cosCameraHeightAngle * CameraRadius;
	newLocation.Z = sinCameraHeightAngle * CameraRadius;

	

	newRotation = (FVector(0.0f, 0.0f, 0.0f) - newLocation).Rotation();
	//newRotation = (cameraLocation - newLocation).Rotation();
	//GEngine->AddOnScreenDebugMessage(-1, 1.f, FColor::Red, FString::Printf(TEXT("Location : %f , %f, %f"), newLocation.X, newLocation.Y, newLocation.Z));
	//GEngine->AddOnScreenDebugMessage(-1, 1.f, FColor::Red, FString::Printf(TEXT("Rotation : %f , %f, %f"), newRotation.Pitch, newRotation.Yaw, newRotation.Roll));
	// new camera location and rotation
	CameraComponent->SetRelativeLocation(newLocation);
	CameraComponent->SetRelativeRotation(newRotation);
}
void AArchivaSkyBirdPawn::CircleMovement()
{
	GetRootComponent()->SetRelativeLocation(FVector(0.0f, 0.0f, 0.0f));
}

void AArchivaSkyBirdPawn::CircleCamera(float DeltaTime)
{
	
	angle360 += 10;
	CameraZAnlge = angle360;
	CameraRadius = 4000.0f;
	CameraHeightAngle = 45.0f;
	RepositionMoveCamera();

	if (angle360 > 360)
	{
		angle360 = 0;
		IsCircleMovement = false;
	}
}

FVector AArchivaSkyBirdPawn::GetViewLocation()
{
	FVector CurLocation(CameraZAnlge, CameraHeightAngle, CameraRadius);
	return CurLocation;
	
}
void AArchivaSkyBirdPawn::SetViewLocation(FVector cameraLocation)
{
	CameraZAnlge = cameraLocation.X;
	CameraHeightAngle = cameraLocation.Y;
	CameraRadius = cameraLocation.Z;
	RepositionMoveCamera();
	

}
//------------------------------------------------------------


void AArchivaSkyBirdPawn::MoveCameraForward(float Direction)
{
	float MovementValue = Direction * CameraMovementSpeed;
	FVector DeltaMovement = MovementValue * GetIsolatedCameraYaw().Vector();
	FVector NewLocation = GetActorLocation() + DeltaMovement;

	SetActorLocation(NewLocation);
}


void AArchivaSkyBirdPawn::MoveCameraRight(float Direction)
{
	float MovementValue = Direction * CameraMovementSpeed;
	FVector DeltaMovement = MovementValue * (FRotator(0.0f, 90.0f, 0.0f) + GetIsolatedCameraYaw()).Vector();
	FVector NewLocation = GetActorLocation() + DeltaMovement;

	SetActorLocation(NewLocation);
}


FRotator AArchivaSkyBirdPawn::GetIsolatedCameraYaw()
{
	// FRotator containing Yaw only
	return FRotator(0.0f, CameraComponent->ComponentToWorld.Rotator().Yaw, 0.0f);
}
FRotator AArchivaSkyBirdPawn::GetIsolatedCameraPitch()
{
	// FRotator containing Yaw only
	return FRotator(0.0f, CameraComponent->ComponentToWorld.Rotator().Pitch, 0.0f);
}
//---------------

void AArchivaSkyBirdPawn::MoveCameraUp(float Direction)
{
	float MovementValue = Direction * CameraMovementSpeed;
	FVector DeltaMovement = FVector(0.0f, 0.0f, MovementValue);
	FVector NewLocation = GetActorLocation() + DeltaMovement;
	NewLocation.Z = FMath::Clamp(NewLocation.Z, CameraRadiusMin, CameraRadiusMax);

	SetActorLocation(NewLocation);
}

//---------------

void AArchivaSkyBirdPawn::ZoomCameraIn(float Direction)
{
	float MovementValue = Direction * CameraMovementSpeed*10;                // zoom speed is too low here
	CameraRadius += MovementValue;
	CameraRadius = FMath::Clamp(CameraRadius, CameraRadiusMin, CameraRadiusMax);

	RepositionCamera();
}


void AArchivaSkyBirdPawn::TurnCameraUp(float Direction)
{
	CameraHeightAngle -= Direction * CameraRotationSpeed * 10.0f;        // rotation speed is too low
	CameraHeightAngle = FMath::Clamp(CameraHeightAngle, CameraHeightAngleMin, CameraHeightAngleMax);

	RepositionCamera();
}


void AArchivaSkyBirdPawn::TurnCameraRight(float Direction)
{
	CameraZAnlge += Direction * CameraRotationSpeed * 10.0f;            // rotation speed is too low here

	RepositionCamera();
}
void AArchivaSkyBirdPawn::TurnLeftCamera(float Direction)
{
	APlayerController* OurPlayerController = UGameplayStatics::GetPlayerController(this, 0);

	if (OurPlayerController->IsInputKeyDown(EKeys::MiddleMouseButton))
	{
		float MovementValue = Direction * CameraMovementSpeed / 200;
		FVector DeltaMovement = MovementValue * (FRotator(0.0f, 90.0f, 0.0f) + GetIsolatedCameraYaw()).Vector();
		FVector NewLocation = GetActorLocation() - DeltaMovement;

		SetActorLocation(NewLocation);
	}
	else
	{
		CameraZAnlge += Direction * CameraRotationSpeed * 1.0f;            // rotation speed is too low here
		RepositionCamera();
	}

}

void AArchivaSkyBirdPawn::TurnUpCamera(float Direction)
{
	APlayerController* OurPlayerController = UGameplayStatics::GetPlayerController(this, 0);

	if (OurPlayerController->IsInputKeyDown(EKeys::MiddleMouseButton))
	{
		float MovementValue = Direction * CameraMovementSpeed / 200;
		
		FVector DeltaMovement = MovementValue * GetIsolatedCameraYaw().Vector();
		FVector NewLocation = GetActorLocation() - DeltaMovement;
		SetActorLocation(NewLocation);
	}
	else
	{
		CameraHeightAngle -= Direction * CameraRotationSpeed * 1.0f;        // rotation speed is too low
		CameraHeightAngle = FMath::Clamp(CameraHeightAngle, CameraHeightAngleMin, CameraHeightAngleMax);
		RepositionCamera();
	}
}
//////////////////////////////////////////////////////////////////


void AArchivaSkyBirdPawn::Tick(float DeltaSeconds)
{
	Super::Tick(DeltaSeconds);

	// mouse position and screen size
	FVector2D MousePosition;
	FVector2D ViewportSize;

	UGameViewportClient* GameViewport = GEngine->GameViewport;

	check(GameViewport);
	GameViewport->GetViewportSize(ViewportSize);

	// if viewport is focused, contains the mouse, and camera movement is allowed
	if (
		GameViewport->IsFocused(GameViewport->Viewport)
		&&
		GameViewport->GetMousePosition(MousePosition)
		&&
		bCanMoveCamera
		)
	{
		//// movement direction
		//if (MousePosition.X < CameraScrollBoundary)
		//{
		//	MoveRightValue = -1.0f;
		//}
		//else if (ViewportSize.X - MousePosition.X < CameraScrollBoundary)
		//{
		//	MoveRightValue = 1.0f;
		//}

		//if (MousePosition.Y < CameraScrollBoundary)
		//{
		//	MoveForwardValue = 1.0f;
		//}
		//else if (ViewportSize.Y - MousePosition.Y < CameraScrollBoundary)
		//{
		//	MoveForwardValue = -1.0f;
		//}

		//CircleMovement
		if (IsCircleMovement == true)
		{
			CircleCamera(DeltaSeconds);
			return ;
		}
		// rotate
		if (RotateValue != 0.0f)
		{
			TurnCameraUp(MoveForwardValue * FastMoveValue * DeltaSeconds);
			TurnCameraRight(MoveRightValue * FastMoveValue * DeltaSeconds);
		}
		// move horizontal
		else
		{
			MoveCameraForward(MoveForwardValue * FastMoveValue * DeltaSeconds);
			MoveCameraRight(MoveRightValue * FastMoveValue * DeltaSeconds);
		}

		// move vertical
		MoveCameraUp(MoveUpValue * FastMoveValue * DeltaSeconds);

		// zoom
		ZoomCameraIn(ZoomInValue * FastMoveValue * DeltaSeconds);
	}
}




