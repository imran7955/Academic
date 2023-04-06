#include<iostream>
using namespace std;
class Time{
private:
	int hours,minutes,seconds;
public:
	Time() : hours(0),minutes(0),seconds(0){}
	Time(int h,int m,int s){
		hours = h,minutes = m,seconds = s;
	}
	void display()
	{
		cout << hours << ":" << minutes << ":" << seconds << endl;
	}
	Time operator +(Time t2)
	{
		Time sum;
		sum.hours = hours + t2.hours;
		sum.minutes = minutes + t2.minutes;
		sum.seconds = seconds + t2.seconds;

		if(sum.seconds >= 60) sum.minutes++,sum.seconds -= 60;
		if(sum.minutes >= 60) sum.hours++,sum.minutes -= 60;

		return sum;
	}
	Time operator ++(int)
	{
		Time previous = (*this);
		seconds++;
		if(seconds >= 60) minutes++,seconds -= 60;
		if(minutes >= 60) hours++,minutes -= 60;
		return previous;
	}
	Time operator ++()
	{
		seconds++;
		if(seconds >= 60) minutes++,seconds -= 60;
		if(minutes >= 60) hours++,minutes -= 60;
		return (*this);
	}
};
int main()
{
	Time a(1,58,56),b(1,1,5),c;
	c = a + b;
	c.display();
	
	Time d = c++;
	d.display();
	c.display();

	d = ++c;
	d.display();
	c.display();
	return 0;
}