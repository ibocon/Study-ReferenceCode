#include<stdio.h>

int order[10]={1, 4, 9, 2, 3, 7, 8, 0, 5, 6};
int group[10][5]={{1, 11}, {2, 8, 12}, {1, 13}, {1, 10}, {1, 6}, {1, 7}, {2, 4, 5}, {1, 1}, {2, 0, 2}, {3, 3, 14, 15}};
int clock[16];


void solve()
{
	int i, j, k, target[4], count=0;
	for(i=0; i<16; i++)	scanf("%d", &clock[i]);
	for(i=0; i<10; i++)
	{
		for(j=0; j<group[i][0]; j++)	target[j]=group[i][1+j];
		for(j=0; j<group[i][0]; j++)
		{
			while(clock[target[j]]!=12)
			{
				switch(order[i])
				{
				case 0:	clock[0]+=3; clock[1]+=3; clock[2]+=3;
					break;
				case 1:	clock[3]+=3; clock[7]+=3; clock[9]+=3; clock[11]+=3;
					break;
				case 2:	clock[4]+=3; clock[10]+=3; clock[14]+=3; clock[15]+=3;
					break;
				case 3:	clock[0]+=3; clock[4]+=3; clock[5]+=3; clock[6]+=3; clock[7]+=3;
					break;
				case 4:	clock[6]+=3; clock[7]+=3; clock[8]+=3; clock[10]+=3; clock[12]+=3;
					break;
				case 5:	clock[0]+=3; clock[2]+=3; clock[14]+=3; clock[15]+=3;
					break;
				case 6:	clock[3]+=3; clock[14]+=3; clock[15]+=3;
					break;
				case 7:	clock[4]+=3; clock[5]+=3; clock[7]+=3; clock[14]+=3; clock[15]+=3;
					break;
				case 8:	clock[1]+=3; clock[2]+=3; clock[3]+=3; clock[4]+=3; clock[5]+=3;
					break;
				case 9:	clock[3]+=3; clock[4]+=3; clock[5]+=3; clock[9]+=3; clock[13]+=3;
					break;
				}
				count++;
				for(k=0; k<16; k++)	if(clock[k]>12)	clock[k]-=12;
			}
		}
	}
	for(i=0; i<16; i++)
	{
		if(clock[i]!=12)
		{
			printf("-1\n");
			return;
		}
	}
	printf("%d\n", count);
}

int main()
{
	int i, n;
	scanf("%d", &n);
	for(i=0; i<n; i++)	solve();
	return 0;
}
