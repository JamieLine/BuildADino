This is the full source code for BuildADino, a small game I abandoned as life got in the way and I felt I had learned what I was trying to learn from the project. 

This work is presented as a demonstration that I have written C# code as a hobbyist, with the following caveats:
    1. This work was never originally intended to be distributed. While the code is not written with reckless abandon, this project is noticeably missing documentation and many features.
        b. On my GitHub page at https://github.com/JamieLine/ I have code available in other languages which is written with a greater degree of care, including my automated testing framework, UnmaskedCXX.
    2. This game was only ever written for the sake of refreshing my knowledge with the C# ecosystem. Mainly, investigating Visual Studio Code's support for the language, and seeing how MonoGame worked on a modern version of C#. The previous time I had used MonoGame, it was still called XNA.
    3. I am aware that if this were a production project there should be linters, CI/CD, and automatic documentation. My current project, UnmaskedCXX, has the linters already (which are even configured to run on every commit and only allow suitable code) and automatic documentation is on the GitHub Issues list. The CI/CD is missing as the project is not yet large enough to justify the cost.
    4. This project is only tested under Windows 11 x64. Theoretically everything should work on other platforms, but this game was developed on and for Windows.

Features:
The game launches with a graphical window displaying a fixed "map" of tiles.
Each tile updates a hidden counter for how many resources the player owns regularly without input.
Those tiles can be selected with a mouse click, and display a counter for how many times they have been upgraded (UCount)
Once a tile is selected, pressing Enter will attempt to upgrade them
    If the player has insufficient resources, the attempt will silently fail.
        There is currently no way to see the resources you have or require for this, this project was abandoned quickly.

I would like to re-iterate that this project is being distributed as evidence of my ability to write basic C#, and that there was no attempt at any true "software engineering" here beyond effectively mashing MonoGame's prebuilt functionality together.
My current project, UnmaskedCXX, is where I try to actually solve a real problem with some semblance of good design, which is often changing as it is a small project running in a pseudo-Agile methodology. At the time of writing, the main parsing engine for UnmaskedCXX is being re-written to use a recursive descent parser, in the branch labelled "Micro_Parsers".

To run the game, run Build.bat and then the final executable will be found under the `bin` directory. This software is shipped with no warranties or guarantees.