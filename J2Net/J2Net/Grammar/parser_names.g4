parser grammar parser_names;

import parser_packages;




/************ NAMES  ************/

//BUG: causes unknown build error. Each of the following calls itself in a forever loop
packageName : DOT;
typeName : DOT;
packageOrTypeName : DOT;
expressionName : DOT;
ambiguousName : DOT; 

//packageName
//	:	Identifiers
//	|	packageName DOT Identifiers
//	;

//typeName
//	:	Identifiers
//	|	packageOrTypeName DOT Identifiers
//	;

//packageOrTypeName
//	:	Identifiers
//	|	packageOrTypeName DOT Identifiers
//	;

//expressionName
//	:	Identifiers
//	|	ambiguousName DOT Identifiers
//	;

//ambiguousName
//	:	Identifiers
//	|	ambiguousName DOT Identifiers
//	;

methodName
	:	Identifiers
	;