lexer grammar lexer_identifiers;

// §3.8 Identifiers

//BUG: Are we missing an Identifiers token? Many rules request for an Identifiers token but none is found.

Identifiers
	:	Letters	LettersOrDigits*
	;

fragment
Letters
    :   [a-zA-Z$_]
    ;

fragment
LettersOrDigits
    :   [a-zA-Z0-9$_] 
    ;
	

	