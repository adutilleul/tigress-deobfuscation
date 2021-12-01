// Taken from https://github.com/werew/qsynth-artifacts/tree/master/datasets/custom_EA

/*
 * - Grammar definition: {'bitsize': 64, 'variables_names': ['a', 'b', 'c', 'd', 'e'], 'variables_sizes': [64, 64, 64, 64, 64], 'operators_names': ['ADD_64', 'MUL_64', 'OR_64', 'AND_64', 'SUB_64', 'NEG_64', 'XOR_64', 'NOT_64']}
 * - Inputs: [{'a': 1, 'b': 2, 'c': 3, 'd': 4, 'e': 5}, {'a': 42, 'b': 42, 'c': 42, 'd': 42, 'e': 42}]
 * - Min vars: 2, Max vars: 3
 * - Min derivations: 4, Max derivations: 10
 */

#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>

int target_0(int a, int b, int c, int d, int e){
	return (((((b-(b&b))&b)*(b^e))+(((b^e)^b)*(e&e))));
}

int target_1(int a, int b, int c, int d, int e){
	return (((~((b*d)^(d*d)))-b));
}

int target_2(int a, int b, int c, int d, int e){
	return ((((e+(c|c))-(c+e))|(-(c&c))));
}

int target_3(int a, int b, int c, int d, int e){
	return ((-((~(-(e-(a&a))))&(-(e&(-c))))));
}

int target_4(int a, int b, int c, int d, int e){
	return (((d^(~(d+a)))&(((a-d)|(a+(a&d)))^(~d))));
}

int target_5(int a, int b, int c, int d, int e){
	return (((((a&e)*e)|c)|(e+a)));
}

int target_6(int a, int b, int c, int d, int e){
	return ((((b^b)|(e+b))^(-(((e|e)|(e*e))^(-e)))));
}

int target_7(int a, int b, int c, int d, int e){
	return ((~(((-c)-(a*c))&(-(a|c)))));
}

int target_8(int a, int b, int c, int d, int e){
	return (((-e)-((e-(c+c))+((e|c)^(-e)))));
}

int target_9(int a, int b, int c, int d, int e){
	return ((((a|c)|((a^c)-(-a)))^(~((b&a)^(~a)))));
}

int target_10(int a, int b, int c, int d, int e){
	return (((-(~a))*(~(e*d))));
}

int target_11(int a, int b, int c, int d, int e){
	return ((-((e-a)|(e&(e*(e-e))))));
}

int target_12(int a, int b, int c, int d, int e){
	return (((~d)|(~(-b))));
}

int target_13(int a, int b, int c, int d, int e){
	return ((~(-(((-a)+(~(~a)))^((a-a)+a)))));
}

int target_14(int a, int b, int c, int d, int e){
	return (((-(~((a-(~e))-(e^e))))-(e-(e|e))));
}

int target_15(int a, int b, int c, int d, int e){
	return ((((~d)^(-(d+d)))^(~((~d)|((d^a)|a)))));
}

int target_16(int a, int b, int c, int d, int e){
	return ((-((-((e-c)^(e*c)))-(-(-(c&(~e)))))));
}

int target_17(int a, int b, int c, int d, int e){
	return ((((d-c)-(d|c))-(c|(c|c))));
}

int target_18(int a, int b, int c, int d, int e){
	return (((d|(c|(c&d)))-((c|d)-(((d^c)|c)^d))));
}

int target_19(int a, int b, int c, int d, int e){
	return ((-((e&e)*(c-e))));
}

int target_20(int a, int b, int c, int d, int e){
	return (((a-a)|((-e)^((a|a)+e))));
}

int target_21(int a, int b, int c, int d, int e){
	return (((~(-(~(-e))))-(a^(-(e*e)))));
}

int target_22(int a, int b, int c, int d, int e){
	return ((-((-(-(a^(a^a))))^((a|a)+(b+a)))));
}

int target_23(int a, int b, int c, int d, int e){
	return (((-(d&(-(~e))))^(d|d)));
}

int target_24(int a, int b, int c, int d, int e){
	return ((~(((-(~b))+(a-a))^(e|(b^(a-b))))));
}

int target_25(int a, int b, int c, int d, int e){
	return ((~(~((~b)&d))));
}

int target_26(int a, int b, int c, int d, int e){
	return (((~(c|b))*((c|b)^(b*(b-c)))));
}

int target_27(int a, int b, int c, int d, int e){
	return ((~(((a&a)^(a-a))-(~(-(a+(b|b)))))));
}

int target_28(int a, int b, int c, int d, int e){
	return ((-(-((((~c)+d)*((d*b)&c))-(~(-d))))));
}

int target_29(int a, int b, int c, int d, int e){
	return (((-(c&(b&a)))&(a^a)));
}

int target_30(int a, int b, int c, int d, int e){
	return ((((d-c)^(d+d))&((d*(c-d))&c)));
}

int target_31(int a, int b, int c, int d, int e){
	return ((-((-(b&e))&(~(b+b)))));
}

int target_32(int a, int b, int c, int d, int e){
	return ((((d+a)-(~((a|d)^(a-d))))&(a&d)));
}

int target_33(int a, int b, int c, int d, int e){
	return (((-(-b))-((-e)&((e^b)*a))));
}

int target_34(int a, int b, int c, int d, int e){
	return (((a&(a^b))^(((a+a)*a)^(a^a))));
}

int target_35(int a, int b, int c, int d, int e){
	return ((-((((a-d)+a)-(-d))^((-a)-(~(d-d))))));
}

int target_36(int a, int b, int c, int d, int e){
	return ((((d&d)^(-d))^(b*(~b))));
}

int target_37(int a, int b, int c, int d, int e){
	return ((~((c+(~(c-c)))*((c|(e&e))|(~c)))));
}

int target_38(int a, int b, int c, int d, int e){
	return (((-(((b-b)^a)&(b+(b+(b|b)))))|((b&a)^b)));
}

int target_39(int a, int b, int c, int d, int e){
	return (((~(a|a))&((-(d|a))|b)));
}

int target_40(int a, int b, int c, int d, int e){
	return ((((b|(d*c))|((-b)+c))+(c&d)));
}

int target_41(int a, int b, int c, int d, int e){
	return ((-((-c)-(b|b))));
}

int target_42(int a, int b, int c, int d, int e){
	return ((((d|d)^((-d)+a))-(~((d+(~a))&(d^a)))));
}

int target_43(int a, int b, int c, int d, int e){
	return ((((c|(b-e))&b)^(~(~(~((c*c)-(c^c)))))));
}

int target_44(int a, int b, int c, int d, int e){
	return (((b+(b-a))&((-(a|(a&a)))&((b+b)^b))));
}

int target_45(int a, int b, int c, int d, int e){
	return (((e-(c-a))^((e|e)^(((c^a)&e)*a))));
}

int target_46(int a, int b, int c, int d, int e){
	return ((b^(-(~(a|a)))));
}

int target_47(int a, int b, int c, int d, int e){
	return ((-(((e&d)*(e^d))+(-d))));
}

int target_48(int a, int b, int c, int d, int e){
	return (((d&d)-(d-(~d))));
}

int target_49(int a, int b, int c, int d, int e){
	return ((-((((a*(-b))^a)*(-(~(b-b))))+(d+a))));
}

int target_50(int a, int b, int c, int d, int e){
	return (((e^(-c))+(e^(-e))));
}

int target_51(int a, int b, int c, int d, int e){
	return ((((e*e)-(-d))*(-((-e)+((e|d)^(d-d))))));
}

int target_52(int a, int b, int c, int d, int e){
	return (((-b)|(-(b|(-b)))));
}

int target_53(int a, int b, int c, int d, int e){
	return ((((b^b)*c)|(b+(c*c))));
}

int target_54(int a, int b, int c, int d, int e){
	return ((((b^b)-(e+b))*((e+b)-e)));
}

int target_55(int a, int b, int c, int d, int e){
	return ((~((((d&b)^(c|c))-((c^b)*d))+((d-c)^c))));
}

int target_56(int a, int b, int c, int d, int e){
	return ((((b^b)+(b+a))^(((b*(~a))^b)^(a|a))));
}

int target_57(int a, int b, int c, int d, int e){
	return (((((b+d)+b)*d)-b));
}

int target_58(int a, int b, int c, int d, int e){
	return (((a|a)^(-(e*(e-b)))));
}

int target_59(int a, int b, int c, int d, int e){
	return ((-(~((e*e)-(c+(c&e))))));
}

int target_60(int a, int b, int c, int d, int e){
	return ((-((e^a)*(d+a))));
}

int target_61(int a, int b, int c, int d, int e){
	return ((-((d+((d|d)&a))-((b^b)*b))));
}

int target_62(int a, int b, int c, int d, int e){
	return ((((~c)-(-a))&(((c-c)-c)|(~a))));
}

int target_63(int a, int b, int c, int d, int e){
	return ((~(-((~(e^(c+e)))|(~(-c))))));
}

int target_64(int a, int b, int c, int d, int e){
	return (((~(d|(-e)))&(-b)));
}

int target_65(int a, int b, int c, int d, int e){
	return (((-(-((e+a)+(a^a))))|(a^b)));
}

int target_66(int a, int b, int c, int d, int e){
	return ((-(-((((~c)|d)-(b+b))+d))));
}

int target_67(int a, int b, int c, int d, int e){
	return (((e^(a*e))+((~e)^e)));
}

int target_68(int a, int b, int c, int d, int e){
	return (((c*((c*a)^a))|(c|a)));
}

int target_69(int a, int b, int c, int d, int e){
	return (((b+(-b))-(-((a*c)*(c-c)))));
}

int target_70(int a, int b, int c, int d, int e){
	return ((((~(a+(b*b)))*(b|b))&((b*b)^(b+b))));
}

int target_71(int a, int b, int c, int d, int e){
	return ((((c+(d*d))-(-d))*(((c-b)^d)*(d^c))));
}

int target_72(int a, int b, int c, int d, int e){
	return (((-((d+c)&(c^c)))|((-c)-((c&c)+d))));
}

int target_73(int a, int b, int c, int d, int e){
	return ((((b|b)&a)|(b*a)));
}

int target_74(int a, int b, int c, int d, int e){
	return ((~(d&((a-d)*a))));
}

int target_75(int a, int b, int c, int d, int e){
	return (((~(c*d))-(((d|d)|(~d))^(c|c))));
}

int target_76(int a, int b, int c, int d, int e){
	return ((~(~(-((b-(b&b))+(a|b))))));
}

