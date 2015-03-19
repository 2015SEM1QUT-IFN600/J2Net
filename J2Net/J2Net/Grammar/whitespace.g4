lexer grammar whitespace;

WS  :  (' ' | '\t' | '\r' | '\n' | '\u000C') -> skip
    ;
