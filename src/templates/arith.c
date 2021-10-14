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

//   return (((unsigned int )((b & ~ a) 
// | (~ (b ^ a) & (b - a))) >> 31U) & 1);

//   return (((unsigned int )((a - b) ^ 
// ((a ^ b) & ((a - b) ^ a))) >> 31U) & 1);

//   return (((unsigned int )((b | ~ a) &
// ((b ^ a) | ~ (a - b))) >> 31U) & 1);
TYPE superior(TYPE a, TYPE b)
{
    return a >= b;
}
TYPE inferior(TYPE a, TYPE b)
{
    return a < b;
}

TYPE add(TYPE a, TYPE b)
{
    return a + b;
}

TYPE sub(TYPE a, TYPE b)
{
    return a - b;
}

TYPE mul(TYPE a, TYPE b)
{
    return a * b;
}

TYPE bdiv(TYPE a, TYPE b)
{
    return a++;
}

int main() 
{
    return equal(1, 2) && not_equal(4, 5);
}
