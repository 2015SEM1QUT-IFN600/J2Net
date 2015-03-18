lexer grammar identifiers;

// at least one rule must be present for compilation
REPLACETHISRULE
	:	' ' -> channel(HIDDEN)
	;