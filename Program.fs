namespace CS20200Game

open System

module Program =
    let private highScoreStore = HighScore.create "high_scores.txt"

    let rec private readGridSize () =
        printf "Grid size N (default 9): "
        let input = Console.ReadLine().Trim()

        if input = "" then
            9
        else
            match Int32.TryParse input with
            | true, size when size >= 3 -> size
            | true, _ ->
                printfn "Grid size must be at least 3."
                readGridSize ()
            | false, _ ->
                printfn "Please enter a number."
                readGridSize ()

    let private showScores () =
        match HighScore.listScores highScoreStore with
        | [] -> printfn "No high scores recorded yet."
        | scores ->
            printfn "High scores:"

            scores
            |> List.iter (fun (size, score) ->
                printfn "- %dx%d: %d" size size score)

    [<EntryPoint>]
    let main args =
        if args |> Array.contains "--reset-scores" then
            HighScore.reset highScoreStore
            printfn "High scores have been reset."
            0
        elif args |> Array.contains "--show-scores" then
            showScores ()
            0
        else
            let size = readGridSize ()
            let state = Game.create size 1.0
            Game.run highScoreStore state
            0
