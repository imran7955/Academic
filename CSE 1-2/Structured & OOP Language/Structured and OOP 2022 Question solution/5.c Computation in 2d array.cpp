#include <iostream>
using namespace std;
int main() {
    int table[4][3] = {{310,275,365},{210,190,325},{405,235,240},{260,300,380}};
    // Total value of sales by each girl
    for(int i = 0; i < 4; i++)
    {
        int total = 0;
        for(int j = 0; j < 3; j++)
            total += table[i][j];
        cout << "Salesgirl#" << i+1 << " total sale = " << total << endl;
    }
    // Total value of each item sold
    for(int j = 0; j < 3; j++)
    {
        int total = 0;
        for(int i = 0; i < 4; i++)
            total += table[i][j];
        cout << "Item" << j+1 << " total sale = " << total << endl;
    }
    // Grand total of sales of all items by all girls
    int gTotal = 0;
    for(int i = 0; i < 4; i++)
        for(int j = 0; j < 3; j++)
            gTotal += table[i][j];
    cout << "Grand Total = " << gTotal << endl;
    return 0;
}
/* OUTPUT:
    Salesgirl#1 total sale = 950
    Salesgirl#2 total sale = 725
    Salesgirl#3 total sale = 880
    Salesgirl#4 total sale = 940
    Item1 total sale = 1185
    Item2 total sale = 1000
    Item3 total sale = 1310
    Grand Total = 3495
*/

