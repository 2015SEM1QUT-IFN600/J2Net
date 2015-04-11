parser grammar parser_statements;

// Statements and Expressions
import parser_classes;

// UNCOMMENT THESE RULES IF COMMENTING IMPORT CHAIN //
//classDeclaration : DOT;
//variableModifier : DOT;
//unannType : DOT;
//unannClassType : DOT;
//variableDeclaratorList : DOT;
//variableDeclaratorId : DOT;


/************ STATEMENTS  ************/

block
	: '{' blockStatements? '}'
	;

blockStatements
	: blockStatement '{'blockStatement'}'
	;

blockStatement
	: localVariableDeclarationStatement
	| classDeclaration
	| statement
	;

localVariableDeclarationStatement
	: localVariableDeclaration ';'
	;

localVariableDeclaration
	: (variableModifier)* unannType variableDeclaratorList
	; 

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
	: Identifiers ':' statement
	;

labeledStatementNoShortIf
	: Identifiers ':' statementNoShortIf
	;

expressionStatement
	: statementExpression ';'
	;

statementExpression
	: assignment						
	| preIncrementExpression			
	| preDecrementExpression			
	| postIncrementExpression			
	| postDecrementExpression			
	| methodInvocation					
	| classInstanceCreationExpression	
	;

ifThenStatement
	: 'if' '(' expression ')' statement
	;	

ifThenElseStatement
	: 'if' '(' expression ')' statement 'else' statement
	;	

ifThenElseStatementNoShortIf
	: 'if' '(' expression ')' statementNoShortIf 'else' statementNoShortIf
	;	

assertStatement
	: 'assert' expression (':' expression)? ';'
	;	

switchStatement
	: 'switch' '(' expression ')' switchBlock
	;	

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
	;	

whileStatementNoShortIf
	: 'while' '(' expression ')' statementNoShortIf
	;	

doStatement
	: 'do' statement 'while' '(' expression ')' ';'
	;	

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
	;	

basicForStatementNoShortIf
	: 'for' '(' forInit? ';' expression? ';' forUpdate? ')' statementNoShortIf
	;	

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
	;	

enhancedForStatementNoShortIf
	:'for' '('( variableModifier)* unannType variableDeclaratorId ':' expression ')' statementNoShortIf
	;	

breakStatement
	: 'break' Identifiers? ';'
	;

continueStatement
	: 'continue' Identifiers? ';'
	;

returnStatement
	: 'return' expression? ';'
	;	

throwStatement
	: 'throw' expression ';'
	;	

synchronizedStatement
	: 'synchronized' '(' expression ')' block
	;	

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
	;	

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
	; 	
