#include<iostream>
using namespace std;
class Mammals{
public:
	void display()
	{
		cout << "I am Mammals" << endl;
	}
};
class MarineAnimal{
public:
	void display()
	{
		cout << "I am MarineAnimal" << endl;
	}
};
class BlueWhale : public Mammals,public MarineAnimal
{
public:
	void display()
	{
		cout << "I belong to both the categories" << endl;
	}
};

int main()
{
	Mammals mam;
	MarineAnimal mA;
	BlueWhale bw;

	mam.display();
	mA.display();
	bw.display();

	bw.Mammals::display();
	bw.MarineAnimal::display();
	return 0;
}