#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

#define TYPE int
#define LITTERAL INT_MAX - 10

TYPE global_res = LITTERAL;
TYPE global_res2 = LITTERAL;

static const TYPE foo = LITTERAL;

TYPE tigress_obf()
{
    return LITTERAL;
}

int main() 
{
    TYPE res = tigress_obf();
    TYPE res2 = res + 4LL;
    TYPE loop[666] = {LITTERAL, LITTERAL, LITTERAL, LITTERAL};
    for(size_t i = 0; i < sizeof(loop); i++)
        res2 -= loop[i];
    global_res = res;
    return res;
}
