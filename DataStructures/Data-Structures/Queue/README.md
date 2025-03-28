[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/7YPFIlw2)
# Queue List Assignment

In this assignment, you will make a *wrapper* class around the LinkedList class we previously defined. 
This class will add functionality and attributes to the LinkedList in order to have it meet the definitions, and act like, a Queue.

---

## Set Up
You will need to clone this repository in order to begin working on this assignment.

This repository is a *scaffold* of the ListQueue class. The structure has been defined, but you will need to fill in the functionality. 

Currently all functions are going to throw a `NotImplementedError`. You must overwrite this error with functionality.

---

## Tests
There is a set of unit tests that will run. You can use these to troubleshoot your implementation.

The test suite will run whenever you `git push` your code to the remote repository. If you go to the **Actions** tab on your GitHub repository for the assignment, the most recent commit message will be at the top. While on the Actions tab, click the most recent commit message and then click on **autograding** once redirected. Expanding on "**Run education/autograding@v1**" will drop down a test log which will give you more information on which tests passed/failed.

You can also run the tests locally by installing the python pytest package and then running `pytest` in a terminal window. This should be identical to what is run on GitHub.

---

## *Tip
The ListQueue is going to have some functions that have the same logic as the LinkedList, but are more restricted. You can use calls to the LinkedList functions, which this class inherits, in your implementation. You will still need to modify behavior around these calls, however.