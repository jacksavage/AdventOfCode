# https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/set-strictmode?view=powershell-7.5
Set-StrictMode -Version 3.0

# set dial starting position
$dialPosition = 50
$positionCount = 100

# given a value between 0 and $positionCount - 1 that represents the number of clicks to move to the right
# returns the offset for the new position
# the offset will be a value between -($positionCount - 1) and ($positionCount - 1) inclusive
function right($remainderClicks) {
  Write-Debug "right $remainderClicks"
  # if we roll past the end of the dial
  if (($remainderClicks + $dialPosition) -gt ($positionCount - 1)) {
    Write-Debug 'overflow'
    # our relative position can be found by subtracting by the total number of positions
    return $remainderClicks - $positionCount
  }

  # if we don't go past the end of the dial
  # we just move that many clicks forward
  return $remainderClicks
}

# see the definition for `right`
# this does the same thing given the number of clicks to move to the left
function left($remainderClicks) {
  Write-Debug "left $remainderClicks"

  # if we roll past the start of the dial
  if (($dialPosition - $remainderClicks) -lt 0) {
    Write-Debug 'underflow'
    # our relative position can be found by subtracting from the total number of positions
    return $positionCount - $remainderClicks
  }

  # if we don't go before the start of the dial
  # we just move this number of clicks backward
  return - ($remainderClicks)
}

# track the number of times the dial lands on zero
$zeroCount = 0
$input | ForEach-Object {
  Write-Debug '---'
  Write-Debug "dial: $dialPosition"
  Write-Debug "input: $_"

  # get the offset
  $clicks = [int]$_.Substring(1)
  $remainderClicks = $clicks % $positionCount

  # get the direction
  $direction = $_.Substring(0, 1)

  Write-Debug "remainder: $remainderClicks"

  # move dial
  # first letter of each line should be L or R
  switch ($direction) {
    'L' { $offset = left($remainderClicks) }
    'R' { $offset = right($remainderClicks) }
  }

  Write-Debug "offset: $offset"

  # adjust dial by the offset
  $dialPosition += $offset
  Write-Debug "new dial: $dialPosition"

  # check for zero position
  if ($dialPosition -eq 0) { $zeroCount++; Write-Debug 'zero!' }

  Write-Debug "---`n"
}

Write-Output "part 1: $zeroCount"
