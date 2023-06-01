#include<iostream>
using namespace std;

class base
{
public:
    //~base()
    virtual ~base()
    {
        cout << "Base destructor is called" << endl;
    }
};
class derived : public base
{
public:
    ~derived(){
        cout << "Derived destructor is called" << endl;
    }
};
int main()
{

    base* p = new base;
    delete p; // instruction1
    p = new derived;
    delete p; // instruction2
    return 0;
}

/* OUTPUT - if base destructor isn't virtual
    Base destructor is called // for instruction1
    Base destructor is called // for instruction2

    OUTPUT - if base destructor is virtual

    Base destructor is called // for instruction1
    Derived destructor is called // for instruction2
    Base destructor is called  // for instruction2

    Here the third line is there because,
    A derived class calls its base constructor then self-constructor,
    A derived class calls its self-destructor and then the base destructor.
*/
