#include<iostream>
using namespace std;
class human // base 1
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
class citizen // base 2
{
protected:
    int nid;
public:
    citizen() : nid(0) {}
    citizen(int n) { nid = n;}
    void introduce()
    {
        cout << "Nid = " << nid << endl;
    }
};
class student : public human,public citizen
{
protected:
    int roll; float cgpa;
public:
    student(string n,int id,int a,int r,float c)
    {
        name = n,age = a; // from base class 1
        nid = id; // from base class 2
        roll = r,cgpa = c;
    }
    void introduce()
    {
        human::introduce(); // from base class 1
        citizen::introduce(); // from base class 2
        cout << "Roll = " << roll << endl;
        cout << "CGPA = " << cgpa << endl;
    }
};
int main()
{
    student s1("Mike",13457897,24,14,3.45);
    s1.introduce();
    return 0;
}
/* OUTPUT
    Hello, I am Mike, age = 24
    Nid = 13457897
    Roll = 14
    CGPA = 3.45
*/
