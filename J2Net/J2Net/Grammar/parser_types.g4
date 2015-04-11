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
	| annotation* BOOLEAN
	;

numericType
	: integralType
	| floatingPointType
	;

integralType
	: BYTE
	| SHORT
	| INT
	| LONG
	| CHAR
	;

floatingPointType
	: FLOAT
	| DOUBLE
	;

referenceType
	: classOrInterfaceType
	| typeVariable
	| arrayType
	;

//Old way which cause LR
//classOrInterfaceType
//	: classType 
//	| interfaceType
//	;

classOrInterfaceType
	: ( lfNo_classType
	  | lfNo_interfaceType
	  )
	  ( lf_classType
	  | lf_interfaceType
	  )*
	;

lf_classType
	: DOT annotation* Identifiers typeArguments?
	;

lfNo_classType
	: annotation* Identifiers typeArguments?
	;

lf_interfaceType
	: lf_classType
	;

lfNo_interfaceType
	: lfNo_classType
	;

//BUG: The following sets of rules are mutually left-recursive [classOrInterfaceType, classType, interfaceType]
//classType : DOT;
//Bug fixed (Eric)
classType
	: annotation* Identifiers typeArguments?
	| classOrInterfaceType DOT annotation* Identifiers typeArguments?
	;

interfaceType
	: classType
	;

typeVariable
	: annotation* Identifiers
	;

arrayType
	: primitiveType dims
	| classOrInterfaceType dims
	| typeVariable dims
	;

dims
	: annotation* LBRACK RBRACK (annotation* LBRACK RBRACK)*
	;

typeParameter
	: typeParameterModifier* Identifiers typeBound?
	| Identifiers typeBound?
	;

typeParameterModifier
	: annotation
	;

typeBound
	: EXTENDS typeVariable
	| EXTENDS classOrInterfaceType additionalBound*
	;

additionalBound
	: BITAND interfaceType
	;

typeArguments
	: LT typeArgumentList GT
	;

typeArgumentList
	: typeArgument (COMMA typeArgument)*
	;

typeArgument
	: referenceType
	| wildCard
	;

wildCard
	: annotation* QUESTION wildcardBounds?
	;

wildcardBounds
	: EXTENDS referenceType
	| SUPER referenceType
	;

