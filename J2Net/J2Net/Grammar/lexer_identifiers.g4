lexer grammar lexer_identifiers;

// §3.8 Identifiers

Identifiers			
	:	Letters		LettersOrDigits*
	;

fragment 
Letters
	:	[a-zA-Z_]
	;

fragment
LettersOrDigits
	:	[a-zA-Z0-9_]
	;
