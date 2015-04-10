lexer grammar lexer_identifiers;

// §3.8 Identifiers

//BUG: Are we missing an Identifier token? Many rules request for an Identifier token but none is found.

Identifiers
	:	Letters	LettersOrDigits*
	;

fragment
Letters
	:	[a-zA-Z$_]+
	;

fragment
LettersOrDigits
	:	[a-zA-Z0-9$_]+
	;
