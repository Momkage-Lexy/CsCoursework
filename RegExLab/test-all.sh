#!/bin/sh
# This script will test all of your regular expressions.
# DO NOT MODIFY THIS FILE

TESTS="area-codes color-codes emails pit words"

num_fails=0
for test in $TESTS ; do
    echo === $test ===
    if ./$test.sh | diff - results/$test.txt ; then
        echo $test.sh is correct  ✅
    else
        echo $test.sh is incorrect - check the diff output above  ❌
        num_fails=`expr $num_fails + 1`
    fi
done

exit $num_fails
