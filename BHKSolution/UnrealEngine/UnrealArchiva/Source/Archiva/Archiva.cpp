// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"

IMPLEMENT_PRIMARY_GAME_MODULE(FDefaultGameModuleImpl, Archiva, "Archiva");

/**
* \brief 테스트를 위한 로그 카테고리 정의
*
* 프로젝트를 테스트하기 위해, 필요한 로그 카테고리를 생성해서 출력한다.
*/

//기본적인 로그 카테고리
DEFINE_LOG_CATEGORY(MyLog);

//게임 시작 시, 발생하는 로그 카테고리
DEFINE_LOG_CATEGORY(MyInit);

//반드시 확인해야 할 치명적인 에러 로그 카테고리
DEFINE_LOG_CATEGORY(MyError);

/*
* <C++과 Unreal의 조합>
*	<모델>
*	모델의 종류는 Dynamic과 Static이 있다. 모든 모델은 MyModel을 상속받는다.
*	Dynamic이 핵심적인 부분을 담당하고 있으며, 모든 Dynamic 모델은 DynamicModel을 상속받는다.
*	Light과 관련한 모델은 Static과 거의 동일하지만 능동적으로 배치하기 위해, 특이한 형태를 가진다.
*
*		<모델 추가시 유의사항>
*		모델을 추가할 때는 XmlReader, ModelData, Blueprint 중 MaterialList, ArchivaGameMode 등
*		연동되어 있는 것이 많아 주의를 요한다.
*
*	<XML>
*	XmlReader를 활용하여, XML을 읽고 ModelData이 모델과 XML 사이에 데이터를 전달하는 역할을 한다.
*
*	<맵을 제작할 때의 유의사항>
*	BP_SunControl을 배치해야 Sun을 조종할 수 있다.
*	NavMeshBoundVolume를 배치해야 클릭과 이동을 할 수 있다.
*	PlayerStart를 배치해야 Home을 이용할 수 있다.
*
*/

/*
*	TODO:
*	<버그목록>
*	1. Camera가 XML에서 추가되면 전환이 이상해지는 오류가 있다. >> XML에서 추가 할 때, 번호의 시작이 0부터 순차적으로 진행되어야 한다.
*	2. BirdView에서 CharacterView로 전환할 때, 부자연스러운 면이 있다. >> BirdView가 LandScape와 Collision을 가져야 한다.
*	3. Camera끼리의 전환에서 더블클릭을 해야 작동하는 버그가 있다. >> 원인 불명
*	4. Floor의 표면이 뒤집어지는 버그가 발생한다. >> Floor을 생성하는 규칙은 방향과 밀접하며(y축으로 먼저 이동), 각 전환점마다 바둑판과 같이 좌표가 추가되어야 한다.
*	5. Present 기능을 사용 후에, Tab & Go가 남아 있다. >> 원인 불명
*	6. NavMesh와 SelectModel이 동일한 Input을 사용하여 우선순위 쟁탈로 인한 버그가 있다. >> Shift + Left Mouse는 SelectModel을, NavMesh는 Left Mouse를
*/