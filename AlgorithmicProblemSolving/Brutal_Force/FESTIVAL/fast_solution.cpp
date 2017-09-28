#include <cstdio>

using namespace std;

int data[1001];

struct frac_s
{
    int n;
	int d;
	inline frac_s(int n = 0, int d = 1) : n(n), d(d) { }

	inline bool friend operator < (const frac_s &p, const frac_s &q)
	{
		return p.n * q.d < p.d * q.n;
	}
};

frac_s fracs[1001];
frac_s total;
int fracs_head;
int fracs_tail;

inline int nextInt() {
    #define getchar getchar_unlocked
	register int s = 0, ch;
	for(ch = getchar(); ch < '0' || ch > '9'; ch = getchar());
	for(s = ch - '0', ch = getchar(); ch >= '0' && ch <= '9'; ch = getchar())
		s = s * 10 + ch - '0';
	return s;
}

inline void push(int v)
{
	fracs[fracs_tail++] = frac_s(v);
	for (;fracs_tail - fracs_head >= 2;)
	{
		frac_s &p = fracs[fracs_tail - 1];
		frac_s &q = fracs[fracs_tail - 2];
		if (q < p)
		{
			q.n += p.n;
			q.d += p.d;
			fracs_tail--;
		}
		else
			break;
	}
}

inline void pop()
{
	for (;fracs_tail - fracs_head >= 1;)
	{
		frac_s &p = fracs[fracs_head];
		if (total < p)
		{
			total.n -= p.n;
			total.d -= p.d;
			fracs_head++;
		}
		else
			break;
	}
}

int main()
{
	int tc = nextInt();
	for (;tc--;)
	{
		int N = nextInt();
		int L = nextInt();
		int i;
		for (i = 0;i < N;i++)
			data[i] = nextInt();

		fracs_head = fracs_tail = 0;
		total = frac_s(0, 0);
		for (i = 0;i < L - 1;i++)
		{
			total.n += data[i];
			total.d++;
		}

		frac_s ans(1, 0);
		for (;i < N;i++)
		{
			total.n += data[i];
			total.d++;
			pop();
			if (total < ans)
				ans = total;
			push(data[i - (L - 1)]);
		}

		printf("%.8lf\n", ans.n / (double)ans.d);
	}
	return 0;
}
