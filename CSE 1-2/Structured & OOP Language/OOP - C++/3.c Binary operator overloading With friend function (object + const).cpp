/*
Case 1: object + object
Case 2: object + constant
Case 3: constant + object
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
    // + operator overloading for object + object
    Distance operator +(Distance d)
    {
        Distance ans;
        ans.feet = feet + d.feet;
        ans.inch = inch + d.inch;
        if(ans.inch >= 12) ans.inch -= 12,ans.feet++;
        return ans;
    }
    friend Distance operator + (Distance,int);  // object + const
    friend Distance operator + (int,Distance); // const + object
    void show(){
        cout << feet << "'" << inch << "\"" << endl;
    }
};
Distance operator +(Distance d,int v)
{
    /* return d + Distance(0,v);
    Only this line is enough if have the normal (+) operator overloaded. */
    Distance ans;
    ans.feet = d.feet;
    ans.inch = d.inch + v;
    if(ans.inch >= 12) ans.inch -= 12,ans.feet++;
    return ans;
}
Distance operator +(int v,Distance d)
{
    /*
    return d + Distance(0,v);
    Only this line is enough if have the normal (+) operator overloaded.
    */
    Distance ans;
    ans.feet = d.feet;
    ans.inch = d.inch + v;
    if(ans.inch >= 12) ans.inch -= 12,ans.feet++;
    return ans;
}
int main() {
    Distance d1(3,11),d2(3,3);
    Distance d3 = d1 + d2;
    d3.show(); // d3 = 7'2"
    d3 = d1 + 3;
    d3.show(); // d3 = 4'2"
    d3 = 5 + d1;
    d3.show(); // d3 = 4'4"
    return 0;
}
/* OUTPUT:
        7'2"
        4'2"
        4'4"
*/
