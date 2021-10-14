tigress --Environment=x86_64:Linux:Gcc:9.3 \
        --Transform=EncodeData \
        --Functions=main,tigress_obf \
        --LocalVariables='main:res,loop' \
        --GlobalVariables='global_res' \
        --EncodeDataCodecs=poly1 \
        --out=./../res/generated/litterals.c \
        --Seed=0 ./../src/templates/litterals.c