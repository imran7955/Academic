#include <iostream>
using namespace std;
int main() {
    int x = 1,y = 1, z,m;
    cin >> m;
    for(int i = 1; i <= m; i++)
    {
        if(i < 3)
        {
            cout << 1 << " ";
        }
        else
        {
            z = x+y;
            cout << z << " ";
            x = y, y = z;
        }
    }
    return 0;
}

