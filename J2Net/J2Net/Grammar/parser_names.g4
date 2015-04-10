parser grammar parser_names;

import parser_packages;

// TO BE OVERRIDDEN IN CHILD RULES //
//none


/************ NAMES  ************/

//BUG: causes unknown build error. Each of the following calls itself in a forever loop
packageName : DOT;
typeName : DOT;
packageOrTypeName : DOT;
expressionName : DOT;
ambiguousName : DOT; 

//packageName
//	:	Identifiers
//	|	packageName DOT Identifier
//	;

//typeName
//	:	Identifiers
//	|	packageOrTypeName DOT Identifier
//	;

//packageOrTypeName
//	:	Identifiers
//	|	packageOrTypeName DOT Identifier
//	;

//expressionName
//	:	Identifiers
//	|	ambiguousName DOT Identifier
//	;

//ambiguousName
//	:	Identifiers
//	|	ambiguousName DOT Identifiers
//	;

methodName
	:	Identifiers
	;