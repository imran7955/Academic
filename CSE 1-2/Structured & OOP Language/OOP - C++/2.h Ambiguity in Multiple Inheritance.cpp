/*
    -> Class A has a function named, introduce()
    -> Class B has a function named, introduce()
    -> Class C derives from both A and B

    when introduce() is called from a C-type object,
    the compiler arises the Ambiguity problem.
    It can be solved using the scope resolution (::) operator to
    indicate which class's function we want.
*/
#include<iostream>
using namespace std;
class A // base 1
{
public:
    void introduce()
    {
        cout << "Hello from Class A" << endl;
    }
};

class B // base 2
{
public:
    void introduce()
    {
        cout << "Hello from Class B" << endl;
    }
};
class C : public A,public B
{
public:
    void show()
    {
        cout << "Hello from Class C" << endl;
    }
};
int main()
{
    C obj;
    //obj.introduce(); error: request for member 'introduce' is ambiguous
    obj.A::introduce();
    obj.B::introduce();
    obj.show();
    return 0;
}
/* OUTPUT
    Hello from Class A
    Hello from Class B
    Hello from Class C
*/
