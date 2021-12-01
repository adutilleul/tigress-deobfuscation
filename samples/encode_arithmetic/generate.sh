#!/bin/bash

# generate all base patterns
if [ "$#" -ne 1 ]; then
    echo "Usage: ./generate.sh [tigress_runs]"
    exit 2
fi

rm -r out
mkdir out 
mkdir out/sorted
chmod u+x out

for ((i=1; i<=$1; i++)); do 
    ./patterns.sh > /dev/null 2>&1; 
    echo "[+] Tigress output generated [n=$i]!"
done

for entry in "out"/*.mba
do
    [ -e "$entry" ] || continue
    # get all unique expressions
    sort -u $entry > "out/sorted/$(basename "$entry" .mba)"
done

tigress --Seed=0 --Environment=x86_64:Linux:Gcc:9.3 \
        --Transform=EncodeArithmetic \
        --Functions=target_0,target_1,target_2,target_3,target_4,target_5,target_6,target_7,target_8,target_9,target_10,target_11,target_12,target_13,target_14,target_15,target_16,target_17,target_18,target_19,target_20,target_21,target_22,target_23,target_24,target_25,target_26,target_27,target_28,target_29,target_30,target_31,target_32,target_33,target_34,target_35,target_36,target_37,target_38,target_39,target_40,target_41,target_42,target_43,target_44,target_45,target_46,target_47,target_48,target_49,target_50,target_51,target_52,target_53,target_54,target_55,target_56,target_57,target_58,target_59,target_60,target_61,target_62,target_63,target_64,target_65,target_66,target_67,target_68,target_69,target_70,target_71,target_72,target_73,target_74,target_75,target_76,target_77,target_78,target_79,target_80,target_81,target_82,target_83,target_84,target_85,target_86,target_87,target_88,target_89,target_90,target_91,target_92,target_93,target_94,target_95,target_96,target_97,target_98,target_99,target_100,target_101,target_102,target_103,target_104,target_105,target_106,target_107,target_108,target_109,target_110,target_111,target_112,target_113,target_114,target_115,target_116,target_117,target_118,target_119,target_120,target_121,target_122,target_123,target_124,target_125,target_126,target_127,target_128,target_129,target_130,target_131,target_132,target_133,target_134,target_135,target_136,target_137,target_138,target_139,target_140,target_141,target_142,target_143,target_144,target_145,target_146,target_147,target_148,target_149,target_150,target_151,target_152,target_153,target_154,target_155,target_156,target_157,target_158,target_159,target_160,target_161,target_162,target_163,target_164,target_165,target_166,target_167,target_168,target_169,target_170,target_171,target_172,target_173,target_174,target_175,target_176,target_177,target_178,target_179,target_180,target_181,target_182,target_183,target_184,target_185,target_186,target_187,target_188,target_189,target_190,target_191,target_192,target_193,target_194,target_195,target_196,target_197,target_198,target_199,target_200,target_201,target_202,target_203,target_204,target_205,target_206,target_207,target_208,target_209,target_210,target_211,target_212,target_213,target_214,target_215,target_216,target_217,target_218,target_219,target_220,target_221,target_222,target_223,target_224,target_225,target_226,target_227,target_228,target_229,target_230,target_231,target_232,target_233,target_234,target_235,target_236,target_237,target_238,target_239,target_240,target_241,target_242,target_243,target_244,target_245,target_246,target_247,target_248,target_249,target_250,target_251,target_252,target_253,target_254,target_255,target_256,target_257,target_258,target_259,target_260,target_261,target_262,target_263,target_264,target_265,target_266,target_267,target_268,target_269,target_270,target_271,target_272,target_273,target_274,target_275,target_276,target_277,target_278,target_279,target_280,target_281,target_282,target_283,target_284,target_285,target_286,target_287,target_288,target_289,target_290,target_291,target_292,target_293,target_294,target_295,target_296,target_297,target_298,target_299,target_300,target_301,target_302,target_303,target_304,target_305,target_306,target_307,target_308,target_309,target_310,target_311,target_312,target_313,target_314,target_315,target_316,target_317,target_318,target_319,target_320,target_321,target_322,target_323,target_324,target_325,target_326,target_327,target_328,target_329,target_330,target_331,target_332,target_333,target_334,target_335,target_336,target_337,target_338,target_339,target_340,target_341,target_342,target_343,target_344,target_345,target_346,target_347,target_348,target_349,target_350,target_351,target_352,target_353,target_354,target_355,target_356,target_357,target_358,target_359,target_360,target_361,target_362,target_363,target_364,target_365,target_366,target_367,target_368,target_369,target_370,target_371,target_372,target_373,target_374,target_375,target_376,target_377,target_378,target_379,target_380,target_381,target_382,target_383,target_384,target_385,target_386,target_387,target_388,target_389,target_390,target_391,target_392,target_393,target_394,target_395,target_396,target_397,target_398,target_399,target_400,target_401,target_402,target_403,target_404,target_405,target_406,target_407,target_408,target_409,target_410,target_411,target_412,target_413,target_414,target_415,target_416,target_417,target_418,target_419,target_420,target_421,target_422,target_423,target_424,target_425,target_426,target_427,target_428,target_429,target_430,target_431,target_432,target_433,target_434,target_435,target_436,target_437,target_438,target_439,target_440,target_441,target_442,target_443,target_444,target_445,target_446,target_447,target_448,target_449,target_450,target_451,target_452,target_453,target_454,target_455,target_456,target_457,target_458,target_459,target_460,target_461,target_462,target_463,target_464,target_465,target_466,target_467,target_468,target_469,target_470,target_471,target_472,target_473,target_474,target_475,target_476,target_477,target_478,target_479,target_480,target_481,target_482,target_483,target_484,target_485,target_486,target_487,target_488,target_489,target_490,target_491,target_492,target_493,target_494,target_495,target_496,target_497,target_498,target_499 \
        --out=out/dataset1.c dataset1.c 

for i in {0..499};
do
   function_name="int target_$i(int a , int b , int c , int d , int e )"
   (grep -A 5 "^$function_name $" out/dataset1.c | tail -1 | tr -d ";" | sed 's/return*//' | sed 's/^ *//g') >> "out/dataset1.mba"
done

rm a.out