int target_77(int a, int b, int c, int d, int e){
	return ((((a+d)&a)^(a+c)));
}

int target_78(int a, int b, int c, int d, int e){
	return (((-(d+(~b)))+d));
}

int target_79(int a, int b, int c, int d, int e){
	return ((((c+c)&((c+a)|(c*c)))-((a|(~(~a)))|c)));
}

int target_80(int a, int b, int c, int d, int e){
	return (((~c)|(c*(b+c))));
}

int target_81(int a, int b, int c, int d, int e){
	return ((-((-((-a)-(a|(b-b))))&((b^b)*(~b)))));
}

int target_82(int a, int b, int c, int d, int e){
	return ((((c|(a^a))^(b*c))+(((a|b)+b)+(~(-c)))));
}

int target_83(int a, int b, int c, int d, int e){
	return (((~(b|(b*b)))^(~((b*(-e))^(e-(-b))))));
}

int target_84(int a, int b, int c, int d, int e){
	return (((-(e&e))-(~(-((e*d)|e)))));
}

int target_85(int a, int b, int c, int d, int e){
	return ((-(~((-(d&e))^((d^d)&((-d)|d))))));
}

int target_86(int a, int b, int c, int d, int e){
	return ((-((c&(b+(c*c)))&((c&d)|(b&c)))));
}

int target_87(int a, int b, int c, int d, int e){
	return (((~(e^e))+((a|c)&(c&(e^a)))));
}

int target_88(int a, int b, int c, int d, int e){
	return (((~(d-(b+(b&b))))*(-(c^(c^d)))));
}

int target_89(int a, int b, int c, int d, int e){
	return (((~c)-(e+(a^e))));
}

int target_90(int a, int b, int c, int d, int e){
	return ((((a*a)^(-(d&a)))|((d+a)^(d-d))));
}

int target_91(int a, int b, int c, int d, int e){
	return (((-e)*((((b*d)+d)^(-b))+d)));
}

int target_92(int a, int b, int c, int d, int e){
	return ((((e-e)+(e*c))*(a-a)));
}

int target_93(int a, int b, int c, int d, int e){
	return (((~(b&e))|((c^c)*c)));
}

int target_94(int a, int b, int c, int d, int e){
	return ((((c+d)&(-d))*(-((-c)+(c+c)))));
}

int target_95(int a, int b, int c, int d, int e){
	return (((d*c)*((c|c)^c)));
}

int target_96(int a, int b, int c, int d, int e){
	return ((((e&c)&((e+e)+(c*c)))-(~c)));
}

int target_97(int a, int b, int c, int d, int e){
	return (((b-((b&b)-(~(d^b))))^(~(d+d))));
}

int target_98(int a, int b, int c, int d, int e){
	return ((((~c)+c)*((-(-d))^(~(c&(c*d))))));
}

int target_99(int a, int b, int c, int d, int e){
	return (((b-c)^((c|c)^(c+b))));
}

int target_100(int a, int b, int c, int d, int e){
	return ((-(((c^(c&b))|c)^a)));
}

int target_101(int a, int b, int c, int d, int e){
	return (((-(e^e))^(((d+e)|(e+e))|d)));
}

int target_102(int a, int b, int c, int d, int e){
	return ((d+((-(e*d))-(a^a))));
}

int target_103(int a, int b, int c, int d, int e){
	return ((((d+a)-(a|(d|a)))^((d&a)&(d-d))));
}

int target_104(int a, int b, int c, int d, int e){
	return (((a-a)&((d*d)^(d*a))));
}

int target_105(int a, int b, int c, int d, int e){
	return ((-((c+c)&(c|b))));
}

int target_106(int a, int b, int c, int d, int e){
	return (((~(~(-a)))*((~((b+b)|(a*b)))+(b+a))));
}

int target_107(int a, int b, int c, int d, int e){
	return ((-(~((((-e)+(e^(b^a)))&(~b))+(a&b)))));
}

int target_108(int a, int b, int c, int d, int e){
	return (((-e)-((~e)+a)));
}

int target_109(int a, int b, int c, int d, int e){
	return ((~(~((((e|a)|a)-(d+a))^(((e^d)-a)^e)))));
}

int target_110(int a, int b, int c, int d, int e){
	return (((((a^b)-b)^a)^(-(-b))));
}

int target_111(int a, int b, int c, int d, int e){
	return ((b&((-(-(b&b)))*((-a)^(a|(b*b))))));
}

int target_112(int a, int b, int c, int d, int e){
	return ((~(d|(d|(d&b)))));
}

int target_113(int a, int b, int c, int d, int e){
	return (((e+(~e))^(((e&b)|e)-(-(-b)))));
}

int target_114(int a, int b, int c, int d, int e){
	return ((-(-(~(a+a)))));
}

int target_115(int a, int b, int c, int d, int e){
	return (((((e&a)+d)|(d^d))|((a-(d|e))-(e|a))));
}

int target_116(int a, int b, int c, int d, int e){
	return ((d+((a*a)+(a^(e|d)))));
}

int target_117(int a, int b, int c, int d, int e){
	return ((~(-((~((e+b)-((c^c)-e)))^(~c)))));
}

int target_118(int a, int b, int c, int d, int e){
	return (((b^c)^(b|(b|c))));
}

int target_119(int a, int b, int c, int d, int e){
	return (((-((d*d)^((~c)|c)))+((d^d)&(~d))));
}

int target_120(int a, int b, int c, int d, int e){
	return ((~(((b*d)^c)+((c+d)^d))));
}

int target_121(int a, int b, int c, int d, int e){
	return ((d*((b*d)-(((d^d)^b)|d))));
}

int target_122(int a, int b, int c, int d, int e){
	return (((c+(~e))+((e-(c^b))*((-b)-b))));
}

int target_123(int a, int b, int c, int d, int e){
	return ((-(b^((d*d)+d))));
}

int target_124(int a, int b, int c, int d, int e){
	return ((((d+e)|((e+e)^d))*(d-e)));
}

int target_125(int a, int b, int c, int d, int e){
	return ((-((a|(b|b))^(d&d))));
}

int target_126(int a, int b, int c, int d, int e){
	return ((((d+c)*a)+(-(~(a|a)))));
}

int target_127(int a, int b, int c, int d, int e){
	return ((((a|a)*((d+a)-(-(a-d))))^((a&a)&(d&a))));
}

int target_128(int a, int b, int c, int d, int e){
	return (((a|(d|a))*((a|a)&b)));
}

int target_129(int a, int b, int c, int d, int e){
	return ((a-(~(-(-(-a))))));
}

int target_130(int a, int b, int c, int d, int e){
	return (((a&(b^b))&(a&b)));
}

int target_131(int a, int b, int c, int d, int e){
	return (((-((c-c)|((c+c)|c)))&(-((d*d)+c))));
}

int target_132(int a, int b, int c, int d, int e){
	return ((~(-(-(c*(a*b))))));
}

int target_133(int a, int b, int c, int d, int e){
	return (((e&(d*d))|(d^e)));
}

int target_134(int a, int b, int c, int d, int e){
	return (((~(e|((e|e)-b)))*(b-(b|(-e)))));
}

int target_135(int a, int b, int c, int d, int e){
	return ((((a*e)-(e*(e+a)))-((-e)*(e-e))));
}

int target_136(int a, int b, int c, int d, int e){
	return ((((c|((e&b)|e))&((b*b)*c))^(b^(e*b))));
}

int target_137(int a, int b, int c, int d, int e){
	return ((~(-((a&a)&(a*e)))));
}

int target_138(int a, int b, int c, int d, int e){
	return ((-(~((c+c)*(c|(b-b))))));
}

int target_139(int a, int b, int c, int d, int e){
	return ((~(~(~(b*b)))));
}

int target_140(int a, int b, int c, int d, int e){
	return ((((b^b)^c)|(e|((e*c)|b))));
}

int target_141(int a, int b, int c, int d, int e){
	return (((((-a)+(a^a))|(-a))^(a*a)));
}

int target_142(int a, int b, int c, int d, int e){
	return ((((a-d)*(a^b))*((a*(a^b))^((~b)*(d-b)))));
}

int target_143(int a, int b, int c, int d, int e){
	return (((-(((a*c)|a)|(a-(-(b&a)))))^((-b)-b)));
}

int target_144(int a, int b, int c, int d, int e){
	return ((((d&c)+(-(c*a)))^(-((d-(~c))-(-c)))));
}

int target_145(int a, int b, int c, int d, int e){
	return (((((a|a)&(~(-b)))*(c|c))*(-(~c))));
}

int target_146(int a, int b, int c, int d, int e){
	return ((-(b|(b&(-e)))));
}

int target_147(int a, int b, int c, int d, int e){
	return ((((e^c)&e)&(((c-e)*(a+e))&((a-c)^e))));
}

int target_148(int a, int b, int c, int d, int e){
	return (((~(-c))^((c^a)-a)));
}

int target_149(int a, int b, int c, int d, int e){
	return ((((~a)-(a+(a*d)))+((b^a)+(a&((b-a)^a)))));
}

int target_150(int a, int b, int c, int d, int e){
	return ((((c&(~c))-d)+(((d+a)|(-a))*a)));
}

int target_151(int a, int b, int c, int d, int e){
	return ((~(-(-(((a+e)*(b^a))*(b+a))))));
}

int target_152(int a, int b, int c, int d, int e){
	return (((b^e)+(b+(e+e))));
}

int target_153(int a, int b, int c, int d, int e){
	return (((-((c*d)&(c+c)))^(~(~(-(d|d))))));
}

int target_154(int a, int b, int c, int d, int e){
	return ((d*(-((c&d)|c))));
}

int target_155(int a, int b, int c, int d, int e){
	return (((a+(a|a))+(a-b)));
}

int target_156(int a, int b, int c, int d, int e){
	return ((((d+(-(~(-d))))-(b^d))-((b-d)*b)));
}

int target_157(int a, int b, int c, int d, int e){
	return ((((d-(b^b))*(b-b))*(((d*d)|d)+d)));
}

int target_158(int a, int b, int c, int d, int e){
	return (((((e*e)*(a|e))|a)^(~((e^a)*(a-e)))));
}

int target_159(int a, int b, int c, int d, int e){
	return ((((-c)*(d-d))|(d|(d|c))));
}

int target_160(int a, int b, int c, int d, int e){
	return ((((d-a)+(d^d))^(a^d)));
}

