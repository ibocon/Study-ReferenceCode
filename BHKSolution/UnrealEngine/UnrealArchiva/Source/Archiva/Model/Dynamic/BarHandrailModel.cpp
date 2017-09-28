// Fill out your copyright notice in the Description page of Project Settings.

#include "Archiva.h"
#include "BarHandrailModel.h"

ABarHandrailModel::ABarHandrailModel()
{
	H_length = 200;
	H_width = 10;
	H_height = 7;

	P_length = 6;
	P_width = H_width - 2;
	P_height = 100;

	B_height = 15;
	B_width = P_width - 5;

	interval = 50;

}

void ABarHandrailModel::Create(FModelData data)
{
	model->ClearAllMeshSections();
	meshNum = 0;
	SavedMaterial = data.materials;
	//data값을 입력받아 저장하는 과정
	H_length = data.size.X;
	P_height = data.height - H_height;
	angle = data.pitch;
	interval = data.interval;

	float trig_x = CalcTrigX(H_length);
	float trig_y = CalcTrigY(H_length);

	/*손잡이*/
	FVector cordinate[8];
	cordinate[0] = FVector(0, H_width / 2, P_height);
	cordinate[1] = FVector(trig_x, H_width / 2, P_height + trig_y);
	cordinate[2] = FVector(trig_x, H_width / 2, P_height + H_height + trig_y);
	cordinate[3] = FVector(0, H_width / 2, P_height + H_height);

	cordinate[4] = FVector(0, -(H_width / 2), P_height);
	cordinate[5] = FVector(trig_x, -(H_width / 2), P_height + trig_y);
	cordinate[6] = FVector(trig_x, -(H_width / 2), P_height + H_height + trig_y);
	cordinate[7] = FVector(0, -(H_width / 2), P_height + H_height);

	//왼쪽 side
	CreateQuad(cordinate[4], cordinate[0], cordinate[3], cordinate[7], "handle", FVector(-1, 0, 0));
	//위쪽 side
	CreateQuad(cordinate[3], cordinate[2], cordinate[6], cordinate[7], "handle", FVector(0, 0, 1));
	//오른쪽 side
	CreateQuad(cordinate[1], cordinate[5], cordinate[6], cordinate[2], "handle", FVector(1, 0, 0));
	//아래쪽 side
	CreateQuad(cordinate[4], cordinate[5], cordinate[1], cordinate[0], "handle", FVector(0, 0, -1));
	//앞면
	CreateQuad(cordinate[0], cordinate[1], cordinate[2], cordinate[3], "handle", FVector(0, 1, 0));
	//뒷면
	CreateQuad(cordinate[7], cordinate[6], cordinate[5], cordinate[4], "handle", FVector(0, -1, 0));

	/*첫번째 기둥*/
	CreatePillar(FVector(P_length / 2 + 2, 0, 0));
	/*마지막 기둥*/
	CreatePillar(FVector(trig_x - P_length / 2 - 2, 0, trig_y));

	/*반복되는 부분 제작*/
	float startP = P_length / 2 + (P_length + interval);
	float endP = H_length - P_length / 2;
	while (startP < endP)
	{
		CreatePillar(FVector(CalcTrigX(startP), 0, CalcTrigY(startP)));

		startP += (P_length + interval);
	}
	float barP = 10;
	while (barP < (P_height - 10))
	{
		CreateBar(barP);
		barP += (10 + B_height);
	}

	//material을 변경하는 함수
	ApplyMaterial(data.materials);
}

void ABarHandrailModel::CreatePillar(FVector std)
{
	float l = P_length / 2;
	float w = P_width / 2;
	float h = P_height + H_height / 2;

	FVector cordinate[8];
	cordinate[0] = FVector(std.X - l, std.Y + w, std.Z);
	cordinate[1] = FVector(std.X + l, std.Y + w, std.Z);
	cordinate[2] = FVector(std.X + l, std.Y + w, std.Z + h);
	cordinate[3] = FVector(std.X - l, std.Y + w, std.Z + h);

	cordinate[4] = FVector(std.X - l, std.Y - w, std.Z);
	cordinate[5] = FVector(std.X + l, std.Y - w, std.Z);
	cordinate[6] = FVector(std.X + l, std.Y - w, std.Z + h);
	cordinate[7] = FVector(std.X - l, std.Y - w, std.Z + h);

	//왼쪽 side
	CreateQuad(cordinate[4], cordinate[0], cordinate[3], cordinate[7], "pillar", FVector(-1, 0, 0));
	//위쪽 side
	CreateQuad(cordinate[3], cordinate[2], cordinate[6], cordinate[7], "pillar", FVector(0, 0, 1));
	//오른쪽 side
	CreateQuad(cordinate[1], cordinate[5], cordinate[6], cordinate[2], "pillar", FVector(1, 0, 0));
	//아래쪽 side
	CreateQuad(cordinate[4], cordinate[5], cordinate[1], cordinate[0], "pillar", FVector(0, 0, -1));
	//앞면
	CreateQuad(cordinate[0], cordinate[1], cordinate[2], cordinate[3], "pillar", FVector(0, 1, 0));
	//뒷면
	CreateQuad(cordinate[7], cordinate[6], cordinate[5], cordinate[4], "pillar", FVector(0, -1, 0));
}

void ABarHandrailModel::CreateBar(float pos)
{
	float trig_x = CalcTrigX(H_length);
	float trig_y = CalcTrigY(H_length);

	FVector barCord[8];
	barCord[0] = FVector(P_length / 2 + 2, B_width / 2, pos);
	barCord[1] = FVector(trig_x - P_length / 2 - 2, B_width / 2, pos + trig_y);
	barCord[2] = FVector(trig_x - P_length / 2 - 2, B_width / 2, pos + B_height + trig_y);
	barCord[3] = FVector(P_length / 2 + 2, B_width / 2, pos + B_height);

	barCord[4] = FVector(P_length / 2 + 2, -(B_width / 2), pos);
	barCord[5] = FVector(trig_x - P_length / 2 - 2, -(B_width / 2), pos + trig_y);
	barCord[6] = FVector(trig_x - P_length / 2 - 2, -(B_width / 2), pos + B_height + trig_y);
	barCord[7] = FVector(P_length / 2 + 2, -(B_width / 2), pos + B_height);

	//왼쪽 side
	CreateQuad(barCord[4], barCord[0], barCord[3], barCord[7], "bar", FVector(-1, 0, 0));
	//위쪽 side
	CreateQuad(barCord[3], barCord[2], barCord[6], barCord[7], "bar", FVector(0, 0, 1));
	//오른쪽 side
	CreateQuad(barCord[1], barCord[5], barCord[6], barCord[2], "bar", FVector(1, 0, 0));
	//아래쪽 side
	CreateQuad(barCord[4], barCord[5], barCord[1], barCord[0], "bar", FVector(0, 0, -1));
	//앞면
	CreateQuad(barCord[0], barCord[1], barCord[2], barCord[3], "bar", FVector(0, 1, 0));
	//뒷면
	CreateQuad(barCord[7], barCord[6], barCord[5], barCord[4], "bar", FVector(0, -1, 0));
}

float ABarHandrailModel::CalcTrigX(float length)
{
	return length * cos(angle*PI / 180);
}

float ABarHandrailModel::CalcTrigY(float length)
{
	return length * sin(angle*PI / 180);
}

