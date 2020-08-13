#ifndef __PATTERN_H__
#define __PATTERN_H__

// Includes
//---------------------------------
#include <stdint.h>       
#include <avr/pgmspace.h>  

// Pattern that LED cube will display
//--------------------------------- 
const PROGMEM uint16_t pattern_table[] = {
    0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 250,
    0x0000, 0x0000, 0x0000, 0x0000, 250,
    0x0000, 0x0000, 0x0010, 0x0000, 10,
};
#endif
