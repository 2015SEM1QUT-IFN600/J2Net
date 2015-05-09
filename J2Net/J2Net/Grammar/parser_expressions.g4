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
	:	( primaryNoNewArray_no_primary | arrayCreationExpression ) (primaryNoNewArray_primary)*
	//|	( primaryNoNewArray | arrayCreationExpression ) (primaryNoNewArray)*
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

primaryNoNewArray_arrayAccess
	:
	;

primaryNoNewArray_no_arrayAccess
	:	literal
	|	classLiteral
	|	THIS
	|	typeName DOT THIS
	|	LPAREN expression RPAREN
	|	classInstanceCreationExpression
	|	fieldAccess
	|	methodInvocation
	|	methodReference
	;

primaryNoNewArray_primary
	: classInstanceCreationExpression_primary
	| fieldAccess_primary
	| arrayAccess_primary
	| methodInvocation_primary
	| methodReference_primary
	;

primaryNoNewArray_primary_arrayAccess_primary
	:
	;

primaryNoNewArray_primary_no_arrayAccess_primary 
	: classInstanceCreationExpression_primary
	| fieldAccess_primary
	| methodInvocation_primary
	| methodReference_primary
	;

primaryNoNewArray_no_primary
	:	literal
	|	classLiteral
	|	THIS
	|	typeName DOT THIS
	|	RPAREN expression LPAREN
	|	classInstanceCreationExpression_no_primary
	|	fieldAccess_no_primary
	|	arrayAccess_no_primary
	|	methodInvocation_no_primary
	|	methodReference_no_primary
	;

primaryNoNewArray_no_primary_arrayAccess_no_primary
	:
	;

primaryNoNewArray_no_primary_no_arrayAccess_no_primary
	:	literal
	|	classLiteral
	|	THIS
	|	typeName DOT THIS
	|	LPAREN expression RPAREN
	|	classInstanceCreationExpression_no_primary
	|	fieldAccess_no_primary
	|	methodInvocation_no_primary
	|	methodReference_no_primary
	;

classLiteral
	: typeName (LBRACK RBRACK)* DOT CLASS
	| numericType (LBRACK RBRACK)* DOT CLASS
	| BOOLEAN (LBRACK RBRACK)* DOT CLASS
	| VOID DOT CLASS
	;

classInstanceCreationExpression
	//: NEW typeArguments? annotation* Identifiers (DOT annotation* Identifiers)* typeArgumentsOrDiamond? LPAREN argumentList? RPAREN classBody?
	//| expressionName DOT NEW typeArguments? annotation* Identifiers typeArgumentsOrDiamond? LPAREN argumentList? RPAREN classBody?
	//| primary DOT typeArguments? annotation* Identifiers typeArgumentsOrDiamond? LPAREN argumentList? RPAREN classBody?
	: unqualifiedClassInstanceCreationExpression
	| expressionName DOT unqualifiedClassInstanceCreationExpression
	| primary DOT unqualifiedClassInstanceCreationExpression // <-- not sure if needed
	;

classInstanceCreationExpression_primary
	//:	DOT NEW typeArguments? annotation* Identifiers typeArgumentsOrDiamond? LPAREN argumentList? RPAREN classBody?
	: DOT unqualifiedClassInstanceCreationExpression
	;

classInstanceCreationExpression_no_primary
	//:	NEW typeArguments? annotation* Identifiers (DOT annotation* Identifiers)* typeArgumentsOrDiamond? LPAREN argumentList? RPAREN classBody?
	//|	expressionName DOT NEW typeArguments? annotation* Identifiers typeArgumentsOrDiamond? LPAREN argumentList? RPAREN classBody?
	: unqualifiedClassInstanceCreationExpression
	| expressionName DOT unqualifiedClassInstanceCreationExpression
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
	| primary DOT Identifiers // <-- not sure if needed
	;

fieldAccess_primary
	:	DOT Identifiers
	;

fieldAccess_no_primary
	:	SUPER DOT Identifiers
	|	typeName DOT SUPER DOT Identifiers
	;

