grammar Java;

import 
		comments,	//Eric
		identifiers, //Aaron
		keywords,	//Dori
		integerLiterals, //Matt
		separators, //Mana
		operators;	//Monte


/*
 * Parser Rules
 */

compileUnit
	:	EOF
	;

/*
 * Lexer Rules
 */

WS
	:	' ' -> channel(HIDDEN)
	;