int target_161(int a, int b, int c, int d, int e){
	return (((d^d)|(-(d^(a^a)))));
}

int target_162(int a, int b, int c, int d, int e){
	return ((((~e)^d)&(d&(~(c-(d|e))))));
}

int target_163(int a, int b, int c, int d, int e){
	return ((((b^(-b))-b)+(((-d)+d)&(d|(b*b)))));
}

int target_164(int a, int b, int c, int d, int e){
	return ((-((-(-(d|e)))|((~(e-d))+(d-e)))));
}

int target_165(int a, int b, int c, int d, int e){
	return ((((~(d*e))*(d^(d^(e&e))))*(d^(d&d))));
}

int target_166(int a, int b, int c, int d, int e){
	return (((c|(((-b)+c)|(~b)))+(-b)));
}

int target_167(int a, int b, int c, int d, int e){
	return ((~((b|b)*(~(e*(e-(b-b)))))));
}

int target_168(int a, int b, int c, int d, int e){
	return (((-(-(b*(-a))))|((b+a)^a)));
}

int target_169(int a, int b, int c, int d, int e){
	return ((~(((c*a)*(c-(e^a)))|(~((a*e)-(e&c))))));
}

int target_170(int a, int b, int c, int d, int e){
	return ((((-((d|a)-(a&e)))-(d*a))&((e^(a+a))|d)));
}

int target_171(int a, int b, int c, int d, int e){
	return ((((~((e*d)^e))+d)+(((-e)^d)+(d&(d^d)))));
}

int target_172(int a, int b, int c, int d, int e){
	return (((-((-(a+b))-(c&a)))*((a+(a|c))&(c+b))));
}

int target_173(int a, int b, int c, int d, int e){
	return ((((e+b)|(b|e))*((-b)^e)));
}

int target_174(int a, int b, int c, int d, int e){
	return ((((~b)^e)+(((b+b)^b)+((-e)^e))));
}

int target_175(int a, int b, int c, int d, int e){
	return ((~(~(((e+b)+b)|(-a)))));
}

int target_176(int a, int b, int c, int d, int e){
	return ((-(((d&(c^c))^((c-d)+(d|c)))|(~(c^c)))));
}

int target_177(int a, int b, int c, int d, int e){
	return (((-((d-c)+(~d)))|((-e)|(-(e-d)))));
}

int target_178(int a, int b, int c, int d, int e){
	return (((-(~((e|d)&e)))-(-((d|a)&(~a)))));
}

int target_179(int a, int b, int c, int d, int e){
	return ((((-d)-(e^e))+((e*e)&(-(-e)))));
}

int target_180(int a, int b, int c, int d, int e){
	return (((a&b)-(~(e^e))));
}

int target_181(int a, int b, int c, int d, int e){
	return (((((a*e)&e)*b)-((~b)^(a^(-(-e))))));
}

int target_182(int a, int b, int c, int d, int e){
	return (((c-(a&c))-((c&(a+a))*c)));
}

int target_183(int a, int b, int c, int d, int e){
	return ((((d|d)^e)^((~(-((c|d)-c)))*(e-e))));
}

int target_184(int a, int b, int c, int d, int e){
	return ((~((~(d-d))&((a&(d^(d-a)))&d))));
}

int target_185(int a, int b, int c, int d, int e){
	return (((((-d)*(~c))^d)+d));
}

int target_186(int a, int b, int c, int d, int e){
	return (((~(b^b))|((-e)-(b-b))));
}

int target_187(int a, int b, int c, int d, int e){
	return ((((c^b)+a)^((c|c)-(b+c))));
}

int target_188(int a, int b, int c, int d, int e){
	return (((a+(-(b-(-a))))|((e&a)&(~b))));
}

int target_189(int a, int b, int c, int d, int e){
	return ((((c^c)&((c*b)&(b-b)))+((b^(b*b))&(~c))));
}

int target_190(int a, int b, int c, int d, int e){
	return (((a^(a*a))^(~((d^a)|a))));
}

int target_191(int a, int b, int c, int d, int e){
	return (((d+(c&c))+((~((c|(c+d))*d))|(~(d&d)))));
}

int target_192(int a, int b, int c, int d, int e){
	return (((d+(d*(d+a)))+(d&a)));
}

int target_193(int a, int b, int c, int d, int e){
	return (((e-(a+a))+(~(c&(c^c)))));
}

int target_194(int a, int b, int c, int d, int e){
	return (((b|(e|b))+(b|b)));
}

int target_195(int a, int b, int c, int d, int e){
	return (((a*e)^(d-(e&e))));
}

int target_196(int a, int b, int c, int d, int e){
	return (((((d&e)&b)-(((b&d)-e)+e))+(b^(-(e-d)))));
}

int target_197(int a, int b, int c, int d, int e){
	return ((((a|a)|c)-(~(-(~(a&((c-a)+c)))))));
}

int target_198(int a, int b, int c, int d, int e){
	return (((e*c)|(c|(e|b))));
}

int target_199(int a, int b, int c, int d, int e){
	return ((-((e*((-e)&e))|((d-e)-(d&d)))));
}

int target_200(int a, int b, int c, int d, int e){
	return ((((~c)*e)-((c-c)&(c+(e^e)))));
}

int target_201(int a, int b, int c, int d, int e){
	return (((c*(((a*c)^c)|a))+(a-a)));
}

int target_202(int a, int b, int c, int d, int e){
	return (((e-e)+((a&e)*a)));
}

int target_203(int a, int b, int c, int d, int e){
	return ((((c^e)+(-(a-c)))*(((c&e)+c)&a)));
}

int target_204(int a, int b, int c, int d, int e){
	return ((-(((c-(-c))|(~(c-d)))*(a*(c|a)))));
}

int target_205(int a, int b, int c, int d, int e){
	return (((-((e+e)-((e|a)*e)))*(~(a&e))));
}

int target_206(int a, int b, int c, int d, int e){
	return ((((c-(-a))|(e*c))+(a^(e*e))));
}

int target_207(int a, int b, int c, int d, int e){
	return ((~(-(~((b+b)+((-a)*e))))));
}

int target_208(int a, int b, int c, int d, int e){
	return (((c+(-d))-((c^e)-c)));
}

int target_209(int a, int b, int c, int d, int e){
	return ((((a^(d|a))*a)+(a|(-(a&(a^d))))));
}

int target_210(int a, int b, int c, int d, int e){
	return ((~((e^((e-a)-e))&a)));
}

int target_211(int a, int b, int c, int d, int e){
	return ((((e-(e|(~e)))-d)*(((b-b)|b)-(b+(-d)))));
}

int target_212(int a, int b, int c, int d, int e){
	return ((a&(~((~c)|(~(e+(~(e&e))))))));
}

int target_213(int a, int b, int c, int d, int e){
	return ((-((((~a)+d)&(-b))^(((b-d)^b)*(~b)))));
}

int target_214(int a, int b, int c, int d, int e){
	return (((~((e&e)+(b-b)))^((b^b)+b)));
}

int target_215(int a, int b, int c, int d, int e){
	return (((b-(-a))&(a|(b&a))));
}

int target_216(int a, int b, int c, int d, int e){
	return ((((c&a)-a)*(-(a|(a^(a+c))))));
}

int target_217(int a, int b, int c, int d, int e){
	return (((b*((b-(a^d))-((~b)|(a+d))))&((b*a)-a)));
}

int target_218(int a, int b, int c, int d, int e){
	return ((((-a)|(~e))&(-a)));
}

int target_219(int a, int b, int c, int d, int e){
	return (((-(b&c))|(e|c)));
}

int target_220(int a, int b, int c, int d, int e){
	return ((d+((e|d)^((d^d)^e))));
}

int target_221(int a, int b, int c, int d, int e){
	return (((((~c)-c)|e)+(a+a)));
}

int target_222(int a, int b, int c, int d, int e){
	return (((~(c*c))|((c|(b-c))^(c^c))));
}

int target_223(int a, int b, int c, int d, int e){
	return (((((d&a)-d)|(e+d))|(e*d)));
}

int target_224(int a, int b, int c, int d, int e){
	return ((((b^(a-(-b)))*a)*((-(-a))-(a-a))));
}

int target_225(int a, int b, int c, int d, int e){
	return ((((~(e*c))+(c&(b&e)))&((b^e)*(e-b))));
}

int target_226(int a, int b, int c, int d, int e){
	return ((~((((e|e)&(e&e))^(~a))+(a|(-a)))));
}

int target_227(int a, int b, int c, int d, int e){
	return ((((b&c)-(c*b))|(((~b)^c)^(-(-c)))));
}

int target_228(int a, int b, int c, int d, int e){
	return ((((c&b)-e)^((b&c)*(-b))));
}

int target_229(int a, int b, int c, int d, int e){
	return ((((d|(d+b))+d)-((~(-(b*a)))^(b+b))));
}

int target_230(int a, int b, int c, int d, int e){
	return ((((b|d)&(d*(a*b)))^(((d^b)+b)*(d^(b^a)))));
}

int target_231(int a, int b, int c, int d, int e){
	return (((((b^b)-c)-e)+((c*e)|(b-b))));
}

int target_232(int a, int b, int c, int d, int e){
	return ((~((d-c)|(~(d&d)))));
}

int target_233(int a, int b, int c, int d, int e){
	return (((~(~((c+d)-(-c))))-(c|b)));
}

int target_234(int a, int b, int c, int d, int e){
	return (((((b+(-b))|b)|(b*b))|((d^b)-b)));
}

int target_235(int a, int b, int c, int d, int e){
	return (((a&(-(c+(-c))))|((-(d&a))&(~(d|d)))));
}

int target_236(int a, int b, int c, int d, int e){
	return (((e+((e*c)+e))-((~a)+(a&e))));
}

int target_237(int a, int b, int c, int d, int e){
	return ((-(((c+(e+e))|(c^c))+((-a)-(e^(~a))))));
}

int target_238(int a, int b, int c, int d, int e){
	return (((((-c)^c)*b)&(~(b+(c*c)))));
}

int target_239(int a, int b, int c, int d, int e){
	return (((((e|e)+a)*(~(a&c)))*((a^a)&e)));
}

int target_240(int a, int b, int c, int d, int e){
	return ((a&((-(a|c))-(~a))));
}

int target_241(int a, int b, int c, int d, int e){
	return (((e+e)&(e|(e|b))));
}

int target_242(int a, int b, int c, int d, int e){
	return (((d+d)&((~e)^((d*b)|(e*d)))));
}

