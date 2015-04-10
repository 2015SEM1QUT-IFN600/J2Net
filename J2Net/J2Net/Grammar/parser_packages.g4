parser grammar parser_packages;

/************ NAMES  ************/
importDeclaration
	:	singleTypeImportDeclaration 
	|	typeImportOnDemandDeclaration 
	|	singleStaticImportDeclaration
	|	staticImportOnDemandDeclaration
	;

singleTypeImportDeclaration
    :   'import' typeName ';'
    ;

typeImportOnDemandDeclaration
    :   'import' packageOrTypeName '.' '*' ';'
    ;

singleStaticImportDeclaration
    :   'import' 'static' typeName '.' Identifier ';'
    ;

staticImportOnDemandDeclaration
    :   'import' 'static' typeName '.' '*' ';'
    ;

compilationUnit
    :   packageDeclaration? importDeclaration* typeDeclaration* EOF
    ;

packageDeclaration
    :   annotation* 'package' Identifier ('.' Identifier)* ';'
    ;

typeDeclaration
	:	classDeclaration
	|	interfaceDeclaration
	|	';'
	;

