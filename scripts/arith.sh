tigress --Environment=x86_64:Linux:Gcc:9.3 \
        --Transform=EncodeArithmetic\
        --Functions=equal,not_equal,greater,lesser,greater_or_equal,lesser_or_equal,main,add,sub,mul,bdiv,inc,mod,assignments,xor,logic_and,logic_or,logic_neg,bit_or,bit_xor,bit_neg,bit_and,bit_lshift,bit_rshift \
        --out=./../res/generated/arith.c \
        --Seed=0 ./../src/templates/arith.c
