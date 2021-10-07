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

int main() 
{
    return equal(1, 2) && not_equal(4, 5);
}
