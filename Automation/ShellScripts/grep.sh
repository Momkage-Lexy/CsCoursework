#!/bin/bash

# Set pattern to user input: param 1
pattern=$1

# Set file to user input: param 2
file=$2

# Call grep on file matching the pattern
# Output: lines that match the name pattern
grep -- "$pattern" "$file"