param(
  [int]$year,
  [int]$day,
  [int]$part
)

$path = "$PSScriptRoot\$($year.ToString('d2'))\$($day.ToString('d2'))\main.ps1"
if (-not (Test-Path $path)) {
  Write-Error "No solution for Year $year Day $day"
  exit 1
}

$input | & $path -part $part @PSBoundParameters