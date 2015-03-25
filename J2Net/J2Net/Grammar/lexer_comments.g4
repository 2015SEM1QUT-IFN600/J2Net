lexer grammar lexer_comments;

// at least one rule must be present for compilation
COMMENT
    :   '/*' .*? '*/' -> skip
    ;

LINE_COMMENT
    :   '//' ~[\r\n]* -> skip
    ;