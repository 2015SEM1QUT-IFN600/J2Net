parser grammar parser_expressions;

primary
	: primaryNoNewArray
	| arrayCreationExpression
	;

primaryNoNewArray
	: literal
	| classLiteral
	| 'this'
	| typeName '.' 'this'
	| '(' Expression ')'
	| classInstanceCreationExpression
	| fieldAccess
	| arrayAccess
	| methodInvocation
	| methodReference
	;

classLiteral
	: typeName '('( )?')' '.' 'class'
	| numericType '('( )?')' '.' 'class'
	| 'boolean' '('( )?')' '.' 'class'
	| 'void' '.' 'class'
	;

classInstanceCreationExpression
	: unqualifiedClassInstanceCreationExpression
	| expressionName '.' unqualifiedClassInstanceCreationExpression
	| primary '.' unqualifiedClassInstanceCreationExpression
	;

unqualifiedClassInstanceCreationExpression
	: 'new' (typeArguments)? classOrInterfaceTypeToInstantiate ( (argumentList)? )? (classBody)?
	;

classOrInterfaceTypeToInstantiate
	: (annotation)* identifier ('.' (annotation)* identifier)* (typeArgumentsOrDiamond)?
	;

typeArgumentsOrDiamond
	: typeArguments
	| '<>'
	;

fieldAccess
	: primary '.' identifier
	| 'super' '.' identifier
	| typeName '.' 'super' '.' identifier
	;

arrayAccess
	: expressionName ( expression )?
	| primaryNoNewArray ( expression )?
	;

methodInvocation
	: MethodName '(' (argumentList)? ')'
	| typeName '.' (typeArguments)? identifier '(' (argumentList)? ')'
	| expressionName '.' (typeArguments)? identifier '(' (argumentList)? ')'
	| primary '.' (typeArguments)? identifier '(' (argumentList)? ')'
	| 'super' '.' (typeArguments)? identifier '(' (argumentList)? ')'
	| typeName '.' 'super' '.' (typeArguments)? identifier '(' (argumentList)? ')'
	;

argumentList
	: expression (',' expression)*
	;

methodReference
	: expressionName '::' (typeArguments)? identifier
	| referenceType '::' (typeArguments)? identifier
	| primary '::' (typeArguments)? identifier
	| 'super' '::' (typeArguments)? identifier
	| typeName '.' 'super' '::' (typeArguments)? identifier
	| classType '::' (typeArguments)? 'new'
	| arrayType '::' 'new'
	;

arrayCreationExpression
	: 'new' primitiveType dimExprs ('dims')?
	| 'new' classOrInterfaceType dimExprs ('dims')?
	| 'new' primitiveType 'dims' arrayInitializer
	| 'new' classOrInterfaceType 'dims' arrayInitializer
	;

dimExprs
	: dimExpr (dimExpr)*
	;

dimExpr
	: (annotation)* ( expression )?
	;

expression
	: lambdaExpression
	| assignmentExpression
	; 

lambdaExpression
	: lambdaParameters '->' lambdaBody
	;

lambdaParameters
	: identifier
	| '(' (formalParameterList)? ')'
	| '(' inferredFormalParameterList')'
	;

inferredFormalParameterList
	: identifier (',' identifier)?
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
	: '='  
	| '*='  
	| '/='  
	| '%='  
	| '+=' 
	| '-=' 
	| '<<=' 
	| '>>='  
	| '>>>='  
	| '&='  
	| '^='  
	| '|='
	;

conditionalExpression
	: conditionalOrExpression
	| conditionalOrExpression '?' expression ':' conditionalExpression
	| conditionalOrExpression '?' expression ':' lambdaExpression
	;

conditionalOrExpression
	: conditionalAndExpression
	| conditionalOrExpression '||' conditionalAndExpression
	;

conditionalAndExpression
	: inclusiveOrExpression
	| conditionalAndExpression '&&' inclusiveOrExpression
	;

inclusiveOrExpression
	: exclusiveOrExpression
	| inclusiveOrExpression '|' exclusiveOrExpression
	;

exclusiveOrExpression
	: andExpression
	| exclusiveOrExpression '^' andExpression
	;

andExpression
	: equalityExpression
	| andExpression '&' equalityExpression
	;

equalityExpression
	: relationalExpression
	| equalityExpression '==' relationalExpression
	| equalityExpression '!=' relationalExpression
	;

relationalExpression
	: shiftExpression
	| relationalExpression '<' shiftExpression
	| relationalExpression '>' shiftExpression
	| relationalExpression '<=' shiftExpression
	| relationalExpression '>=' shiftExpression
	| relationalExpression 'instanceof' referenceType
	;

shiftExpression
	: additiveExpression
	| shiftExpression '<<' additiveExpression
	| shiftExpression '>>' additiveExpression
	| shiftExpression '>>>' additiveExpression
	;

additiveExpression
	: multiplicativeExpression
	| additiveExpression '+' multiplicativeExpression
	| mdditiveExpression '-' multiplicativeExpression
	;

multiplicativeExpression
	: unaryExpression
	| multiplicativeExpression '*' unaryExpression
	| multiplicativeExpression '/' unaryExpression
	| multiplicativeExpression '%' unaryExpression
	;

unaryExpression
	: preIncrementExpression
	| preDecrementExpression
	| '+' unaryExpression
	| '-' unaryExpression
	| unaryExpressionNotPlusMinus
	;

preIncrementExpression
	: '++' unaryExpression
	;

preDecrementExpression
	: '--' unaryExpression
	;

unaryExpressionNotPlusMinus
	: postfixExpression
	| '~' unaryExpression
	| '!' unaryExpression
	| castExpression
	;

postfixExpression
	: primary
	| expressionName
	| postIncrementExpression
	| postDecrementExpression
	;

postIncrementExpression
	: postfixExpression '++'
	;

postDecrementExpression
	: postfixExpression '--'
	;

castExpression
	: '(' primitiveType ')' unaryExpression
	| '(' referenceType (additionalBound)* ')' unaryExpressionNotPlusMinus
	| '(' referenceType (additionalBound)* ')' lambdaExpression
	;

constantExpression
	: expression
	;

compileUnit
	: EOF
	;