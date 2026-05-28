namespace CS20200Game

type Position =
    { Row: int
      Col: int }

type Direction =
    | Up
    | Down
    | Left
    | Right

type Wall =
    { Position: Position
      Direction: Direction }

module Entities =
    let movePosition deltaRow deltaCol position =
        { Row = position.Row + deltaRow
          Col = position.Col + deltaCol }

    let directionDelta direction =
        match direction with
        | Up -> (-1, 0)
        | Down -> (1, 0)
        | Left -> (0, -1)
        | Right -> (0, 1)

    let directionSymbol direction =
        match direction with
        | Up -> "↑"
        | Down -> "↓"
        | Left -> "←"
        | Right -> "→"

    let nextWallPosition wall =
        let deltaRow, deltaCol = directionDelta wall.Direction
        movePosition deltaRow deltaCol wall.Position
