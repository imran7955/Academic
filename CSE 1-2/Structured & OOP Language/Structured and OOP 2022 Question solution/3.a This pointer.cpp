#include <iostream>
using namespace std;
class Point{
private:
    int x,y;
public:
    Point(){
        x = y = 0;
    }
    Point(int a,int b){
        x = a,y = b;
    }
    void display()
    {
        cout << "(" << x << "," << y << ")" << endl;
    }
    // Use of this pointer
    Point increment()
    {
        x++,y++ ;
        return *this;
    }
};
int main() {
    Point p(3,4),q;
    q = p.increment();
    q.display();
    return 0;
}
/* OUTPUT:
        (4,5)
*/
