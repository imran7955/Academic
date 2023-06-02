/*
In practice, you should define the default and parameterized constructors
of the derived class by inheriting the same type of constructor of the base class.
*/
#include <iostream>
using namespace std;

class rect
{
protected:
    int width,length;
public:
    rect()
    {
        width = length = 0;
    }
    rect(int wd,int ln)
    {
        width = wd,length = ln;
    }
    void show()
    {
        cout << "Width = " << width << endl;
        cout << "Length = " << length << endl;
    }
};
class cube :  rect
{
    int height;
public:
    cube()  : rect()
    {
        height = 0;
    }
    cube(int w,int l,int h) : rect(w,l)
    {
        height = h;
    }
    void show()
    {
        rect::show();
        cout << "Height = " << height << endl;
    }
};
int main()
{
    cube c1;
    c1.show();
    cube c2(2,4,3);
     c2.show();
    return 0;
}
/* OUTPUT-
            Width = 0
            Length = 0
            Height = 0
            Width = 2
            Length = 4
            Height = 3
*/
