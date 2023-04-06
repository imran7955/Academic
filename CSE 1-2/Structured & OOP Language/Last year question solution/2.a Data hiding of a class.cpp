// How a class achieve data hiding
#include<iostream>
using namespace std;
class human{
private:
	int nid;
public:
	human (int n){
		nid = n;
	}
	void display()
	{
		cout << "NID = " << nid << endl;
	}
};
int main()
{
	human h1(46871545);
	// cout << h1.nid << endl; => ERROR!
	h1.display(); 
	return 0;
}