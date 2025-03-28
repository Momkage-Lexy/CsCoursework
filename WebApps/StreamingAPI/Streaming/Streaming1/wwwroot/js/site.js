// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Optimized with ChatGPT
// Add an event listener to the actor search input to detect 'keyup' events
document.getElementById('actorSearch')?.addEventListener('keyup', async function () {
    const name = this.value; // Get the current input value in the search bar
    console.log(name); // Log the name to the console for debugging
    if (name.trim() === "") {
        // Hide suggestions box if input is empty
        suggestions.style.display = 'none';
        shows.style.display = 'none';
        return;
    }
    try {
        // Fetch actor suggestions based on the input name, encoding it for safe URL usage
        const response = await fetch(`/api/search/actor?name=${encodeURIComponent(name)}`);
        
        // Check if the response is successful
        if (!response.ok) {
            throw new Error(`Response Status: ${response.status}`); // Throw an error if the response is not OK
        }

        const data = await response.json(); // Parse the response as JSON
        console.log(data); // Log the actor data for debugging
        
        displaySuggestions(data); // Call the function to display suggestions to the user
    } catch (error) {
        console.error(error.message); // Log any errors that occur during the fetch
    }
});

let showIds = []; // Array to store show IDs related to the selected actor

// Function to display actor suggestions below the search bar
function displaySuggestions(actors) {
    const suggestions = document.getElementById('suggestions');
    suggestions.innerHTML = ''; // Clear any previous suggestions

    // Display message if no actors are found
    if (actors.length === 0) {
        suggestions.innerHTML = '<div>No results found</div>';
        suggestions.style.display = 'block'; 
        return;
    }

    // Loop through each actor and create a suggestion element for them
    actors.forEach(actor => {
        const suggestion = document.createElement('div');
        suggestion.innerText = actor.fullName;  // Display the actor's full name
        suggestion.classList.add('suggestion-item'); // Add a CSS class for styling

        // Add a click event listener to each suggestion item
        suggestion.addEventListener('click', async () => {
            document.getElementById('actorSearch').value = actor.fullName; // Set the search bar text to the selected actor's name
            suggestions.innerHTML = ''; // Clear the suggestions list
            suggestions.style.display = 'none'; // Hide suggestions after selection

            // Store the actor's show IDs and fetch details for each show
            showIds = actor.showIds;
            await fetchAndDisplayShows(showIds); // Fetch and display shows associated with this actor
        });

        suggestions.appendChild(suggestion); // Add the suggestion to the suggestions container
    });
suggestions.style.display = 'block';
}

// Function to fetch and display shows based on an array of show IDs
async function fetchAndDisplayShows(showIds) {
    const showsContainer = document.getElementById('shows');
    showsContainer.innerHTML = ''; // Clear any previously displayed shows

    // Loop through each show ID and fetch details for each show
    for (const showId of showIds) {
        try {
            // Fetch show details based on the show ID
            const response = await fetch(`/api/actor/shows?id=${encodeURIComponent(showId)}`);
            
            // Check if the fetch was successful
            if (!response.ok) {
                throw new Error(`Failed to fetch show with ID ${showId}: ${response.statusText}`); // Throw an error if unsuccessful
            }

            const show = await response.json(); // Parse the response JSON
            displayShow(show); // Call function to display each show's details
        } catch (error) {
            console.error("Error fetching show:", error.message); // Log errors during fetch
        }
    }
}

// Function to display the details of a single show
function displayShow(show) {
    const showsContainer = document.getElementById('shows');
    // Make the #shows container visible when shows are available
    showsContainer.style.display = 'block';
    let showTypeText;
    if (show.showTypeId === 1) {
        showTypeText = "Show";
    } else if (show.showTypeId === 2) {
        showTypeText = "Movie";
    } else {
        showTypeText = "Unknown"; // Fallback in case of an unrecognized type
    }

    const ageCertificationMapping = {
        1: "TV-PG",
        2: "PG",
        3: "G",
        4: "PG-13",
        5: "R",
        6: "TV-G",
        7: "TV-Y",
        8: "TV-14",
        9: "NC-17",
        10: "TV-Y7",
        11: "TV-MA"
    };


    // Determine the display text for agecertificationid using the mapping
    const ageCertificationText = ageCertificationMapping[show.ageCertificationId] || "Unknown";

    // Log the mapped text to confirm the correct mapping is used
    console.log("Mapped ageCertificationText:", ageCertificationText);
    // Create a container for the show information
    const showInfo = document.createElement('div');
    showInfo.classList.add('show-item'); // Add a CSS class for styling
    showInfo.innerHTML = `
        <h3>${show.title}</h3>
        <p>Director: ${show.director}</p>
        <p>${show.description}</p>
        <p>Type: ${showTypeText}</p>
        <p>Rating: ${ageCertificationText}</p>
        <p>Release Year: ${show.releaseYear}</p>
        <p>Runtime: ${show.runtime}</p>
        <p>IMDb Rating: ${show.imdbScore}</p>
        <p>TMDB Rating: ${show.tmdbScore}</p>
    `;

    showsContainer.appendChild(showInfo); // Append the show information to the shows container
}


// TO DO: List shows in order


// Function to toggle the form mode between adding a new person and updating an existing person
function toggleFormMode() {
    // Get the value of the selected radio button (either "add" or "update")
    const formMode = document.querySelector('input[name="formMode"]:checked').value;

    // Get the form sections for adding and updating
    const addFields = document.getElementById("addFields");
    const updateFields = document.getElementById("updateFields");

    if (formMode === "add") {
        // Show fields for adding a new person
        addFields.style.display = "block";  // Display JustWatchPersonId and FullName inputs
        updateFields.style.display = "none"; // Hide the ID field
        document.getElementById("personId").value = "0"; // Set the ID to 0, indicating a new person
    } else if (formMode === "update") {
        // Show fields for updating an existing person
        addFields.style.display = "none"; // Hide the JustWatchPersonId input
        updateFields.style.display = "block"; // Display the ID and FullName inputs for updating
      //  document.getElementById("personId").value = ""; // Clear the ID field for user input
    }
}

// Function to handle form submission for both adding and updating a person
async function handleFormSubmit(event) {
    event.preventDefault();

    const personId = document.getElementById("personId").value;
    const justWatchPersonId = document.getElementById("justWatchPersonId").value;
    const fullName = document.getElementById("fullName").value;

    const personData = {
        Id: personId,
        JustWatchPersonId: justWatchPersonId,
        FullName: fullName
    };

    try {
        const response = await fetch(`/api/actor/${personId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(personData)
        });
        console.log("personId:", personId); // Ensure it outputs a valid ID

        const responseMessage = document.getElementById("responseMessage");

        if (response.ok) {
            responseMessage.innerText = personId === "0" ? "Person created successfully!" : "Person updated successfully!";
            responseMessage.style.color = "green";
        } else {
            // Handle response with no JSON content or partial content
            const errorData = await response.json().catch(() => ({ detail: "An error occurred." }));
            responseMessage.innerText = errorData.detail || "An error occurred.";
            responseMessage.style.color = "red";
        }
    } catch (error) {
        console.error("Error:", error);
        document.getElementById("responseMessage").innerText = "An error occurred. Please try again.";
    }
}
