# Regular Expressions Assignment

This is a simple(?) exercise with regular expressions. Although it looks
like a bunch of programs/scripts (five of them), it's really just five
regular expressions. The scripts are just for automated testing.

For each item, say `area-codes` there is a shell script in the root with the
name `area-codes.sh`. It contains a `grep` (or `egrep`) command that runs a
regular express over a text file in the `data` directory named
`area-codes.txt`. In the `results` directory there is another file called
`area-codes.txt` that contains the expected results of the `grep` command -
i.e., the lines from the file that should match the regular expression.
There is also a script called `test-all.sh` that will run on GitHub to check
your submissions.

Your assignment is to go through the five scripts and insert a regular
expression that will match the correct patterns described below. Only modify
the regular expression which has an initial value of `CHANGE-ME`.

To work on these, you can just run the `grep` command in the script in a
terminal window. That should work on MacOS, WSL, and (probably) MinGW. Once
you have a regular expression that works, put it in the corresponding shell
script and move on to the next one. (Ideally, commit each script to git as
you get it working.) If you want to check your results against the exected
results, you can run the same command that `test-all.sh` runs. For example:
```
./area-codes.sh | diff - results/area-codes.txt
```

## Area Codes - `area-codes.sh`
Old US telephone area codes used to conform to a specific pattern: they were
three digits long, the middle digit was always 0 or 1, and the first digit
was never 0 or 1 (the final digit could be any number). New area codes like
`(971)` do not conform to this pattern. Write a regular expression that will match
any line containing an old area code including the parentheses. The first
five lines of `area-codes.txt` are the valid ones.

## Color Codes - `color-codes.sh`
Valid HTML color codes consist of a number sign followed by exactly six
hexadecimal digits. Write a regular expression that will match any line that
contains a valid HTML color code. Note that the letters A-F in the code can
be upper or lower case.  Again, only the first 5 lines should matche.
(You can assume that the valid color codes are always followed by a space,
though of course, the situation isn't quite that simple in reality).

## Emails Addresses - `emails.sh`
Write a regular expression that will match any line that contains a valid
American university email address. That is, it will match
`syncope@cats.ucsc.edu`, but not `smith@cambridge.edu.uk` (the `.uk` suffix
indicates that this is an address in the United Kingdom). 
Again, the first 5 lines should match. (You can assume that
valid email addresses make use of letters, numbers, dashes, and underscores,
separated by dots, and are followed by a space.)

## The Pits - `pit.sh`
Write a regular expression which matches lines where Edgar Allan Poe used
the word "pit" and its derivatives in "The Pit and the Pendulum" in
`data/pit.txt`.  You should match the singular and the plural, capital and
lowercase versions, and following punctuation marks. It should not count
words like "pitcher" or "spite".

## Spelling Bee - `words.sh`
We've all heard the expression "i before e except after c", and we all know
that it simply isn't true because, English. Prove your first grade teacher
wrong by writing regular expression that will match all words in
`data/words.txt` (copied from `/usr/share/dict/words` on MacOS) that break
this maxim (that is, all words with i before e after c, and all words with e
before i after other letters besides c).  As usual, the expected results are
in `results/words.txt`. Both the input file and the expect results file are
huge. The output of `grep` or `grep` piped into `diff` will be huge. You
could modify the manual testing command I gave above by adding the `head`
command to the pipeline. (This is what I did during development.)
```
./words.sh | diff - results/words.txt | head
```
