lexer grammar comments;

// at least one rule must be present for compilation
REPLACETHISRULE
	:	' ' -> channel(HIDDEN)
	;