#include<iostream>
using namespace std;
class Base{
public:
	void sum(int a,int b)
	{
		cout << "Sum = " << a+b << endl;
	}
	virtual void display()
	{
		cout << "This is base class" << endl;
	}
};
class Derived : public Base
{
public:
	void sum(int a,int b,int c)
	{
		cout << "Sum = " << a+b+c << endl;
	}
	void display()
	{
		cout << "This is derived class" << endl;
	}
};
int main()
{
	Base b;
	Derived d;
	Base* ptr;

	// function overloading
	b.sum(1,2);  
	d.sum(1,2,3);

	// function overriding
	ptr = &b;
	ptr->display();
	ptr = &d;
	ptr->display();
	return 0;
}