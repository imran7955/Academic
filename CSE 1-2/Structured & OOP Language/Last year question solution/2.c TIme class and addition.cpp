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
		Time addTime(Time t1,Time t2)
		{
			Time sum;
			sum.hours = t1.hours + t2.hours;
			sum.minutes = t1.minutes + t2.minutes;
			sum.seconds = t1.seconds + t2.seconds;

			if(sum.seconds >= 60) sum.minutes++,sum.seconds -= 60;
			if(sum.minutes >= 60) sum.hours++,sum.minutes -= 60;

			return sum;
		}
	};
	int main()
	{
		Time a(1,58,56),b(1,1,5),c;
		c = a.addTime(a,b);
		c.display();
		return 0;
	}