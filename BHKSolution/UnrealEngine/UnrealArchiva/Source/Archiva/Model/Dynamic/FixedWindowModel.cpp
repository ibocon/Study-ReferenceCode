// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "FixedWindowModel.h"


// Sets default values
AFixedWindowModel::AFixedWindowModel()
{
}

void AFixedWindowModel::Create(FModelData data)
{
	model->ClearAllMeshSections();

	meshNum = 0;
	SavedMaterial = data.materials;

	length = data.size.X / 2;
	width = data.size.Y / 2;
	height = data.size.Z;
	
	frame = data.frame;

	cordFrameOut[0] = FVector(-length, width, 0);
	cordFrameOut[1] = FVector(length, width, 0);
	cordFrameOut[2] = FVector(length, width, height);
	cordFrameOut[3] = FVector(-length, width, height);

	cordFrameOut[4] = FVector(-length, -width, 0);
	cordFrameOut[5] = FVector(length, -width, 0);
	cordFrameOut[6] = FVector(length, -width, height);
	cordFrameOut[7] = FVector(-length, -width, height);

	cordFrameIn[0] = FVector((-length) + frame, width, frame);
	cordFrameIn[1] = FVector(length - frame, width, frame);
	cordFrameIn[2] = FVector(length - frame, width, height - frame);
	cordFrameIn[3] = FVector((-length) + frame, width, height - frame);

	cordFrameIn[4] = FVector((-length) + frame, -width, frame);
	cordFrameIn[5] = FVector(length - frame, -width, frame);
	cordFrameIn[6] = FVector(length - frame, -width, height - frame);
	cordFrameIn[7] = FVector((-length) + frame, -width, height - frame);

	//Frame Mesh
	InitVertices(64);

	/*바깥쪽 side*/
	{
		//왼쪽 side
		Vertices[0] = cordFrameOut[4];
		Vertices[1] = cordFrameOut[0];
		Vertices[2] = cordFrameOut[3];
		Vertices[3] = cordFrameOut[7];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 0, 1, 2, 3);

		//위쪽 side
		Vertices[4] = cordFrameOut[3];
		Vertices[5] = cordFrameOut[2];
		Vertices[6] = cordFrameOut[6];
		Vertices[7] = cordFrameOut[7];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 4, 5, 6, 7);

		//오른쪽 side
		Vertices[8] = cordFrameOut[1];
		Vertices[9] = cordFrameOut[5];
		Vertices[10] = cordFrameOut[6];
		Vertices[11] = cordFrameOut[2];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 8, 9, 10, 11);

		//아래쪽 side
		Vertices[12] = cordFrameOut[4];
		Vertices[13] = cordFrameOut[5];
		Vertices[14] = cordFrameOut[1];
		Vertices[15] = cordFrameOut[0];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 12, 13, 14, 15);
	}
	/*안쪽 side*/
	{
		//왼쪽 side
		Vertices[16] = cordFrameIn[7];
		Vertices[17] = cordFrameIn[3];
		Vertices[18] = cordFrameIn[0];
		Vertices[19] = cordFrameIn[4];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 16, 17, 18, 19);

		//위쪽 side
		Vertices[20] = cordFrameIn[7];
		Vertices[21] = cordFrameIn[6];
		Vertices[22] = cordFrameIn[2];
		Vertices[23] = cordFrameIn[3];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 20, 21, 22, 23);

		//오른쪽 side
		Vertices[24] = cordFrameIn[2];
		Vertices[25] = cordFrameIn[6];
		Vertices[26] = cordFrameIn[5];
		Vertices[27] = cordFrameIn[1];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 24, 25, 26, 27);

		//아래쪽 side
		Vertices[28] = cordFrameIn[0];
		Vertices[29] = cordFrameIn[1];
		Vertices[30] = cordFrameIn[5];
		Vertices[31] = cordFrameIn[4];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 28, 29, 30, 31);
	}

	/*내부 면*/
	{
		//왼쪽 면
		Vertices[32] = cordFrameOut[7];
		Vertices[33] = FVector(cordFrameIn[7].X, cordFrameIn[7].Y, height);
		Vertices[34] = FVector(cordFrameIn[4].X, cordFrameIn[4].Y, 0);
		Vertices[35] = cordFrameOut[4];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 32, 33, 34, 35);

		//윗쪽 면
		Vertices[36] = FVector(cordFrameIn[7].X, cordFrameIn[7].Y, height);
		Vertices[37] = FVector(cordFrameIn[6].X, cordFrameIn[6].Y, height);
		Vertices[38] = cordFrameIn[6];
		Vertices[39] = cordFrameIn[7];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 36, 37, 38, 39);

		//오른쪽 면
		Vertices[40] = FVector(cordFrameIn[6].X, cordFrameIn[6].Y, height);
		Vertices[41] = cordFrameOut[6];
		Vertices[42] = cordFrameOut[5];
		Vertices[43] = FVector(cordFrameIn[5].X, cordFrameIn[5].Y, 0);
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 40, 41, 42, 43);

		//아래쪽 면
		Vertices[44] = cordFrameIn[4];
		Vertices[45] = cordFrameIn[5];
		Vertices[46] = FVector(cordFrameIn[5].X, cordFrameIn[5].Y, 0);
		Vertices[47] = FVector(cordFrameIn[4].X, cordFrameIn[4].Y, 0);
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 44, 45, 46, 47);
	}

	/*외부 면*/
	{
		//외쪽 면
		Vertices[48] = cordFrameOut[0];
		Vertices[49] = FVector(cordFrameIn[0].X, cordFrameIn[0].Y, 0);
		Vertices[50] = FVector(cordFrameIn[3].X, cordFrameIn[3].Y, height);
		Vertices[51] = cordFrameOut[3];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 48, 49, 50, 51);

		//윗쪽 면
		Vertices[52] = cordFrameIn[3];
		Vertices[53] = cordFrameIn[2];
		Vertices[54] = FVector(cordFrameIn[2].X, cordFrameIn[2].Y, height);
		Vertices[55] = FVector(cordFrameIn[3].X, cordFrameIn[3].Y, height);
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 52, 53, 54, 55);

		//오른쪽 면
		Vertices[56] = FVector(cordFrameIn[1].X, cordFrameIn[1].Y, 0);
		Vertices[57] = cordFrameOut[1];
		Vertices[58] = cordFrameOut[2];
		Vertices[59] = FVector(cordFrameIn[2].X, cordFrameIn[2].Y, height);
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 56, 57, 58, 59);

		//아래쪽 면
		Vertices[60] = FVector(cordFrameIn[0].X, cordFrameIn[0].Y, 0);
		Vertices[61] = FVector(cordFrameIn[1].X, cordFrameIn[1].Y, 0);
		Vertices[62] = cordFrameIn[1];
		Vertices[63] = cordFrameIn[0];
		UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 60, 61, 62, 63);
	}
	UVs[0] = UVs[4] = UVs[8] = UVs[12] = UVs[16] = UVs[20] = UVs[24] = UVs[28] = UVs[32] = UVs[36] = UVs[40] = UVs[44] = UVs[48] = UVs[52] = UVs[56] = UVs[60] = FVector2D(0.f, 1.f);
	UVs[1] = UVs[5] = UVs[9] = UVs[13] = UVs[17] = UVs[21] = UVs[25] = UVs[29] = UVs[33] = UVs[37] = UVs[41] = UVs[45] = UVs[49] = UVs[53] = UVs[57] = UVs[61] = FVector2D(1.f, 1.f);
	UVs[2] = UVs[6] = UVs[10] = UVs[14] = UVs[18] = UVs[22] = UVs[26] = UVs[30] = UVs[34] = UVs[38] = UVs[42] = UVs[46] = UVs[50] = UVs[54] = UVs[58] = UVs[62] = FVector2D(1.f, 0.f);
	UVs[3] = UVs[7] = UVs[11] = UVs[15] = UVs[19] = UVs[23] = UVs[27] = UVs[31] = UVs[35] = UVs[39] = UVs[43] = UVs[47] = UVs[51] = UVs[55] = UVs[59] = UVs[63] = FVector2D(0.f, 0.f);

	UKismetProceduralMeshLibrary::CalculateTangentsForMesh(Vertices, Triangles, UVs, Normals, Tangents);

	MaterialPart.Add(meshNum, "frame");
	model->CreateMeshSection(meshNum++, Vertices, Triangles, Normals, UVs, TArray<FColor>(), Tangents, true);

	//Window Mesh
	InitVertices(8);

	Vertices[0] = FVector(cordFrameIn[0].X, 1, cordFrameIn[0].Z);
	Vertices[1] = FVector(cordFrameIn[1].X, 1, cordFrameIn[1].Z);
	Vertices[2] = FVector(cordFrameIn[2].X, 1, cordFrameIn[2].Z);
	Vertices[3] = FVector(cordFrameIn[3].X, 1, cordFrameIn[3].Z);
	UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 0, 1, 2, 3);

	Vertices[4] = FVector(cordFrameIn[7].X, -1, cordFrameIn[7].Z);
	Vertices[5] = FVector(cordFrameIn[6].X, -1, cordFrameIn[6].Z);
	Vertices[6] = FVector(cordFrameIn[5].X, -1, cordFrameIn[5].Z);
	Vertices[7] = FVector(cordFrameIn[4].X, -1, cordFrameIn[4].Z);
	UKismetProceduralMeshLibrary::ConvertQuadToTriangles(Triangles, 4, 5, 6, 7);

	UVs[0] = UVs[4] = FVector2D(0.f, 1.f);
	UVs[1] = UVs[5] = FVector2D(1.f, 1.f);
	UVs[2] = UVs[6] = FVector2D(1.f, 0.f);
	UVs[3] = UVs[7] = FVector2D(0.f, 0.f);

	UKismetProceduralMeshLibrary::CalculateTangentsForMesh(Vertices, Triangles, UVs, Normals, Tangents);

	MaterialPart.Add(meshNum, "glass");
	model->CreateMeshSection(meshNum++, Vertices, Triangles, Normals, UVs, TArray<FColor>(), Tangents, true);

	ApplyMaterial(data.materials);
}
