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
class C : public B
{
public:
    void introduceC()
    {
        cout << "Hello from class C" << endl;
    }
};
int main()
{
    C obj;
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
