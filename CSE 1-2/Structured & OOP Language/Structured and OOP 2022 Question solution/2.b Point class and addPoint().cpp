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
    Point addPoint(Point a,Point b)
    {
        return Point(a.x + b.x, a.y + b.y);
    }
};
int main() {
    Point m(3,4),n(2,7),sum;
    sum = m.addPoint(m,n);
    sum.display();
    return 0;
}
