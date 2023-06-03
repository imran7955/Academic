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
    // prefix increment
    Distance operator ++()
    {
        inch++;
        if(inch == 12) inch = 0,feet++;
        return *this;
    }
    // postfix increment
    Distance operator ++(int)
    {
        Distance temp = *this;
        inch++;
        if(inch == 12) inch = 0,feet++;
        return temp;
    }
    // prefix decrement
    Distance operator --()
    {
        inch--;
        if(inch == -1) inch = 11,feet--;
        return *this;
    }
    // postfix decrement
    Distance operator --(int)
    {
        Distance temp = *this;
        inch--;
        if(inch == -1) inch = 11,feet--;
        return temp;
    }
    void show(){
        cout << feet << "'" << inch << "\"" << endl;
    }
};

int main() {
    Distance d1(3,11),d2(3,1);
    Distance d3 = ++d1;
    d3.show(); // d3 = 4'0"
    d1.show(); // d1 = 4'0"
    d3 = d1++;
    d3.show(); // d3 = 4'0"
    d1.show(); // // d1 = 4'1"
    cout << endl;
    d3 = --d2;
    d3.show(); // d3 = 3'0"
    d2.show(); // d2 = 3'0"
    d3 = d2--;
    d3.show(); // d3 = 3'0"
    d2.show(); // d2 = 2'11"
    return 0;
}
/* OUTPUT:
        4'0"
        4'0"
        4'0"
        4'1"

        3'0"
        3'0"
        3'0"
        2'11"
*/
