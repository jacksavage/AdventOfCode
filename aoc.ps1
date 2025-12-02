param(
  [Parameter(Mandatory)][int]$part,
  [Parameter(Mandatory)][int]$day,
  [int]$year = (Get-Date).Year
)

switch ($year) {
  2020 { dotnet run --project .\csharp $year $day $part }
  default { Write-Error "No solutions for year $year" }
}
