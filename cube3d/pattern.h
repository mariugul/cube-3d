#ifndef __PATTERN_H__
#define __PATTERN_H__

// Includes
//---------------------------------
#include <stdint.h>        // Use uint_t
#include <avr/pgmspace.h>  // Store patterns in program memory

// Pattern that LED cube will display
//--------------------------------- 
const PROGMEM uint16_t pattern_table[] = {
     0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10,
     0x0000, 0x0000, 0x0000, 0x0000, 10,
     0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 100000,
     0x0000, 0x0000, 0x0000, 0x0000, 10,
     0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 10,
     0x0000, 0x0000, 0x0000, 0x0000, 10,
};
#endif
