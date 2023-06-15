#include <iostream>
using namespace std;
class Point{
private:
    float x,y;
public:
    Point(){
        x = y = 0;
    }
    Point(float a,float b){
        x = a,y = b;
    }
    // The conversion function
    operator float(){
        return x+y;
    }
};
int main() {
    Point p(3,4.5);
    float x = p;
    cout << x << endl;
    x = 10 + p;
    cout << x << endl;
    return 0;
}
