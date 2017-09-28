// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "ArchivaGameMode.h"

#include "Engine/Blueprint.h"

ArchivaGameMode::ArchivaGameMode(const FObjectInitializer& ObjectInitializer) : Super(ObjectInitializer)
{	
	this->userDir = FPaths::Combine(FPlatformProcess::UserDir(), TEXT("Archiva"));
	this->xmlReader = CreateDefaultSubobject<UArchivaXmlReader>(TEXT("Reader"));
	
	/*블루프린트 정보를 찾는 과정*/
	ConstructorHelpers::FObjectFinder<UClass> ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_BarHandrailModel.BP_BarHandrailModel_C'"));
	if (ItemBlueprint.Object) {
		BP_BarHandrailModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_ColumnModel.BP_ColumnModel_C'"));
	if (ItemBlueprint.Object) {
		BP_ColumnModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_DoorModel.BP_DoorModel_C'"));
	if (ItemBlueprint.Object) {
		BP_DoorModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_DynamicModel.BP_DynamicModel_C'"));
	if (ItemBlueprint.Object) {
		BP_DynamicModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_FixedWindowModel.BP_FixedWindowModel_C'"));
	if (ItemBlueprint.Object) {
		BP_FixedWindowModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_FloorModel.BP_FloorModel_C'"));
	if (ItemBlueprint.Object) {
		BP_FloorModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_FullStairModel.BP_FullStairModel_C'"));
	if (ItemBlueprint.Object) {
		BP_FullStairModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_GlassHandrailModel.BP_GlassHandrailModel_C'"));
	if (ItemBlueprint.Object) {
		BP_GlassHandrailModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_HollowStairModel.BP_HollowStairModel_C'"));
	if (ItemBlueprint.Object) {
		BP_HollowStairModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_MyModel.BP_MyModel_C'"));
	if (ItemBlueprint.Object) {
		BP_MyModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_PointLightModel.BP_PointLightModel_C'"));
	if (ItemBlueprint.Object) {
		BP_PointLightModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_SkyLightModel.BP_SkyLightModel_C'"));
	if (ItemBlueprint.Object) {
		BP_SkyLightModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_SlideWindowModel.BP_SlideWindowModel_C'"));
	if (ItemBlueprint.Object) {
		BP_SlideWindowModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_SpotLightModel.BP_SpotLightModel_C'"));
	if (ItemBlueprint.Object) {
		BP_SpotLightModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_StaticModel.BP_StaticModel_C'"));
	if (ItemBlueprint.Object) {
		BP_StaticModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_WallModel.BP_WallModel_C'"));
	if (ItemBlueprint.Object) {
		BP_WallModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_WindowModel.BP_WindowModel_C'"));
	if (ItemBlueprint.Object) {
		BP_WindowModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_CombinationRoofModel.BP_CombinationRoofModel_C'"));
	if (ItemBlueprint.Object) {
		BP_CombinationRoofModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_HipRoofModel.BP_HipRoofModel_C'"));
	if (ItemBlueprint.Object) {
		BP_HipRoofModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_FlatRoofModel.BP_FlatRoofModel_C'"));
	if (ItemBlueprint.Object) {
		BP_FlatRoofModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Models/BP_HollowedMansardRoofModel.BP_HollowedMansardRoofModel_C'"));
	if (ItemBlueprint.Object) {
		BP_HollowedMansardRoofModel = ItemBlueprint.Object;
	}
	ItemBlueprint = ConstructorHelpers::FObjectFinder<UClass>(TEXT("Class'/Game/Organized/Gameplay/BP_ArchivaCamera.BP_ArchivaCamera_C'"));
	if (ItemBlueprint.Object) {
		BP_ArchivaCamera = ItemBlueprint.Object;
	}
}

FActorSpawnParameters ArchivaGameMode::SpawnInformation(FName name)
{
	FActorSpawnParameters SpawnInfo;

	SpawnInfo.Name = name;
	//SpawnInfo.bNoCollisionFail = true;
	SpawnInfo.SpawnCollisionHandlingOverride = ESpawnActorCollisionHandlingMethod::AlwaysSpawn;
	SpawnInfo.Owner = NULL;
	SpawnInfo.Instigator = NULL;
	SpawnInfo.bDeferConstruction = false;

	return SpawnInfo;
}

void ArchivaGameMode::SpawnModel(FModelData data)
{
	//UE_LOG(MyLog,Log, TEXT("ArchivaGameMode::SpawnModel(ModelData data) - START to spawn Model!"))
	UWorld* const World = GetWorld();
	if (World)
	{
		FString random = FString::FromInt(rand()%100);
		data.name.AppendString(random);

		AMyModel* model = NULL;
		AArchivaCamera * view = NULL;

		switch (ModelName[data.meshType]) {
		case ModelList::Static:
		{
			model = World->SpawnActor<AMyModel>(BP_StaticModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::Dynamic:
		{
			model = World->SpawnActor<AMyModel>(BP_DynamicModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::Wall:
		{
			model = World->SpawnActor<AMyModel>(BP_WallModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::Floor:
		{
			model = World->SpawnActor<AMyModel>(BP_FloorModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::Window:
		{
			model = World->SpawnActor<AMyModel>(BP_WindowModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::Door:
		{
			model = World->SpawnActor<AMyModel>(BP_DoorModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::Column:
		{
			model = World->SpawnActor<AMyModel>(BP_ColumnModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::FixedWindow:
		{
			model = World->SpawnActor<AMyModel>(BP_FixedWindowModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::SlideWindow:
		{
			model = World->SpawnActor<AMyModel>(BP_SlideWindowModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::GlassHandrail:
		{
			model = World->SpawnActor<AMyModel>(BP_GlassHandrailModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::BarHandrail:
		{
			model = World->SpawnActor<AMyModel>(BP_BarHandrailModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::PointLight:
		{
			model = World->SpawnActor<AMyModel>(BP_PointLightModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::SkyLight:
		{
			model = World->SpawnActor<AMyModel>(BP_SkyLightModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::SpotLight:
		{
			model = World->SpawnActor<AMyModel>(BP_SpotLightModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::FullStair:
		{
			model = World->SpawnActor<AMyModel>(BP_FullStairModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::HollowStair:
		{
			model = World->SpawnActor<AMyModel>(BP_HollowStairModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::HipRoof:
		{
			model = World->SpawnActor<AMyModel>(BP_HipRoofModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::CombinationRoof:
		{
			model = World->SpawnActor<AMyModel>(BP_CombinationRoofModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::FlatRoof:
		{
			model = World->SpawnActor<AMyModel>(BP_FlatRoofModel, data.placement, SpawnInformation(data.name));
			break;
		}
		case ModelList::HollowedMansardRoof:
		{
			model = World->SpawnActor<AMyModel>(BP_HollowedMansardRoofModel, data.placement, SpawnInformation(data.name));
			break;
		}
		//카메라는 모델과 달리 Create이라는 함수를 통해, 설정하지 않으므로, 여기서 직접 값을 설정한다.
		case ModelList::View:
		{
			view = World->SpawnActor<AArchivaCamera>(BP_ArchivaCamera, data.placement, SpawnInformation(data.name));
			if (view != NULL)
			{
				view->CameraNumber = data.CameraNumber;
				CameraList.SetNum(data.CameraNumber, false);
				CameraList.Insert(view, data.CameraNumber);
			}
			break;
		}
		default:
			UE_LOG(MyLog, Warning, TEXT("ArchivaGameMode::Spawn(ModelData data) - Invalid Model Type! = %s"), *data.meshType);
			break;
		}

		if (model != NULL)
		{
			//UE_LOG(MyLog, Warning, TEXT("ArchivaGameMode::SpawnModel(ModelData data) - Create '%s' "), *data.meshType);
			ModelList.Add(model);
			model->Create(data);
		}

		ModelDataList.Add(data);
	}
}

void ArchivaGameMode::SpawnCamera(FTransform loc)
{
	UWorld* const World = GetWorld();
	if(World)
	{
		AArchivaCamera* cam = World->SpawnActor<AArchivaCamera>(BP_ArchivaCamera, loc, SpawnInformation(FName("Camera" + CameraList.Num())));
		//CameraList.Add(cam);
	}
}

void ArchivaGameMode::ChangeCamera(APlayerController* palyer, int num)
{
	if(CameraList.Num() <= 0 || num >= CameraList.Num())
	{
		UE_LOG(MyLog, Warning, TEXT("ArchivaGameMode::ChangeCamera - CameraList is %d"), CameraList.Num())
		return;
	}

	const float TimeBetweenCameraChanges = 2.0f;
	const float SmoothBlendTime = 0.75f;

	if(palyer != nullptr)
	{
		palyer->SetViewTargetWithBlend(CameraList[num], SmoothBlendTime, EViewTargetBlendFunction::VTBlend_Cubic);
	}
}

void ArchivaGameMode::ResetModel(FString path)
{

	for(int i=0; i<ModelList.Num(); i++)
	{
		if(ModelList[i])
		{
			ModelList[i]->Destroy();
		}
	}
	ModelList.Empty();

	for (int i = 0; i<CameraList.Num(); i++)
	{
		if(CameraList[i])
		{
			CameraList[i]->Destroy();
		}
	}
	CameraList.Empty();
	ModelDataList.Empty();
	if (!path.IsEmpty()) 
	{
		xmlReader->ReadXml(this, path);
	}
	
}


TArray<FString> ArchivaGameMode::GetAllFilesInDirectory(const FString directory, const bool fullPath, const FString onlyFilesStartingWith, const FString onlyFilesWithExtension)
{
	// Get all files in directory
	TArray<FString> directoriesToSkip;
	IPlatformFile &PlatformFile = FPlatformFileManager::Get().GetPlatformFile();
	FLocalTimestampDirectoryVisitor Visitor(PlatformFile, directoriesToSkip, directoriesToSkip, false);
	PlatformFile.IterateDirectory(*directory, Visitor);
	TArray<FString> files;

	for (TMap<FString, FDateTime>::TIterator TimestampIt(Visitor.FileTimes); TimestampIt; ++TimestampIt)
	{
		const FString filePath = TimestampIt.Key();
		const FString fileName = FPaths::GetCleanFilename(filePath);
		bool shouldAddFile = true;

		// Check if filename starts with required characters
		if (!onlyFilesStartingWith.IsEmpty())
		{
			const FString left = fileName.Left(onlyFilesStartingWith.Len());

			if (!(fileName.Left(onlyFilesStartingWith.Len()).Equals(onlyFilesStartingWith)))
				shouldAddFile = false;
		}

		// Check if file extension is required characters
		if (!onlyFilesWithExtension.IsEmpty())
		{
			if (!(FPaths::GetExtension(fileName, false).Equals(onlyFilesWithExtension, ESearchCase::IgnoreCase)))
				shouldAddFile = false;
		}

		// Add full path to results
		if (shouldAddFile)
		{
			files.Add(fullPath ? filePath : fileName);
		}
	}

	return files;
}

