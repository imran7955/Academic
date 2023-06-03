#include <iostream>
using namespace std;

class Distance
{
private:
    int feet,inch;
public:
    Distance() {
        feet = inch = 0;
    }
    Distance(int f,int i){
        feet = f,inch = i;
    }
    // < operator overloading
    bool operator <(Distance d)
    {
        if( (feet*12 + inch) < (d.feet*12 + d.inch))
            return true;
        else return false;
    }
    // > operator overloading
    bool operator >(Distance d)
    {
        if( (feet*12 + inch) > (d.feet*12 + d.inch))
            return true;
        else return false;
    }
    // == operator overloading
    bool operator ==(Distance d)
    {
        if( (feet*12 + inch) == (d.feet*12 + d.inch))
            return true;
        else return false;
    }
    // != operator overloading
    bool operator !=(Distance d)
    {
        if( (feet*12 + inch) != (d.feet*12 + d.inch))
            return true;
        else return false;
    }
    // <= operator overloading
    bool operator <=(Distance d)
    {
        if( (feet*12 + inch) <= (d.feet*12 + d.inch))
            return true;
        else return false;
    }
     // >= operator overloading
    bool operator >=(Distance d)
    {
        if( (feet*12 + inch) >= (d.feet*12 + d.inch))
            return true;
        else return false;
    }
    void show(){
        cout << feet << "'" << inch << "\"" << endl;
    }
};

int main() {
    Distance d1(3,11),d2(3,3),d3(3,11);
    if(d2 < d1) cout << "d2 is less than d1" << endl;
    if(d1 > d2) cout << "d1 is greater than d2" << endl;
    if(d2 <= d1) cout << "d2 is less than or equal to d1" << endl;
    if(d3 >= d1) cout << "d3 is greater than or equal to d1" << endl;
    if(d1 != d2) cout << "d1 is not equal to d2" << endl;
    if(d1 == d3) cout << "d1 is equal to d3" << endl;
    return 0;
}
/* OUTPUT:
        d2 is less than d1
        d1 is greater than d2
        d2 is less than or equal to d1
        d3 is greater than or equal to d1
        d1 is not equal to d2
        d1 is equal to d3
*/
