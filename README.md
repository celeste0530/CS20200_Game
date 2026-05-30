# CS20200_Game

CLI Wall Survival Game written in F#.

## How to run

```bash
dotnet run
```

Enter a grid size, then move with `W`, `A`, `S`, `D`, or the arrow keys. Movement keys are handled immediately, so you do not need to press Enter after each move.

The default grid size is `9`. The minimum grid size is `3`.

## Project structure

- `CS20200_Game.fsproj`: F# project configuration.
- `Program.fs`: Program entry point and command-line option handling.
- `Game.fs`: Main game loop, player input, wall movement, collision handling, and game-over logic.
- `Entities.fs`: Shared data types for positions, directions, and walls.
- `Board.fs`: Terminal board rendering.
- `HighScore.fs`: High score loading, saving, listing, and resetting.

## High scores

Best scores are saved separately for each grid size in `high_scores.txt`.

Show saved high scores:

```bash
dotnet run -- --show-scores
```

Reset saved high scores:

```bash
dotnet run -- --reset-scores
```

## Rules

- The player starts at the center of an `N x N` grid.
- The player is shown as `p`.
- Empty cells are shown as `.`.
- Walls are shown as `↑`, `↓`, `←`, and `→`.
- The outer one-cell ring previews where the next wall will enter.
- Between `1` and `N - 1` preview walls are created every turn.
- Every wall, including preview walls, moves one cell in its arrow direction each turn.
- The player survives by avoiding walls for as many turns as possible.
- If two adjacent walls move across each other's positions, they destroy each other while crossing.
- If multiple walls move into the same cell, they destroy each other.
- A wall disappears when it moves outside the grid.
- The game ends when a wall reaches the player or crosses paths with the player.
- The final score is the number of turns survived.
- Best scores are saved separately for each grid size in `high_scores.txt`.

## Details of requirements.md

The following details are clarified after the original requirements document was written. The main game remains the same: the player moves inside the grid and survives by avoiding randomly generated moving walls.

- An outer one-cell preview ring was added to show where new walls will enter on the next turn. The original requirements stated that walls are randomly produced along the boundary, but displaying a wall only after it entered the grid gave the player no way to anticipate it. The preview ring makes the existing wall generation rule visible and the game behavior easier to understand.
- When two adjacent walls move across each other's previous positions during the same turn, both walls are destroyed. The original requirements already specified that walls moving into the same cell are destroyed. This additional case makes the collision rule consistent when two walls pass through each other between cells.
- A high-score file and commands for listing and resetting scores were added as optional features. They do not change the original final score rule.

## Use LLM

Large Language Models were used as an assistant during this project.

- LLM assistance was used to draft and revise F# code for the game and README documentation.
- When the game rules and desired behavior were provided, LLM assistance was used to translate them into F# game code.
- When LLM-generated code did not behave as expected, the developer identified the relevant cases and provided the expected results before asking the LLM to revise the implementation.
- The main issue that required correction was wall collision handling. In particular, the implementation needed to handle walls moving into the same cell, adjacent walls crossing each other, walls leaving the grid, and collisions between a player and a wall that exchange positions during one turn.
- The developer reviewed the generated code and used concrete edge cases to check the order in which wall collisions and wall movement are resolved.
- LLM assistance was also used to create sample tests for these edge cases and verify the resulting behavior.
