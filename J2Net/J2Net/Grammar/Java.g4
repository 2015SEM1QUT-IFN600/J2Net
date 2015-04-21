grammar Java;
options {
	language=CSharp;
}

/*
RULES OF PARSER INHERITANCE:
	1) Child parser rules cannot call Sibling rules 
	2) Child parser rules override Parent rules
	3) First imported child rule overrides Sibling rules
	4) Child parser rules can call Parent rules
	5) Parent parser rules can call Child rules
//*/

/*
RULES OF RULES:
	1) NEVER make forever looping parser rules like { myRule : myRule; instead use code such as myRule: myRule? or myRule: myRule myRule*} this will then allow the parser to continue on through the rule rather than continue into a loop
	2) Tokens should be used in place of all literals -- http://stackoverflow.com/questions/16102540/is-implicit-token-definition-in-parser-rule-something-to-worry-about
	2.5) All tokens should already exist. If not, they should be made. But no Token should be duplicated.
	3) Do not match empty strings. This is avoided if you used Tokens -- http://stackoverflow.com/questions/26041293/antlr-4-warning-rule-contains-an-optional-block-with-at-least-one-alternative
	4) Do not make rules that are mutually left-recursive -- http://stackoverflow.com/questions/18451290/how-to-fix-mutually-left-recursive-in-antlr4-rules
	4.5) mutual left-recursion can occur with rules like { x1 : y2; y2 : x1; } -- https://wincent.com/wiki/ANTLR_grammar_problems <-- BEST EXPLANATION
//*/

import 
		lexer_comments,			//Eric
		lexer_keywords,			//Dori
		lexer_literals,			//Matt
		lexer_separators,		//Mana
		lexer_operators,		//Monte
		lexer_whitespace,		//Eric
		lexer_identifiers,		//Aaron

		parser_types;
		// ORDER OF INHERITENCE
		// types -> expressions -> statements -> classes -> interfaces -> names -> packages

		/*//
		parser_literal,			//Eric (combined into parser_types)
		parser_names,			//CT
		parser_interfaces,		//Monte and Aaron
		parser_types,			//jason (completed by Eric) 
		parser_packages,		//Mana
		parser_classes,			//Dori
		parser_arrays,			//Harsh (combined into parser_types)
		parser_statements,		//Eric
		parser_expressions;		//Monte
		parser_exceptions,		//Matty (cancelled, no work to be done here)
		//*/
