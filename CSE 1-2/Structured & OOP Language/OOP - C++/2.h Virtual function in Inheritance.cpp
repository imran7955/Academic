/*
    -> Base Class human has a function named, introduce()
    -> Derived Class student has a function named, introduce()

        human -> virtual void introduce() {}
          /
      student -> void introduce() {}

    To call a derived class function using base class pointer
    you have to make the base class function virtual.
*/
#include<iostream>
using namespace std;
class human
{
public:
    //void introduce()
    virtual void introduce()
    {
        cout << "Hello, I am a Human" << endl;
    }
};

class student : public human
{
public:
    void introduce()
    {
        cout << "Hello, I am a Student" << endl;
    }
};

int main()
{
    human* p = new human;
    p -> introduce();
    p = new student;
    p -> introduce();
    return 0;
}
/* OUTPUT:
    if base class function isn't virtual,
        Hello, I am a Human
        Hello, I am a Human
    if base class function is virtual,
        Hello, I am a Human
        Hello, I am a Student
*/
