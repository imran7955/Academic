#include<iostream>
using namespace std;
class MyClass{
public:
	void f();
};
void MyClass::f()
{
	cout << "Function is working" << endl;
}
int main()
{
	MyClass obj;
	obj.f();
	return 0;
}