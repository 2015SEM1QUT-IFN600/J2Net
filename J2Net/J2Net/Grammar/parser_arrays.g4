parser grammar parser_arrays;

compileUnit
	:	EOF
	;

arrayInitializer
	:	 LBRACE LBRACK variableInitializerList RBRACK LBRACK COMMA RBRACK RBRACE
	;
variableInitializerList
	:	variableInitializerList LBRACE COMMA variableInitializer RBRACE
	;