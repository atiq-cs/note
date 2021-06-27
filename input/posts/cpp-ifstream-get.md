Title: C++ ifstream::get method overloads
Published: 11/27/2012
Tags:
  - C++
---
As stated on the reference, `istream::get()` has 6 overloads,

    int get();
    istream& get ( char& c );
    istream& get ( char* s, streamsize n );
    istream& get ( char* s, streamsize n, char delim );
    istream& get ( streambuf& sb);
    istream& get ( streambuf& sb, char delim );

It is also stated that `int get();` Extracts a character from the stream and returns its value (casted to an integer).

On the other hand.

    istream& get ( char& c );

extracts a character from the stream and stores it in c.

    istream& get (char* s, streamsize n );

[cplusplus.com - istream::get][1] has further details.

## Sampe Program and Output
We shall write a simple program that counts words on each line of a text file. If we cut to the chess, strategy is to take every character, if the character is a separator set a flag, depending on that when a char is found other than separator increment word count and unset the flag until next separator is found. When newline character is found print number of words found on that line. Here's the program,

```cpp
#include <ifstream>
#include <iostream>
using namespace std;
    
int main () {
        string separators(":;.!\"\' \t\n");
        ifstream ifs ( "test.txt" , ifstream::in );
        bool shouldCountNewWord = true;
        int wordCount=0;
    
        while (ifs.good()) {
            char ch = ifs.get();
            int i = 0;
            for (; i<separators.length(); i++) {
                if (ch == separators[i]) {
                    shouldCountNewWord = true;
                    break;
                }
            }
    
            if (i== (int) separators.length() && shouldCountNewWord == true) {
                wordCount++;
                shouldCountNewWord = false;
            }
    
    
            if (ch == '\n') {
                cout<<"We have encountered "<<wordCount<<" words in this line."<<endl;
                wordCount = 0;
            }
        }
        ifs.close();
        return 0;
}
```

So the input text file test.txt. Let's create it inside the same directory with some random contents,

    Mazel tov! Jewish tradition.
    He is not stingy; she is not like him.
    "Experilus!" He cried.
    I have 2 houses. How many do you have?

After compiling and executing the binary output of this program appears,

    We have encountered 4 words in this line.
    We have encountered 9 words in this line.
    We have encountered 3 words in this line.
    We have encountered 9 words in this line.

If you are using Linux stay aware that on Linux, editors like vi, gedit adds an extra newline before end of line as convention. Hence, Hence on that platform, if you add a newline yourself after last line you will see that output prints an extra line saying it has encountered 0 lines on that line. Therefore don't add any newline after last line.

### The Variation
We have tested `int istream::get()`. Let's go ahead and replace it with `istream& istream::get ( char& c )`. Let's just replace the line `char ch = ifs.get();` with lines as shown below,

    while (ifs.good()) {
         char ch;
         ifs.get(ch);
         .............. // code continues
    }

After we compile and run the program new output is,

    We have encountered 4 words in this line.
    We have encountered 9 words in this line.
    We have encountered 3 words in this line.
    We have encountered 9 words in this line.
    We have encountered 0 words in this line.

### Reason for such unexpected output
Apparently second version of the code is getting an extra newline character. But the file hasn't changed. Where did it come from? If we add an extra statement and print all characters taken as input we see that there variable ch contains newline twice! On the last one ch contains newline value which 10 though eof has already been reached.

Why this behavior? Answer is that, when function get (signature: istream& istream::get ( char& c )) encounters EOF it does not assign it to the character variable because it is not really valid character.

### First Solution
A solution can be resetting variable 'ch' after each time loop runs as presented in code below,

       if (ch == '\n') {
            cout<<"We have encountered "<<wordCount<<" words in this line."<<endl;
            wordCount = 0;
       }
       ch = '\0';
    }

Previously read character is erased each time the loop runs and ch cannot contain the same newline value anymore unless it really occurs on the input.

### Second Solution

We can check whether input stream is still good after character has been read like this,

    ifs.get(ch);
    if (!ifs.good())
       break;
       // code

### Finally
We can improve it further,

    #include <ifstream>
    #include <iostream>
    using namespace std;
      
    int main () {
        string separators(":;.!\"\' \t\n");
        ifstream ifs ( "test.txt" , ifstream::in );
        bool shouldCountNewWord = true;
        int wordCount=0;
     
        char ch;
     
        while (ifs.get(ch).good()) {
            int i = 0;
            for (; i<separators.length(); i++) {
                if (ch == separators[i]) {
                    shouldCountNewWord = true;
                    break;
                }
            }
     
            if (i== (int) separators.length() && shouldCountNewWord == true) {
                wordCount++;
                shouldCountNewWord = false;
            }
     
     
            if (ch == '\n') {
                cout<<"We have encountered "<<wordCount<<" words in this line."<<endl;
                wordCount = 0;
            }
        }
        ifs.close();
        return 0;
    }

Conclusive remark is that it is easy to make mistake without remembering how istream& istream::get ( char& c ) behaves. It is always safe to use int istream::get() because whenever it encounters EOF it returns -1.

And remember that, on Linux/Unix editors like vi, gedit adds an extra new line before END OF FILE.

Code has been tested on Microsoft Compiler, Visual Studio and on Linux Compiler [ gcc version 4.4.6 20110731 (Red Hat 4.4.6-3) (GCC) ]


  [1]: http://www.cplusplus.com/reference/istream/istream/get
