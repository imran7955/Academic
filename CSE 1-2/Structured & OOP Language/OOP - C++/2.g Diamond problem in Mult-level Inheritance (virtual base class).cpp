/*
    -> Class A has a function named, introduce()
    -> Class B and C derive from Class A.
    -> Class D derives from both B and C

        A
      /   \
     B     C
      \   /
        D

    The problem is, in Class D the introduce() function of class A
    is copied two times which results in ambiguity. To avoid that we
    have to inherit Class A virtually.
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

class B : virtual public A
{

};
class C : virtual public A
{

};
class D : public B,public C
{

};
int main()
{
    D obj;
    obj.introduce();
    return 0;
}
/* OUTPUT
    Hello from Class A
*/
