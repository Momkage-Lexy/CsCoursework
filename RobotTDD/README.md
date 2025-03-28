[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/sSaFXNQv)
# CS367-HW1

This first assignment will have you write the tests for the following specification for a "robot" program. The next assignment will have you implement code based on the tests.

## Background

A robot is standing at the `(0, 0)` point of the Cartesian plane and is oriented towards the vertical (y) axis in the direction of increasing y values (in other words, he's facing up, or north). The robot executes several commands each of which is a single positive integer. When the robot is given a positive integer K it moves K squares forward and then turns 90 degrees clockwise.

The commands are such that both of the robot's coordinates stay non-negative.

Your task is to determine if there is a square that the robot visits at least twice after executing all the commands.

# Example

For `a = [4, 4, 3, 2, 2, 3]`, the output should be `true`.

The path of the robot looks like this:

![](https://codefightsuserpics.s3.amazonaws.com/tasks/robotWalk/img/path.png?_tm=1486563151922)

The point `(4, 3)` is visited twice, so the answer is `true`.

# Input/Output

- `[input]` integer array a

An array of positive integers, each number representing a command.

Constraints:

`3 ≤ a.length ≤ 100`

`1 ≤ a[i] ≤ 10000`

- `[output]` a boolean value

`true` if there is a square that the robot visits at least twice, `false` otherwise.

## Instructions

1. Accept the course invitation from GitHub classroom using this link: [https://classroom.github.com/a/sSaFXNQv](https://classroom.github.com/a/sSaFXNQv)
2. Use Test-Driven Development to develop the specifications above.
    - Develop as many tests as you feel makes you feel confident in your square-dancing robot.
    - You should have **at least** one test per requirement described above.
        - You will have to determine what the requirements are, given the description.
        - (you can, and should, use the strategies covered in lecture)
    - **You should have a multi-line comment above each test explicitly stating which requirement your test is evaluating.**
    - Each test you write should have a multi-line comment explaining the test, including what is being evaluated and what it's expected output (pass or fail) is.
    - **Minimum of 3 git commits required for full credit.**

## Submission

You need to do **two things** in order for this assignment to be graded:

- Push your code to your forked version of the assignment repository
- Create a release tag, and submit the resulting URL to this canvas assignment.
    - [Link to instructions](https://docs.github.com/en/repositories/creating-and-managing-repositories/creating-a-repository-from-a-template#:~:text=Creating%20a%20repository%20from%20a%20template%20is%20similar%20to%20forking%20a%20repository%2C%20but%20there%20are%20important%20differences%3A)