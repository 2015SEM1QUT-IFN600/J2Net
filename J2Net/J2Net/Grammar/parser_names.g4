parser grammar parser_names;

compileUnit
	:	EOF
	;

packageName			//From 6.5
	:	Identifier
	|	PackageName DOT Identifier
	;

//Reference to undefined rule:Identifier is occurred as following:
/*
TypeName			//From 6.5
	:	Identifier
	|	PackageOrTypeName DOT Identifier
	;


//*
PackageOrTypeName	//From 6.5
	:	Identifier
	|	PackageOrTypeName DOT Identifier
	;


ExpressionName		//From 6.5
	:	Identifier
	|	AmbiguousName DOT Identifier
	;


MethodName			//From 6.5
	:	Identifier
	;

AmbiguousName		//From 6.5
	:	Identifier
	|	AmbiguousName DOT Identifier
	;
//*/
