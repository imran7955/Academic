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
    // Overloading the (+) operator
    Point operator +(Point p)
    {
         return Point(x+p.x, y+p.y);
    }
    // Overloading the (-) operator
    Point operator -(Point p)
    {
        return Point(x-p.x, y-p.y);
    }
    // Overloading the (++) operator in prefix form
    Point operator ++()
    {
        y++;
        return *this;
    }
};
int main() {
    Point m(3,4),n(2,7),sum,diff,inc;
    sum = m + n;
    sum.display();
    diff = m - n;
    diff.display();
    inc = ++m;
    inc.display();
    return 0;
}

/* OUTPUT:
        (5,11)
        (1,-3)
        (3,5)
*/
