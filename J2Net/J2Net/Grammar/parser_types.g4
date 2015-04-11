parser grammar parser_types;

import parser_expressions;


// UNCOMMENT THESE RULES IF COMMENTING IMPORT CHAIN //
//annotation : DOT;
//variableInitializer: DOT;


/************ ARRAYS  ************/

//BUG: rule 'arrayInitializer' contains a closure with at least one alternative that can match an empty string
arrayInitializer : DOT; 
//arrayInitializer
//	:	 (variableInitializerList? ','?)*
//	;

variableInitializerList
	:	variableInitializer (',' variableInitializer)*
	;

/************ LITERALS  ************/

literal
	:	IntegerLiteral
	|	FloatingPointLiteral
	|	BooleanLiteral
	|	CharacterLiteral
	|	StringLiteral
	|	NullLiteral
	;

/************ TYPES  ************/

type
	: primitiveType
	| referenceType
	;

primitiveType
	: annotation* numericType
	| annotation* 'boolean'
	;

numericType
	: integralType
	| floatingPointType
	;

integralType
	: 'byte'
	| 'short'
	| 'int'
	| 'long'
	| 'char'
	;

floatingPointType
	: 'float'
	| 'double'
	;

referenceType
	: classOrInterfaceType
	| typeVariable
	| arrayType
	;


classOrInterfaceType
	: classType
	| interfaceType
	;

//BUG: The following sets of rules are mutually left-recursive [classOrInterfaceType, classType, interfaceType]
classType : DOT;
//classType
//	: annotation* Identifier typeArguments?
//	| classOrInterfaceType '.' annotation* Identifier typeArguments?
//	;	//waiting for this to be construct

interfaceType
	: classType
	;

typeVariable
	: annotation* Identifier
	;

arrayType
	: primitiveType dims
	| classOrInterfaceType dims
	| typeVariable dims
	;

dims
	: annotation* '['']' (annotation* '['']')*
	;

typeParameter
	: typeParameterModifier* Identifier typeBound?
	| Identifier typeBound?
	;

typeParameterModifier
	: annotation
	;

typeBound
	: 'extends' typeVariable
	| 'extends' classOrInterfaceType additionalBound*
	;

additionalBound
	: '&' interfaceType
	;

typeArguments
	: '<' typeArgumentList '>'
	;

typeArgumentList
	: typeArgument (',' typeArgument)*
	;

typeArgument
	: referenceType
	| wildCard
	;

wildCard
	: annotation* '?' wildcardBounds?
	;

wildcardBounds
	: 'extends' referenceType
	| 'super' referenceType
	;

