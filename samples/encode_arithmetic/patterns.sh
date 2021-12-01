#!/bin/bash

# launch Tigress
tigress --Seed=0 --Environment=x86_64:Linux:Gcc:9.3 \
        --Transform=EncodeArithmetic\
        --Functions=equal,not_equal,greater,lesser,greater_or_equal,lesser_or_equal,main,add,sub,mul,bdiv,mod,logic_and,logic_or,logic_neg,bit_or,bit_xor,bit_neg,bit_and,bit_lshift,bit_rshift,add3,sub3 \
        --out=patterns_out.c \
        patterns.c
        
patterns=(
        "int bit_xor(int a , int b , int c , int d )" 
        "int bit_and(int a , int b )" 
        "int bit_or(int a , int b )" 
        "int add(int a , int b )" 
        "int sub(int a , int b )" 
        "int mul(int a , int b , int c )" 
        "int add3(int a , int b , int c )" 
        "int sub3(int a , int b , int c )"
        "int not_equal(int a , int b )"
        "int equal(int a , int b )"
        "int greater(int a , int b )"
        "int greater_or_equal(int a , int b )"
        "int lesser_or_equal(int a , int b )"
)

fnames=(
        "bit_xor" 
        "bit_and" 
        "bit_or" 
        "add" 
        "sub" 
        "mul"
        "add3" 
        "sub3"
        "not_equal"
        "equal"
        "greater"
        "greater_or_equal"
        "lesser_or_equal"
)

# get the obfuscated expression and save it to a temporary file
for i in "${!fnames[@]}"
do
   (grep -A 5 "^${patterns[i]} $" patterns_out.c | tail -1 | tr -d ";" | sed 's/return*//' | sed 's/^ *//g') >> "out/${fnames[i]}.mba"
done

rm a.out
rm patterns_out.c