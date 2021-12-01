#!/bin/bash

rm -R out 
mkdir out 

tigress --Seed=0 --Environment=x86_64:Linux:Gcc:9.3 --Inputs='+2:int:1?2147483647' --Transform=InitEntropy --Transform=InitOpaque --Functions=* --Transform=AddOpaque --Functions=* --AddOpaqueCount=10 --AddOpaqueKinds=call,bug,true,junk,question  --InitOpaqueStructs=* --out=out/opaque.c opaque.c

rm a.out