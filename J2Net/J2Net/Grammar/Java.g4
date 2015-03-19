grammar Java;

import 
		comments,	//Eric
		identifiers, //Aaron
		keywords,	//Dori
		literals, //Matt
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
