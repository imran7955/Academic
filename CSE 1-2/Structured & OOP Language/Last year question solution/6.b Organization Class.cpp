#include<iostream>
using namespace std;
class Organization{
private:
	string name,address;
	int stablishment_year;
public:
	Organization(string n,int y,string a)
	{
		name = n,stablishment_year = y,address = a;
	}
	~Organization() {}
	void display()
	{
		cout << "Name       = " << name << endl;
		cout << "Stablished = " << stablishment_year << endl;
		cout << "address    = " << address << endl;
	}
};

int main()
{
	Organization o1("United Nations",1945,"Ney York - 10017");
	o1.display();
	return 0;
}