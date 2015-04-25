parser grammar parser_classes;

import parser_interfaces;

// UNCOMMENT THESE RULES IF COMMENTING IMPORT CHAIN //
//interfaceDeclaration : DOT;



/************ CLASSES  ************/

classDeclaration
	:	normalClassDeclaration 
	|	enumDeclaration
	;

normalClassDeclaration
    	//:   classModifier CLASS Identifiers LBRACK typeParameters RBRACK LBRACK superclass RBRACK LBRACK superinterfaces RBRACK classBody
		:   classModifier* CLASS Identifiers (typeParameters)? (superclass)? (superinterfaces)? classBody
    	;

classModifier
    :    annotation
    |	 PUBLIC
    |    PROTECTED
    |    PRIVATE
	|    ABSTRACT
    |    STATIC
    |    FINAL
	|    STRICTFP
    ;

typeParameters
    :   LT typeParameterList GT
    ;

typeParameterList
	:	typeParameter (COMMA typeParameter)*
	;


superclass
	:	EXTENDS classType
	;

superinterfaces
	:	IMPLEMENTS interfaceTypeList
	;

interfaceTypeList
	:	interfaceType (COMMA interfaceType)*
	;

classBody
	:	LBRACE (classBodyDeclaration)* RBRACE
	;

classBodyDeclaration
	:	classMemberDeclaration
	|	instanceInitializer
	|	staticInitializer
	|	constructorDeclaration
	;

classMemberDeclaration
	:	fieldDeclaration
	|	methodDeclaration
	|	classDeclaration
	|	interfaceDeclaration
	|	SEMI
	;

fieldDeclaration
	:	fieldModifier* unannType variableDeclaratorList SEMI
	;

fieldModifier
	:	annotation
	|	PUBLIC 
	|	PROTECTED
	|	PRIVATE
	|	STATIC
	|	FINAL
	|	TRANSIENT
	|	VOLATILE
	; 

variableDeclaratorList
	:	variableDeclarator (COMMA variableDeclarator)*
	;

variableDeclarator
	:	variableDeclaratorId (ASSIGN variableInitializer)?
	;

variableDeclaratorId
	:	Identifiers dims?
	;

variableInitializer
	:	expression
	|	arrayInitializer
	;

unannType
	:	unannPrimitiveType 
	|	unannReferenceType
	;

unannPrimitiveType
	:	numericType
	|	BOOLEAN
	;

unannReferenceType
	:	unannClassOrInterfaceType 
	|	unannTypeVariable 
	|	unannArrayType
	;

/*unannClassOrInterfaceType
	:	unannClassType 
	|	unannInterfaceType
	;
*/

unannClassOrInterfaceType
	:	( unannClassType_lfno_unannClassOrInterfaceType
		| unannInterfaceType_lfno_unannClassOrInterfaceType
		)
		( unannClassType_lf_unannClassOrInterfaceType
		| unannInterfaceType_lf_unannClassOrInterfaceType
		)*
	;

unannClassType_lfno_unannClassOrInterfaceType
	:	Identifiers typeArguments?
	;

unannClassType_lf_unannClassOrInterfaceType
	:	DOT annotation* Identifiers typeArguments?
	;

unannInterfaceType_lfno_unannClassOrInterfaceType
	:	unannClassType_lfno_unannClassOrInterfaceType
	;

unannInterfaceType_lf_unannClassOrInterfaceType
	:	unannClassType_lf_unannClassOrInterfaceType
	;

unannClassType
	:	Identifiers typeArguments?
	|	unannClassOrInterfaceType DOT annotation? Identifiers typeArguments?
	;

unannInterfaceType
	:	unannClassType
	;

unannTypeVariable
	:	Identifiers
	;

unannArrayType
	:	unannPrimitiveType dims
	|	unannClassOrInterfaceType dims
	|	unannTypeVariable dims
	;

methodDeclaration
	:	methodModifier* methodHeader methodBody
	;

methodModifier
	:	annotation
	|	PUBLIC 
	|	PROTECTED 
	|	PRIVATE
	|	ABSTRACT
	|	STATIC
	|	FINAL
	|	SYNCHRONIZED
	|	NATIVE
	|	STRICTFP
	; 

methodHeader
	:	result methodDeclarator THROWS?
	|	typeParameters annotation* result methodDeclarator THROWS?
	;

result
	:	unannType 
	|	VOID
	;

methodDeclarator
	:	Identifiers LPAREN formalParameterList? RPAREN dims?
	;

formalParameterList
	:	receiverParameter 
	|	formalParameters COMMA lastFormalParameter 
	|	lastFormalParameter
	;

formalParameters
	:	formalParameter (COMMA formalParameter)*
	|	receiverParameter (COMMA formalParameter)*
	;

formalParameter
	:	variableModifier* unannType variableDeclaratorId
	;

variableModifier
	: annotation
	| FINAL
	;

lastFormalParameter
	:	variableModifier* unannType annotation* TRPDOT variableDeclaratorId
	|	formalParameter 
	;

receiverParameter
	:	annotation* unannType (Identifiers DOT)? THIS
	;

THROWS
	:	THROWS exceptionTypeList
	;

exceptionTypeList
	:	exceptionType (COMMA exceptionType)*
	;

exceptionType
	:	classType 
	|	typeVariable
	;

methodBody
	:	block
	|	SEMI
	;

instanceInitializer
	:	block
	;

staticInitializer
	:	STATIC block
	;

constructorDeclaration
	:	(constructorModifier)* constructorDeclarator THROWS? constructorBody
	;

constructorModifier
	:	annotation
	|	PUBLIC 
	|	PROTECTED 
	|	PRIVATE
	;

constructorDeclarator
	:	typeParameters? simpleTypeName LPAREN formalParameterList? LPAREN
	;

simpleTypeName
	:	Identifiers
	;

constructorBody
	:	LBRACE explicitConstructorInvocation? blockStatements? LBRACK
	;

explicitConstructorInvocation
	:	typeArguments? THIS LPAREN argumentList? RPAREN SEMI
	|	typeArguments? SUPER LPAREN argumentList? RPAREN SEMI 
	|	expressionName DOT typeArguments? SUPER LPAREN argumentList? RPAREN SEMI
	|	primary DOT typeArguments? SUPER LPAREN argumentList? RPAREN SEMI
	;

enumDeclaration
	:	classModifier* ENUM Identifiers (superinterfaces)? enumBody
	;

enumBody
	:	LBRACE (enumConstantList)? (COMMA)? (enumBodyDeclarations)? RBRACE
	;

enumConstantList
	:	enumConstant (COMMA enumConstant)*
	;

enumConstant
	:	enumConstantModifier* Identifiers (LPAREN argumentList? RPAREN)? classBody?
	;

enumConstantModifier
	:	annotation
	;

enumBodyDeclarations
	:	SEMI classBodyDeclaration*
	;

