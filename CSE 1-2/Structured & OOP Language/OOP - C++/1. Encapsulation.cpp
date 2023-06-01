// Encapsulation is a process of combining member functions and data members in a single unit called a class.
// The purpose is to prevent access to the data directly.
#include<iostream>
using namespace std;

class student
{
private:
    float cgpa;
public:
    int roll;
    student(int r,float c) : roll(r),cgpa(c) {}
    void showCgpa()
    {
        cout << "CGPA = " << cgpa << endl;
    }
};
int main()
{
    student s1(30,3.41);
    cout << "Roll = " << s1.roll << endl; // direct access
    s1.showCgpa(); // indirect access
    return 0;
}

/* OUTPUT

    Roll = 30
    CGPA = 3.41
*/
