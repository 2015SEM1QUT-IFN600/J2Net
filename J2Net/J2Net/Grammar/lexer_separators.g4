lexer grammar lexer_separators;

// at least one rule must be present for compilation
//REPLACETHISRULE
//	:	' ' -> channel(HIDDEN)
//	;

LPAREN : '(';
RPAREN : ')';
LBRACE : '{';
RBRACE : '}';
LBRACK : '[';
RBRACK : ']';
SEMI : ';';
COMMA : ',';
DOT : '.';
TRPDOT : '...';
ATSIGN : '@';
DBLCOLON : '::';
ASTERISK : '*';
