using System;
using System.Collections.Generic;
namespace RobotTest;

public class RobotMoveSquareTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AssignmentExampleTest()
    {
        // Arrange
        var robot = new SquareRobot();
        var commands = new int[] { 4, 4, 3, 2, 2, 3 };

        // Act
        var result = robot.Move(commands);

        // Assert
        Assert.IsTrue(result);
    }


    [Test]
    // Used ChatGPT for guidance on Act portion of the test
    /* REQUIREMENT: Program can detect if a point is revisited
    This test's purpose is to test the implentation of checking if the 
    robot returns to the same point twice. This test should return a bool value of 
    FALSE since these commands do not lead to any squares. */
    public void NoRevisitTest()
    {
        // Arrange
        var robot = new SquareRobot(); // Initialize new bot
        var commands = new int [] {4, 4, 4, 2}; // These commands should NOT make a square

        // Act
        bool result = robot.Move(commands); // Implement IsSquare method to IRobot.cs in assignment 2

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    /* REQUIREMENT: Does not go into the negatives on the grid
    This test's purpose is to verify the robot does not go into the negative plane
    even when given coordinates that would normally lead there. The test should return a 
    bool value of FALSE since the program should incorporate an exception for such inputs */
    public void NoNegativeTest()
    {
        // Arrange
        var robot = new SquareRobot(); // Initialize new bot
        /* These commands would make the robot go into the negative plane unless
        the program contains an exception */
        var commands = new int[] {1, 1, 5, 5}; 

        // Act
        /* Implement Locate method to IRobot.cs in assignment 2 which returns the 
        (x, y) coordinates after each command */
        var negativeLocation = robot.Locate(commands); 

        /* Used ChatGPT for logic assistance on the following line
        which basically returns True if a negative coordinates exists and False otherwise */
        var result = negativeLocation.Exists(loc => loc.Item1 < 0 || loc.Item2 < 0); 

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    /* REQUIREMENT: Minimum of 3 commands
    This test much like the no revisit test, checks the programs ability 
    to detect if a square was created. This time by inputing the minimum amount of commands which is 3 
    and will not make a square. The test should return FALSE as it should detect that not enough commands were 
    given to create a square. */
    public void MinInputTest()
    {
        // Arrange
        var robot = new SquareRobot(); // Initialize new bot
        var commands = new int[] {4, 3, 2}; // 3 commands will not make a square

        // Act
        var result = robot.Move(commands); 
        
        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    /* REQUIREMENT: Maximum of 100 commands up to 10000
    This test will see how the robot handles the maximum input. If given 
    100 commands each ranging from (1-10000), will the program return the correct statement? The test outcome depends on the input.
    There is a spiral scenerio I can envision as a possibility, which would return FALSE, however, 
    most inputs should return TRUE. */
    public void MaxInputTest()
    {
        // Arrange
        var robot = new SquareRobot(); // Initialize new bot
        var random = new Random(); // Used ChatGPT for assistance in using Random class as I am still new to C#
        var commands = new int[100]; // Array to hold 100 command items
        for (int i = 0; i < commands.Length; i++)
        {
            commands[i] = random.Next(1, 10001); // Random command between 1 and 10000
        }

        // Act
        var result = robot.Move(commands);

        // Assert
        Assert.IsTrue(result);
    }
}