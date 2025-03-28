// Header guard to prevent multiple inclusions of this header file
#ifndef COLORMAP_H
#define COLORMAP_H

// Structure to represent an RGB color
struct rgb {
    unsigned char red;  
    unsigned char green; 
    unsigned char blue;  
};

// Map a floating-point value to an RGB color based on colormap and specified value range
struct rgb value2color(float v, float vmin, float vmax);


#endif
