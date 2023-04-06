#include<iostream>
using namespace std;
class Base{
public:
	virtual void display()
	{
		cout << "This is base class" << endl;
	}
};
class Derived : public Base
{
public:
	void display()
	{
		cout << "This is derived class" << endl;
	}
};
int main()
{
	Base b;
	Derived d;
	Base* ptr[2];
	ptr[0] = &b,ptr[1] = &d;
	for(int i = 0; i < 2; i++)
		ptr[i]->display();
	return 0;
}