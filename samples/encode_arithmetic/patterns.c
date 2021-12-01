#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

#define TYPE int

TYPE equal(TYPE a, TYPE b)
{
    return a == b;
}

TYPE not_equal(TYPE a, TYPE b)
{
    return a != b;
}

TYPE greater(TYPE a, TYPE b)
{
    return a > b;
}

TYPE lesser(TYPE a, TYPE b)
{
    return a < b;
}

TYPE greater_or_equal(TYPE a, TYPE b)
{
    return a >= b;
}

TYPE lesser_or_equal(TYPE a, TYPE b)
{
    return a <= b;
}

TYPE add(TYPE a, TYPE b)
{
    return (a + b) | b;
}

TYPE add3(TYPE a, TYPE b, TYPE c)
{
    return (a + b + c + 5) * c;
}

TYPE sub(TYPE a, TYPE b)
{
    return (a - b) & a;
}

TYPE sub3(TYPE a, TYPE b, TYPE c)
{
    return (a - b - c) * (a * b);
}

TYPE mul(TYPE a, TYPE b, TYPE c)
{
    return a * b;
}

TYPE bdiv(TYPE a, TYPE b)
{
    return a/b;
}

TYPE mod(TYPE a, TYPE b) {
    return a%b;
}

TYPE logic_and(TYPE a, TYPE b)
{
    return a && b;
}

TYPE logic_or(TYPE a, TYPE b)
{
    return a || b;
}

TYPE logic_neg(TYPE a, TYPE b)
{
    return !a;
}

TYPE bit_or(TYPE a, TYPE b)
{
    return a | b;
}

TYPE bit_xor(TYPE a, TYPE b, TYPE c, TYPE d)
{
    return a ^ b;
}

TYPE bit_neg(TYPE a)
{
    return ~a;
}

TYPE bit_and(TYPE a, TYPE b)
{
    return a & b;
}


TYPE bit_lshift(TYPE a, TYPE b)
{
    return a << b;
}

TYPE bit_rshift(TYPE a, TYPE b)
{
    return a >> b;
}

int main() 
{
    return 0;
}
