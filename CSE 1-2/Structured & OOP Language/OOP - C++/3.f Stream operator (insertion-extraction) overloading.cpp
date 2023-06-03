/*
    Return_type operator operator_name(stream object,working object)
    {
        // code
        // Return stream object
    }
    The input parameter of the stream object should be the type of reference.
    The return type of the function should be a reference to the stream object
    because it allows for chaining multiple stream operations together
        i.e:
            cin >> obj1 >> obj2 >> obj3 >> ..........
            cout << obj1 << obj2 << obj3 >> ..........
    Input parameter of the user-defined or working object must be the type of 
    reference in case of input as we want to make changes to the original object.
    In the case of output (<<) it is not a must to take it by reference.
*/
#include <iostream>
#include <cmath>
using namespace std;
class Point
{
    int abscissa,ordinate;
public:
    Point() : abscissa(0),ordinate(0) {}
    Point(int a,int b) : abscissa(a),ordinate(b) {}
    friend ostream& operator <<(ostream&,Point);
    friend istream& operator >>(istream&,Point&);
};
istream& operator >>(istream& cinObj,Point& p)
{
    cinObj >> p.abscissa >> p.ordinate;
    return cinObj;
}
ostream& operator <<(ostream& coutObj,Point p)
{
    coutObj << "Abscissa = " << p.abscissa << endl;
    coutObj << "Ordinate = " << p.ordinate << endl;
    return coutObj;
}
int main()
{
    
    Point p1,p2;
    cin >> p1 >> p2;
    cout << p1 << p2;
    return 0;
}
/*  INPUT: 4 7
           8 9
    OUTPUT: 
    Abscissa = 4
    Ordinate = 7
    Abscissa = 8
    Ordinate = 9
*/