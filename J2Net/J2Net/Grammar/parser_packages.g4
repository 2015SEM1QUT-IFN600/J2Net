parser grammar parser_packages;

compilationUnit
    :   packageDeclaration? importDeclaration* typeDeclaration* EOF
    ;

packageDeclaration
    :   annotation* 'package' Identifier ('.' Identifier)* ';'
    ;

	
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

typeDeclaration
	:	classDeclaration
	|	interfaceDeclaration
	|	';'
	;

/*
typeDeclaration
    :   classOrInterfaceModifier* classDeclaration
    |   classOrInterfaceModifier* enumDeclaration
    |   classOrInterfaceModifier* interfaceDeclaration
    |   classOrInterfaceModifier* annotationTypeDeclaration
    |   ';'
    ;
*/
compileUnit
	:	EOF
	;