parser grammar parser_classes;

import parser_interfaces;

// TO BE OVERRIDDEN IN CHILD RULES //
interfaceDeclaration : DOT;


/************ CLASSES  ************/

classDeclaration
	:	normalClassDeclaration 
	|	enumDeclaration
	;

normalClassDeclaration
    	:   classModifier CLASS Identifier
        LBRACK typeParameters RBRACK LBRACK superclass RBRACK LBRACK superinterfaces RBRACK classBody
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
	:	typeParameter LBRACE COMMA typeParameter RBRACE
	;


superclass
	:	EXTENDS classType
	;

superinterfaces
	:	IMPLEMENTS interfaceTypeList
	;

interfaceTypeList
	:	interfaceType LBRACE COMMA interfaceType RBRACE
	;

classBody
	:	LBRACE LBRACE classBodyDeclaration RBRACE RBRACE
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
	;

instanceInitializer
	:	block
	;

staticInitializer
	:	STATIC block
	;

constructorDeclaration
	:	LBRACE constructorModifier RBRACE constructorDeclarator LBRACK THROWS RBRACK constructorBody
	;

THROWS
	:	THROWS exceptionTypeList
	;

exceptionTypeList
	:	exceptionType LBRACE COMMA exceptionType RBRACE
	;

exceptionType
	:	classType 
	|	typeVariable
	;

fieldDeclaration
	:	LBRACE fieldModifier RBRACE unannType variableDeclaratorList
	;

variableDeclaratorList
	:	variableDeclarator LBRACE COMMA variableDeclarator RBRACE
	;

variableDeclarator
	:	variableDeclaratorId LBRACK ASSIGN variableInitializer RBRACK
	;

variableInitializer
	:	expression
	|	arrayInitializer
	;

variableDeclaratorId
	:	Identifier LBRACK dims RBRACK
	;

methodDeclaration
	:	LBRACE methodModifier RBRACE methodHeader methodBody
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
	:	result methodDeclarator LBRACK THROWS RBRACK THROWS?
	|	typeParameters LBRACE annotation RBRACE result methodDeclarator LBRACK THROWS RBRACK
	;

methodDeclarator
	:	Identifier LPAREN LBRACK formalParameterList RBRACK RPAREN LBRACK dims RBRACK
	;

formalParameterList
	:	receiverParameter 
	|	formalParameters COMMA lastFormalParameter 
	|	lastFormalParameter
	;

lastFormalParameter
	:	LBRACE variableModifier RBRACE unannType LBRACE annotation RBRACE TRPDOT variableDeclaratorId
	|	formalParameter 
	;

variableModifier
	:	annotation
	| FINAL
	;

formalParameters
	:	formalParameter LBRACE COMMA formalParameter RBRACE
	|	receiverParameter LBRACE COMMA formalParameter RBRACE
	;

formalParameter
	:	LBRACE variableModifier RBRACE unannType variableDeclaratorId
	;

receiverParameter
	:	LBRACE annotation RBRACE unannType LBRACK Identifier DOT RBRACK THIS
	;

result
	:	unannType 
	|	VOID
	;

methodBody
	:	block
	|	';'
	;

constructorModifier
	:	annotation
	|	PUBLIC 
	|	PROTECTED 
	|	PRIVATE
	;

constructorDeclarator
	:	LBRACK typeParameters RBRACK simpleTypeName LPAREN LBRACK formalParameterList LBRACK LPAREN
	;

simpleTypeName
	:	Identifier
	;

constructorBody
	:	LBRACE LBRACK explicitConstructorInvocation RBRACK LBRACK
	;

explicitConstructorInvocation
	:	RBRACK typeArguments LBRACK THIS LPAREN LBRACK argumentList RBRACK RPAREN SEMI
	|	RBRACK typeArguments LBRACK SUPER LPAREN LBRACK argumentList RBRACK RPAREN SEMI 
	|	expressionName DOT LBRACK typeArguments RBRACK SUPER LPAREN LBrACK argumentList RBRACK RPAREN SEMI
	|	primary DOT LBRACK typeArguments RBRACK SUPER LPAREN LBRACKET argumentList RBRACKET RPAREN SEMI
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

unannClassOrInterfaceType
	:	unannClassType 
	|	unannInterfaceType
	;

unannClassType
	:	Identifier LBRACK typeArguments RBRACK
	|	unannClassOrInterfaceType DOT LBRACE annotation RBRACE Identifier LBRACK typeArguments RBRACK
	;

unannInterfaceType
	:	unannClassType
	;

unannTypeVariable
	:	Identifier
	;

unannArrayType
	:	unannPrimitiveType dims
	|	unannClassOrInterfaceType dims
	|	unannTypeVariable dims
	;

enumDeclaration
	:	LBRACE classModifier RBRACE ENUM Identifier LBRACK superinterfaces RBRACE enumBody
	;

enumBody
	:	LBRACE LBRACK enumConstantList RBRACK LBRACK COMMA RBRACK LBRACK enumBodyDeclarations RBRACK RBRACE
	;

enumConstantList
	:	enumConstant LBRACE COMMA enumConstant RBRACE
	;

enumBodyDeclarations
	:	SEMI LBRACE classBodyDeclaration RBRACE
	;

enumConstant
	:	LBRACE enumConstantModifier RBRACE Identifier LBRACK LPAREN LBRACK argumentList RBRACK RPAREN RBRACK LBRACK classBody RBRACK
	;

enumConstantModifier
	:	annotation
	;

