#!/bin/sh
# This script should match American university emails.
# All you need to do is fill in your regular expression
# between the single quotes - leave those quotes alone.

# ^beginning of line match boundary
# [A-Za-z0-9._%+-] matches case insenstive alpha-numeric and _%+- characters
# +@ means that the previous regex could repeat the matching until @ is hit
# [A-Za-z0-9-] matches case insenstive alpha-numeric characters
# +\. previous matching continues until . is hit
# \s*$ end of line but accept whitespace
egrep '^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\.*[A-Za-z0-9-]+\.edu\s*$' data/emails.txt
