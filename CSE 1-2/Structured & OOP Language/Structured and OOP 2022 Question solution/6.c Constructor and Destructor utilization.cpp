#include <iostream>
using namespace std;
class animal{
    int id;
public:
    animal()
    {
        id = 0;
        cout << "Constructor is called." << endl;
    }
    ~animal()
    {
        cout << "Destructor is called." << endl;
    }
};
int main() {
    animal obj;
    cout << "Program end." << endl;
    return 0;
}
/* OUTPUT:
    Constructor is called.
    Program end.
    Destructor is called.
*/
