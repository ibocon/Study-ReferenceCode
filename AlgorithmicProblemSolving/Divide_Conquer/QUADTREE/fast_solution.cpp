#include <iostream>
#include <string>
#include <vector>
using namespace std;

string reverse(string::iterator& it)
{
	char head = *it;
	it++;
	if (head == 'b' || head == 'w') return string(1, head);
	string upperLeft = reverse(it);
	string upperRight = reverse(it);
	string lowerLeft = reverse(it);
	string lowerRight = reverse(it);

	return string("x") + lowerLeft + lowerRight + upperLeft + upperRight;
}

int main()
{
	int T;
	cin >> T;
	while (T--)
	{
		string s;
		cin >> s;
		string::iterator it = s.begin();
		cout << reverse(it) << endl;
	}
	return 0;
}
