param(
  [Parameter(Mandatory)][int]$part,
  [Parameter(Mandatory)][int]$day,
  [int]$year = (Get-Date).Year
)


function readInput {
  $dataDir = "data\$year"
  if (-not (Test-Path $dataDir)) {
    New-Item -ItemType Directory -Path $dataDir -Force | Out-Null
  }

  $inputPath = "$dataDir\$($day.ToString('d2'))"
  if (-not (Test-Path $inputPath)) {
    $cookieFile = 'session-cookie'
    if (-not (Test-Path $cookieFile)) {
      Write-Error "Missing file $cookieFile"
      return $null
    }

    $cookie = Get-Content $cookieFile -Raw
    $url = "https://adventofcode.com/$year/day/$day/input"
    Write-Host 'Downloading input file'

    try {
      $headers = @{ Cookie = "session=$cookie" }
      $response = Invoke-WebRequest -Uri $url -Headers $headers -UseBasicParsing
      $response.Content | Set-Content $inputPath -NoNewline
    } catch {
      Write-Error "Failed to download input file: $_"
      return $null
    }
  }

  Get-Content $inputPath
}

switch ($year) {
  2020 {
    readInput | dotnet run --project .\csharp $year $day $part
  }
  2025 {
    Push-Location .\gleam\src
    readInput | gleam run
    Pop-Location
  }
  default { Write-Error "No solutions for year $year" }
}
