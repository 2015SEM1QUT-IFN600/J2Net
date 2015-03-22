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

fragment
OctalNumeral
	:'0' OctalDigits
	|'0' Underscores OctalDigits
	;

fragment
OctalDigits
	:OctalDigit
	|OctalDigit OctalDigitsAndUnderscores? OctalDigit
	;

fragment
OctalDigit
	:[0-7]
	;

fragment
OctalDigitsAndUnderscores
	:OctalDigitOrUnderscore OctalDigitOrUnderscore+
	;

fragment
OctalDigitOrUnderscore
	:OctalDigit	|'_'	;fragmentBinaryNumeral
	:'0' 'b' BinaryDigits
	|'0' 'B' BinaryDigits
	;

fragment
BinaryDigits
	:BinaryDigit
	|BinaryDigit BinaryDigitsAndUnderscores? BinaryDigit
	;

fragment
BinaryDigit
	:[01]
	;

fragment
BinaryDigitsAndUnderscores
	:BinaryDigitOrUnderscore BinaryDigitOrUnderscore+
	;

fragment
BinaryDigitOrUnderscore
	:BinaryDigit
	|'_'
	;

//Floating point

FloatingPointLiteral
	:DecimalFloatingPointLiteral
	|HexadecimalFloatingPointLiteral
	;

fragment
DecimalFloatingPointLiteral
	:Digits '.' Digits? ExponentPart? FloatTypeSuffix?
	|'.' Digits ExponentPart? FloatTypeSuffix?
	|Digits ExponentPart FloatTypeSuffix?
	|Digits ExponentPart? FloatTypeSuffix
	;

fragment
ExponentPart
	:ExponentIndicator SignedInteger
	;

fragment
ExponentIndicator
	:[e E]
	;

fragment
SignedInteger
	:Sign? Digits

fragment
Sign
	:[+ -]
	;

fragment
FloatTypeSuffix
	:[f F d D]
	;

fragment
HexadecimalFloatingPointLiteral
	:HexSignificand BinaryExponent FloatTypeSuffix?
	;

fragment
HexSignificand
	:HexNumeral '.'?
	|'0' 'x' HexDigits? '.' HexDigits
	|'0' 'X' HexDigits? '.' HexDigits
	;

fragment
BinaryExponent
	:BinaryExponentIndicator SignedInteger
	;

fragment
BinaryExponentIndicator:
	:[p P]	;