Title: Sample Coding Template for Problem Solvers in C++
Published: 02/27/2014
Tags:
  - C++
---
In this article I provide my C++ template which,

 - includes mostly used C++ header files
 - enables redirection of cin and cout to file input and file out respectively depending on programmer's choice
 - includes an OOP class as skeleton design taking an example as simple as Ecological Premium

Please have a look at c++ template below. You are encouraged to use it and to provide feedback if it helps you anyway.

Programming contestants have templates of many variations. These variations come on many categories: sometimes on use of OOP, sometimes on use of complex MACROS, sometimes on function designs. Whether a clean OOP design can help us think in an organized way, whether it can help us avoid worries of low level details and myriads of bugs related with acquiring insignificant efficiency problem solvers can tell about it.

** C++ Template can be found at [github link](https://github.com/atiq-cs/Problem-Solving/blob/master/coding-template/cpp/Template.cpp) **

All C++ Templates can be found at [github link](https://github.com/atiq-cs/Problem-Solving/tree/master/coding-template/cpp)

```cpp
    /*******************************************************
    * Problem Name: Problem Title for example "Ecological Premium"
    * Problem ID: Problem Number for example "10300"
    * Occassion: Offline solving volume 103
    * Date: February 27, 2018
    * Template revision: 002, OOP
    *
    * Algorithm: Adhoc
    * Special Case:
    * Judge Status: Accepted
    * Author: Atique
    * Notes: Primitive data structure limit sensitive
    *
    *******************************************************/
    //Uncomment this line if you use C standard IO functions i.e., printf, scanf
    #include <string>
    #include <sstream>
    #include <cmath>
    #include <cstdio>
    #include <vector>
    #include <iostream>
    #include <algorithm>
    #include <map>
    #include //for cout formatting
    #define INF 2147483648
    #define EPS 1e-8
    using namespace std;
     
    // Comment this before submission
    #define ENABLE_IO_REDIRECTION    1 // redirect cin to input
        // file for the problem and cout to output file for the problem
     
    class EcologicalPremium {
    public:
        EcologicalPremium();
        void CalculateBudget();
    };
     
    EcologicalPremium::EcologicalPremium() {
        // Initiaze used variables
    }
     
    void EcologicalPremium::CalculateBudget() {
        // Take Input
        int nTestCase = 0;
     
        // Take number of test cases
        cin>>nTestCase;
     
        while (nTestCase--) {
            // write some code, take more input using cin if necessary
            // call some member functions,
            // output the result using cout
        }
    }
     
    int main() {
        #ifdef  ENABLE_IO_REDIRECTION
            // set problem number here; it helps setting correct input
            // and output file
            string problem_id("10300");
     
            string in_file_path(problem_id); in_file_path += "_in.txt";
            string out_file_path(problem_id); out_file_path +=
                    "_out.txt";
     
            // Redirect cin to in file
            ifstream subinput(in_file_path);
            streambuf* orig_cin = NULL;
            if (subinput) {
                orig_cin = cin.rdbuf(); //save old buf
                cin.rdbuf(subinput.rdbuf()); //redirect std::cin to
                        in.txt!
            }
            else
                cout<<"Input file problem " << problem_id << " does
                    not exist."<<endl;
     
            // Redirect cout to out file
            ofstream suboutput(out_file_path);
            streambuf *orig_cout = NULL;
            if (suboutput) {
                orig_cout = cout.rdbuf(); //save old buf
                cout.rdbuf(suboutput.rdbuf()); //redirect std::cout
                // to out.txt!
            }
     
        #endif
     
        // Code here
        EcologicalPremium EPObj;
        EPObj.CalculateBudget();
     
        #ifdef  ENABLE_IO_REDIRECTION
            if (suboutput)
                cout.rdbuf(orig_cout);
            if (suboutput)
                cin.rdbuf(orig_cin);
        #endif
     
        return 0;
    }
     
    /*
     cout.setf (ios::fixed, ios::floatfield);
     cout.setf(ios::showpoint);
     cout<<setprecision(2)<<sum_c + eps<<endl;
    */
```

Before submiting the solution to an online judge you should undefine `ENABLE_IO_REDIRECTION` (comment the referred line). Depending on the problem id set by you on line number 62 the output executable's input file will be set as `number_in.txt` and output file name will be set as `number_out.txt`. For example, for problem 10300 its input file becomes `10300_in.txt` and output file becomes `10300_out.txt`