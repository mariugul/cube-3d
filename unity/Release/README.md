# Release
This folder is meant for making a ready release of the application with an installer. The install wizard used is called [Inno Setup](https://jrsoftware.org/isinfo.php). 

## Make Release
* All the contents of the `builds` folder from Unity has to be put into the release folder when the project is compiled. There is just one small detail, the folders `Cube 3D_data` and `MonoBleedingEdge` needs to be put inside empty folders with the same name. This is something to do with Inno Setup so it can find the files. 

* After placing these folders, place `Cube 3D.exe`, `UnityCrasHandler64.exe` and `UnityPlayer.dll` on the top level of the release folder. 

* When all content has been placed with the newest build, open `Script.iss` in Inno Setup Compiler.

* Run the compiler, and a `Cube 3D Setup (x64).exe` should be created.

* Upon a new release, attach the binary `Cube 3D Setup (x64).exe` and the release is ready.