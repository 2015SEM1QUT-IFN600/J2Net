parser grammar parser_packages;

compilationUnit
    :   packageDeclaration? importDeclaration* typeDeclaration* EOF
    ;

packageDeclaration
    :   annotation* 'package' identifier ('.' identifier)? ';'
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
    :   'import' typeName ('.' '*')? ';'
    ;

singleStaticImportDeclaration
    :   'import' 'static' typeName '.' identifier ';'
    ;

staticImportOnDemandDeclaration
    :   'import' 'static' typeName ('.' '*')? ';'
    ;

typeDeclaration
    :   classOrInterfaceModifier* classDeclaration
    |   classOrInterfaceModifier* enumDeclaration
    |   classOrInterfaceModifier* interfaceDeclaration
    |   classOrInterfaceModifier* annotationTypeDeclaration
    |   ';'
    ;