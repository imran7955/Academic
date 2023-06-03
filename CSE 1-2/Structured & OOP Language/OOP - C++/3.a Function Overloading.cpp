#include <iostream>
using namespace std;

class calc {
   public:
    void sum(int x,int y){
        cout << "Sum(" << x << "," << y << ") = " << x+y << endl;
    }
    void sum(double x,double y){
        cout << "Sum(" << x << "," << y << ") = " << x+y << endl;
    }
    void sum(int x,int y,int z){
        cout << "Sum(" << x << "," << y << "," << z << ") = " << x+y+z << endl;
    }
};

int main() {
    calc obj;
    obj.sum(3,3);
    obj.sum(3.5,2.4);
    obj.sum(3,3,2);
    return 0;
}
/* OUTPUT:
    Sum(3,3) = 6
    Sum(3.5,2.4) = 5.9
    Sum(3,3,2) = 8
*/