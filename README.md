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

## Use LLM

Large Language Models were used as an assistant during this project.

- LLM assistance was used to draft and revise F# code for the game and README documentation.
- When the game rules and desired behavior were provided, LLM assistance was used to translate them into F# game code.
- When LLM-generated code did not behave as expected, the developer suggested which parts might be incorrect and provided the expected behavior so that LLM assistance could identify the cause.
- During this process, the developer also suggested parts that seemed incorrect.
- When testing edge cases, LLM assistance was used to create sample cases and test whether the game logic handled them correctly.
