/*
    0 = has only default constructor
    1 = has both default and parameterized constructor
    
    base      derived   empty/argumented call     constructor called
                        of creating object
    0           0           0                   base d, derived d
    0           1           0                   base d, derived d
    0           1           1                   base d, derived p
    1           0           0                   base d, derived d
    1           1           0                   base d, derived d
    1           1           1                   base d, derived p
    
    The fact is when you create an object of the derived class, one constructor of the base class must be called.
    Either you call any of them (default/parameterized) by your derived class constructor or the default constructor
    will be called automatically.
*/
#include <iostream>
using namespace std;

class base{
public:
    base(){
        cout << "Base default" << endl;
    }   
    //*
    base(int x){
        cout << "Base parameterized" << endl;
    }
    //*/
} ;
class derived : public base
{
 public:
    derived(){
        cout << "Derived default" << endl;
    }
    //*
    derived(int n){
        cout << "Derived param" << endl;
    }
    //*/
};
int main()
{
    //derived obj;
    derived ob(4);
    return 0;
}