int target_243(int a, int b, int c, int d, int e){
	return ((-(-((e^e)-(-c)))));
}

int target_244(int a, int b, int c, int d, int e){
	return (((((c&e)^d)-e)*(((c+c)|(d+d))-((c+e)|d))));
}

int target_245(int a, int b, int c, int d, int e){
	return (((d*b)^(~(~((d*d)+b)))));
}

int target_246(int a, int b, int c, int d, int e){
	return ((~(((a|a)-c)*(a+c))));
}

int target_247(int a, int b, int c, int d, int e){
	return ((((~(-b))^(~(b^e)))*((-b)^(-e))));
}

int target_248(int a, int b, int c, int d, int e){
	return (((-((c+(a*c))-c))&(a^a)));
}

int target_249(int a, int b, int c, int d, int e){
	return (((b|b)&((~(((b|b)&d)-b))|(b&(b-b)))));
}

int target_250(int a, int b, int c, int d, int e){
	return ((((~e)+(-(a|a)))+(-(((d^d)-e)*(e*d)))));
}

int target_251(int a, int b, int c, int d, int e){
	return ((((-c)*(-(d+a)))|(-((d&c)&(a^c)))));
}

int target_252(int a, int b, int c, int d, int e){
	return (((e+(d*a))|((d^a)&(a-e))));
}

int target_253(int a, int b, int c, int d, int e){
	return (((((d|d)*(-(d^b)))+(b+b))|((b+(b-b))^b)));
}

int target_254(int a, int b, int c, int d, int e){
	return ((((-(~c))|d)^(((-d)+d)|((c+c)-(c|c)))));
}

int target_255(int a, int b, int c, int d, int e){
	return ((((b+(~(-b)))^((~e)^b))*b));
}

int target_256(int a, int b, int c, int d, int e){
	return (((((c+c)|d)+(d|d))|(~(c|(c|d)))));
}

int target_257(int a, int b, int c, int d, int e){
	return (((-((-d)*e))|(d^((d+d)+b))));
}

int target_258(int a, int b, int c, int d, int e){
	return ((((b-(c*b))&(-b))|(-(d-c))));
}

int target_259(int a, int b, int c, int d, int e){
	return ((((-(d-(b*(b&d))))-b)-(~(-(b^d)))));
}

int target_260(int a, int b, int c, int d, int e){
	return (((~(-(~(e-b))))*(e&e)));
}

int target_261(int a, int b, int c, int d, int e){
	return (((~b)|(e|(c-b))));
}

int target_262(int a, int b, int c, int d, int e){
	return (((((b-e)|b)|(b&e))|(b^b)));
}

int target_263(int a, int b, int c, int d, int e){
	return ((-((-(c&(a&e)))*(((c&a)+e)*(c-(-a))))));
}

int target_264(int a, int b, int c, int d, int e){
	return ((((d&(-e))|(d*e))&(e^(e|d))));
}

int target_265(int a, int b, int c, int d, int e){
	return (((c|a)^(((-c)*a)+(c*d))));
}

int target_266(int a, int b, int c, int d, int e){
	return (((-(a^(~(b+c))))^((a^b)&c)));
}

int target_267(int a, int b, int c, int d, int e){
	return (((~(~c))-(~c)));
}

int target_268(int a, int b, int c, int d, int e){
	return ((((c&e)^((c+e)&(c|c)))|((c-e)|(e-c))));
}

int target_269(int a, int b, int c, int d, int e){
	return ((-(~(~((-(~(d&d)))+e)))));
}

int target_270(int a, int b, int c, int d, int e){
	return ((~(~(((a+c)+(c-a))&(-((a+c)+(c|c)))))));
}

int target_271(int a, int b, int c, int d, int e){
	return (((-((b&a)|b))|((b-a)*(-(a+a)))));
}

int target_272(int a, int b, int c, int d, int e){
	return ((~((~(~(~d)))^((b&e)|b))));
}

int target_273(int a, int b, int c, int d, int e){
	return ((~((e+b)|(~(e-(~b))))));
}

int target_274(int a, int b, int c, int d, int e){
	return ((((b-e)|(a^e))&(((e*a)&b)*e)));
}

int target_275(int a, int b, int c, int d, int e){
	return ((~((d&a)^(a^c))));
}

int target_276(int a, int b, int c, int d, int e){
	return ((((-a)&(~a))-((e*((a-a)^a))-(a-a))));
}

int target_277(int a, int b, int c, int d, int e){
	return ((~((d&(a^a))|((a-(a*a))+(a*((-a)*a))))));
}

int target_278(int a, int b, int c, int d, int e){
	return (((~b)&(~((b|c)*c))));
}

int target_279(int a, int b, int c, int d, int e){
	return ((-((d&(c*d))*d)));
}

int target_280(int a, int b, int c, int d, int e){
	return ((((a+(d&b))^(-(d+d)))-((d^(-b))-(d&d))));
}

int target_281(int a, int b, int c, int d, int e){
	return ((((~d)|((~c)+d))*((a-(a-d))|a)));
}

int target_282(int a, int b, int c, int d, int e){
	return ((e*((b+b)&(b+b))));
}

int target_283(int a, int b, int c, int d, int e){
	return (((a*(b&a))|(-(b+a))));
}

int target_284(int a, int b, int c, int d, int e){
	return (((c|((b+(c|b))*b))|((~a)&(~(b&b)))));
}

int target_285(int a, int b, int c, int d, int e){
	return (((d*e)|((e-d)+d)));
}

int target_286(int a, int b, int c, int d, int e){
	return (((-(-(~b)))&(-(b-d))));
}

int target_287(int a, int b, int c, int d, int e){
	return (((b|(b-(b&a)))^((b*a)+(b*b))));
}

int target_288(int a, int b, int c, int d, int e){
	return ((((d-d)-(d+b))+(~(d|((d^b)&b)))));
}

int target_289(int a, int b, int c, int d, int e){
	return ((((-(~e))-e)-((b&(b-b))+((b+b)^b))));
}

int target_290(int a, int b, int c, int d, int e){
	return ((((b|(-b))|d)&(-(((d&a)|b)&(d-b)))));
}

int target_291(int a, int b, int c, int d, int e){
	return ((((e-e)&((e*e)^c))|(c|e)));
}

int target_292(int a, int b, int c, int d, int e){
	return (((-(-c))^(~e)));
}

int target_293(int a, int b, int c, int d, int e){
	return ((((e&a)^a)-((e*c)*(c|(-e)))));
}

int target_294(int a, int b, int c, int d, int e){
	return (((((b^a)*a)+(~b))^(b&b)));
}

int target_295(int a, int b, int c, int d, int e){
	return (((d&((-a)+d))|(-(e*a))));
}

int target_296(int a, int b, int c, int d, int e){
	return ((~((-a)&(a+(a&a)))));
}

int target_297(int a, int b, int c, int d, int e){
	return ((~((-a)*((a^a)*(b-(b&(b-b)))))));
}

int target_298(int a, int b, int c, int d, int e){
	return ((-(a^((~b)&a))));
}

int target_299(int a, int b, int c, int d, int e){
	return ((((-(e+(e+a)))|(-b))*((a&a)*a)));
}

int target_300(int a, int b, int c, int d, int e){
	return ((((c^d)&((c-c)+d))^(~(-(a+(~(d*d)))))));
}

int target_301(int a, int b, int c, int d, int e){
	return (((~c)-(~(a|a))));
}

int target_302(int a, int b, int c, int d, int e){
	return ((-((-(b*b))*b)));
}

int target_303(int a, int b, int c, int d, int e){
	return ((((a*d)^(a-d))*((d*(d^d))^((d+(a*d))*a))));
}

int target_304(int a, int b, int c, int d, int e){
	return (((b|a)-((-b)^d)));
}

int target_305(int a, int b, int c, int d, int e){
	return ((-(a^(a|(-a)))));
}

int target_306(int a, int b, int c, int d, int e){
	return ((-(-((a^a)+(c*(a+a))))));
}

int target_307(int a, int b, int c, int d, int e){
	return ((~(((b^d)&(c-(~d)))^(c&(b+(-(d&b)))))));
}

int target_308(int a, int b, int c, int d, int e){
	return (((~((b^d)&b))-(~(~((d^d)-e)))));
}

int target_309(int a, int b, int c, int d, int e){
	return ((((d+d)^a)+(((d&d)-a)+(a|a))));
}

int target_310(int a, int b, int c, int d, int e){
	return (((((d-c)|c)+(c^(c*d)))^(((d-d)*d)&(c*c))));
}

int target_311(int a, int b, int c, int d, int e){
	return (((e|e)|(~(~d))));
}

int target_312(int a, int b, int c, int d, int e){
	return (((-(~(b&c)))&((-((~c)+b))*(c*(c*b)))));
}

int target_313(int a, int b, int c, int d, int e){
	return (((-(d-a))&((d-d)-a)));
}

int target_314(int a, int b, int c, int d, int e){
	return ((((e&a)-e)-((e*a)+((-e)-e))));
}

int target_315(int a, int b, int c, int d, int e){
	return ((((~d)|(-(~(~d))))^(b&b)));
}

int target_316(int a, int b, int c, int d, int e){
	return ((~((~(((a*d)+(d-d))*d))*(a^(d+d)))));
}

int target_317(int a, int b, int c, int d, int e){
	return (((a^b)^((~a)-(-b))));
}

int target_318(int a, int b, int c, int d, int e){
	return ((-(((-c)-a)+(-c))));
}

int target_319(int a, int b, int c, int d, int e){
	return (((~b)&(a&(c&b))));
}

int target_320(int a, int b, int c, int d, int e){
	return (((((-a)&a)+(a-a))|(~((-b)^(a^b)))));
}

int target_321(int a, int b, int c, int d, int e){
	return (((e^a)+((~a)+e)));
}

int target_322(int a, int b, int c, int d, int e){
	return (((~b)*((-(b&b))|(-(b+((c&b)-b))))));
}

int target_323(int a, int b, int c, int d, int e){
	return ((((e*a)-((-e)*a))|(a|((e|(-a))-(d&d)))));
}

int target_324(int a, int b, int c, int d, int e){
	return ((~((e^d)|((e-d)^e))));
}

int target_325(int a, int b, int c, int d, int e){
	return ((-((b|(d&b))-(d*b))));
}

int target_326(int a, int b, int c, int d, int e){
	return ((((c&c)|((c+c)&(c^a)))+(-(c*a))));
}

int target_327(int a, int b, int c, int d, int e){
	return (((b&(((~b)*e)|a))|(-b)));
}

