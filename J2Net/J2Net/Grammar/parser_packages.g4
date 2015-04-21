parser grammar parser_packages;


/************ NAMES  ************/


compilationUnit
    :   packageDeclaration? importDeclaration* typeDeclaration* EOF
    ;

packageDeclaration
    :   (PackageModifier)* PACKAGE Identifiers (DOT Identifiers)* SEMI
    ;

PackageModifier
	:	annotation
	;

importDeclaration
	:	singleTypeImportDeclaration 
	|	typeImportOnDemandDeclaration 
	|	singleStaticImportDeclaration
	|	staticImportOnDemandDeclaration
	;

singleTypeImportDeclaration
    :   IMPORT typeName SEMI
    ;

typeImportOnDemandDeclaration
    :   IMPORT packageOrTypeName DOT ASTERISK SEMI
    ;

singleStaticImportDeclaration
    :   IMPORT STATIC typeName DOT Identifiers SEMI
    ;

staticImportOnDemandDeclaration
    :   IMPORT STATIC typeName DOT ASTERISK SEMI
    ;

typeDeclaration
	:	classDeclaration
	|	interfaceDeclaration
	|	SEMI
	;

