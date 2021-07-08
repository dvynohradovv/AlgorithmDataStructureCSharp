#include<iostream>
using namespace std;


int Evclid(int a, int b)//Алгоритм Евклида итерационный
{
    int i;
    while (a && b)
        if (a >= b) a %= b;
        else b %= a;
    return a | b;
}

int LoopEvclid(int a, int b)//Алгоритм Евклида итерационный
{
    if (a == b) {
        return a;
    }
    if (a > b) {
        int tmp = a;
        a = b;
        b = tmp;
    }
    return LoopEvclid(a, b - a);
}

int main()
{
    int  a, b;


    cout << "1: ";
    cin >> a;
    cout << "2: ";
    cin >> b;
    cout << "NOD=" << Evclid(a, b) << endl;

    //cout << "NOD=" << LoopEvclid(a, b) << endl;


}