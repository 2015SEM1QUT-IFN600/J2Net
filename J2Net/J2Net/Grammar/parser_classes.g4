parser grammar parser_classes;

compileUnit
	:	EOF
	;

classDeclaration
	:	normalClassDeclaration 
	|	enumDeclaration
	;

normalClassDeclaration
    :   classModifier  CLASS //identifier // Depends on identifier being finished in chapter 3.8 (Lexical Structure)
        LBRACK typeParameters RBRACK LBRACK superclass RBRACK LBRACK superinterfaces RBRACK classBody
    ;

classModifier
    :    //annotation // Depends on annotation being finished in chapter 9.7 (Interfaces)
    //|    
		 PUBLIC
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
	:	//typeParameter LBRACE COMMA typeParameter RBRACE // Depends on typeParameter being finished in chapter 4.4 (Types, Values, and Variables)
	;

superclass
	:	//EXTENDS classType // Depends on classType being finished in chapter 4.3 (Types, Values, and Variables)
	;

superinterfaces
	:	IMPLEMENTS interfaceTypeList
	;

interfaceTypeList
	:	//interfaceType LBRACE COMMA interfaceType RBRACE // Depends on interfaceType being finished in chapter 4.3 (Types, Values, and Variables)
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
	//|	interfaceDeclaration // Depends on 9.1 (Interfaces)
	;

instanceInitializer
	:	//block // Depends on 14.2 (Blocks and Statements)
	;

staticInitializer
	:	//STATIC block // Depends on 14.2 (Blocks and Statements)
	;

constructorDeclaration
	:	LBRACE constructorModifier RBRACE constructorDeclarator LBRACK THROWS RBRACK constructorBody // throws?
	;

//throws
//	:	THROWS exceptionTypeList
//	;

//exceptionTypeList
//	:	exceptionType LBRACE COMMA exceptionType RBRACE
//	;

//exceptionType
//	:	classType 
//	|	typeVariable
//	;

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
	:	//expression // Depends on 15.2 (Expressions)
	//|	
		//arrayInitializer // Depends on 10.6 (Arrays)
	;

variableDeclaratorId
	:	//identifier LBRACK dims RBRACK // Depends on 3.8 (Lexical Structure)
	;

methodDeclaration
	:	LBRACE methodModifier RBRACE methodHeader methodBody
	;

methodModifier
	:	//annotation // Depends on 9.7
	//|	
		PUBLIC 
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
	:	result methodDeclarator LBRACK THROWS RBRACK // throws?
	//|	typeParameters LBRACE annotation RBRACE result methodDeclarator LBRACK THROWS RBRACK // Depends on 9.7
	;

methodDeclarator
	:	//identifier LPAREN LBRACK formalParameterList RBRACK RPAREN LBRACK dims RBRACK // Depends on 4.3
	;

formalParameterList
	:	receiverParameter 
	|	formalParameters COMMA lastFormalParameter 
	|	lastFormalParameter
	;

lastFormalParameter
	:	//LBRACE variableModifier RBRACE unannType LBRACE annotation RBRACE TRPDOT variableDeclaratorId // Depends on 9.7
	|	formalParameter 
	;

variableModifier
	:	//annotation // Depends on 9.7
	//|	
		FINAL
	;

formalParameters
	:	formalParameter LBRACE COMMA formalParameter RBRACE
	|	receiverParameter LBRACE COMMA formalParameter RBRACE
	;

formalParameter
	:	LBRACE variableModifier RBRACE unannType variableDeclaratorId
	;

receiverParameter
	:	//LBRACE annotation RBRACE unannType LBRACK identifier DOT RBRACK THIS // Depends on 9.7 and 3.8
	;

result
	:	unannType 
	|	VOID
	;

methodBody
	:	//block // Depends on 14.2
	;

constructorModifier
	:	//annotation // Depends on  9.7 (Interfaces)
	//|	
		PUBLIC 
	|	PROTECTED 
	|	PRIVATE
	;

constructorDeclarator
	:	LBRACK typeParameters RBRACK simpleTypeName LPAREN LBRACK formalParameterList LBRACK LPAREN
	;

simpleTypeName
	:	//identifier // Depends on 3.8
	;

constructorBody
	:	LBRACE LBRACK explicitConstructorInvocation RBRACK LBRACK //blockStatements RBRACK RBRACE // Depends on 14.2
	;

explicitConstructorInvocation
	:	//RBRACK typeArguments LBRACK THIS LPAREN LBRACK argumentList RBRACK RPAREN SEMI // Depends on 4.5.1 and 15.12
	//|	RBRACK typeArguments LBRACK SUPER LPAREN LBRACK argumentList RBRACK RPAREN SEMI 
	//|	expressionName DOT LBRACK typeArguments RBRACK SUPER LPAREN LBrACK argumentList RBRACK RPAREN SEMI // Depends on 6.5
	//|	primary DOT LBRACK typeArguments RBRACK SUPER LPAREN LBRACKET argumentList RBRACKET RPAREN SEMI // Depends on 15.8
	;

fieldModifier
	:	//annotation // Depends on  9.7 (Interfaces)
	//|	
		PUBLIC 
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
	:	//numericType // Depends on 4.2
	//|	
		BOOLEAN
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
	:	//identifier LBRACK typeArguments RBRACK // Depends in 3.8
	//|	unannClassOrInterfaceType DOT LBRACE annotation RBRACE identifier LBRACK typeArguments RBRACK // Dependencies...
	;

unannInterfaceType
	:	unannClassType
	;

unannTypeVariable
	:	//identifier // Depends on 3.8
	;

unannArrayType
	:	//unannPrimitiveType dims // Depends on 4.3
	//|	unannClassOrInterfaceType dims // Depends on 4.3
	//|	unannTypeVariable dims // Depends on 4.3
	;

enumDeclaration
	:	LBRACE classModifier RBRACE ENUM //identifier LBRACK superinterfaces RBRACE enumBody // Depends on 3.8
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
	:	//LBRACE enumConstantModifier RBRACE identifier LBRACK LPAREN LBRACK argumentList RBRACK RPAREN RBRACK LBRACK classBody RBRACK // Depends on 9.7 and 15.12
	;

enumConstantModifier
	:	//annotation // Depends on 9.7
	;