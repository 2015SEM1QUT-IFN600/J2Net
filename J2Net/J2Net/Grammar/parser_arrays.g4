parser grammar parser_arrays;

arrayInitializer
	:	 (variableInitializerList? ','?)*
	;
variableInitializerList
	:	variableInitializer (',' variableInitializer)*
	;

compileUnit
	:	EOF
	;