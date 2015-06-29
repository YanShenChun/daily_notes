// BasicCalculator.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string>
#include <stack>

using namespace std;

class Solution {
public:
    int calculate(string s) {
        // Convert the infix expression to Reverse Polish Notation
        stack<string> oper_stack;

        
        return 0;
    }

    string read_next_token(string &s, int index) {
        int start_index = index;

        while (s[index] == ' ') index++;

        string result = "";

        return result;
    }

    bool is_symbol(char ch) {
        switch (ch) {
        case '+':
        case '-':
        case '*':
        case '/':
        case '(':
        case ')':
            return true;
        default:
            return false;
        }
    }
};


int _tmain(int argc, _TCHAR* argv[])
{
    return 0;
}

