parser grammar parser_types;

options {
	tokenVocab = Java;
}

type
	: primitiveType
	| referenceType
	;

primitiveType
	: annotation* numericType
	| annotation* 'boolean'
	;	//waiting for this to be construct

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

classType
	: annotation* Identifier typeArguments?
	| classOrInterfaceType '.' annotation* Identifier typeArguments?
	;	//waiting for this to be construct

interfaceType
	: classType
	;

typeVariable
	: annotation* Identifier
	;	//waiting for this to be construct

arrayType
	: primitiveType dims
	| classOrInterfaceType dims
	| typeVariable dims
	;

dims
	:annotation* '['']' (annotation* '['']')*
	;	//waiting for this to be construct

typeParameter
	:typeParameterModifier* Identifier typeBound?
	//:// typeParameterModifier+ Identifier typeBound?
	//| Identifier typeBound?
	;	//waiting for this to be construct

typeParameterModifier
	: annotation
	;	//waiting for this to be construct

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

compileUnit
	:	EOF
	;