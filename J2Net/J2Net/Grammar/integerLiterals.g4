lexer grammar integerLiterals;

// at least one rule must be present for compilation
REPLACETHISRULE
	:	' ' -> channel(HIDDEN)
	;