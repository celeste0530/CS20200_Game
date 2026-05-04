Project Title: CLI Wall Survival Game

Overview:
This project is a command-line survival game where the player tries to survive as long as possible in a grid while walls continuously move in predetermined directions. The player must move to avoid walls.

Requirements:
1. The game initializes a grid of size N * N is displayed in the terminal with an outer boundary and player is placed at the center cell.
2. The player is displayed with ‘p’, the walls are displayed with arrow characters (↑, ↓, ←, →), the empty cells are displayed with dots.
2. Player can move with four inputs (W, A, S, D) and if player tries to move outside the grid, the move is ignored and the player can enter the input again.
3. Wall is randomly produced along the boundary cells of the grid and after the player moves, all walls move one step in their determined direction. If the wall meets the opposite side of the grid and passes it, then the wall disappears. And if two or more walls move into the same cell, all walls are destroyed and that cell becomes empty. It means, if the player moves to the cell where the wall collision occurs, the player survives.
4. The game is over if the player meets the wall in the same cell. Also, the game ends if the player and a wall move into each other's previous positions during the same turn.
5. When the game ends, final score which is number of turns that the player survives is displayed.
