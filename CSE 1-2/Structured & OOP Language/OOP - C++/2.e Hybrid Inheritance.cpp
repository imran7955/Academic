/*
          A
         /
        B      C
         \   /
           D
There is a single inheritance between A and B. D has a Multiple inheritance with B,C.
As B is also a derived class, the inheritance between D and B can be called multi-level
inheritance.
*/
#include<iostream>
using namespace std;
class A
{
public:
    void introduceA()
    {
        cout << "Hello from class A" << endl;
    }
};
class B : public A
{
public:
    void introduceB()
    {
        cout << "Hello from class B" << endl;
    }
};
class C
{
public:
    void introduceC()
    {
        cout << "Hello from class C" << endl;
    }
};
class D : public B, public C
{
public:
    void introduceD()
    {
        cout << "Hello from class D" << endl;
    }
};
int main()
{
    D obj;
    obj.introduceA();
    obj.introduceB();
    obj.introduceC();
    return 0;
}
/* OUTPUT
    Hello from class A
    Hello from class B
    Hello from class C
*/
