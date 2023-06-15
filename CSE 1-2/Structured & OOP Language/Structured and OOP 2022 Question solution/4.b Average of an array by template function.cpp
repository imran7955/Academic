#include <iostream>
using namespace std;
// The template function
template<class T>
double avg(T arr[],int n)
{
    double sum = 0;
    for(int i = 0; i < n; i++)
        sum += arr[i];
    return sum/n;
}
int main() {
    double average;
    int Int_arr[] = {2,3,1,4};
    average = avg(Int_arr,4);
    cout << average << endl;

    long int L_arr[] = {3,2,5};
    cout << avg(L_arr,3) << endl;

    double D_arr[] = {2.5,4,5.4};
    cout << avg(D_arr,3) << endl;

    char msg[] = "abcd";
    cout << avg(msg,4) << endl;
    return 0;
}

/* OUTPUT:
        2.5
        3.33333
        3.96667
        98.5
*/
