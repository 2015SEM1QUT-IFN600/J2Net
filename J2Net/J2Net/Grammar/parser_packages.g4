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
    :   'import' 'static' typeName '.' Identifiers ';'
    ;

staticImportOnDemandDeclaration
    :   'import' 'static' typeName '.' '*' ';'
    ;

compilationUnit
    :   packageDeclaration? importDeclaration* typeDeclaration* EOF
    ;

packageDeclaration
    :   annotation* 'package' Identifiers ('.' Identifiers)* ';'
    ;

typeDeclaration
	:	classDeclaration
	|	interfaceDeclaration
	|	';'
	;

