lexer grammar lexer_identifiers;

// §3.8 Identifiers

Identifiers			
	:	Letters		LettersOrDigits*
	;

fragment 
Letters
	:	[a-zA-Z$_]
	;

fragment
LettersOrDigits
	:	[a-zA-Z0-9$_]
	;
