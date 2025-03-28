[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/DOXfeFWz)
# LinkedList Assignment CS260 

This homework will have you define a class that has two definitions. You will use the properties of the pre-established, private subclass, the _Node objects, to implement two insert functions: `insert_before`, and `insert_after`.
---

## Set Up
You will need to clone this repository in order to begin working on this assignment

This repository is a *scaffold* of the LinkedList class. The structure has been defined, but you will need to fill in the functionality. 

Currently all functions (except the node constructor) are going to throw a `NotImplementedError`. You must overwrite this error with functionality.

---

## Tests
There is a set of unit tests that will run. You can use these to troubleshoot your LinkedList implementation.

The test suite will run whenever you `git push` your code to the remote repository. If you go to the **Actions** tab on your GitHub repository for the assignment, the most recent commit message will be at the top. While on the Actions tab, click the most recent commit message and then click on **autograding** once redirected. Expanding on "**Run education/autograding@v1**" will drop down a test log which will give you more information on which tests passed/failed.

You can also run the tests locally by installing the python pytest package (`pip install pytest`) and then running `pytest` in a terminal window. This should be identical to what is run on GitHub.

You can use tricks like `pytest -k name_of_test_function_here` to check the output of a single test, or `pytest -v` to give more detailed output on passed/failed tests.

---

## *Tip
You need to take advantage of all of the node objects and attributes available to you. The `head` and `tail` attributes will be your entrance point, and you will need to leverage the `previous` and `next` attributes in order to traverse through the list to the desired location.