arrayAccess
	: ( expressionName LBRACK expression RBRACK
	  | primaryNoNewArray_no_arrayAccess LBRACK expression RBRACK
	  )
	  ( primaryNoNewArray_arrayAccess LBRACK expression RBRACK
	  )*
	//: expressionName LBRACK expression RBRACK
	//| primaryNoNewArray LBRACK expression RBRACK // <-- not sure if needed
	;

arrayAccess_primary
	: ( primaryNoNewArray_primary_no_arrayAccess_primary LBRACK expression RBRACK
	  )
	  ( primaryNoNewArray_primary_arrayAccess_primary LBRACK expression RBRACK
	  )
	;

arrayAccess_no_primary
	: ( expressionName LBRACK expression RBRACK
	  | primaryNoNewArray_no_primary_no_arrayAccess_no_primary LBRACK expression RBRACK
	  )
	  ( primaryNoNewArray_no_primary_arrayAccess_no_primary LBRACK expression RBRACK
	  )*
	;

methodInvocation
	: methodName LPAREN argumentList? RPAREN
	| typeName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| expressionName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| primary DOT typeArguments? Identifiers LPAREN argumentList? RPAREN // <-- not sure if needed
	| SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| typeName DOT SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	;

methodInvocation_primary
	:	DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	;

methodInvocation_no_primary
	:	methodName LPAREN argumentList? RPAREN
	|	typeName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	|	expressionName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	|	SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	|	typeName DOT SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	;

argumentList
	: expression (COMMA expression)*
	;

methodReference
	: expressionName DBLCOLON typeArguments? Identifiers
	| referenceType DBLCOLON typeArguments? Identifiers
	| primary DBLCOLON typeArguments? Identifiers // <-- not sure if needed
	| SUPER DBLCOLON typeArguments? Identifiers
	| typeName DOT SUPER DBLCOLON typeArguments? Identifiers
	| classType DBLCOLON typeArguments? NEW
	| arrayType DBLCOLON NEW
	;

methodReference_primary
	: DBLCOLON typeArguments? Identifiers
	;

methodReference_no_primary
	: expressionName DBLCOLON typeArguments? Identifiers
	| referenceType DBLCOLON typeArguments? Identifiers
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
	: oderOfOperations3 (AND oderOfOperations2)?
	;

oderOfOperations3
	: oderOfOperations4 (BITOR oderOfOperations3)?
	;

oderOfOperations4
	: oderOfOperations5 (CARET oderOfOperations4)?
	;

oderOfOperations5
	: oderOfOperations6 (BITAND oderOfOperations5)?
	;

oderOfOperations6
	: oderOfOperations7 (EQUAL oderOfOperations6)?
	| oderOfOperations7 (NOTEQUAL oderOfOperations6)?
	;
	
oderOfOperations7
	: oderOfOperations8 (LT oderOfOperations7)?
	| oderOfOperations8 (GT oderOfOperations7)?
	| oderOfOperations8 (LE oderOfOperations7)?
	| oderOfOperations8 (GE oderOfOperations7)?
	| oderOfOperations8 (INSTANCEOF oderOfOperations7)?
	;

oderOfOperations8
	: oderOfOperations9 (LEFT_SIGNED oderOfOperations8)?
	| oderOfOperations9 (RIGHT_SIGNED oderOfOperations8)?
	| oderOfOperations9 (RIGHT_UNSIGNED oderOfOperations8)?
	;

oderOfOperations9
	: oderOfOperations10 (ADD oderOfOperations9)?
	| oderOfOperations10 (SUB oderOfOperations9)?
	;

oderOfOperations10
	: unaryExpression (MUL oderOfOperations10)?
	| unaryExpression (DIV oderOfOperations10)?
	| unaryExpression (MOD oderOfOperations10)?
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
	: (expressionName | primary) (postIncrementExpression | postDecrementExpression)*
	;

postIncrementExpression
	: postfixExpression INC
	;

postDecrementExpression
	: postfixExpression DEC
	;


castExpression
	: LPAREN primitiveType RPAREN unaryExpression
	| LPAREN referenceType additionalBound* RPAREN unaryExpressionNotPlusMinus
	| LPAREN referenceType additionalBound* RPAREN lambdaExpression
	;

constantExpression
	: expression
	;

