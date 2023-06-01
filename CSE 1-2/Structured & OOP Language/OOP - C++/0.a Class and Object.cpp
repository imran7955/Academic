#include<iostream>
using namespace std;

class student
{
public:
    int roll; float cgpa;
    string name;
    void show()
    {
        cout << "Name = " << name << endl;
        cout << "Roll = " << roll << endl;
        cout << "CGPA = " << cgpa << endl;
    }
};
int main()
{
    student s1;
    s1.name = "John",s1.roll = 7,s1.cgpa = 3.67;
    s1.show();
    return 0;
}
