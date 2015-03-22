lexer grammar literals;

WS
	:	' ' -> channel(HIDDEN)
	;

IntegerLiteral
	:DecimalIntegerLiteral
	|HexIntegerLiteral
	|OctalIntegerLiteral
	|BinaryIntegerLiteral
	;

fragment
DecimalIntegerLiteral
	:DecimalNumeral IntegerTypeSuffix?
	;

fragment
HexIntegerLiteral
	:HexNumeral IntegerTypeSuffix?
	;

fragment
OctalIntegerLiteral
	:OctalNumeral IntegerTypeSuffix?
	;

fragment
BinaryIntegerLiteral
	:BinaryNumeral IntegerTypeSuffix?
	;

fragment
IntegerTypeSuffix
	:[lL]
	;

fragment
DecimalNumeral:
	:'0'
	|NonZeroDigit Digits?
	|NonZeroDigit Underscores Digits
	;

fragment
NonZeroDigit
	:[1-9]
	;

fragment
Digits
	:Digit
	|Digit DigitsAndUnderscores? Digit
	;

fragment
Digit
	:'0'
	|NonZeroDigit
	;

fragment
DigitsAndUnderscores
	:DigitOrUnderscore DigitOrUnderscore+
	;

fragment
DigitOrUnderscore
	:Digit
	|'_'
	;

fragment
Underscores
	:'_'+
	;

fragment
HexNumeral
	:'0' 'x' HexDigits
	|'0' 'X' HexDigits
	;

fragment
HexDigits
	:HexDigit
	|HexDigit HexDigitsAndUnderscores? HexDigit
	;

fragment
HexDigit
	:[0-9 a-f A-F]
	;

fragment
HexDigitsAndUnderscores
	:HexDigitOrUnderscore HexDigitOrUnderscore+
	;

fragment
HexDigitOrUnderscore
	:HexDigit
	|'_'
	;