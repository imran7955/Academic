#include <iostream>
using namespace std;

void inc(int x)
{
    x++;
    cout << x << endl;
}
int main()
{
    int n = 6;
    inc(n);
    cout << n << endl; // no change of n
    return 0;
}