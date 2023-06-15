#include <bits/stdc++.h>
using namespace std;
int factorial(int n)
{
    if(n < 0) throw 0;
    int ans = 1;
    for(int i = 1; i <= n; i++)
        ans *= i;
    return ans;
}
int main() {
    int n;
    cout << "Enter a number: ";
    cin >> n;
    try{
        int ans = factorial(n);
        cout << "Factorial of " << n << " is = " << ans << endl;
    }
    catch(int x){
        cout << "Factorial of Negative number is undefined." << endl;
    }
    return 0;
}

