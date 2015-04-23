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




//BUG: mutual left-recursion -- see top of file
primary : DOT;
//primary
//	: primaryNoNewArray
//	| arrayCreationExpression
//	;

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

//BUG: rule 'classLiteral' contains a closure with at least one alternative that can match an empty string
classLiteral : DOT;
//classLiteral
//	: typeName (( )?)* DOT CLASS
//	| numericType (( )?)* DOT CLASS
//	|  BOOLEAN (( )?) DOT CLASS
//	| VOID DOT CLASS
//	;

classInstanceCreationExpression
	: unqualifiedClassInstanceCreationExpression
	| expressionName DOT unqualifiedClassInstanceCreationExpression
	| primary DOT unqualifiedClassInstanceCreationExpression
	;

unqualifiedClassInstanceCreationExpression
	: NEW typeArguments? classOrInterfaceTypeToInstantiate LBRACE argumentList? RBRACE classBody?
	;

classOrInterfaceTypeToInstantiate
	: annotation* Identifiers (DOT annotation* Identifiers)* typeArgumentsOrDiamond?
	;

typeArgumentsOrDiamond
	: typeArguments
	| LT GT
	;

fieldAccess
	: primary DOT Identifiers
	| SUPER DOT Identifiers
	| typeName DOT SUPER DOT Identifiers
	;


//BUG: mutual left-recursion -- see top of file
arrayAccess : DOT;
//arrayAccess
//	: expressionName expression?
//	| primaryNoNewArray expression?
//	;

methodInvocation
	: methodName LPAREN argumentList? RPAREN
	| typeName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| expressionName DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| primary DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	| typeName DOT SUPER DOT typeArguments? Identifiers LPAREN argumentList? RPAREN
	;

argumentList
	: expression (COMMA expression)*
	;

methodReference
	: expressionName DBLCOLON typeArguments? Identifiers
	| referenceType DBLCOLON typeArguments? Identifiers
	| primary DBLCOLON typeArguments? Identifiers
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

//BUG: rule 'dimExprs' contains a closure with at least one alternative that can match an empty string
dimExprs : DOT;
//dimExprs
//	: dimExpr dimExpr*
//	;

dimExpr
	: annotation* expression?
	;

expression
	: lambdaExpression
	| assignmentExpression
	; 

lambdaExpression
	: lambdaParameters COMMENT lambdaBody
	;

lambdaParameters
	: Identifiers
	| LPAREN formalParameterList? RPAREN
	| LPAREN inferredFormalParameterList RPAREN
	;

inferredFormalParameterList
	: Identifiers (COMMA Identifiers)?
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
	: conditionalOrExpression
	| conditionalOrExpression  QUESTION expression COLON conditionalExpression
	| conditionalOrExpression  QUESTION expression COLON lambdaExpression
	;

//BUG: causes unknown build error. Each of the following calls itself in a forever loop
conditionalOrExpression : DOT;
conditionalAndExpression : DOT;
inclusiveOrExpression : DOT;
exclusiveOrExpression : DOT;
andExpression : DOT;
equalityExpression : DOT;
relationalExpression : DOT;
shiftExpression : DOT;
additiveExpression : DOT;
multiplicativeExpression : DOT;

/*conditionalOrExpression
	: conditionalAndExpression+
	| conditionalOrExpression OR conditionalAndExpression
	;

conditionalAndExpression
	: inclusiveOrExpression+
	| conditionalAndExpression+ AND inclusiveOrExpression
	;

inclusiveOrExpression
	: exclusiveOrExpression+
	| inclusiveOrExpression+ BITOR exclusiveOrExpression
	;

exclusiveOrExpression
	: andExpression+
	| exclusiveOrExpression+ CARET andExpression
	;

andExpression
	: equalityExpression+
	| andExpression BITAND equalityExpression
	;

equalityExpression
	: relationalExpression+
	| equalityExpression EQUAL relationalExpression
	| equalityExpression NOTEQUAL relationalExpression
	;

relationalExpression
	: shiftExpression+
	| relationalExpression LT shiftExpression
	| relationalExpression GT shiftExpression
	| relationalExpression LE shiftExpression
	| relationalExpression GE shiftExpression
	| relationalExpression 'instanceof' referenceType
	;

shiftExpression
	: additiveExpression+
	| shiftExpression LEFT_SIGNED additiveExpression
	| shiftExpression RIGHT_SIGNED additiveExpression
	| shiftExpression RIGHT_UNSIGNED additiveExpression
	;

additiveExpression
	: multiplicativeExpression+
	| additiveExpression+ ADD multiplicativeExpression
	| additiveExpression+ SUB multiplicativeExpression
	;

multiplicativeExpression
	: unaryExpression+
	| multiplicativeExpression+ MUL unaryExpression
	| multiplicativeExpression+ DIV unaryExpression
	| multiplicativeExpression+ MOD unaryExpression
	; */

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

//BUG: mutual left-recursion -- see top of file
postfixExpression : DOT;
//postfixExpression
//	: primary
//	| expressionName
//	| postIncrementExpression
//	| postDecrementExpression
//	;

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

