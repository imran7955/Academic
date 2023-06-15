#include <iostream>

using namespace std;

class world{
protected:
    string country_name;
    int area,savings;
public:
    world(){ country_name = "",area = 0,savings = 0;}
    world(string n,int a,int s) {country_name = n,area = a,savings = s;}
    virtual void status() = 0;
};

class big_country : world
{
public:
    big_country(string n,int a,int s) : world(n,a,s) {}
    void status()
    {
        if(area >= 200000) 
            cout << country_name << " is a Big Country" << endl;
        else 
            cout << country_name << " is Not a Big Country" << endl;
    }
};
class developed_country : world
{
public:
    developed_country(string n,int a,int s) : world(n,a,s) {}
    void status()
    {
        if(savings >= 100000) 
            cout << country_name << " is a Developed Country" << endl;
        else 
            cout << country_name << " is Not a Developed Country" << endl;
    }
    
};
int main()
{
    big_country b("Switzerland",41285,8450000);
    b.status();
    developed_country bd("Bangladesh",147570,94000);
    bd.status();
    return 0;
}
/* OUTPUT:
    Switzerland is Not a Big Country
    Bangladesh is Not a Developed Country
*/