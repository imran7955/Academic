#include <iostream>
#include <cmath>
using namespace std;
class Point
{
    int abscissa,ordinate;
public:
    Point() : abscissa(0),ordinate(0) {}
    Point(int a,int b)
    {
        abscissa = a, ordinate = b;
    }
    int operator [](int f)
    {
        if(f == 0) return abscissa;
        else return ordinate;
    }
    double operator ()(string s)
    {
        if(s == "dis") 
            return sqrt(abscissa*abscissa + ordinate*ordinate);
        else if(s == "angle")
            return atan(ordinate*1.0 / abscissa) * 180 / 3.14;
        else return 0;
    }
    Point* operator ->()
    {
        return this;
    }
    
    void print()
    {
        cout << "Abscissa = " << abscissa << endl;
        cout << "Ordinate = " << ordinate << endl;
    }
};

int main()
{
    
    Point p1(4,2);
    cout << "Abscissa by [] = " << p1[0] << endl;
    cout << "Distance from Origin by () = " << p1("dis") << endl;
    cout << "Angle by () = " << ("angle") << endl;
    p1 -> print();
    
    return 0;
}
/* OUTPUT: 
    Abscissa by [] = 4
    Distance from Origin by () = 4.47214
    Angle by () = angle
    Abscissa = 4
    Ordinate = 2
*/