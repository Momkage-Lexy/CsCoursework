#!/bin/sh
# This script should match historically correct area codes.
# All you need to do is fill in your regular expression
# between the single quotes - leave those quotes alone.

# (): matches parenthesis
# [][][]: matches 3 character format
# 2-9: matches first character to digit not 0 or 1
# 01: matches digit 0 or 1
# 0-9: matches any digit
grep '([2-9][01][0-9])' data/area-codes.txt
