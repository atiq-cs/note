Title: Dennis Ritchie - The C Programming Language
Lead: Small excerpt from book The C Programming Language
Published: 03/19/2021
Tags:
  - book
  - programming
---

### Format Specifiers
Right justification example,

    printf("%3d %6d\n", fahr, celsius);

### Automatic Type Casting
If an arithmetic operator has integer operands, an integer operation is performed. If an arithmetic operator has one floating-point operand and one integer operand, however, the integer will be converted to floating point before the operation is done. If we had written (fahr-32), the 32 would be automatically converted to floating point. Nevertheless, writing floating-point constants with explicit decimal points even when they have integral values emphasizes their floating-point nature for human readers.

Among others, `printf` recognizes,
- %o for octal
- %x for hexadecimal
- %c for character
- %s for character string and,
- %% for itself

### getchar and putchar
The problem is distinguishing the end of input from valid data. The solution is that getchar returns a distinctive value when there is no more input, a value that cannot be confused with any real character. This value is called EOF, for "end of file". We must declare c to be a type big enough to hold any value that getchar returns. We can't use char since c must be big enough to hold EOF in addition to any possible char. Therefore we use int.

EOF is an integer defined in `<stdio.h>`, but the specific numeric value doesn't matter as long as it is not the same as any char value. By using the symbolic constant, we are assured that nothing in the program depends on the specific numeric value.

    #include <stdio.h>  

    /* copy input to output; 2nd version */  
    main() {
    int c;

    while ((c = getchar()) != EOF)  
        putchar(c);  
    }

### IN and OUT Modifiers
We prefer the symbolic constants `IN` and `OUT` to the literal values 1 and 0 because they make the program more readable.

    #include <stdio.h>

    #define IN 1 /* inside a word */
    #define OUT 0 /* outside a word */

    /* count lines, words, and characters in input */
    main() {
        int c, nl, nw, nc, state;
        state = OUT;
        nl = nw = nc = 0;

        while ((c = getchar()) != EOF) {
            ++nc;

            if (c == '\n')
            ++nl;

            if (c == ' ' || c == '\n' || c = '\t')
            state = OUT;

            else if (state == OUT) {
            state = IN;
            ++nw;
            }
        }

        printf("%d %d %d\n", nl, nw, nc);
    }

In a program as tiny as this, it makes little difference, but in larger programs, the increase in clarity is well worth the modest extra effort to write it this way from the beginning. You'll also find that it's easier to make extensive changes in programs where magic numbers appear only as symbolic constants.

### Arguments and parameters
The function power is called twice by main, in the line

    printf("%d %d %d\n", i, power(2,i), power(-3,i));

Each call passes two arguments to power, which each time returns an integer to be formatted and printed. In an expression, power(2,i) is an integer just as 2 and i are. (Not all functions produce an integer value; we will take this up in Chapter 4.)

The first line of power itself,

    int power(int base, int n)

declares the parameter types and names, and the type of the result that the function returns. The names used by power for its parameters are local to power, and are not visible to any other function: other routines can use the same names without conflict. This is also true of the variables i and p: the i in power is unrelated to the i in main.

_We will generally use `parameter` for a variable named in the parenthesized list in a function._

### Argument Passing: Call by Value
In C, all function arguments are passed "by value". This means that the called function is given the values of its arguments in temporary variables rather than the originals. This leads to some different properties than are seen with "call by reference" languages like Fortran or with var parameters in Pascal, in which the called routine has access to the original argument, not a local copy.

### Constant Expressions
A _constant expression_ is an expression that involves only constants. Such expressions may be evaluated at during compilation rather than run-time, and accordingly may be used in any place that a constant can occur, as in

    #define MAXLINE 1000
    char line[MAXLINE+1];

Or,

    #define LEAP 1 /* in leap years */
    int days[31+28+LEAP+31+30+31+30+31+31+30+31+30+31];

### String Constants

String constants can be concatenated at compile time:

    "hello, " "world"

is equivalent to

    "hello, world"

This is useful for splitting up long strings across several source lines.

### Enumeration
An enumeration is a list of constant integer values, as in

    enum boolean { NO, YES };

