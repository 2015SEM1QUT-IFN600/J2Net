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
	: (annotation)* Identifier ('.' (annotation)* Identifier)* (typeArgumentsOrDiamond)?
	;

typeArgumentsOrDiamond
	: typeArguments
	| '<>'
	;

fieldAccess
	: primary '.' Identifier
	| 'super' '.' Identifier
	| typeName '.' 'super' '.' Identifier
	;

arrayAccess
	: expressionName ( expression )?
	| primaryNoNewArray ( expression )?
	;

methodInvocation
	: MethodName '(' (argumentList)? ')'
	| typeName '.' (typeArguments)? Identifier '(' (argumentList)? ')'
	| expressionName '.' (typeArguments)? Identifier '(' (argumentList)? ')'
	| primary '.' (typeArguments)? Identifier '(' (argumentList)? ')'
	| 'super' '.' (typeArguments)? Identifier '(' (argumentList)? ')'
	| typeName '.' 'super' '.' (typeArguments)? Identifier '(' (argumentList)? ')'
	;

argumentList
	: expression (',' expression)*
	;

methodReference
	: expressionName '::' (typeArguments)? Identifier
	| referenceType '::' (typeArguments)? Identifier
	| primary '::' (typeArguments)? Identifier
	| 'super' '::' (typeArguments)? Identifier
	| typeName '.' 'super' '::' (typeArguments)? Identifier
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
	: Identifier
	| '(' (formalParameterList)? ')'
	| '(' inferredFormalParameterList')'
	;

inferredFormalParameterList
	: Identifier (',' Identifier)?
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
	| additiveExpression '-' multiplicativeExpression
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
	:	EOF
	;