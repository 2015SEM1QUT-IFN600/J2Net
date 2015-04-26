parser grammar parser_types;

import parser_expressions;


// UNCOMMENT THESE RULES IF COMMENTING IMPORT CHAIN //
//annotation : DOT;
//variableInitializer: DOT;

/************ parser_names  ************/
//The reason this is being moved out of the parser_name.g4 file is because of the error that it was producing.
//After hours of research + testing, it appears that parser_names was running out of indexs when compiling due to LR being used too much.
//Currently this is a solution, however, some people might think that the code might not be so organized.

expressionName
	: Identifiers
	| ambiguousName DOT Identifiers		//From Java spec
//	| Identifiers (DOT ambiguousName)+
	;

ambiguousName
	: Identifiers
	| ambiguousName DOT Identifiers		//From Java spec
//	| Identifiers (DOT ambiguousName)+
	;

/************ ARRAYS  ************/

//BUG: rule 'arrayInitializer' contains a closure with at least one alternative that can match an empty string
//arrayInitializer : DOT; 
arrayInitializer
	:	 LBRACE variableInitializerList? COMMA? RBRACE
	;

variableInitializerList
	:	variableInitializer (COMMA variableInitializer)*
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
	//: (annotation* LBRACK RBRACK)+
	: annotation* LBRACK RBRACK (annotation* LBRACK RBRACK)*
	;

typeParameters
	: LT typeParameterList GT
	;

typeParameterList
	: typeParameter (COMMA typeParameter)*
	;

typeParameter
	: typeParameterModifier* Identifiers typeBound?
	//| Identifiers typeBound?
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

