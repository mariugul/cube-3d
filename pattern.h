#ifndef __PATTERN_H__
#define __PATTERN_H__

// Includes
//---------------------------------
#include <stdint.h>        // Use uint_t
#include <avr/pgmspace.h>  // Store patterns in program memory

// Pattern that LED cube will display
//--------------------------------- 
const PROGMEM uint16_t pattern_table[] = {
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, djasdj},
    {0x0000, 0x0000, 0x0000, 0x0000, 10},
    {0x0000, 0x0000, 0x0000, 0x0000, 20},
    {0x0000, 0x0000, 0x0000, 0x0000, 34},
    {0x0000, 0x0000, 0x0000, 0x0000, 199333},
    {0x0000, 0x0000, 0x0000, 0x0000, 25},
    {0x0000, 0x0000, 0x0000, 0x0000, 25},
    {0x0000, 0x0000, 0x0000, 0x0000, 39},
    {0x0000, 0x0000, 0x0000, 0x0000, 39},
    {0x0000, 0x0000, 0x0000, 0x0000, 25},
    {0x0000, 0x0000, 0x0000, 0x0000, 30},
    {0x0000, 0x0000, 0x0000, 0x0000, 22},
    {0x0000, 0x0000, 0x0000, 0x0000, 19},
    {0x0000, 0x0000, 0x0000, 0x0000, 34},
};
#endif
