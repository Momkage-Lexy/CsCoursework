#!/bin/sh
# This script should match HTML color code.
# All you need to do is fill in your regular expression
# between the single quotes - leave those quotes alone.


# cie matches containing cie
# | second pattern
# [^c] any character except c
egrep 'cie|[^Cc]ei' data/words.txt
