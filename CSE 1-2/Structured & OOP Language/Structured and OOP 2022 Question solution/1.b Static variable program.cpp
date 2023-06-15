#include <iostream>
using namespace std;
void function() {
    static int cnt = 0;
    cnt++;
    cout << "I have been called " << cnt << " times." << endl;
}

int main() {
    for (int i = 0; i < 10; i++) {
        function();
    }
    return 0;
}
