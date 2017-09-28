// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Engine.h"

//사용할 수 있는 모든 Procedural Mesh 모델 종류를 포함시킨다.
#include "Model/MyModel.h"

#include "Model/StaticModel.h"
#include "Model/DynamicModel.h"
/*
#include "Model/Dynamic/WallModel.h"
#include "Model/Dynamic/FloorModel.h"
#include "Model/Dynamic/HipRoofModel.h"
#include "Model/Dynamic/FullStairModel.h"
#include "Model/Dynamic/HollowStairModel.h"
#include "Model/Dynamic/FixedWindowModel.h"
#include "Model/Dynamic/SlideWindowModel.h"
#include "Model/Dynamic/GlassHandrailModel.h"
#include "Model/Dynamic/BarhandrailModel.h"

#include "Model/Static/WindowModel.h"
#include "Model/Static/DoorModel.h"
#include "Model/Static/ColumnModel.h"
*/
#include "Model/Light/PointLightModel.h"
#include "Model/Light/SkyLightModel.h"
#include "Model/Light/SpotLightModel.h"

#include "ArchivaCamera.h"
#include "Xml/ArchivaXmlReader.h"


/**
* \brief 테스트를 위한 로그 카테고리 정의
*
* 프로젝트를 테스트하기 위해, 필요한 로그 카테고리를 생성해서 출력한다.
*/

//기본적인 로그 카테고리
DECLARE_LOG_CATEGORY_EXTERN(MyLog, Log, All);

//게임 시작 시, 발생하는 로그 카테고리
DECLARE_LOG_CATEGORY_EXTERN(MyInit, Log, All);

//반드시 확인해야 할 치명적인 에러 로그 카테고리
DECLARE_LOG_CATEGORY_EXTERN(MyError, Log, All);