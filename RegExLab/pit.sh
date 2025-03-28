#!/bin/sh
# This script should match the word pit.
# All you need to do is fill in your regular expression
# between the single quotes - leave those quotes alone.

# \b\b Word boundary
# [Pp] matching capital or lower p
# s? matching s in plural case
egrep '\b[Pp]its?\b' data/pit.txt
