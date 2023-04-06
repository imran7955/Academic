#include<iostream>
using namespace std;
class Parallelogram{
public:
	Parallelogram()
	{
		cout << "Parallelogram consturctor is called" << endl;
	}
	void display(){
		cout << "From the Base class" << endl;
	}
};
class Rectangle : virtual public Parallelogram{

};
class Rhombus : virtual public Parallelogram{

};
class Square : public Rectangle, public Rhombus{

};
int main()
{
	Square obj;
	obj.display();
	return 0;
}