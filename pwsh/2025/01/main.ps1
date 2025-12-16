param([int]$part)

# https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/set-strictmode?view=powershell-7.5
Set-StrictMode -Version 3.0

class RotateResult {
  [int]$offset
  [bool]$overflow # or underflow

  RotateResult([int]$offset, [bool]$overflow) {
    $this.offset = $offset
    $this.overflow = $overflow
  }
}

# given a value between 0 and $positionCount - 1 that represents the number of clicks to move to the right
# returns the offset for the new position
# the offset will be a value between -($positionCount - 1) and ($positionCount - 1) inclusive
function right($remainderClicks) {
  Write-Debug "right $remainderClicks"
  # if we roll past the end of the dial
  if (($remainderClicks + $dialPosition) -gt ($positionCount - 1)) {
    Write-Debug 'overflow'

    # our relative position can be found by subtracting by the total number of positions
    return [RotateResult]::new($remainderClicks - $positionCount, $true)
  }

  # if we don't go past the end of the dial
  # we just move that many clicks forward
  return [RotateResult]::new($remainderClicks, $false)
}

# see the definition for `right`
# this does the same thing given the number of clicks to move to the left
function left($remainderClicks) {
  Write-Debug "left $remainderClicks"

  # if we roll past the start of the dial
  if (($dialPosition - $remainderClicks) -lt 0) {
    Write-Debug 'underflow'

    # our relative position can be found by subtracting from the total number of positions
    return [RotateResult]::new($positionCount - $remainderClicks, $true)
  }

  # if we don't go before the start of the dial
  # we just move this number of clicks backward
  return [RotateResult]::new(- $remainderClicks, $false)
}

# set dial starting position
$dialPosition = 50
$positionCount = 100

# track the number of times the dial lands on zero or crosses zero
$zeroLandings = 0
$zeroCrossings = 0

# iterate through the input commands
$input | ForEach-Object {
  Write-Debug '---'
  Write-Debug "dial: $dialPosition"
  Write-Debug "input: $_"

  # get the offset
  $clicks = [int]$_.Substring(1)
  $remainderClicks = $clicks % $positionCount

  # count zero crossings due to full rotations
  $fullRotations = [Math]::Floor($clicks / $positionCount)
  Write-Debug "full rotation zero crossings: $fullRotations"
  $zeroCrossings += $fullRotations

  # get the direction
  $direction = $_.Substring(0, 1)

  Write-Debug "remainder: $remainderClicks"

  # move dial
  # first letter of each line should be L or R
  switch ($direction) {
    'L' { $rotateResult = left($remainderClicks) }
    'R' { $rotateResult = right($remainderClicks) }
  }

  # adjust dial by the offset
  $dialPosition += $rotateResult.offset
  Write-Debug "new dial: $dialPosition"

  # check for zero position
  if ($dialPosition -eq 0) {
    $zeroLandings++
    Write-Debug 'zero landing!'
  } else {
    # add a zero crossing if we had an overflow or underflow
    # and we didn't land on right zero
    if ($rotateResult.overflow) {
      $zeroCrossings++
      Write-Debug 'overflow zero crossing!'
    }
  }

  Write-Debug "total zero crossings: $zeroCrossings"
  Write-Debug "offset: $($rotateResult.offset)"
  Write-Debug "---`n"
}

# print solution output
switch ($part) {
  1 {
    Write-Information 'part 1 output:'
    Write-Output $zeroLandings
  }
  2 {
    Write-Information 'part 2 output:'
    Write-Output ($zeroCrossings + $zeroLandings)
  }
}
