document.addEventListener("DOMContentLoaded", () => {
    // Get references to key elements in the DOM
    const searchButton = document.getElementById("search-button");
    const searchInput = document.getElementById("movie-search");
    const resultsContainer = document.getElementById("results-container");
    const errorMessage = document.getElementById("error-message");

    // Function to call the API and display movie results
    async function searchMovies() {
        // Get the user input and trim any leading/trailing spaces
        const query = searchInput.value.trim();

        if (!query) {
            // Display an error message if the input is empty
            errorMessage.textContent = "Please enter a movie title.";
            errorMessage.style.display = "block";
        } else {
            // Hide the error message if input is valid
            errorMessage.style.display = "none";
            console.log("Search term is valid:", query);

            // Clear previous results before displaying new ones
            resultsContainer.innerHTML = "";

            try {
                // Fetch data from the movies search API endpoint
                const response = await fetch(`/api/search/movies?query=${encodeURIComponent(query)}`, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json"
                    }
                });

                if (!response.ok) {
                    // Handle cases where the API returns an error
                    if (response.status === 404) {
                        resultsContainer.innerHTML = "<p>No movies found. Please try a different search.</p>";
                    } else {
                        throw new Error(`Error: ${response.status}`);
                    }
                    return;
                }

                // Parse the API response as JSON
                const data = await response.json();

                // Check if the data contains an array of movies
                if (Array.isArray(data) && data.length > 0) {
                    displayResults(data); // Pass the movie data to the display function
                } else {
                    // Show a message if no movies are found
                    resultsContainer.innerHTML = "<p>No movies found.</p>";
                }
            } catch (error) {
                // Handle any network or unexpected errors
                console.error("Error searching for movies:", error);
                resultsContainer.innerHTML = `<p>An error occurred: ${error.message}</p>`;
            }
        }
    }

    // Function to display movie results in the DOM
    function displayResults(movies) {
        // Sort movies by popularity in descending order
        movies.sort((a, b) => b.popularity - a.popularity);
        
        // Clear any existing results in the container
        const resultsContainer = document.getElementById("results-container");
        resultsContainer.innerHTML = "";

        // Loop through each movie and create UI elements
        movies.forEach(movie => {
            // Create a row container for each movie
            const row = document.createElement("div");
            row.className = "row mb-4 movie-item";
            row.style.cursor = "pointer";

            // Create a column for the poster image
            const posterCol = document.createElement("div");
            posterCol.className = "col-md-4";

            // Determine the image URL or use a placeholder if none is available
            const imageUrl = movie.backdropPath
                ? `https://image.tmdb.org/t/p/w500${movie.backdropPath}`
                : "https://via.placeholder.com/500x281?text=No+Image";
            const posterImg = document.createElement("img");
            posterImg.src = imageUrl;
            posterImg.alt = movie.Title || movie.title || "No Title";
            posterImg.style.width = "100%";
            posterImg.style.borderRadius = "8px";

            posterCol.appendChild(posterImg);

            // Truncate the description to 140 characters if it's too long
            const truncatedOverview = movie.Overview || movie.overview
                ? (movie.Overview || movie.overview).length > 140
                    ? (movie.Overview || movie.overview).slice(0, 140) + "..."
                    : movie.Overview || movie.overview
                : "No description available.";

            // Create a column for movie details
            const detailsCol = document.createElement("div");
            detailsCol.className = "col-md-8";
            detailsCol.innerHTML = `
                <h3>${movie.Title || movie.title || "No Title"}</h3>
                <p><strong>Release Date:</strong> ${movie.releaseDate}</p>
                <p>${truncatedOverview}</p>
            `;

            // Append the poster and details columns to the row
            row.appendChild(posterCol);
            row.appendChild(detailsCol);

            // Add a click event listener to open the modal with movie details
            row.addEventListener("click", () => {
                openMovieModal(movie);
            });

            // Append the row to the results container
            resultsContainer.appendChild(row);
        });
    }

    // Function to populate and display the modal with movie details
    async function openMovieModal(movie) {
        const modalPoster = document.getElementById("modal-poster");
        const modalDetails = document.getElementById("modal-details");
        const modalTitle = document.getElementById("movieModalLabel");

        // Set the modal's title
        modalTitle.textContent = movie.title;

        try {
            // Fetch movie details
            const detailsResponse = await fetch(`/api/search/details/${movie.id}`);
            if (!detailsResponse.ok) {
                throw new Error(`Failed to fetch details: ${detailsResponse.status}`);
            }
            const detailsData = await detailsResponse.json();

            // Fetch credits for the movie
            const creditsResponse = await fetch(`/api/search/${movie.id}/credits`);
            if (!creditsResponse.ok) {
                throw new Error(`Failed to fetch credits: ${creditsResponse.status}`);
            }
            const creditsData = await creditsResponse.json();

            // Set the poster image or a placeholder
            const imageUrl = detailsData.posterPath
                ? `https://image.tmdb.org/t/p/w500${detailsData.posterPath}`
                : "https://via.placeholder.com/500x281?text=No+Image";
            modalPoster.innerHTML = `<img src="${imageUrl}" alt="${movie.title}" style="width: 100%; border-radius: 8px;" />`;

            // Format genres and revenue
            const genres = detailsData.genres
                ? detailsData.genres.map(genre => genre.name).join(", ")
                : "N/A";
            const formattedRevenue = detailsData.revenue
                ? new Intl.NumberFormat("en-US", { style: "currency", currency: "USD" }).format(detailsData.revenue)
                : "Not Available";

            // Populate modal details with fetched data
            modalDetails.innerHTML = `
                <p><strong>Release Date:</strong> ${detailsData.releaseDate || "Not Available"}</p>
                <p><strong>Runtime:</strong> ${detailsData.runtime || "Not Available"}</p>
                <p><strong>Genres:</strong> ${genres}</p>
                <p><strong>Overview:</strong> ${detailsData.overview || "No description available."}</p>
                <p><strong>Revenue:</strong> ${formattedRevenue}</p>
                <p><strong>Popularity:</strong> ${detailsData.popularity || "Not Available"}</p>
                <hr>
                <h5>Cast</h5>
                <ul>
                    ${creditsData.cast
                        .slice(0, 10) // Limit to top 10 cast members
                        .map(
                            cast => `<li><strong>${cast.name}</strong> as ${cast.character || "Not Available"}</li>`
                        )
                        .join("")}
                </ul>
            `;
        } catch (error) {
            // Handle errors during modal population
            console.error("Error fetching movie details or credits:", error);
            modalDetails.innerHTML = `<p>An error occurred while fetching additional information.</p>`;
        }

        // Display the modal
        const movieModal = new bootstrap.Modal(document.getElementById("movie-modal"));
        movieModal.show();
    }

    // Attach an event listener to the search button
    searchButton.addEventListener("click", searchMovies);

    // Trigger search when the Enter key is pressed in the input field
    searchInput.addEventListener("keypress", (event) => {
        if (event.key === "Enter") {
            searchMovies();
        }
    });
});
