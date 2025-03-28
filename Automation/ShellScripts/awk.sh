#!/bin/bash

# Set pattern to user input: param 1
pattern=$1

# Set replacement to user input: param 2
replacement=${2:-}

# Set file to user input: param 3
file=$3

# Call awk on text file
# Output: lines, transformed with all instances of the name pattern replaced by the specified replacement text
awk -v pattern="$pattern" -v replacement="$replacement" '{
  gsub(pattern, replacement)
  print
}' "$file"