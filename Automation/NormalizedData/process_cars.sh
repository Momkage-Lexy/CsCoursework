#!/bin/bash
# Lexy Ramos

# Define input and output files
input_file="cars2.txt"
output_file="output.txt"

# Write CSV header to the output file
echo "Vehicle,Description,Starting MSRP,Average Price Paid,Invoice,Overall Rating,Basic Warranty,Drivetrain Warranty,Roadside Assistance,Bluetooth,Navigation,Satellite Radio,Keyless Ignition,Cruise Control,Adaptive Cruise Control,Parking Assistance,Upholstery,Heated Seats,Sunroof,Drivetrain,Engine Power,Engine Torque,Engine Displacement,Transmission,Tire Size,Wheel Size,City/Hwy/Combined MPG,Fuel Capacity,Curb Weight,Ground Clearance,Seating Capacity" > "$output_file"

# Function to process a car and append to the output file
process_car() {
    local car_number=$1
    local start_line=$2

    echo "Processing $car_number..."

    # Read and process the car data into an array
    mapfile -t values < <(grep -v '^[[:space:]]*$' "$input_file" | \
    awk -v start_line="$start_line" '
    (NR < 40 || NR > 104) && (NR>=start_line && NR<start_line+2 || (NR-start_line+7)%8==0) {
        gsub(",", ";", $0);  # Replace commas in values with semicolons to avoid conflicts
        print $0;
    }')

    # Ensure the array has the correct number of elements for the headers
    while [ "${#values[@]}" -lt 31 ]; do
        values+=("") # Add empty values for missing data
    done

    # Write to the output file
    printf '%s\n' "$car_number,${values[*]}" | sed 's/ /,/g' >> "$output_file"
}

# Process each car by specifying the start line
process_car "Car 1" 1
process_car "Car 2" 3
process_car "Car 3" 5
process_car "Car 4" 7

echo "Data extraction complete. Results saved to $output_file"

