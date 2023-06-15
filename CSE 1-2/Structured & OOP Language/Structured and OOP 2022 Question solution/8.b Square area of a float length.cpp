#include <iostream>

using namespace std;
class Distance{
    int feet; float inches;
public:
    Distance(int f,float i)
    {
        feet = f,inches = i;
    }
    void showdist()
    {
       cout << feet << "'" << inches << "\"" << endl; 
    }
    friend float square(Distance);
};

float square(Distance d)
{
    float total = d.feet + d.inches / 12;
    return total * total;
}

int main()
{
    Distance dist(3,6.0);
    float sqft;
    sqft = square(dist);
    cout << "Distance = "; dist.showdist();
    cout << "\nSquare = " << sqft << " square feet\n";
    return 0;
}
/* OUTPUT:
    Distance = 3'6"

    Square = 12.25 square feet
*/