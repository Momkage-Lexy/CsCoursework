// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getTeamName() {
    fetch('/app_data/animals.txt') // Path to group names file
        .then(response => response.text())
        .then(data => {
            let namesArray = data.split('\n').map(name => name.trim()); // Split by new lines and trim any extra spaces
            
            // Function to shuffle the names array
            function shuffleArray(array) {
                for (let i = array.length - 1; i > 0; i--) {
                    const j = Math.floor(Math.random() * (i + 1));
                    [array[i], array[j]] = [array[j], array[i]]; // Swap elements
                }
                return array;
            }

            // Shuffle the names array randomly
            namesArray = shuffleArray(namesArray);

            let nameIndex = 0;

            // Get all group divs by their class name
            const groupDivs = document.querySelectorAll('.groupContent');

            // Loop through each group div and assign a random name
            groupDivs.forEach(div => {
                if (nameIndex < namesArray.length) {
                    div.innerText = namesArray[nameIndex]; // Assign a random name from the shuffled list
                    nameIndex++;
                }
            });
        })
        .catch(error => console.error('Error fetching file:', error));
}