The first name in an enum has value 0, the next 1, and so on, unless explicit values are specified. If not all values are specified, unspecified values continue the progression from the last specified value, as the second of these examples:

    enum escapes { BELL = '\a', BACKSPACE = '\b', TAB = '\t',
        NEWLINE = '\n', VTAB = '\v', RETURN = '\r' };

    enum months { JAN = 1, FEB, MAR, APR, MAY, JUN,
        JUL, AUG, SEP, OCT, NOV, DEC }; /* FEB = 2, MAR = 3, etc. */

Names in different enumerations must be distinct.

_Values need not be distinct in the same enumeration._

### Variable Initialization
External and static variables are initialized to zero by default. Automatic variables for which is no explicit initializer have undefined (i.e., garbage) values.

### Precedence
The precedence of `&&` is higher than that of ||, and both are lower than relational and equality operators, so expressions like

    i < lim-1 && (c=getchar()) != '\n' && c != EOF

need no extra parentheses. But since the precedence of != is higher than assignment, parentheses are needed in

    (c=getchar()) != '\n'

to achieve the desired result of assignment to c and then comparison with '\n'.

By definition, the numeric value of a relational or logical expression is 1 if the relation is true, and 0 if the relation is false.

### Type Conversions
Expressions that might lose information, like assigning a longer integer type to a shorter, or a floating-point type to an integer, may draw a warning, but they are not illegal.

### Bitwise Operators
The bitwise AND operator & is often used to mask off some set of bits, for example

    n = n & 0177;

sets to zero all but the low-order 7 bits of n.

The bitwise OR operator | is used to turn bits on:

    x = x | SET\_ON;

sets to one in x the bits that are set to one in SET\_ON.

The bitwise exclusive OR operator ^ sets a one in each bit position where its operands have different bits, and zero where they are the same.

The unary operator ~ yields the one's complement of an integer; that is, it converts each 1-bit into a 0-bit and vice versa. For example

    x = x & ~077

sets the last six bits of x to zero. Note that `x & ~077` is independent of word length, and is thus preferable to, for example, `x & 0177700`, which assumes that x is a 16-bit quantity. The portable form involves no extra cost, since `~077` is a constant expression that can be evaluated at compile time.

### Precedence of bitwise operators
Note that the precedence of the bitwise operators &, ^, and | falls below == and !=. This implies that bit-testing expressions like

    if ((x & MASK) == 0) ...

must be fully parenthesized to give proper results.

### Switch Statement
_Each case is labeled by one or more integer-valued constants or constant expressions._ If a case matches the expression value, execution starts at that case. All case expressions must be different. The case labeled default is executed if none of the other cases are satisfied. A default is optional; if it isn't there and if none of the cases match, no action at all takes place. Cases and the default clause can occur in any order.

    switch (expression) {
        case const-expr: statements
        case const-expr: statements
        default: statements
    }

The break statement causes an immediate exit from the switch. Because cases serve just as labels, after the code for one case is done, execution falls through to the next unless you take explicit action to escape. break and return are the most common ways to leave a switch.

_The commas that separate function arguments, variables in declarations, etc., are not comma operators, and do not guarantee left to right evaluation._

Because the else part of an if-else is optional, there is an ambiguity when an else if omitted from a nested if sequence. This is resolved by associating the else with the closest previous else-less if. For example, in

    if (n > 0)
        if (a > b)
            z = a;
        else
            z = b;

The else goes to the inner if, as we have shown by indentation. If that isn't what you want, braces must be used to force the proper association:

    if (n > 0) {
        if (a > b)
            z = a;
    }
    else
        z = b;


### Reverse Number Example
I tried a usual implementation of reverse number as I was learning,

    #include <stdio.h>

    main() {
        int n;

        printf("Enter number to reverse: ");
        scanf("%d", &n);

        printf("Reverse of that number is %d\n", ReverseNum(n));
    }

    int ReverseNum(int a) {
        int res = 0;
        while (a) {
            res = res * 10 + a % 10;
            a /= 10;
        }
        return res;
    }

_Recursive version_

    #include <stdio.h>

    main() {
        int n;

        printf("Enter number to reverse: ");
        scanf("%d", &n);

        printf("Reverse of that number is %d\n", ReverseNum(n, 0));
    }

    int ReverseNum(int a, int res) {
        if (a == 0)
            return res;

        res = ReverseNum(a/10, res) * 10 + a % 10;
        return res;
    }
