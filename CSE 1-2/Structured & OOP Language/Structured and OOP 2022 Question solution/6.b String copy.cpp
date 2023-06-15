#include <iostream>
using namespace std;

int main() {
    string a,b;
    int cnt = 0;
    cin >> a >> b;
    for(int i = 0; i < b.size(); i++)
    {
        if(i >= a.size()) break;
        a[i] = b[i];
        cnt++;
    }
    cout << a << endl << cnt << endl;
    return 0;
}
