#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

#define TYPE int 
#define LITTERAL INT_MAX

TYPE global_res = LITTERAL;

TYPE tigress_obf()
{
    return LITTERAL;
}

int main() 
{
    TYPE res = tigress_obf();
    global_res = res;
    return 1;
}
