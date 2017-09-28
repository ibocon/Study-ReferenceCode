// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Character.h"
#include "ArchivaFirstPersonChara.generated.h"


/**
* @class AArchivaFirstPersonChara
* 유저가 컨트롤할 수 있는 1인칭 관점을 구현한 Character
*/

UCLASS()
class ARCHIVA_API AArchivaFirstPersonChara : public ACharacter
{
	GENERATED_BODY()

public:

	/**
	* 기본 생성된 Constructor
	* Sets default values for this character's properties
	*/
	AArchivaFirstPersonChara();


	/**
	* 기본 생성된 BeginPlay
	* Called when the game starts or when spawned
	*/
	virtual void BeginPlay() override;
	
	/**
	* 기본 생성된 Tick
	* Called every frame
	*/
	virtual void Tick( float DeltaSeconds ) override;


	/**
	* 유저 입력을 처리하는 Key Binding 과정
	* Called to bind functionality to input
	*/
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;

	
	/**
	* Handles input for moving forward and backward.
	*/
	UFUNCTION(BlueprintCallable, Category = "ArchivaFirstPerson")
	void MoveForward(float Value); /**< 전진과 후진 처리 */

	/**
	* Handles input for moving right and left.
	*/
	UFUNCTION(BlueprintCallable, Category = "ArchivaFirstPerson")
	void MoveRight(float Value); /**< 좌진과 우진 처리 */

	/**
	* Sets jump flag when key is pressed.
	*/
	UFUNCTION(BlueprintCallable, Category = "ArchivaFirstPerson")
	void StartJump(); /**< 점프 시작 처리 */

	/**
	* Clears jump flag when key is released.
	*/
	UFUNCTION(BlueprintCallable, Category = "ArchivaFirstPerson")
	void StopJump(); /**< 점프 중단 처리 */

};
