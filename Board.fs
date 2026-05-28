namespace CS20200Game

module Board =
    let private emptyCell = "."
    let private playerSymbol = "p"

    let isInside size position =
        position.Row >= 0
        && position.Row < size
        && position.Col >= 0
        && position.Col < size

    let draw size player walls pendingWalls score =
        let wallSymbols =
            walls
            |> List.map (fun wall -> wall.Position, Entities.directionSymbol wall.Direction)
            |> Map.ofList

        let pendingWallSymbols =
            pendingWalls
            |> List.map (fun wall -> wall.Position, Entities.directionSymbol wall.Direction)
            |> Map.ofList

        let cellAt position =
            if position = player then
                playerSymbol
            else
                match Map.tryFind position wallSymbols with
                | Some symbol -> symbol
                | None ->
                    match Map.tryFind position pendingWallSymbols with
                    | Some symbol -> symbol
                    | None -> emptyCell

        let innerCells row =
            [ for col in 0 .. size - 1 do
                  cellAt { Row = row; Col = col } ]
            |> String.concat "  "

        let border = "   +" + String.replicate (size * 3) "-" + "+"

        printfn ""

        printfn "     %s" (innerCells -1)
        printfn "%s" border

        for row in 0 .. size - 1 do
            let leftPreview = cellAt { Row = row; Col = -1 }
            let rightPreview = cellAt { Row = row; Col = size }
            printfn "%s  | %s |  %s" leftPreview (innerCells row) rightPreview

        printfn "%s" border
        printfn "     %s" (innerCells size)

        printfn "Score: %d" score
