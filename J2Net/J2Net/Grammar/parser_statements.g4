parser grammar parser_statements;



block
	: '{' blockStatements? '}'
	;

blockStatements
	: blockStatement '{'blockStatement'}'
	;

blockStatement
	: localVariableDeclarationStatement
	| classDeclaration	//waiting for this to be construct
	| statement
	;

localVariableDeclarationStatement
	: localVariableDeclaration ';'
	;

localVariableDeclaration
	: (variableModifier)* unannType variableDeclaratorList
	; //waiting for this to be construct

statement
	: statementWithoutTrailingSubstatement
	| labeledStatement
	| ifThenStatement
	| ifThenElseStatement
	| whileStatement
	| forStatement
	;

statementNoShortIf
	: statementWithoutTrailingSubstatement
	| labeledStatementNoShortIf
	| ifThenElseStatementNoShortIf
	| whileStatementNoShortIf
	| forStatementNoShortIf
	;

statementWithoutTrailingSubstatement
	: block
	| emptyStatement
	| expressionStatement
	| assertStatement
	| doStatement
	| breakStatement
	| continueStatement
	| returnStatement
	| synchronizedStatement
	| throwStatement
	| tryStatement
	;

emptyStatement
	:
	;

labeledStatement
	: Identifier ':' statement
	;

labeledStatementNoShortIf
	: Identifier ':' statementNoShortIf
	;

expressionStatement
	: statementExpression ';'
	;

statementExpression
	: assignment						//waiting for this to be construct
	| preIncrementExpression			//waiting for this to be construct
	| preDecrementExpression			//waiting for this to be construct
	| postIncrementExpression			//waiting for this to be construct
	| postDecrementExpression			//waiting for this to be construct
	| methodInvocation				//waiting for this to be construct
	| classInstanceCreationExpression	//waiting for this to be construct
	;

ifThenStatement
	: 'if' '(' expression ')' statement
	;	//waiting for this to be construct

ifThenElseStatement
	: 'if' '(' expression ')' statement 'else' statement
	;	//waiting for this to be construct

ifThenElseStatementNoShortIf
	: 'if' '(' expression ')' statementNoShortIf 'else' statementNoShortIf
	;	//waiting for this to be construct

assertStatement
	: 'assert' expression (':' expression)? ';'
	;	//waiting for this to be construct

switchStatement
	: 'switch' '(' expression ')' switchBlock
	;	//waiting for this to be construct

switchBlock
	: '{' (switchBlockStatementGroup)* (switchLabel)* '}'
	;

switchBlockStatementGroup
	: switchLabels blockStatements
	;

switchLabels
	: switchLabel (switchLabel)*
	;

switchLabel
	: 'case' constantExpression ':'
	| 'case' eNumConstantname ':'
	| 'default' ':'
	;

eNumConstantname
	: Identifiers
	;

whileStatement
	: 'while' '(' expression ')' statement
	;	//waiting for this to be construct

whileStatementNoShortIf
	: 'while' '(' expression ')' statementNoShortIf
	;	//waiting for this to be construct

doStatement
	: 'do' statement 'while' '(' expression ')' ';'
	;	//waiting for this to be construct

forStatement
	: basicForStatement
	| enhancedForStatement
	;

forStatementNoShortIf
	: basicForStatementNoShortIf
	| enhancedForStatementNoShortIf
	;

basicForStatement
	: 'for' '(' forInit? ';' expression? ';' forUpdate? ')' statement
	;	//waiting for this to be construct

basicForStatementNoShortIf
	: 'for' '(' forInit? ';' expression? ';' forUpdate? ')' statementNoShortIf
	;	//waiting for this to be construct

forInit
	: statementExpressionList
	| localVariableDeclaration
	;

forUpdate
	: statementExpressionList
	;

statementExpressionList
	: statementExpression (',' statementExpression)*
	;

enhancedForStatement
	:'for' '(' (variableModifier)* unannType variableDeclaratorId ':' expression ')' statement
	;	//waiting for this to be construct

enhancedForStatementNoShortIf
	:'for' '('( variableModifier)* unannType variableDeclaratorId ':' expression ')' statementNoShortIf
	;	//waiting for this to be construct

breakStatement
	: 'break' Identifiers? ';'
	;

continueStatement
	: 'continue' Identifiers? ';'
	;

returnStatement
	: 'return' expression? ';'
	;	//waiting for this to be construct

throwStatement
	: 'throw' expression ';'
	;	//waiting for this to be construct

synchronizedStatement
	: 'synchronized' '(' expression ')' block
	;	//waiting for this to be construct

tryStatement
	: 'try' block catches
	| 'try' block (catches)? finallyBlock
	| tryWithResourcesStatement
	;

catches
	: catchClause (catchClause)*
	;

catchClause
	: 'catch' '(' catchFormalParameter ')' block
	;

catchFormalParameter
	: (variableModifier)* catchType variableDeclaratorId
	;	//waiting for this to be construct

catchType
	: unannClassType ('|' classType)*
	;

finallyBlock
	: 'finally' block
	;

tryWithResourcesStatement
	: 'try' resourceSpecification block (catches)? (finallyBlock)?
	;

resourceSpecification
	: '(' resourceList (';')? ')'
	;

resourceList
	: resource (';' resource)*
	;

resource
	:  (variableModifier)* unannType variableDeclaratorId '=' expression
	; 	//waiting for this to be construct

