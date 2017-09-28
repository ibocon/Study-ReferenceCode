// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "ArchivaFirstPersonChara.h"


/**
* Sets default values
*/
AArchivaFirstPersonChara::AArchivaFirstPersonChara()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

/**
* Called when the game starts or when spawned
*/
void AArchivaFirstPersonChara::BeginPlay()
{
	Super::BeginPlay();
	
}

/**
* Called every frame
*/
void AArchivaFirstPersonChara::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

/**
* Called to bind functionality to input
*/
void AArchivaFirstPersonChara::SetupPlayerInputComponent(class UInputComponent* InputComponent)
{
	Super::SetupPlayerInputComponent(InputComponent);

	// Set up "movement" bindings.
	InputComponent->BindAxis("MoveForward", this, &AArchivaFirstPersonChara::MoveForward);
	InputComponent->BindAxis("MoveRight", this, &AArchivaFirstPersonChara::MoveRight);

	// Set up "look" bindings.
	InputComponent->BindAxis("Turn", this, &AArchivaFirstPersonChara::AddControllerYawInput);
	InputComponent->BindAxis("LookUp", this, &AArchivaFirstPersonChara::AddControllerPitchInput);

	// Set up "action" bindings.
	InputComponent->BindAction("Jump", IE_Pressed, this, &AArchivaFirstPersonChara::StartJump);
	InputComponent->BindAction("Jump", IE_Released, this, &AArchivaFirstPersonChara::StopJump);
}

void AArchivaFirstPersonChara::MoveForward(float Value)
{
	// Find out which way is "forward" and record that the player wants to move that way.
	FVector Direction = FRotationMatrix(Controller->GetControlRotation()).GetScaledAxis(EAxis::X);
	AddMovementInput(Direction, Value);
}

void AArchivaFirstPersonChara::MoveRight(float Value)
{
	// Find out which way is "right" and record that the player wants to move that way.
	FVector Direction = FRotationMatrix(Controller->GetControlRotation()).GetScaledAxis(EAxis::Y);
	AddMovementInput(Direction, Value);
}

void AArchivaFirstPersonChara::StartJump()
{
	bPressedJump = true;
}

void AArchivaFirstPersonChara::StopJump()
{
	bPressedJump = false;
}
