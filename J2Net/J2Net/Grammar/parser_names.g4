parser grammar parser_names;

packageName			//From 6.5
	:	Identifier
	|	PackageName DOT Identifier
	;

typeName			//From 6.5
	:	Identifier
	|	PackageOrTypeName DOT Identifier
	;

packageOrTypeName	//From 6.5
	:	Identifier
	|	PackageOrTypeName DOT Identifier
	;

expressionName		//From 6.5
	:	Identifier
	|	AmbiguousName DOT Identifier
	;


methodName			//From 6.5
	:	Identifier
	;

ambiguousName		//From 6.5
	:	Identifier
	|	AmbiguousName DOT Identifier
	;