int target_328(int a, int b, int c, int d, int e){
	return ((((d*d)*d)+(e^e)));
}

int target_329(int a, int b, int c, int d, int e){
	return ((~((a|e)^(b-(a+b)))));
}

int target_330(int a, int b, int c, int d, int e){
	return ((((d|d)^c)-(-(c|(-(d+(a-d)))))));
}

int target_331(int a, int b, int c, int d, int e){
	return (((d&d)|((d*a)|a)));
}

int target_332(int a, int b, int c, int d, int e){
	return (((e*e)|((a|(a+e))+(~e))));
}

int target_333(int a, int b, int c, int d, int e){
	return (((((~(c*d))&(-c))^d)-((e-d)|c)));
}

int target_334(int a, int b, int c, int d, int e){
	return (((~(a^e))&((-a)^e)));
}

int target_335(int a, int b, int c, int d, int e){
	return ((-((-((e*e)&c))^(e&c))));
}

int target_336(int a, int b, int c, int d, int e){
	return ((e+(c+(-(c-c)))));
}

int target_337(int a, int b, int c, int d, int e){
	return ((b&(~(~((d+d)-(d|(b|b)))))));
}

int target_338(int a, int b, int c, int d, int e){
	return (((~(e+c))+(((~e)|c)|((e&c)^(c|c)))));
}

int target_339(int a, int b, int c, int d, int e){
	return ((-((b|((c+d)*c))*((d&d)-b))));
}

int target_340(int a, int b, int c, int d, int e){
	return (((e+(e*b))*((e-e)+b)));
}

int target_341(int a, int b, int c, int d, int e){
	return (((-d)+(((b|b)&d)|(b*b))));
}

int target_342(int a, int b, int c, int d, int e){
	return ((((~(a&b))^((~a)*b))+a));
}

int target_343(int a, int b, int c, int d, int e){
	return ((((c+a)+a)*((-(a-e))&e)));
}

int target_344(int a, int b, int c, int d, int e){
	return ((((d-c)^d)^(c|d)));
}

int target_345(int a, int b, int c, int d, int e){
	return ((~(-(c+(b*b)))));
}

int target_346(int a, int b, int c, int d, int e){
	return (((d^(e+d))&(~(-(~(d+d))))));
}

int target_347(int a, int b, int c, int d, int e){
	return ((((d+b)*d)&(-(((b&b)|c)+(d|b)))));
}

int target_348(int a, int b, int c, int d, int e){
	return (((-((c+b)^(-(b*c))))&c));
}

int target_349(int a, int b, int c, int d, int e){
	return ((((e+(-(d*c)))-((e&(c+d))-e))^(e+d)));
}

int target_350(int a, int b, int c, int d, int e){
	return ((e-(~((c*e)*c))));
}

int target_351(int a, int b, int c, int d, int e){
	return (((((-a)+((a-a)+a))|(~(a^a)))&(d+(d|e))));
}

int target_352(int a, int b, int c, int d, int e){
	return ((((-c)*(a^c))-((a*a)+(~a))));
}

int target_353(int a, int b, int c, int d, int e){
	return ((-(~(-(~((e^d)*e))))));
}

int target_354(int a, int b, int c, int d, int e){
	return (((d-(d+d))&((a+d)+(~((a|d)|a)))));
}

int target_355(int a, int b, int c, int d, int e){
	return ((-(((a*d)*d)*a)));
}

int target_356(int a, int b, int c, int d, int e){
	return (((d|(((d|e)-a)*e))+((~a)&(e|e))));
}

int target_357(int a, int b, int c, int d, int e){
	return ((((((e&e)^c)-e)|(a|(a&e)))^(a+((e|c)|e))));
}

int target_358(int a, int b, int c, int d, int e){
	return (((-a)&(((e&e)-e)+(a-e))));
}

int target_359(int a, int b, int c, int d, int e){
	return (((((e-e)|(-b))+b)^((b^e)-((b+e)&b))));
}

int target_360(int a, int b, int c, int d, int e){
	return (((-(a&(-c)))^((-(c&(a|b)))*(b+c))));
}

int target_361(int a, int b, int c, int d, int e){
	return (((b*b)&((~(~(b^c)))|(c|c))));
}

int target_362(int a, int b, int c, int d, int e){
	return (((-(b^c))|((c|c)*b)));
}

int target_363(int a, int b, int c, int d, int e){
	return (((-((c|(c|b))*(c+c)))&(b-(b&b))));
}

int target_364(int a, int b, int c, int d, int e){
	return (((-((c&e)|(c+e)))|((e*e)-((c+e)^e))));
}

int target_365(int a, int b, int c, int d, int e){
	return ((((b&a)-a)-((b|(b|e))|(e&(e*e)))));
}

int target_366(int a, int b, int c, int d, int e){
	return (((a^(-(a&c)))|(-(a^a))));
}

int target_367(int a, int b, int c, int d, int e){
	return ((((e&e)|e)-(-((e+e)^a))));
}

int target_368(int a, int b, int c, int d, int e){
	return (((~((((c|b)+c)*(-(-b)))^c))&(b|c)));
}

int target_369(int a, int b, int c, int d, int e){
	return (((c^e)^(~(~c))));
}

int target_370(int a, int b, int c, int d, int e){
	return (((b-b)-(-(((-(~b))&(b*c))^(b|b)))));
}

int target_371(int a, int b, int c, int d, int e){
	return (((~(c*c))|(d^(c-d))));
}

int target_372(int a, int b, int c, int d, int e){
	return ((-((((c^c)|(~b))*(b|c))+(c*e))));
}

int target_373(int a, int b, int c, int d, int e){
	return ((((-((b-c)&e))|((c*b)+c))*(~(-(c&b)))));
}

int target_374(int a, int b, int c, int d, int e){
	return (((-((b^(b+(e-e)))&(b&b)))-(~b)));
}

int target_375(int a, int b, int c, int d, int e){
	return ((~((e+e)+(e|d))));
}

int target_376(int a, int b, int c, int d, int e){
	return (((c+(a^a))^((~((c*a)-e))+(-(c+a)))));
}

int target_377(int a, int b, int c, int d, int e){
	return ((-((b|((a|b)|b))^((b*(-b))|(-a)))));
}

int target_378(int a, int b, int c, int d, int e){
	return (((~((c|e)+c))-(-e)));
}

int target_379(int a, int b, int c, int d, int e){
	return (((e^(b^(e-(e+e))))|(e-b)));
}

int target_380(int a, int b, int c, int d, int e){
	return (((b*(-(~d)))*(a-d)));
}

int target_381(int a, int b, int c, int d, int e){
	return ((((d-d)-(-d))*(~d)));
}

int target_382(int a, int b, int c, int d, int e){
	return ((-(-(((b-b)^b)-((a|(a+a))-a)))));
}

int target_383(int a, int b, int c, int d, int e){
	return (((-(d&d))*(-d)));
}

int target_384(int a, int b, int c, int d, int e){
	return ((-(((a-c)*c)|(~((~a)*a)))));
}

int target_385(int a, int b, int c, int d, int e){
	return (((((b&c)*a)*b)+a));
}

int target_386(int a, int b, int c, int d, int e){
	return ((((e&(a+(a*a)))&(c|(a*c)))|((c*c)*e)));
}

int target_387(int a, int b, int c, int d, int e){
	return ((-((~((e^a)-(e+a)))*((-(a^e))+(a|e)))));
}

int target_388(int a, int b, int c, int d, int e){
	return ((-((b^a)&((e^b)|(b&b)))));
}

int target_389(int a, int b, int c, int d, int e){
	return ((((d-b)&d)+((a^b)&d)));
}

int target_390(int a, int b, int c, int d, int e){
	return ((((b|b)+b)|(-b)));
}

int target_391(int a, int b, int c, int d, int e){
	return ((((e+e)&e)|(c-e)));
}

int target_392(int a, int b, int c, int d, int e){
	return ((((~(d|c))-(d^c))*(((c|c)&((d|c)|c))^d)));
}

int target_393(int a, int b, int c, int d, int e){
	return (((~(d-a))+((a|c)*d)));
}

int target_394(int a, int b, int c, int d, int e){
	return (((d+(~(e&e)))*((d+d)-(((b+e)-(d-d))&b))));
}

int target_395(int a, int b, int c, int d, int e){
	return ((~((a+((a+a)&(b&b)))&((-a)*(a+a)))));
}

int target_396(int a, int b, int c, int d, int e){
	return ((((d*a)|(a^d))+(a^(a+a))));
}

int target_397(int a, int b, int c, int d, int e){
	return ((-(((d*e)|(e-d))+(e&((e*d)*(d-e))))));
}

int target_398(int a, int b, int c, int d, int e){
	return ((((c^e)|c)|(-(a-e))));
}

int target_399(int a, int b, int c, int d, int e){
	return ((((~(a*d))*(d+a))-(~((c+c)-((d-a)^d)))));
}

int target_400(int a, int b, int c, int d, int e){
	return (((a-(c*c))|((c*c)|a)));
}

int target_401(int a, int b, int c, int d, int e){
	return (((~((d-c)+c))&((~c)^(c|d))));
}

int target_402(int a, int b, int c, int d, int e){
	return ((((-(b&b))*(~b))*(d*(-(b&(d&b))))));
}

int target_403(int a, int b, int c, int d, int e){
	return (((c*((-e)-(c-e)))*(((~d)*c)|(c^c))));
}

int target_404(int a, int b, int c, int d, int e){
	return ((-((~b)|(~(~((b&b)*b))))));
}

int target_405(int a, int b, int c, int d, int e){
	return ((((d-e)^d)|(d+((d&d)|e))));
}

int target_406(int a, int b, int c, int d, int e){
	return ((((-(-a))^(a&a))|((-a)+(~((-a)^a)))));
}

int target_407(int a, int b, int c, int d, int e){
	return ((~(((d*c)-(d&c))+(~(c*d)))));
}

int target_408(int a, int b, int c, int d, int e){
	return (((((e&b)*(~c))|(b-c))+((~e)&e)));
}

int target_409(int a, int b, int c, int d, int e){
	return ((-(-(((-(e&d))*(-e))|(~((~d)*d))))));
}

int target_410(int a, int b, int c, int d, int e){
	return ((((d*d)&(c-e))|(~(-((c&d)|((-d)&c))))));
}

int target_411(int a, int b, int c, int d, int e){
	return (((e-(e+(b+b)))|((e^(b|a))-((~e)^a))));
}

