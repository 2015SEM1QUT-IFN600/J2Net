parser grammar parser_names;

import parser_packages;




/************ NAMES  ************/
/*
Identifier:
IdentifierChars but not a Keyword or BooleanLiteral or NullLiteral
IdentifierChars:
JavaLetter {JavaLetterOrDigit}
JavaLetter:
any Unicode character that is a "Java letter"
JavaLetterOrDigit:
any Unicode character that is a "Java letter-or-digit"
*/

packageName
	: Identifiers
	| packageName DOT Identifiers		//From Java spec - pass!!!!!!
//	| Identifiers (DOT packageName)+	//-Don't need
	;

typeName
	: Identifiers
	| packageOrTypeName (DOT Identifiers)+	//From Java spec
	;

packageOrTypeName
	: Identifiers
//	| packageOrTypeName (DOT Identifiers)+	//From Java spec
	| Identifiers (DOT packageOrTypeName)+
	;

expressionName
	: Identifiers
//	| ambiguousName (DOT Identifiers)+		//From Java spec
	| Identifiers (DOT ambiguousName)+
	;

methodName
	: Identifiers
	;

ambiguousName
	: Identifiers
//	| ambiguousName (DOT Identifiers)+		//From Java spec - got error
	| Identifiers (DOT ambiguousName)+
	;