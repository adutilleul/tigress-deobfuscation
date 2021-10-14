tigress --Environment=x86_64:Linux:Gcc:9.3 \
        --Transform=EncodeArithmetic\
        --Functions=equal,not_equal,superior,inferior,main,add,sub,mul,bdiv \
        --out=./../res/generated/arith.c \
        --Seed=0 ./../src/templates/arith.c