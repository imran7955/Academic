#include <iostream>
using namespace std;
class A{
public:
    void introduce()
    {
        cout << "Hello from class A." << endl;
    }
};
class B{
public:
    void introduce()
    {
        cout << "Hello from class B." << endl;
    }
};
class C : public A, public B
{

};
int main() {

    C obj;
    // obj.introduce(); -> error: request for member 'introduce'  is ambiguous
    obj.A::introduce();
    obj.B::introduce();
    return 0;
}
/* OUTPUT:
    Hello from class A.
    Hello from class B.
*/
