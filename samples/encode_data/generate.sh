#!/bin/bash

rm -R out 
mkdir out 
encodingtypes=(
        "poly1" 
        "xor" 
        "add" 
)

for i in "${!encodingtypes[@]}"
do
        tigress --Environment=x86_64:Linux:Gcc:9.3 \
                --Transform=EncodeData \
                --Functions=main \
                --LocalVariables='main:argc,loop,res,res2,i' \
                --GlobalVariables='global_res' \
                --EncodeDataCodecs="${encodingtypes[i]}" \
                --out=./out/"${encodingtypes[i]}".c \
                --Seed=0 ./litterals.c
done        

rm a.out