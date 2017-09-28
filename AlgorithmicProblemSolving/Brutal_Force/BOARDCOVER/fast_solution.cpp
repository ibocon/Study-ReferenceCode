#include <stdio.h>

int H, W, ans;
char map[20][21];

int dir[4][3][2] = {
	{ {0, 0}, {1, 0}, {1, -1} },
	{ {0, 0}, {1, 0}, {1, 1} },
	{ {0, 0}, {0, 1}, {1, 1} },
	{ {0, 0}, {0, 1}, {1, 0} }
};

void dfs(int y, int x){
	int i, j, ny, nx;

	if (x>=W) x = 0, y ++;
	if (y>=H){
		ans ++;
		return;
	}
	if (map[y][x]=='#'){
		dfs(y, x+1);
		return;
	}

	for (i=0; i<4; i++){
		for (j=0; j<3; j++){
			ny = y+dir[i][j][0];
			nx = x+dir[i][j][1];

			if (ny<0 || ny>=H || nx<0 || nx>=W) break;
			if (map[ny][nx]=='#') break;
		}
		if (j==3){
			for (j=0; j<3; j++) map[y+dir[i][j][0]][x+dir[i][j][1]] = '#';
			dfs(y, x+1);
			for (j=0; j<3; j++) map[y+dir[i][j][0]][x+dir[i][j][1]] = '.';
		}
	}
}

int main(void){
	int t, i;

	scanf("%d", &t);
	while (t--){
		scanf("%d%d", &H, &W);
		for (i=0; i<H; i++) scanf("%s", map[i]);

		ans = 0;
		dfs(0, 0);
		printf("%d\n", ans);
	}
	return 0;
}