int target_412(int a, int b, int c, int d, int e){
	return ((~(((~(a+((a^a)|a)))&d)+(c|c))));
}

int target_413(int a, int b, int c, int d, int e){
	return ((((~a)+(c*c))^(e^(((a+c)+a)*c))));
}

int target_414(int a, int b, int c, int d, int e){
	return ((((d-a)^(~a))&((b*(a&d))&(d^a))));
}

int target_415(int a, int b, int c, int d, int e){
	return ((-((d*a)^(a-a))));
}

int target_416(int a, int b, int c, int d, int e){
	return (((a^(~(d*(c+a))))-(c*(-c))));
}

int target_417(int a, int b, int c, int d, int e){
	return ((-((~(d*(e*(e|d))))-(d*(e|(e+e))))));
}

int target_418(int a, int b, int c, int d, int e){
	return (((e-e)+((b^b)|e)));
}

int target_419(int a, int b, int c, int d, int e){
	return (((a*a)+(b-(a+a))));
}

int target_420(int a, int b, int c, int d, int e){
	return (((((a^d)-d)&(d-a))^(-(-(a+(~a))))));
}

int target_421(int a, int b, int c, int d, int e){
	return (((b-((b-b)*b))-((((-d)&d)|d)|(d*b))));
}

int target_422(int a, int b, int c, int d, int e){
	return ((-((-((e^e)^(e-e)))^(~(~((c^c)*c))))));
}

int target_423(int a, int b, int c, int d, int e){
	return ((((~c)^(d-e))-((d|(d|e))-((e+d)-e))));
}

int target_424(int a, int b, int c, int d, int e){
	return ((-((b*b)-(~b))));
}

int target_425(int a, int b, int c, int d, int e){
	return (((((~b)-b)^b)+((d|b)&(-b))));
}

int target_426(int a, int b, int c, int d, int e){
	return (((~((~c)*(b-(c*b))))-(c+(b*(b&c)))));
}

int target_427(int a, int b, int c, int d, int e){
	return ((((e+a)&c)-((c-a)&a)));
}

int target_428(int a, int b, int c, int d, int e){
	return ((~(-((-(~a))&(d|(d|d))))));
}

int target_429(int a, int b, int c, int d, int e){
	return (((~((d&a)^c))+((c&(a^c))|(-a))));
}

int target_430(int a, int b, int c, int d, int e){
	return (((((-b)&b)&c)-(-(a+c))));
}

int target_431(int a, int b, int c, int d, int e){
	return ((((~a)&((-(a|b))*e))|(-((~(a^a))&a))));
}

int target_432(int a, int b, int c, int d, int e){
	return ((-(c-(c&(b-c)))));
}

int target_433(int a, int b, int c, int d, int e){
	return (((~(((b-b)^c)^(-c)))-((c*c)^(b*(-c)))));
}

int target_434(int a, int b, int c, int d, int e){
	return ((((e&(~a))|(b-a))|(~((e|b)-e))));
}

int target_435(int a, int b, int c, int d, int e){
	return (((~((b+(e*e))*(a&a)))*((~(a*b))-a)));
}

int target_436(int a, int b, int c, int d, int e){
	return ((~(~(((a|a)^((a+a)&a))^(a|((c-a)^a))))));
}

int target_437(int a, int b, int c, int d, int e){
	return ((~(((-(c^e))*b)+((b*e)^(~((~c)+c))))));
}

int target_438(int a, int b, int c, int d, int e){
	return (((-(e+(c-c)))^((~b)*(b&(b+e)))));
}

int target_439(int a, int b, int c, int d, int e){
	return ((((-d)|d)-((~((d^(a+a))^(-a)))+(d-a))));
}

int target_440(int a, int b, int c, int d, int e){
	return (((-((e|e)+((~e)&c)))+(-((-c)*(-b)))));
}

int target_441(int a, int b, int c, int d, int e){
	return ((~((a+((a^a)&a))-(a*(a*((a*a)-a))))));
}

int target_442(int a, int b, int c, int d, int e){
	return ((-(-(((b&b)|(a-(b+b)))|(b-(~(b^b)))))));
}

int target_443(int a, int b, int c, int d, int e){
	return (((c*((~b)*b))-((b|(d|b))*(b|(~b)))));
}

int target_444(int a, int b, int c, int d, int e){
	return ((-((~(d^b))-(-e))));
}

int target_445(int a, int b, int c, int d, int e){
	return ((((~(-e))&(a^(~e)))^(e-(e+a))));
}

int target_446(int a, int b, int c, int d, int e){
	return ((e|(~(-(~((~c)-d))))));
}

int target_447(int a, int b, int c, int d, int e){
	return ((((c+c)&(d&d))+((((~d)&(d^c))&c)|(c|d))));
}

int target_448(int a, int b, int c, int d, int e){
	return (((~b)+((e*b)&(~((e^b)|b)))));
}

int target_449(int a, int b, int c, int d, int e){
	return (((e-c)^(-(~c))));
}

int target_450(int a, int b, int c, int d, int e){
	return ((-(a-((e|(a|e))^a))));
}

int target_451(int a, int b, int c, int d, int e){
	return (((((-c)&b)|(-b))&((c-b)*(b-(c|(b+b))))));
}

int target_452(int a, int b, int c, int d, int e){
	return (((c|d)+(((~c)&c)|(-d))));
}

int target_453(int a, int b, int c, int d, int e){
	return (((((a&d)-((d^c)|c))&(d-d))-((d^(-c))*d)));
}

int target_454(int a, int b, int c, int d, int e){
	return (((b-(b|e))&((-((-b)^e))-(-(e-e)))));
}

int target_455(int a, int b, int c, int d, int e){
	return ((((~(d+e))|(e*e))-(e*d)));
}

int target_456(int a, int b, int c, int d, int e){
	return (((((d*d)-(d|e))*((e-e)*d))-(d&(d|(-e)))));
}

int target_457(int a, int b, int c, int d, int e){
	return ((((a&(-(~a)))+(b*(~a)))^((a^a)*(a*a))));
}

int target_458(int a, int b, int c, int d, int e){
	return ((((a|e)+c)&(-(a&(c*e)))));
}

int target_459(int a, int b, int c, int d, int e){
	return (((c+(c*(a*c)))|(-(a*a))));
}

int target_460(int a, int b, int c, int d, int e){
	return ((((-((a-(-d))-a))-(d+d))-((-(d|a))^d)));
}

int target_461(int a, int b, int c, int d, int e){
	return ((-((~(~(~b)))|(~((e&c)^(b+(b+c)))))));
}

int target_462(int a, int b, int c, int d, int e){
	return ((-(~(((a+d)+b)+(d^(a&b))))));
}

int target_463(int a, int b, int c, int d, int e){
	return ((((b+b)|a)&(-(~(a|e)))));
}

int target_464(int a, int b, int c, int d, int e){
	return ((((b|a)+a)+(b&(b&b))));
}

int target_465(int a, int b, int c, int d, int e){
	return ((((c*d)*((~(-c))&d))|((c*d)^(d*d))));
}

int target_466(int a, int b, int c, int d, int e){
	return ((-(-(((-d)&d)+((~d)-d)))));
}

int target_467(int a, int b, int c, int d, int e){
	return (((((c+d)+c)&a)^((c-d)*(c*c))));
}

int target_468(int a, int b, int c, int d, int e){
	return (((e^(e|d))+(-(~(d-(e&e))))));
}

int target_469(int a, int b, int c, int d, int e){
	return ((((b^c)-b)*(-b)));
}

int target_470(int a, int b, int c, int d, int e){
	return (((b^(e^e))&(((e&b)&(~e))*(b+(~e)))));
}

int target_471(int a, int b, int c, int d, int e){
	return (((b+b)+(e+(b-b))));
}

int target_472(int a, int b, int c, int d, int e){
	return ((((d-e)-(-(d-d)))-((e&e)-(d|a))));
}

int target_473(int a, int b, int c, int d, int e){
	return (((-((c|c)+c))-(b-(~b))));
}

int target_474(int a, int b, int c, int d, int e){
	return (((-(a*(a+a)))|((-b)|a)));
}

int target_475(int a, int b, int c, int d, int e){
	return (((-(c+a))&((-(d&(-d)))|(c^d))));
}

int target_476(int a, int b, int c, int d, int e){
	return ((~((~(~(a*c)))&(b|((c&b)*c)))));
}

int target_477(int a, int b, int c, int d, int e){
	return (((-(((e&e)+e)-(~a)))+(-(~(~(a*a))))));
}

int target_478(int a, int b, int c, int d, int e){
	return (((a^a)+(((~b)^(a+b))|b)));
}

int target_479(int a, int b, int c, int d, int e){
	return (((a*((a|d)&a))&(~((d&a)+(d*d)))));
}

int target_480(int a, int b, int c, int d, int e){
	return (((~((e*a)^d))&((~(a+d))*(e|e))));
}

int target_481(int a, int b, int c, int d, int e){
	return ((((e-e)&(e-d))-d));
}

int target_482(int a, int b, int c, int d, int e){
	return ((~((b-(c&b))|(b&b))));
}

int target_483(int a, int b, int c, int d, int e){
	return (((-(a|(a*c)))*((a|c)*((c+a)^a))));
}

int target_484(int a, int b, int c, int d, int e){
	return (((b-(-(b+b)))^(~(d+d))));
}

int target_485(int a, int b, int c, int d, int e){
	return (((d*a)|(-(a^d))));
}

int target_486(int a, int b, int c, int d, int e){
	return ((((a+a)*e)^((e^e)|(e*a))));
}

int target_487(int a, int b, int c, int d, int e){
	return ((((e&b)&b)*(c&c)));
}

int target_488(int a, int b, int c, int d, int e){
	return ((-(~(~((b&b)*b)))));
}

int target_489(int a, int b, int c, int d, int e){
	return (((c|b)^(~((-(c|c))*((b^b)^a)))));
}

int target_490(int a, int b, int c, int d, int e){
	return ((~(~(d&(e|e)))));
}

int target_491(int a, int b, int c, int d, int e){
	return ((((b&((e|b)^(e^e)))&(b&b))^((b|a)+a)));
}

int target_492(int a, int b, int c, int d, int e){
	return ((a|((c|(a+a))+(c+a))));
}

int target_493(int a, int b, int c, int d, int e){
	return ((-((-c)&((c&a)*a))));
}

