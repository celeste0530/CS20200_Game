# 🧱 Wall Survival Game

> An F# console survival game where closing walls threaten your every move.

---

## 🎮 Game Overview

You control a character on a grid. Every time you move, the walls surrounding the play area close in by one cell. Survive as long as possible by moving strategically — and watch out for wall collisions that create safe zones!

```
###########
#.........#
#..#####..#
#..#.@.#..#   @ = Player
#..#####..#
#.........#
###########
```

---

## 📋 Rules

- The player starts in the center of the map
- **Every move**, all 4 walls close in by 1 cell toward the player
- If the player moves into a wall → **Game Over**
- If two walls close into the **same cell simultaneously** → the walls cancel out and that cell becomes a **Safe Zone**
- The goal is to **survive as long as possible**

---

## 🕹️ Controls

| Key | Action |
|-----|--------|
| `W` / `↑` | Move Up |
| `S` / `↓` | Move Down |
| `A` / `←` | Move Left |
| `D` / `→` | Move Right |
| `Q` | Quit |

---

## 🎯 Game Modes

| Mode | Description |
|------|-------------|
| **Infinite** | Survive as long as possible. No time limit. |
| **Time Attack** | Score as many moves as possible within a time limit. |
| **Move Limit** | Survive within a fixed number of moves. |

---

## 🔮 Planned Features

- [ ] Wall-breaking item — pick up items to destroy adjacent walls
- [ ] Blinking walls — walls toggle on/off each turn for a rhythm-based challenge
- [ ] Difficulty settings — adjust initial map size and wall speed
- [ ] High score leaderboard

---

## 🛠️ How to Run

### Requirements
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)

### Steps

```bash
git clone https://github.com/celeste0530/CS20200_Game.git
cd CS20200_Game
dotnet run --project src/Game
```

---

## 📁 Project Structure

```
CS20200_Game/
├── README.md
├── .gitignore
├── FSharpWallGame.sln
└── src/
    └── Game/
        ├── Types.fs        # Core types (GameState, Cell, Wall, etc.)
        ├── Map.fs          # Map rendering and cell state management
        ├── Wall.fs         # Wall movement and collision logic
        ├── Player.fs       # Player movement and death detection
        ├── GameMode.fs     # Infinite / Time Attack / Move Limit modes
        ├── GameLoop.fs     # Input → Update → Render loop
        └── Program.fs      # Entry point and main menu
```

---

## 📝 License

MIT License — see [LICENSE](LICENSE) for details.
