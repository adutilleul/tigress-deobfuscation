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
    return a/b;
}

TYPE inc(TYPE a) {
    return a++;
}

TYPE mod(TYPE a, TYPE b) {
    return a%b;
}

TYPE assignments(TYPE a, TYPE b) {
    a += b;
    a -= b;
    a *= b;
    a /= b;
    a %= b;
    a &= b;
    a |= b;
    a ^= b;
    a <<= b;
    a >>= b;
    return a;
}

TYPE xor(TYPE a, TYPE b) {
    return a^b;
}

// TYPE test(TYPE a, TYPE b) {
//
// }


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

// bit_or,bit_xor,bit_neg,bit_and,bit_lshift,bit_rshift

TYPE bit_or(TYPE a, TYPE b)
{
    return a | b;
}


TYPE bit_xor(TYPE a, TYPE b)
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
    return equal(1, 2) && not_equal(4, 5);
}
