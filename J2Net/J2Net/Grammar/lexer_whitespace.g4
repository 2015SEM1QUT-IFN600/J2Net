lexer grammar lexer_whitespace;

WS  :  (' ' | '\t' | '\r' | '\n' | '\u000C') -> skip
    ;
