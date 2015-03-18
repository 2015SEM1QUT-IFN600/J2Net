grammar Java;

import 
		comments, 
		identifiers, 
		keywords, 
		integerLiterals, 
		separators, 
		operators;
/*
		floatingPointLiterals,
		booleanLiterals,
		characterLiterals,
		stringLiterals,
		escapeSequences,
		nullLiterals,
*/


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
