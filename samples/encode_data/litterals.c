#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

#define TYPE int
#define LITTERAL 66
#define LITTERAL2 900

TYPE global_res = LITTERAL;
TYPE global_res2 = LITTERAL;

static const TYPE foo = LITTERAL;

TYPE tigress_obf()
{
    return LITTERAL;
}

int main(TYPE argc) 
{
    TYPE res = tigress_obf();
    TYPE res2 = res + 4LL;
    TYPE loop[5] = {LITTERAL2, LITTERAL2, LITTERAL2, LITTERAL2};
    for(TYPE i = 0; i < (TYPE) sizeof(loop); i++) {
        res2 -= loop[i];
        res--;
        switch(res2++) {
            case 4:
                res2++;
                break;
            default:
                break;
        }
    }
    
    TYPE check = 5;
    while(check = (--check)) {}
    global_res = res;
    return res2 * loop[0];
}
