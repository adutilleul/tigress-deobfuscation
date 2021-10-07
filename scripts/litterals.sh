tigress --Environment=x86_64:Linux:Gcc:9.3 \
        --Transform=EncodeArithmetic \
        --Functions=main,tigress_obf \
        --LocalVariables='main:res' \
        --GlobalVariables='global_res' \
        --out=./../res/generated/litterals.c \
        --Seed=0 ./../src/templates/litterals.c