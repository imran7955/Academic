/*
Method 1:
Distance operator + (const Distance& obj) {
    // code
}
Method 2:
Distance operator + (Distance obj) {
    // code
}

Using & makes our code efficient by referencing the 2nd object instead of making a duplicate object inside the operator function.
Using const is considered a good practice because it prevents the operator function from modifying the 2nd object.
*/
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
    // + operator overloading
    Distance operator +(Distance d)
    {
        Distance ans;
        ans.feet = feet + d.feet;
        ans.inch = inch + d.inch;
        if(ans.inch >= 12) ans.inch -= 12,ans.feet++;
        return ans;
    }
    // - operator overloading
    Distance operator -(Distance d)
    {
        Distance ans;
        int total = (feet*12 + inch) - (d.feet*12 + d.inch);
        ans.feet = total / 12;
        ans.inch = total % 12;
        return ans;
    }
    void show(){
        cout << feet << "'" << inch << "\"" << endl;
    }
};

int main() {
    Distance d1(3,11),d2(3,3);
    Distance d3 = d1 + d2;
    d3.show(); // d3 = 7'2"

    d1 = Distance(2,3),d2 = Distance(1,6);
    d3 = d1 - d2;
    d3.show(); // d3 = 0'9"

    return 0;
}
/* OUTPUT:
        7'2"
        0'9"
*/
