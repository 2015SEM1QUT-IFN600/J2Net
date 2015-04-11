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
	: LBRACE blockStatements? RBRACE
	;

blockStatements
	: blockStatement LBRACE blockStatement RBRACE
	;

blockStatement
	: localVariableDeclarationStatement
	| classDeclaration
	| statement
	;

localVariableDeclarationStatement
	: localVariableDeclaration SEMI
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
	: Identifiers COLON statement
	;

labeledStatementNoShortIf
	: Identifiers COLON statementNoShortIf
	;

expressionStatement
	: statementExpression SEMI
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
	: IF LPAREN expression RPAREN statement
	;	

ifThenElseStatement
	: IF LPAREN expression RPAREN statement ELSE statement
	;	

ifThenElseStatementNoShortIf
	: IF LPAREN expression RPAREN statementNoShortIf ELSE statementNoShortIf
	;	

assertStatement
	: ASSERT expression (COLON expression)? SEMI
	;	

switchStatement
	: SWITCH LPAREN expression RPAREN switchBlock
	;	

switchBlock
	: LBRACE (switchBlockStatementGroup)* (switchLabel)* RBRACE
	;

switchBlockStatementGroup
	: switchLabels blockStatements
	;

switchLabels
	: switchLabel (switchLabel)*
	;

switchLabel
	: CASE constantExpression COLON
	| CASE eNumConstantname COLON
	| DEFAULT COLON
	;

eNumConstantname
	: Identifiers
	;

whileStatement
	: WHILE LPAREN expression RPAREN statement
	;	

whileStatementNoShortIf
	: WHILE LPAREN expression RPAREN statementNoShortIf
	;	

doStatement
	: DO statement WHILE LPAREN expression RPAREN SEMI
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
	: FOR LPAREN forInit? SEMI expression? SEMI forUpdate? RPAREN statement
	;	

basicForStatementNoShortIf
	: FOR LPAREN forInit? SEMI expression? SEMI forUpdate? RPAREN statementNoShortIf
	;	

forInit
	: statementExpressionList
	| localVariableDeclaration
	;

forUpdate
	: statementExpressionList
	;

statementExpressionList
	: statementExpression (COMMA statementExpression)*
	;

enhancedForStatement
	:FOR LPAREN (variableModifier)* unannType variableDeclaratorId COLON expression RPAREN statement
	;	

enhancedForStatementNoShortIf
	: FOR LPAREN( variableModifier)* unannType variableDeclaratorId COLON expression RPAREN statementNoShortIf
	;	

breakStatement
	: BREAK Identifiers? SEMI
	;

continueStatement
	: CONTINUE Identifiers? SEMI
	;

returnStatement
	: RETURN expression? SEMI
	;	

throwStatement
	: THROW expression SEMI
	;	

synchronizedStatement
	: SYNCHRONIZED LPAREN expression RPAREN block
	;	

tryStatement
	: TRY block catches
	| TRY block (catches)? finallyBlock
	| tryWithResourcesStatement
	;

catches
	: catchClause (catchClause)*
	;

catchClause
	: CATCH LPAREN catchFormalParameter RPAREN block
	;

catchFormalParameter
	: (variableModifier)* catchType variableDeclaratorId
	;	

catchType
	: unannClassType (BITOR classType)*
	;

finallyBlock
	: FINALLY block
	;

tryWithResourcesStatement
	: TRY resourceSpecification block (catches)? (finallyBlock)?
	;

resourceSpecification
	: LPAREN resourceList (SEMI)? RPAREN
	;

resourceList
	: resource (SEMI resource)*
	;

resource
	:  (variableModifier)* unannType variableDeclaratorId ASSIGN expression
	; 	
