grammar Java;

import comments
import identifiers
import keywords
import integerLiterals
/*
import floatingPointLiterals
import booleanLiterals
import characterLiterals
import stringLiterals
import escapeSequences
import nullLiterals
*/
import separators
import operators

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
