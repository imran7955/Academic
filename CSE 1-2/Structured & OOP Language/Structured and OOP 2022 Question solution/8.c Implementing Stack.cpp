#include <iostream>

using namespace std;
const int MAX = 100100;
template <class T>
class Stack{
    int top;
    T arr[MAX];
public:
    Stack() { top = -1;}
    void push(T v)
    {
        if(top+1 == MAX)
            cout << "Overflow" << endl;
        else
            arr[++top] = v;
    }
    void pop()
    {
        if(top < 0)
            cout << "Underflow" << endl;
        else top--;
    }
    void display()
    {
        cout << "From top to bottom : ";
        for(int i = top; i >= 0; i--)
            cout << arr[i] << " ";
        cout << endl;
    }
    
};

int main()
{
    Stack<int> stk;
    stk.push(4);
    stk.push(3);
    stk.push(2);
    stk.pop();
    stk.display();
    Stack<string> lng;
    lng.push("C++");
    lng.push("Python");
    lng.display();
    return 0;
}
/* OUTPUT:
    From top to bottom : 3 4 
    From top to bottom : Python C++
*/