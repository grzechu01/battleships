# Battleships - simple battleships game

A simple version of the game Battleships. It allows a single human player to play a one-sided game of Battleships against ships placed by the computer.

You can play on a 10x10 grid, you need to find several ships on the grid at random with the following sizes:

- 1x Battleship (5 squares)
- 2x Destroyers (4 squares)

The player enters coordinates of the form “A5”, where "A" is the column and "5" is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

To exit, use Ctrl + C.

 Application build process requires .NET Core 2.2 SDK to be installed on your computer.

In order to play, clone repository, go to directory with application ("BattleshipsApp"), run

- dotnet build
- dotnet run


Have a nice play!
