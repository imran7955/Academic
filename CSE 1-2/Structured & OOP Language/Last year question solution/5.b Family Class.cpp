#include<iostream>
using namespace std;
class Family{
private:
	int size,cnt;
	int *ages;
	string *names;
public:
	Family(int sz){
		cnt = 0;
		size = sz;
		ages = new int[size];
		names = new string[size];
	}
	void push(string n,int a)
	{
		names[cnt] = n,ages[cnt] = a;
		cnt++;
	}
	void display()
	{
		for(int i = 0; i < cnt; i++)
			cout << names[i] << " " << ages[i] << endl;
	}
	float avg()
	{
		int sum = 0;
		for(int i = 0; i < cnt; i++)
			sum += ages[i];
		return (float)(sum) / cnt;
	}
	int eldest()
	{
		int ans = ages[0];
		for(int i = 1; i < cnt; i++)
			if(ages[i] > ans)
				ans = ages[i];
		return ans;
	}
	int youngest()
	{
		int ans = ages[0];
		for(int i = 1; i < cnt; i++)
			if(ages[i] < ans) 
				ans = ages[i];
		return ans;
	}
};
int main()
{
	Family roy(5);
	roy.push("Sumit Roy",25);
	roy.push("Amit Roy",36);
	roy.push("Barun Roy",21);

	Family khan(5);
	khan.push("F.R Khan",45);
	khan.push("Aamir Khan",29);
	khan.push("Shadav Khan",23);
	
	roy.display();
	cout << endl;
	khan.display();
	cout << endl;
	
	cout << "Average of Roy Family        = " << roy.avg() << endl;
	cout << "Eldest between both family   = " << max(roy.eldest(),khan.eldest()) << endl;
	cout << "youngest between both family = " << min(roy.youngest(),khan.youngest()) << endl;
	return 0;
}