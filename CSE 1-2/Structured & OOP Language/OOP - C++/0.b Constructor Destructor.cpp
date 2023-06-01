#include<iostream>
using namespace std;

class time
{
private:
    int h,m,s;
public:
    // default constructor
    time()
    {
        h = m = s = 0;
    }
    // parameterized constructor
    time(int hour,int minute, int second)
    {
        h = hour,m = minute,s = second;
    }
    /* parameterized constructor - alternative implementation
    time(int hour,int minute, int second) : h(hour),m(minute),s(second) {}
    */

    // copy constructor - default
    /* If the user doesn't define any copy constructor then the compiler
       supplies the copy constructor. It copies all values from the parameter object
       to the created object, as it is in the parameter object.
    */
    // copy constructor - user defined (suppose we want to copy the doubled values)
    time(time &obj)
    {
        h = 2*obj.h, m = 2*obj.m, s = 2*obj.s;
    }
    // destructor
    ~time()
    {
        cout << "Destructor is called" << endl;
    }
    void show()
    {
        cout << h << ":" << m << ":" << s << endl;
    }
};
int main()
{

    time t1(2,3,5);
    time t2;
    time t3 = t1; // copy constructor is called
    time t4(t1); // copy constructor is called
    t1.show();
    t2.show();
    t3.show();
    t4.show();
    return 0;
}

/* OUTPUT - if there is user defined copy constructor
    2:3:5
    0:0:0
    4:6:10
    4:6:10
    Destructor is called
    Destructor is called
    Destructor is called
    Destructor is called

    OUTPUT - if there is no user defined copy constructor
    2:3:5
    0:0:0
    2:3:5
    2:3:5
    Destructor is called
    Destructor is called
    Destructor is called
    Destructor is called
*/
