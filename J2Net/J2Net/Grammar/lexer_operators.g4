lexer grammar lexer_operators;

/*The operators are in order of precedence eg. Left to Right, Right to Left, BOMDAS etc.*/

//Incremental
INC : '++';
DEC : '--';

//unary
TILDE : '~';
NOT : '!';
MUL : '*';
DIV : '/';
MOD : '%';

//Binary
THIS : '.';
ADD : '+';
SUB : '-';
ASSIGN : '=';

//Shift position
LEFT_SIGNED : '<<';
RIGHT_SIGNED : '>>';
RIGHT_UNSIGNED : '>>>';
URSHIFT_ASSIGN : '>>>=';

//Relational
LT : '<';
LE : '<=';
GT : '>';
GE : '>=';
EQUAL : '==';

//Logic
NOTEQUAL : '!=';
BITAND : '&';
CARET : '^';
BITOR : '|';
AND : '&&';
OR : '||';
QUESTION : '?';
COLON : ':';

//Assignment

ADD_ASSIGN : '+=';
SUB_ASSIGN : '-=';
MUL_ASSIGN : '*=';
DIV_ASSIGN : '/=';
AND_ASSIGN : '&=';
OR_ASSIGN : '|=';
XOR_ASSIGN : '^=';
MOD_ASSIGN : '%=';
LSHIFT_ASSIGN : '<<=';
RSHIFT_ASSIGN : '>>=';

LAMBDA_ASSIGN : '->';