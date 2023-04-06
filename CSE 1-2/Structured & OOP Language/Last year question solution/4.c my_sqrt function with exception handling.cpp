#include<iostream>
using namespace std;

float my_sqrt(float n)
{
	if(n < 0) throw -1;
	else
	{
		float root = 0;
		for(int i = 0; i * i <= n; i++)
			root = i;
		float increment = 0.1;
		for(int i = 0; i < 5; i++)
		{
			while(root * root <= n){
				root += increment;
			}
			root -= increment;
			increment /= 10;
		}
		return root;
	}
}
int main()
{
	try{
	cout << my_sqrt(-3) << endl;
	}
	catch(int c)
	{
		cout << "Math ERROR!" << endl;
	}
	return 0;
}