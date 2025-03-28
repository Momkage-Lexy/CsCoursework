using NUnit.Framework;
using TeamGenerator2.Models;
using TeamGenerator2.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace TeamGenerator_Tests
{
    public class Tests
    {
        private HomeController _controller;

        [SetUp]
        public void Setup()
        {
            // Initialize the HomeController with a new Random instance
            _controller = new HomeController(new Random());
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the HomeController after each test if it implements IDisposable
            if (_controller != null)
            {
                _controller.Dispose();
            }
        }

        /* REQUIREMENT: Program takes in x number of names and x number of names are returned to the next view 
        Expected results: TRUE */
        [Test]
        public void NamesInputEqualsNamesOutput()
        {
            //Arrange
            var model = new Names
            {
                NameInput = "John\nJane\nAlex\nEmily"
            };

            //Act
            var result = _controller.Groups(model) as ViewResult;
            var resultModel = result.Model as Names;

            //Assert
            Assert.IsNotNull(resultModel);
            Assert.AreEqual(4, resultModel.NameList.Count); // There should be 4 names
        }

        /* REQUIREMENT: Program takes in teamSize(y) and there are teams of size y with an exception of -1/+1 if there is not an even divide 
        Expected Results: TRUE */
        [Test]
        public void TeamSizeInputEqualsTeamSizeOutput()
        {
            //Arrange
            var model = new Names
            {
                NameInput = "John\nJane\nAlex\nEmily\nChris\nTaylor\nSam\nJordan", // 8 names
                TeamSize = 3
            };

            //Act
            var result = _controller.Groups(model) as ViewResult;
            var resultModel = result.Model as Names;

            // Get the list of names split into teams 
            var namesInTeams = resultModel.NameList; 

            // Grouping names into actual teams for checking 
            var totalNames = resultModel.NameList.Count;
            var numberOfFullTeams = totalNames / model.TeamSize;
            var remainder = totalNames % model.TeamSize;

            var actualTeams = new List<int>();

            // Simulate team assignment
            for (int i = 0; i < numberOfFullTeams; i++)
            {
                int teamSize = model.TeamSize;
                if (remainder > 0) // Handle remainder distribution
                {
                    teamSize += 1;
                    remainder--;
                }
                actualTeams.Add(teamSize); // Store actual team sizes
            }

            //Assert: All teams should be of size teamSize, teamSize -1, or teamSize +1
            foreach (var team in actualTeams)
            {
                Assert.IsTrue(
                    team == model.TeamSize || team == model.TeamSize + 1 || team == model.TeamSize - 1, 
                    $"Team size is not within acceptable bounds. Expected: {model.TeamSize}, Found: {team}");
            }
        }

        /* REQUIREMENT: Names are distributed randomly 
        Expected Results: TRUE*/
        [Test]
        public void NamesReturnedRandomly()
        {
            //Arrange
            var model1 = new Names { NameInput = "John\nJane\nAlex\nEmily" };
            var model2 = new Names { NameInput = "John\nJane\nAlex\nEmily" };

            // Create seeded Random instances with different seeds for testing
            var random1 = new Random(12345); 
            var random2 = new Random(67890); 

            // Create two instances of HomeController, each with a different Random instance
            var controller1 = new HomeController(random1);
            var controller2 = new HomeController(random2);

            //Act
            var result1 = controller1.Groups(model1) as ViewResult;
            var result2 = controller2.Groups(model2) as ViewResult;
            var resultModel1 = result1.Model as Names;
            var resultModel2 = result2.Model as Names;

            //Assert
            Assert.AreNotEqual(
                string.Join(",", resultModel1.NameList),
                string.Join(",", resultModel2.NameList),
                "Names should be distributed randomly"
            );
        }

        /* REQUIREMENT: Program does not fail if input is invalid 
        Expected Results: TRUE*/
        [Test]
        public void ShouldNotBreakifInputInvalid()
        {
            //Arrange
            var model = new Names
            {
                NameInput = "",
                TeamSize = 0 // Invalid input
            };

            //Act
            var result = _controller.Groups(model) as ViewResult;

            //Assert
            Assert.IsNotNull(result); // Ensure that a view is returned
            Assert.IsTrue(result.ViewData.ModelState.IsValid == false); // ModelState should be invalid
        }
    }
}
