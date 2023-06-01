/*
The inheritance in which a single base class is inherited by multiple
derived classes is known as the Hierarchical Inheritance.
            F
         /     \
        C        E
      /   \     /
     A     B   D
In this example F is inherited by C,E. So F,C,E forms a tree.
Further, A and B inherits from C, D inherits from E. Which makes the
tree more visible.
 */
#include<iostream>
using namespace std;
class F
{
public:
    void introduceF()
    {
        cout << "Hello from class F" << endl;
    }
};
class C : public F
{
public:
    void introduceC()
    {
        cout << "Hello from class C" << endl;
    }
};
class E : public F
{
public:
    void introduceE()
    {
        cout << "Hello from class E" << endl;
    }
};
class A : public C
{
public:
    void introduceA()
    {
        cout << "Hello from class A" << endl;
    }
};
class B : public C
{
public:
    void introduceB()
    {
        cout << "Hello from class B" << endl;
    }
};

class D : public E
{
public:
    void introduceD()
    {
        cout << "Hello from class D" << endl;
    }
};


int main()
{
    A obj;
    obj.introduceA();
    obj.introduceC();
    obj.introduceF();
    D obj2;
    obj2.introduceD();
    obj2.introduceE();
    obj2.introduceF();
    return 0;
}
/* OUTPUT
    Hello from class A
    Hello from class C
    Hello from class F
    Hello from class D
    Hello from class E
    Hello from class F
*/
