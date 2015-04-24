parser grammar parser_expressions;

import parser_statements;

// UNCOMMENT THESE RULES IF COMMENTING IMPORT CHAIN //
//typeName : DOT;
//expressionName : DOT;
//classBody : DOT;
//formalParameterList : DOT;
//block : DOT;



/************ EXPRESSION  ************/

//BUG: the following sets of rules are mutally left-recursive [primary, classInstanceCreationExpression, fieldAccess, methodInvocation, methodReference, primaryNoNewArray, arrayAccess] and 
//		[primaryNoNewArray, arrayAccess, primary, classInstanceCreationExpression, fieldAccess, methodInvocation, methodReference] and 
//		[postfixExpression, postIncrementExpression, postDecrementExpression]
//
// worked around by commenting primary, arrayAccess, and postfixExpression
//


primary
	:	( primaryNoNewArray | arrayCreationExpression ) (primaryNoNewArray)*
	;

primaryNoNewArray
	: literal
	| classLiteral
	| THIS
	| typeName DOT THIS
	| LPAREN expression RPAREN
	| classInstanceCreationExpression
	| fieldAccess
	| arrayAccess
	| methodInvocation
	| methodReference
	;

classLiteral
	: typeName (LBRACK RBRACK)* DOT CLASS
	| numericType (LBRACK RBRACK)* DOT CLASS
	| BOOLEAN (LBRACK RBRACK) DOT CLASS
	| VOID DOT CLASS
	;

classInstanceCreationExpression
	: unqualifiedClassInstanceCreationExpression
	| expressionName DOT unqualifiedClassInstanceCreationExpression
	//| primary DOT unqualifiedClassInstanceCreationExpression // <-- not sure if needed
	;

unqualifiedClassInstanceCreationExpression
	: NEW typeArguments? classOrInterfaceTypeToInstantiate LPAREN argumentList? RPAREN classBody?
	;

classOrInterfaceTypeToInstantiate
	: annotation* Identifiers (DOT annotation* Identifiers)* typeArgumentsOrDiamond?
	;

typeArgumentsOrDiamond
	: typeArguments
	| LT GT
	;

fieldAccess
	: SUPER DOT Identifiers
	| typeName DOT SUPER DOT Identifiers
	//| primary DOT Identifiers // <-- not sure if needed
	;

arrayAccess
	: expressionName LBRACK expression RBRACK
	//| primaryNoNewArray LBRACK expression RBRACK // <-- not sure if needed
	;

methodInvocation
	: methodName LPAREN argumentList? RPAREN
	| typeName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| expressionName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	//| primary DOT typeArguments? Identifiers LPAREN argumentList? RPAREN // <-- not sure if needed
	| SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| typeName DOT SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	;

argumentList
	: expression (COMMA expression)*
	;

methodReference
	: expressionName DBLCOLON typeArguments? Identifiers
	| referenceType DBLCOLON typeArguments? Identifiers
	//| primary DBLCOLON typeArguments? Identifiers // <-- not sure if needed
	| SUPER DBLCOLON typeArguments? Identifiers
	| typeName DOT SUPER DBLCOLON typeArguments? Identifiers
	| classType DBLCOLON typeArguments? NEW
	| arrayType DBLCOLON NEW
	;

arrayCreationExpression
	: NEW primitiveType dimExprs dims?
	| NEW classOrInterfaceType dimExprs dims?
	| NEW primitiveType dims arrayInitializer
	| NEW classOrInterfaceType dims arrayInitializer
	;

dimExprs
	: dimExpr+
	;

dimExpr
	: annotation* LBRACK expression RBRACK
	;

expression
	: lambdaExpression
	| assignmentExpression
	; 

lambdaExpression
	: lambdaParameters LAMBDA_ASSIGN lambdaBody
	;

lambdaParameters
	: Identifiers
	| LPAREN formalParameterList? RPAREN
	| LPAREN inferredFormalParameterList RPAREN
	;

inferredFormalParameterList
	: Identifiers (COMMA Identifiers)*
	;

lambdaBody
	: expression
	| block
	;

assignmentExpression
	: conditionalExpression
	| assignment
	;

assignment
	: leftHandSide assignmentOperator expression
	;

leftHandSide
	: expressionName
	| fieldAccess
	| arrayAccess
	;

assignmentOperator
	: ASSIGN  
	| MUL_ASSIGN  
	| DIV_ASSIGN  
	| MOD_ASSIGN  
	| ADD_ASSIGN 
	| SUB_ASSIGN 
	| LSHIFT_ASSIGN 
	| RSHIFT_ASSIGN  
	| URSHIFT_ASSIGN  
	| AND_ASSIGN  
	| XOR_ASSIGN  
	| OR_ASSIGN
	;

conditionalExpression
	: oderOfOperations1
	| oderOfOperations1 QUESTION expression COLON conditionalExpression
	| oderOfOperations1 QUESTION expression COLON lambdaExpression
	;

oderOfOperations1
	: oderOfOperations2 (OR oderOfOperations1)?
	;

oderOfOperations2
	: oderOfOperations3 (AND oderOfOperations1)?
	;

oderOfOperations3
	: oderOfOperations4 (BITOR oderOfOperations1)?
	;

oderOfOperations4
	: oderOfOperations5 (CARET oderOfOperations1)?
	;

oderOfOperations5
	: oderOfOperations6 (BITAND oderOfOperations1)?
	;

oderOfOperations6
	: oderOfOperations7 (EQUAL oderOfOperations1)?
	| oderOfOperations7 (NOTEQUAL oderOfOperations1)?
	;
	
oderOfOperations7
	: oderOfOperations8 (LT oderOfOperations1)?
	| oderOfOperations8 (GT oderOfOperations1)?
	| oderOfOperations8 (LE oderOfOperations1)?
	| oderOfOperations8 (GE oderOfOperations1)?
	| oderOfOperations8 ('instanceof' oderOfOperations1)?
	;

oderOfOperations8
	: oderOfOperations9 (LEFT_SIGNED oderOfOperations1)?
	| oderOfOperations9 (RIGHT_SIGNED oderOfOperations1)?
	| oderOfOperations9 (RIGHT_UNSIGNED oderOfOperations1)?
	;

oderOfOperations9
	: oderOfOperations10 (ADD oderOfOperations1)?
	| oderOfOperations10 (SUB oderOfOperations1)?
	;

oderOfOperations10
	: unaryExpression (MUL oderOfOperations1)?
	| unaryExpression (DIV oderOfOperations1)?
	| unaryExpression (MOD oderOfOperations1)?
	;


unaryExpression
	: preIncrementExpression
	| preDecrementExpression
	| ADD unaryExpression
	| SUB unaryExpression
	| unaryExpressionNotPlusMinus
	;

preIncrementExpression
	: INC unaryExpression
	;

preDecrementExpression
	: DEC unaryExpression
	;

unaryExpressionNotPlusMinus
	: postfixExpression
	| TILDE unaryExpression
	| NOT unaryExpression
	| castExpression
	;

postfixExpression
	: (expressionName | primary) INC
	| (expressionName | primary) DEC
	;

postIncrementExpression
	: postfixExpression
	;

postDecrementExpression
	: postfixExpression
	;


castExpression
	: LPAREN primitiveType RPAREN unaryExpression
	| LPAREN referenceType additionalBound* RPAREN unaryExpressionNotPlusMinus
	| LPAREN referenceType additionalBound* RPAREN lambdaExpression
	;

constantExpression
	: expression
	;

