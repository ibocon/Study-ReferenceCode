#include<stdio.h>
#include<stdlib.h>
#define Max_n 10
#define Max_c 50

int C,N,M;

int Friend[Max_n][Max_n],V[Max_n],Sol[Max_c];
int Sum;

void countParings(int p)
{
    int i;

	if(p==N){
		Sum++;
		return;
	}

	if(V[p]){
		countParings(p+1);
		return;
	}

	V[p]=1;
	for(i=p+1; i<N; i++){
		if(Friend[p][i] && V[i]==0){
			V[i]=1;
			countParings(p+1);
			V[i]=0;
		}
	}
	V[p]=0;
}

int main()
{
	int i,j,k;
	int p1,p2;

	scanf("%d",&C);

	for(i=0; i<C; i++){
		scanf("%d%d",&N,&M);
		for(j=0; j<M; j++){
			scanf("%d%d",&p1,&p2);
			Friend[p1][p2]=Friend[p2][p1]=1;
		}

		countParings(0);
		Sol[i]=Sum;

		Sum=0;
		for(j=0; j<N; j++){
			for(k=0; k<N; k++){
				Friend[j][k]=0;
			}
		}
	}

	for(i=0; i<C; i++){
		printf("%d\n",Sol[i]);
	}
//	system("PAUSE");

	return 0;
}
