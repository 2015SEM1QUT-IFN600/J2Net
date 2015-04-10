param(
	[parameter(Position=1)]
	[string]$searchString,
    
    [switch]$expand=$false,

    [string]$allRulesInFile
)
$searchString = "^" + $searchString + "$"

if ($allRulesInFile -ne "")
{
    $searchString = "^[^\f\n\r\t\v\x85\p{Z}/]+$"
    $x = Get-ChildItem -File $allRulesInFile
    $x | Select-String -pattern $searchString | %{$_.Line + " : DOT;"}
} else {
    $x = Get-ChildItem -Recurse
}

if ($expand)
{
    $x | Select-String -pattern $searchString
} else {
    $x | Select-String -pattern $searchString | group path | select name
}