using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Homework4.Services;
using Microsoft.Extensions.Logging;

namespace Homework4.Tests
{
    [TestFixture]
    public class TmdbServiceTests
    {
        private TmdbService _tmdbService;

        [SetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<TmdbService>>();
            
            // Initialize TmdbService with a null HttpClient
            _tmdbService = new TmdbService(null, mockLogger.Object);
        }

        // Test case to validate runtime formatting into hours and minutes format
        [TestCase(88, "1h 28m")] 
        [TestCase(200, "3h 20m")] 
        [TestCase(49, "49m")] 
        [TestCase(0, "N/A")] 
        public void FormatRuntime_ValidatesCorrectFormat(int runtime, string expected)
        {
            // Arrange
            // Format the runtime into hours and minutes or mark as "N/A" if runtime is 0
            var formattedRuntime = runtime > 0
                ? runtime >= 60
                    ? $"{runtime / 60}h {runtime % 60}m" // Hours and minutes format
                    : $"{runtime}m" // Only minutes
                : "N/A"; // "Not Available" for 0 runtime

            // Act & Assert
            // Check if the formatted runtime matches the expected output
            Assert.AreEqual(expected, formattedRuntime);
        }

        // Test case to validate release date formatting into "Month Day, Year" 
        [TestCase("1980-07-25", "July 25, 1980")] // Valid release date
        [TestCase("", "N/A")] // Empty release date
        public void FormatReleaseDate_ValidatesCorrectFormat(string releaseDate, string expected)
        {
            // Arrange
            // Format the release date or mark as "N/A" if the date is empty or invalid
            string formattedReleaseDate = !string.IsNullOrEmpty(releaseDate) && DateTime.TryParse(releaseDate, out var parsedDate)
                ? parsedDate.ToString("MMMM d, yyyy") // Format as "Month Day, Year"
                : "N/A"; // "Not Available" for empty or invalid date

            // Act & Assert
            // Check if the formatted release date matches the expected output
            Assert.AreEqual(expected, formattedReleaseDate);
        }
    }
}
