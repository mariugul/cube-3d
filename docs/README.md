# Cube 3D 
[![License badge](https://img.shields.io/github/license/mariugul/cube-3d)](https://github.com/mariugul/cube-3d/blob/develop/docs/LICENSE)
[![GitHub last commit](https://img.shields.io/github/last-commit/mariugul/cube-3d)](https://github.com/mariugul/cube-3d/commits/develop)
[![Issues](https://img.shields.io/github/issues/mariugul/cube-3d)](https://github.com/mariugul/cube-3d/issues)
[![Discord badge](https://img.shields.io/discord/710895026435260556)](https://discord.com/invite/ZgxjkC2)

This is an application for generating code for a LED cube. It generates the necessary code for programming the LED cube with either [Arduino](https://www.arduino.cc/en/Main/Software) or C, using [Atmel Studio](https://www.microchip.com/mplab/avr-support/atmel-studio-7). Currently, this works for a 4x4x4 LED cube and will eventually include other sizes. The Arduino/Atmega328 code and details can be found in the [LED-Cube-Code](https://github.com/mariugul/LED-Cube-Code) repository. The application will be simulating the LED cube as well in the future.

[<img src="images/cube3d.png" alt="cube.png" width=""/>](https://github.com/mariugul/cube-3d/blob/develop/docs/images/cube3d.png) 

## Table of Contents  
* [Installing](#Installing)  
* [Usage](#Usage)
* [YouTube Tutorials](#YouTubeTutorials)
* [Future Improvements](#FutureImprovements)
* [Help and Contributing](#HelpContributing)  
* [License](#License)  
* [Authors](#Authors)
* [Acknowledgement](#Acknowledgement)

<a name="Installing"/>

## Installing
Download the latest application (.exe) from [releases](https://github.com/mariugul/cube-3d/releases) and follow the install wizard. It works for Windows 10 and possibly earlier Windows OS'es (not tested).

<a name="Usage"/>

## Usage
The application generates a pattern table for a 4x4x4 LED cube and makes it easy to visualize the patterns. The code for programming the LED cube comes with the application and also exist in its own repository [LED-Cube-Code](https://github.com/mariugul/LED-Cube-Code). 

### Code Editor
[<img src="images/cube3d.png" alt="cube.png" width=""/>](https://github.com/mariugul/cube-3d/blob/develop/docs/images/cube3d.png) 

### Export

### Settings

### File

<a name="YouTubeTutorials"/>

## YouTube Tutorials
Watch the YouTube tutorials: (coming soon)
* [1. LED Cube Intro]()
* [2. LED Cube Theory]()
* [3. LED Cube Wiring]()
* [4. LED CUBE Coding]()
* [5. LED Cube App]()

<a name="FutureImprovements"/>

## Future Improvements
Some of the things on the list for future improvements and features are:
* Simulate the cube. This is halfway implemented already and will probably come in a new release.
* Display the wiring of the cube (planes/columns/LED numbering).
* Change cube color with color picker.
* Improve the zooming functionality.
* Functionality for saving projects and keeping patterns saved in the application.
* Adding example patterns to the application.

<a name="HelpContributing"/>

## Help and Contributing

Check out the [Discord](https://discord.com/invite/ZgxjkC2) server if you need help with the code not working or if you have suggestions for improvement! The [YouTube]() channel has video tutorials to help out as well. (YouTube videos coming soon)

<a name="License"/>

## License
This project is licensed under the MIT license and is open source. You are free to use this project as you wish as long as you credit the work. See the [LICENSE](LICENSE) file for details. I would highly appreciate if you contributed to the project that you share it so this can be a big open source project!


<a name="Authors"/>

## Authors
<img src="https://lh3.googleusercontent.com/fqYJHtyzZzA4vacRzeJoB93QNvA5-mvR-8UB5oVLxdYDSTpfLp_KgYD4IqVGJUgFEJo" alt="" width="15"/> [Marius C. K. Gulbrandsen](https://www.linkedin.com/in/marius-c-k-gulbrandsen-963a69130/) 

<a name="Acknowledgement"/>

## Acknowledgement
Big thanks to my brother [Stig Thomas Gulbrandsen](https://github.com/ribbreaker) for helping with Unity and C# issues, it has been much appreciated. 
Also, thank you to [Denis](https://github.com/Meragon) who created a [Unity-WinForms](https://github.com/Meragon/Unity-WinForms) wrapper. He has been immensely helpful with getting WinForms to work.
