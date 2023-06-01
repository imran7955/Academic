#include<iostream>
using namespace std;
class human
{
protected:
    string name; int age;
public:
    human() : name(""),age(0) {}
    human(string n,int a)
    {
        name = n,age = a;
    }
    void introduce()
    {
        cout << "Hello, I am " << name << ", age = " << age << endl;
    }
};
class student : public human
{
protected:
    int roll; float cgpa;
public:
    student(string n,int a,int r,float c)
    {
        name = n,age = a; // from base class
        roll = r,cgpa = c;
    }
    void introduce()
    {
        human::introduce(); // from base class
        cout << "Roll = " << roll << endl;
        cout << "CGPA = " << cgpa << endl;
    }
};
int main()
{
    student s1("Mike",24,14,3.45);
    s1.introduce();
    return 0;
}

/* OUTPUT
    Hello, I am Mike, age = 24
    Roll = 14
    CGPA = 3.45
*/
