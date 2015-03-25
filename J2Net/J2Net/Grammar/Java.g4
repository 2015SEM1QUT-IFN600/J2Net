grammar Java;

import 
		lexer_comments,			//Eric
		lexer_identifiers,		//Aaron
		lexer_keywords,			//Dori
		lexer_literals,			//Matt
		lexer_separators,		//Mana
		lexer_operators,		//Monte
		lexer_whitespace,		//Eric

		parser_types,			//jason
		parser_names,			//CT
		parser_packages,		//Mana
		parser_classes,			//Dori
		parser_interfaces,		//Aaron
		parser_arrays,			//Harsh
		parser_exceptions,		//Matty
		parser_statements,		//Eric
		parser_expressions;		//Monte


/*
 * Parser Rules
 */

compileUnit
	:	EOF
	;

/*
 * Lexer Rules
 */

//these are just here temporary until we get our literals working.
ID : [a-zA-Z]+ ; // match identifiers
NUM : [0-9]+ ; // match integers
NEWLINE:'\r'? '\n' -> skip; // return newlines to parser (is end-statement signal)