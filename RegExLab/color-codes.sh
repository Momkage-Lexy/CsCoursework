#!/bin/sh
# This script should match HTML color code.
# All you need to do is fill in your regular expression
# between the single quotes - leave those quotes alone.

# hash symbol matches hash
# [A-Fa-f0-9] matches characters A-F upper or lower case and digits 0-9
# {6} matches 6 characters of the previous type
# /b word boundary
egrep '#[A-Fa-f0-9]{6}\b' data/color-codes.txt
