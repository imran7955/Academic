// Program to show the use of friend function
#include<iostream>
using namespace std;
class human{
private:
	int age;
public:
	human(int a) : age(a){}
	friend void sum_age(human,human);
};
void sum_age(human h1,human h2)
{
	cout << "Sum of age is " << h1.age + h2.age << endl;
}
int main()
{
	human h1(24),h2(21);
	sum_age(h1,h2);
	return 0;
}