namespace CS20200Game

open System

type GameState =
    { Size: int
      SpawnChance: float
      Player: Position
      Walls: Wall list
      PendingWalls: Wall list
      Score: int }

type private WallMovement =
    { Previous: Wall
      Moved: Wall }

module Game =
    let private random = Random()

    let private moveKeys =
        Map.ofList
            [ ConsoleKey.W, (-1, 0)
              ConsoleKey.A, (0, -1)
              ConsoleKey.S, (1, 0)
              ConsoleKey.D, (0, 1)
              ConsoleKey.UpArrow, (-1, 0)
              ConsoleKey.LeftArrow, (0, -1)
              ConsoleKey.DownArrow, (1, 0)
              ConsoleKey.RightArrow, (0, 1) ]

    let private createPendingWalls size spawnChance =
        if random.NextDouble() > spawnChance then
            []
        else
            let wallCount = random.Next(1, size)

            [ for _ in 1 .. wallCount do
                  let side = random.Next(4)
                  let index = random.Next(size)

                  match side with
                  | 0 -> { Position = { Row = -1; Col = index }; Direction = Down }
                  | 1 -> { Position = { Row = size; Col = index }; Direction = Up }
                  | 2 -> { Position = { Row = index; Col = -1 }; Direction = Right }
                  | _ -> { Position = { Row = index; Col = size }; Direction = Left } ]

    let create size spawnChance =
        if size < 3 then
            invalidArg (nameof size) "Grid size must be at least 3."

        { Size = size
          SpawnChance = spawnChance
          Player = { Row = size / 2; Col = size / 2 }
          Walls = []
          PendingWalls = createPendingWalls size spawnChance
          Score = 0 }

    let rec private readNextPlayerPosition state =
        printf "Move (W/A/S/D or arrow keys): "
        let key = Console.ReadKey(false).Key

        match Map.tryFind key moveKeys with
        | None ->
            printfn "Invalid input. Use W, A, S, D, or arrow keys."
            readNextPlayerPosition state
        | Some(deltaRow, deltaCol) ->
            let nextPosition = Entities.movePosition deltaRow deltaCol state.Player

            if Board.isInside state.Size nextPosition then
                nextPosition
            else
                printfn "You cannot move outside the grid."
                readNextPlayerPosition state

    let private moveWalls size walls =
        walls
        |> List.choose (fun wall ->
            let nextPosition = Entities.nextWallPosition wall

            if Board.isInside size nextPosition then
                Some
                    { Previous = wall
                      Moved = { wall with Position = nextPosition } }
            else
                None)

    let private removeCrossingWallsBeforeMove walls =
        let crossed first second =
            first.Position = Entities.nextWallPosition second
            && Entities.nextWallPosition first = second.Position

        walls
        |> List.filter (fun wall ->
            let crossingCollision =
                walls
                |> List.exists (fun other ->
                    other <> wall
                    && crossed wall other)

            not crossingCollision)

    let private removeSameCellCollidedWalls movements =
        let positionCounts =
            movements
            |> List.countBy _.Moved.Position
            |> Map.ofList

        movements
        |> List.filter (fun movement ->
            Map.find movement.Moved.Position positionCounts = 1)

    let private resolveWallMovements size walls =
        walls
        |> removeCrossingWallsBeforeMove
        |> moveWalls size
        |> removeSameCellCollidedWalls

    let resolveWalls size walls =
        resolveWallMovements size walls
        |> List.map _.Moved

    let private crossedPath previousPlayer nextPlayer survivingMovements =
        survivingMovements
        |> List.exists (fun wall ->
            wall.Previous.Position = nextPlayer
            && wall.Moved.Position = previousPlayer)

    let private gameOverReason previousPlayer nextPlayer survivingMovements =
        if survivingMovements |> List.exists (fun wall -> wall.Moved.Position = nextPlayer) then
            Some "The player hit a wall."
        elif crossedPath previousPlayer nextPlayer survivingMovements then
            Some "The player hit a wall."
        else
            None

    let rec run highScoreStore state =
        Board.draw state.Size state.Player state.Walls state.PendingWalls state.Score

        let previousPlayer = state.Player
        let nextPlayer = readNextPlayerPosition state
        let previousWalls = state.PendingWalls @ state.Walls
        let survivingMovements = resolveWallMovements state.Size previousWalls
        let survivingWalls = survivingMovements |> List.map _.Moved
        let nextPendingWalls = createPendingWalls state.Size state.SpawnChance

        match gameOverReason previousPlayer nextPlayer survivingMovements with
        | Some reason ->
            let bestScore = HighScore.saveIfBest highScoreStore state.Size state.Score
            Board.draw state.Size nextPlayer survivingWalls [] state.Score
            printfn "Game Over: %s" reason
            printfn "Score: %d" state.Score
            printfn "Best score for %dx%d grid: %d" state.Size state.Size bestScore
        | None ->
            run
                highScoreStore
                { state with
                    Player = nextPlayer
                    Walls = survivingWalls
                    PendingWalls = nextPendingWalls
                    Score = state.Score + 1 }
