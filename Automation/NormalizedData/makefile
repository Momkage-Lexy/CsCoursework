# Makefile for Lexy Ramos' process_cars.sh solution

# Variables
SHELL_SCRIPT = process_cars.sh
INPUT_FILE = cars2.txt
README = README.md
OUTPUT_FILE = output.txt
ZIP_FILE = deployment_package.zip

# Default action
help:
	@echo "Lexy Ramos"
	@echo "To build the deployment package: make build"
	@echo "To deploy the package: make deploy"

# Build action: Bundle solution assets into a ZIP file
build: $(ZIP_FILE)

$(ZIP_FILE): $(SHELL_SCRIPT) $(INPUT_FILE) $(README)
	@echo "Bundling solution assets into $(ZIP_FILE)..."
	zip -r $(ZIP_FILE) $(SHELL_SCRIPT) $(INPUT_FILE) $(README)
	@echo "Build complete: $(ZIP_FILE)"

# Deploy action: Commit ZIP file to a git repository
deploy: build
	@echo "Deploying the package to the git repository..."
	git add $(ZIP_FILE)
	git commit -m "Deploy solution: $(ZIP_FILE)"
	git push origin main
	@echo "Deployment complete."

# Sample input file (to ensure it exists for testing)
$(INPUT_FILE):
	@echo "Sample input file not found. Creating a default $(INPUT_FILE)..."
	echo "Sample car data for testing..." > $(INPUT_FILE)

# README file (to ensure it exists)
$(README):
	@echo "README.md not found. Creating a default README.md..."
	@echo "# Solution for process_cars.sh" > $(README)
	@echo "This solution processes car data from $(INPUT_FILE) and outputs results to $(OUTPUT_FILE)." >> $(README)
	@echo "## How to Use" >> $(README)
	@echo "1. Ensure your input file is named '$(INPUT_FILE)' and formatted correctly." >> $(README)
	@echo "2. Run the shell script with './$(SHELL_SCRIPT)' to generate the output file." >> $(README)
	@echo "3. Use this Makefile for building and deploying the solution package." >> $(README)

# Clean action to remove build artifacts (optional)
clean:
	@echo "Cleaning up build artifacts..."
	rm -f $(ZIP_FILE)
	@echo "Clean complete."
