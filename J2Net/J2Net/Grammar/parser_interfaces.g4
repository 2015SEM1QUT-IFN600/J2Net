parser grammar parser_interfaces;


interfaceDeclaration
	: normalInterfaceDeclaration
	| annotationTypeDeclaration
	;

normalInterfaceDeclaration
	: '{'interfaceModifier'}' interface identifier (typeParameters)? (extendsInterfaces)? interfaceBody
	;

interfaceModifier
	: annotation 'public'
	| annotation 'protected'
	| annotation 'private'
	| annotation 'abstract' 
	| annotation 'static' 
	| annotation 'strictfp'
	;

extendsInterfaces
	: 'extends' interfaceTypeList
	;

interfaceBody
	: '{' '{'interfaceMemberDeclaration'}' '}'
	;

interfaceMemberDeclaration
	: constantDeclaration
	| interfaceMethodDeclaration
	| classDeclaration
	| InterfaceDeclaration
	;

constantDeclaration
	: '{'constantModifier'}' unannType variableDeclaratorList
	;

constantModifier
	: annotation 'public'
	| annotation 'static' 
	| annotation 'final'
	;

interfaceMethodDeclaration
	: '{'interfaceMethodModifier'}' methodHeader methodBody
	;

interfaceMethodModifier
	: annotation 'public'
	| annotation 'abstract' 
	| annotation 'default' 
	| annotation 'static' 
	| annotation 'strictfp'
	;

annotationTypeDeclaration
	: '{'interfaceModifier'}''@' interface identifier annotationTypeBody
	;

annotationTypeBody
	: '{''{'annotationTypeMemberDeclaration'}''}'
	;

annotationTypeMemberDeclaration
	: annotationTypeElementDeclaration
	| constantDeclaration
	| classDeclaration
	| interfaceDeclaration
	;

annotationTypeElementDeclaration
	: '{'annotationTypeElementModifier'}' unannType identifier '('')' ('dims')? (defaultValue)?
	;

annotationTypeElementModifier
	: annotation 'public'
	| annotation 'abstract'
	;

defaultValue
	: default elementValue
	;

annotation
	: normalAnnotation
	| markerAnnotation
	| singleElementAnnotation
	;

normalAnnotation
	: '@' typeName '(' (elementValuePairList)? ')'
	;

eElementValuePairList
	: elementValuePair (',' elementValuePair)*
	;

elementValuePair
	: identifier '=' elementValue
	;

elementValue
	: conditionalexpression
	| elementValueArrayInitializer
	| annotation
	;

elementValueArrayInitializer
	: '{' (elementValueList) '('(',')?')' '}'
	;

elementValueList
	: elementValue (',' elementValue)*
	;

markerAnnotation
	: '@' typeName
	;

singleElementAnnotation
	: '@' typeName '(' elementValue ')'
	;

compileUnit
	:	EOF
	;