int target_494(int a, int b, int c, int d, int e){
	return ((((c&c)&(((a^c)|a)|c))-((c-(c^a))^(a&a))));
}

int target_495(int a, int b, int c, int d, int e){
	return (((((d&a)*a)^(a+d))^((a^a)&d)));
}

int target_496(int a, int b, int c, int d, int e){
	return ((((d*a)&((~((-a)^a))&a))-(((-a)-e)&e)));
}

int target_497(int a, int b, int c, int d, int e){
	return (((a^((a-c)&c))&((a|c)|a)));
}

int target_498(int a, int b, int c, int d, int e){
	return (((c+(-c))&(e&b)));
}

int target_499(int a, int b, int c, int d, int e){
	return ((-((-(-(-e)))&((b&e)+b))));
}

void all_targets(int a, int b, int c, int d, int e){
	target_0(a, b, c, d, e);
	target_1(a, b, c, d, e);
	target_2(a, b, c, d, e);
	target_3(a, b, c, d, e);
	target_4(a, b, c, d, e);
	target_5(a, b, c, d, e);
	target_6(a, b, c, d, e);
	target_7(a, b, c, d, e);
	target_8(a, b, c, d, e);
	target_9(a, b, c, d, e);
	target_10(a, b, c, d, e);
	target_11(a, b, c, d, e);
	target_12(a, b, c, d, e);
	target_13(a, b, c, d, e);
	target_14(a, b, c, d, e);
	target_15(a, b, c, d, e);
	target_16(a, b, c, d, e);
	target_17(a, b, c, d, e);
	target_18(a, b, c, d, e);
	target_19(a, b, c, d, e);
	target_20(a, b, c, d, e);
	target_21(a, b, c, d, e);
	target_22(a, b, c, d, e);
	target_23(a, b, c, d, e);
	target_24(a, b, c, d, e);
	target_25(a, b, c, d, e);
	target_26(a, b, c, d, e);
	target_27(a, b, c, d, e);
	target_28(a, b, c, d, e);
	target_29(a, b, c, d, e);
	target_30(a, b, c, d, e);
	target_31(a, b, c, d, e);
	target_32(a, b, c, d, e);
	target_33(a, b, c, d, e);
	target_34(a, b, c, d, e);
	target_35(a, b, c, d, e);
	target_36(a, b, c, d, e);
	target_37(a, b, c, d, e);
	target_38(a, b, c, d, e);
	target_39(a, b, c, d, e);
	target_40(a, b, c, d, e);
	target_41(a, b, c, d, e);
	target_42(a, b, c, d, e);
	target_43(a, b, c, d, e);
	target_44(a, b, c, d, e);
	target_45(a, b, c, d, e);
	target_46(a, b, c, d, e);
	target_47(a, b, c, d, e);
	target_48(a, b, c, d, e);
	target_49(a, b, c, d, e);
	target_50(a, b, c, d, e);
	target_51(a, b, c, d, e);
	target_52(a, b, c, d, e);
	target_53(a, b, c, d, e);
	target_54(a, b, c, d, e);
	target_55(a, b, c, d, e);
	target_56(a, b, c, d, e);
	target_57(a, b, c, d, e);
	target_58(a, b, c, d, e);
	target_59(a, b, c, d, e);
	target_60(a, b, c, d, e);
	target_61(a, b, c, d, e);
	target_62(a, b, c, d, e);
	target_63(a, b, c, d, e);
	target_64(a, b, c, d, e);
	target_65(a, b, c, d, e);
	target_66(a, b, c, d, e);
	target_67(a, b, c, d, e);
	target_68(a, b, c, d, e);
	target_69(a, b, c, d, e);
	target_70(a, b, c, d, e);
	target_71(a, b, c, d, e);
	target_72(a, b, c, d, e);
	target_73(a, b, c, d, e);
	target_74(a, b, c, d, e);
	target_75(a, b, c, d, e);
	target_76(a, b, c, d, e);
	target_77(a, b, c, d, e);
	target_78(a, b, c, d, e);
	target_79(a, b, c, d, e);
	target_80(a, b, c, d, e);
	target_81(a, b, c, d, e);
	target_82(a, b, c, d, e);
	target_83(a, b, c, d, e);
	target_84(a, b, c, d, e);
	target_85(a, b, c, d, e);
	target_86(a, b, c, d, e);
	target_87(a, b, c, d, e);
	target_88(a, b, c, d, e);
	target_89(a, b, c, d, e);
	target_90(a, b, c, d, e);
	target_91(a, b, c, d, e);
	target_92(a, b, c, d, e);
	target_93(a, b, c, d, e);
	target_94(a, b, c, d, e);
	target_95(a, b, c, d, e);
	target_96(a, b, c, d, e);
	target_97(a, b, c, d, e);
	target_98(a, b, c, d, e);
	target_99(a, b, c, d, e);
	target_100(a, b, c, d, e);
	target_101(a, b, c, d, e);
	target_102(a, b, c, d, e);
	target_103(a, b, c, d, e);
	target_104(a, b, c, d, e);
	target_105(a, b, c, d, e);
	target_106(a, b, c, d, e);
	target_107(a, b, c, d, e);
	target_108(a, b, c, d, e);
	target_109(a, b, c, d, e);
	target_110(a, b, c, d, e);
	target_111(a, b, c, d, e);
	target_112(a, b, c, d, e);
	target_113(a, b, c, d, e);
	target_114(a, b, c, d, e);
	target_115(a, b, c, d, e);
	target_116(a, b, c, d, e);
	target_117(a, b, c, d, e);
	target_118(a, b, c, d, e);
	target_119(a, b, c, d, e);
	target_120(a, b, c, d, e);
	target_121(a, b, c, d, e);
	target_122(a, b, c, d, e);
	target_123(a, b, c, d, e);
	target_124(a, b, c, d, e);
	target_125(a, b, c, d, e);
	target_126(a, b, c, d, e);
	target_127(a, b, c, d, e);
	target_128(a, b, c, d, e);
	target_129(a, b, c, d, e);
	target_130(a, b, c, d, e);
	target_131(a, b, c, d, e);
	target_132(a, b, c, d, e);
	target_133(a, b, c, d, e);
	target_134(a, b, c, d, e);
	target_135(a, b, c, d, e);
	target_136(a, b, c, d, e);
	target_137(a, b, c, d, e);
	target_138(a, b, c, d, e);
	target_139(a, b, c, d, e);
	target_140(a, b, c, d, e);
	target_141(a, b, c, d, e);
	target_142(a, b, c, d, e);
	target_143(a, b, c, d, e);
	target_144(a, b, c, d, e);
	target_145(a, b, c, d, e);
	target_146(a, b, c, d, e);
	target_147(a, b, c, d, e);
	target_148(a, b, c, d, e);
	target_149(a, b, c, d, e);
	target_150(a, b, c, d, e);
	target_151(a, b, c, d, e);
	target_152(a, b, c, d, e);
	target_153(a, b, c, d, e);
	target_154(a, b, c, d, e);
	target_155(a, b, c, d, e);
	target_156(a, b, c, d, e);
	target_157(a, b, c, d, e);
	target_158(a, b, c, d, e);
	target_159(a, b, c, d, e);
	target_160(a, b, c, d, e);
	target_161(a, b, c, d, e);
	target_162(a, b, c, d, e);
	target_163(a, b, c, d, e);
	target_164(a, b, c, d, e);
	target_165(a, b, c, d, e);
	target_166(a, b, c, d, e);
	target_167(a, b, c, d, e);
	target_168(a, b, c, d, e);
	target_169(a, b, c, d, e);
	target_170(a, b, c, d, e);
	target_171(a, b, c, d, e);
	target_172(a, b, c, d, e);
	target_173(a, b, c, d, e);
	target_174(a, b, c, d, e);
	target_175(a, b, c, d, e);
	target_176(a, b, c, d, e);
	target_177(a, b, c, d, e);
	target_178(a, b, c, d, e);
	target_179(a, b, c, d, e);
	target_180(a, b, c, d, e);
	target_181(a, b, c, d, e);
	target_182(a, b, c, d, e);
	target_183(a, b, c, d, e);
	target_184(a, b, c, d, e);
	target_185(a, b, c, d, e);
	target_186(a, b, c, d, e);
	target_187(a, b, c, d, e);
	target_188(a, b, c, d, e);
	target_189(a, b, c, d, e);
	target_190(a, b, c, d, e);
	target_191(a, b, c, d, e);
	target_192(a, b, c, d, e);
	target_193(a, b, c, d, e);
	target_194(a, b, c, d, e);
	target_195(a, b, c, d, e);
	target_196(a, b, c, d, e);
	target_197(a, b, c, d, e);
	target_198(a, b, c, d, e);
	target_199(a, b, c, d, e);
	target_200(a, b, c, d, e);
	target_201(a, b, c, d, e);
	target_202(a, b, c, d, e);
	target_203(a, b, c, d, e);
	target_204(a, b, c, d, e);
	target_205(a, b, c, d, e);
	target_206(a, b, c, d, e);
	target_207(a, b, c, d, e);
	target_208(a, b, c, d, e);
	target_209(a, b, c, d, e);
	target_210(a, b, c, d, e);
	target_211(a, b, c, d, e);
	target_212(a, b, c, d, e);
	target_213(a, b, c, d, e);
	target_214(a, b, c, d, e);
	target_215(a, b, c, d, e);
	target_216(a, b, c, d, e);
	target_217(a, b, c, d, e);
	target_218(a, b, c, d, e);
	target_219(a, b, c, d, e);
	target_220(a, b, c, d, e);
	target_221(a, b, c, d, e);
	target_222(a, b, c, d, e);
	target_223(a, b, c, d, e);
	target_224(a, b, c, d, e);
	target_225(a, b, c, d, e);
	target_226(a, b, c, d, e);
	target_227(a, b, c, d, e);
	target_228(a, b, c, d, e);
	target_229(a, b, c, d, e);
	target_230(a, b, c, d, e);
	target_231(a, b, c, d, e);
	target_232(a, b, c, d, e);
	target_233(a, b, c, d, e);
	target_234(a, b, c, d, e);
	target_235(a, b, c, d, e);
	target_236(a, b, c, d, e);
	target_237(a, b, c, d, e);
	target_238(a, b, c, d, e);
	target_239(a, b, c, d, e);
	target_240(a, b, c, d, e);
	target_241(a, b, c, d, e);
	target_242(a, b, c, d, e);
	target_243(a, b, c, d, e);
	target_244(a, b, c, d, e);
	target_245(a, b, c, d, e);
	target_246(a, b, c, d, e);
	target_247(a, b, c, d, e);
	target_248(a, b, c, d, e);
	target_249(a, b, c, d, e);
	target_250(a, b, c, d, e);
	target_251(a, b, c, d, e);
	target_252(a, b, c, d, e);
	target_253(a, b, c, d, e);
	target_254(a, b, c, d, e);
	target_255(a, b, c, d, e);
	target_256(a, b, c, d, e);
	target_257(a, b, c, d, e);
	target_258(a, b, c, d, e);
	target_259(a, b, c, d, e);
	target_260(a, b, c, d, e);
	target_261(a, b, c, d, e);
	target_262(a, b, c, d, e);
	target_263(a, b, c, d, e);
	target_264(a, b, c, d, e);
	target_265(a, b, c, d, e);
	target_266(a, b, c, d, e);
	target_267(a, b, c, d, e);
	target_268(a, b, c, d, e);
	target_269(a, b, c, d, e);
	target_270(a, b, c, d, e);
	target_271(a, b, c, d, e);
	target_272(a, b, c, d, e);
	target_273(a, b, c, d, e);
	target_274(a, b, c, d, e);
	target_275(a, b, c, d, e);
	target_276(a, b, c, d, e);
	target_277(a, b, c, d, e);
	target_278(a, b, c, d, e);
	target_279(a, b, c, d, e);
	target_280(a, b, c, d, e);
	target_281(a, b, c, d, e);
	target_282(a, b, c, d, e);
	target_283(a, b, c, d, e);
	target_284(a, b, c, d, e);
	target_285(a, b, c, d, e);
	target_286(a, b, c, d, e);
	target_287(a, b, c, d, e);
	target_288(a, b, c, d, e);
	target_289(a, b, c, d, e);
	target_290(a, b, c, d, e);
	target_291(a, b, c, d, e);
	target_292(a, b, c, d, e);
	target_293(a, b, c, d, e);
	target_294(a, b, c, d, e);
	target_295(a, b, c, d, e);
	target_296(a, b, c, d, e);
	target_297(a, b, c, d, e);
	target_298(a, b, c, d, e);
	target_299(a, b, c, d, e);
	target_300(a, b, c, d, e);
	target_301(a, b, c, d, e);
	target_302(a, b, c, d, e);
	target_303(a, b, c, d, e);
	target_304(a, b, c, d, e);
	target_305(a, b, c, d, e);
	target_306(a, b, c, d, e);
	target_307(a, b, c, d, e);
	target_308(a, b, c, d, e);
	target_309(a, b, c, d, e);
	target_310(a, b, c, d, e);
	target_311(a, b, c, d, e);
	target_312(a, b, c, d, e);
	target_313(a, b, c, d, e);
	target_314(a, b, c, d, e);
	target_315(a, b, c, d, e);
	target_316(a, b, c, d, e);
	target_317(a, b, c, d, e);
	target_318(a, b, c, d, e);
	target_319(a, b, c, d, e);
	target_320(a, b, c, d, e);
	target_321(a, b, c, d, e);
	target_322(a, b, c, d, e);
	target_323(a, b, c, d, e);
	target_324(a, b, c, d, e);
	target_325(a, b, c, d, e);
	target_326(a, b, c, d, e);
	target_327(a, b, c, d, e);
	target_328(a, b, c, d, e);
	target_329(a, b, c, d, e);
	target_330(a, b, c, d, e);
	target_331(a, b, c, d, e);
	target_332(a, b, c, d, e);
	target_333(a, b, c, d, e);
	target_334(a, b, c, d, e);
	target_335(a, b, c, d, e);
	target_336(a, b, c, d, e);
	target_337(a, b, c, d, e);
	target_338(a, b, c, d, e);
	target_339(a, b, c, d, e);
	target_340(a, b, c, d, e);
	target_341(a, b, c, d, e);
	target_342(a, b, c, d, e);
	target_343(a, b, c, d, e);
	target_344(a, b, c, d, e);
	target_345(a, b, c, d, e);
	target_346(a, b, c, d, e);
	target_347(a, b, c, d, e);
	target_348(a, b, c, d, e);
	target_349(a, b, c, d, e);
	target_350(a, b, c, d, e);
	target_351(a, b, c, d, e);
	target_352(a, b, c, d, e);
	target_353(a, b, c, d, e);
	target_354(a, b, c, d, e);
	target_355(a, b, c, d, e);
	target_356(a, b, c, d, e);
	target_357(a, b, c, d, e);
	target_358(a, b, c, d, e);
	target_359(a, b, c, d, e);
	target_360(a, b, c, d, e);
	target_361(a, b, c, d, e);
	target_362(a, b, c, d, e);
	target_363(a, b, c, d, e);
	target_364(a, b, c, d, e);
	target_365(a, b, c, d, e);
	target_366(a, b, c, d, e);
	target_367(a, b, c, d, e);
	target_368(a, b, c, d, e);
	target_369(a, b, c, d, e);
	target_370(a, b, c, d, e);
	target_371(a, b, c, d, e);
	target_372(a, b, c, d, e);
	target_373(a, b, c, d, e);
	target_374(a, b, c, d, e);
	target_375(a, b, c, d, e);
	target_376(a, b, c, d, e);
	target_377(a, b, c, d, e);
	target_378(a, b, c, d, e);
	target_379(a, b, c, d, e);
	target_380(a, b, c, d, e);
	target_381(a, b, c, d, e);
	target_382(a, b, c, d, e);
	target_383(a, b, c, d, e);
	target_384(a, b, c, d, e);
	target_385(a, b, c, d, e);
	target_386(a, b, c, d, e);
	target_387(a, b, c, d, e);
	target_388(a, b, c, d, e);
	target_389(a, b, c, d, e);
	target_390(a, b, c, d, e);
	target_391(a, b, c, d, e);
	target_392(a, b, c, d, e);
	target_393(a, b, c, d, e);
	target_394(a, b, c, d, e);
	target_395(a, b, c, d, e);
	target_396(a, b, c, d, e);
	target_397(a, b, c, d, e);
	target_398(a, b, c, d, e);
	target_399(a, b, c, d, e);
	target_400(a, b, c, d, e);
	target_401(a, b, c, d, e);
	target_402(a, b, c, d, e);
	target_403(a, b, c, d, e);
	target_404(a, b, c, d, e);
	target_405(a, b, c, d, e);
	target_406(a, b, c, d, e);
	target_407(a, b, c, d, e);
	target_408(a, b, c, d, e);
	target_409(a, b, c, d, e);
	target_410(a, b, c, d, e);
	target_411(a, b, c, d, e);
	target_412(a, b, c, d, e);
	target_413(a, b, c, d, e);
	target_414(a, b, c, d, e);
	target_415(a, b, c, d, e);
	target_416(a, b, c, d, e);
	target_417(a, b, c, d, e);
	target_418(a, b, c, d, e);
	target_419(a, b, c, d, e);
	target_420(a, b, c, d, e);
	target_421(a, b, c, d, e);
	target_422(a, b, c, d, e);
	target_423(a, b, c, d, e);
	target_424(a, b, c, d, e);
	target_425(a, b, c, d, e);
	target_426(a, b, c, d, e);
	target_427(a, b, c, d, e);
	target_428(a, b, c, d, e);
	target_429(a, b, c, d, e);
	target_430(a, b, c, d, e);
	target_431(a, b, c, d, e);
	target_432(a, b, c, d, e);
	target_433(a, b, c, d, e);
	target_434(a, b, c, d, e);
	target_435(a, b, c, d, e);
	target_436(a, b, c, d, e);
	target_437(a, b, c, d, e);
	target_438(a, b, c, d, e);
	target_439(a, b, c, d, e);
	target_440(a, b, c, d, e);
	target_441(a, b, c, d, e);
	target_442(a, b, c, d, e);
	target_443(a, b, c, d, e);
	target_444(a, b, c, d, e);
	target_445(a, b, c, d, e);
	target_446(a, b, c, d, e);
	target_447(a, b, c, d, e);
	target_448(a, b, c, d, e);
	target_449(a, b, c, d, e);
	target_450(a, b, c, d, e);
	target_451(a, b, c, d, e);
	target_452(a, b, c, d, e);
	target_453(a, b, c, d, e);
	target_454(a, b, c, d, e);
	target_455(a, b, c, d, e);
	target_456(a, b, c, d, e);
	target_457(a, b, c, d, e);
	target_458(a, b, c, d, e);
	target_459(a, b, c, d, e);
	target_460(a, b, c, d, e);
	target_461(a, b, c, d, e);
	target_462(a, b, c, d, e);
	target_463(a, b, c, d, e);
	target_464(a, b, c, d, e);
	target_465(a, b, c, d, e);
	target_466(a, b, c, d, e);
	target_467(a, b, c, d, e);
	target_468(a, b, c, d, e);
	target_469(a, b, c, d, e);
	target_470(a, b, c, d, e);
	target_471(a, b, c, d, e);
	target_472(a, b, c, d, e);
	target_473(a, b, c, d, e);
	target_474(a, b, c, d, e);
	target_475(a, b, c, d, e);
	target_476(a, b, c, d, e);
	target_477(a, b, c, d, e);
	target_478(a, b, c, d, e);
	target_479(a, b, c, d, e);
	target_480(a, b, c, d, e);
	target_481(a, b, c, d, e);
	target_482(a, b, c, d, e);
	target_483(a, b, c, d, e);
	target_484(a, b, c, d, e);
	target_485(a, b, c, d, e);
	target_486(a, b, c, d, e);
	target_487(a, b, c, d, e);
	target_488(a, b, c, d, e);
	target_489(a, b, c, d, e);
	target_490(a, b, c, d, e);
	target_491(a, b, c, d, e);
	target_492(a, b, c, d, e);
	target_493(a, b, c, d, e);
	target_494(a, b, c, d, e);
	target_495(a, b, c, d, e);
	target_496(a, b, c, d, e);
	target_497(a, b, c, d, e);
	target_498(a, b, c, d, e);
	target_499(a, b, c, d, e);
}

int main(int argc, char** argv){
	if (argc <= 5){
		printf("usage: %s <input1> ... <input5>\n",argv[0]);
		exit(1);
	}
	int a = atoi(argv[1]);
	int b = atoi(argv[2]);
	int c = atoi(argv[3]);
	int d = atoi(argv[4]);
	int e = atoi(argv[5]);
	all_targets(a ,b ,c ,d ,e);
	return 0;
}
