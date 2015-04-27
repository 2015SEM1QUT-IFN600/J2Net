parser grammar parser_interfaces;

import parser_names;

/************ INTERFACES  ************/

interfaceDeclaration
	: normalInterfaceDeclaration
	| annotationTypeDeclaration
	;

normalInterfaceDeclaration
	: (interfaceModifier)* INTERFACE Identifiers (typeParameters)? (extendsInterfaces)? interfaceBody
	;

interfaceModifier
	: annotation 
	| (	PUBLIC
	  | PROTECTED
	  | PRIVATE
	  | ABSTRACT 
	  | STATIC 
	  | STRICTFP
	  )
	;

extendsInterfaces
	: EXTENDS interfaceTypeList
	;

interfaceTypeList
	:  interfaceType (COMMA interfaceType)*
	;

interfaceBody
	: LBRACE (interfaceMemberDeclaration)* RBRACE
	;

interfaceMemberDeclaration
	: constantDeclaration
	| interfaceMethodDeclaration
	| classDeclaration
	| interfaceDeclaration
	| SEMI
	;

constantDeclaration
	: (constantModifier)* unannType variableDeclaratorList SEMI
	;

constantModifier
	: annotation 
	| ( PUBLIC
	  | STATIC 
	  | FINAL
	  )
	;

interfaceMethodDeclaration
	: (interfaceMethodModifier)* methodHeader methodBody
	;

interfaceMethodModifier
	: annotation 
	| ( PUBLIC
	  | ABSTRACT
	  | DEFAULT 
	  | STATIC 
	  | STRICTFP
	  )
	;

annotationTypeDeclaration
	: (interfaceModifier)* ATSIGN INTERFACE Identifiers annotationTypeBody
	;

annotationTypeBody
	: LBRACE (annotationTypeMemberDeclaration)* RBRACE
	;

annotationTypeMemberDeclaration
	: annotationTypeElementDeclaration
	| constantDeclaration
	| classDeclaration
	| interfaceDeclaration
	| SEMI
	;

annotationTypeElementDeclaration
	: (annotationTypeElementModifier)* unannType Identifiers LPAREN RPAREN (dims)? (defaultValue)? SEMI
	;

annotationTypeElementModifier
	: annotation 
	| ( PUBLIC
	  | ABSTRACT
	  )
	;

defaultValue
	: DEFAULT elementValue
	;

annotation
	: normalAnnotation
	| markerAnnotation
	| singleElementAnnotation
	;

normalAnnotation
	: ATSIGN typeName LBRACE (elementValuePairList)? RBRACE
	;

elementValuePairList
	: elementValuePair (COMMA elementValuePair)*
	;

elementValuePair
	: Identifiers ASSIGN elementValue
	;

elementValue
	: conditionalExpression
	| elementValueArrayInitializer
	| annotation
	;

elementValueArrayInitializer
	: LBRACE (elementValueList)? (COMMA)? RBRACE
	;

elementValueList
	: elementValue (COMMA elementValue)*
	;

markerAnnotation
	: ATSIGN typeName
	;

singleElementAnnotation
	: ATSIGN typeName LPAREN elementValue RPAREN
	;
