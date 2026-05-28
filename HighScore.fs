namespace CS20200Game

open System
open System.IO

type HighScoreStore =
    { FilePath: string }

module HighScore =
    let create filePath =
        { FilePath = filePath }

    let private tryParseLine (line: string) =
        match line.Split('=', 2) with
        | [| sizeText; scoreText |] ->
            match Int32.TryParse(sizeText.Trim()), Int32.TryParse(scoreText.Trim()) with
            | (true, size), (true, score) -> Some(size, score)
            | _ -> None
        | _ -> None

    let private loadScores store =
        if File.Exists store.FilePath then
            File.ReadAllLines store.FilePath
            |> Array.choose tryParseLine
            |> Map.ofArray
        else
            Map.empty

    let private saveScores store scores =
        let lines =
            scores
            |> Map.toArray
            |> Array.map (fun (size, score) -> $"{size}={score}")

        File.WriteAllLines(store.FilePath, lines)

    let getBestScore store size =
        loadScores store
        |> Map.tryFind size
        |> Option.defaultValue 0

    let listScores store =
        loadScores store
        |> Map.toList

    let reset store =
        if File.Exists store.FilePath then
            File.Delete store.FilePath

    let saveIfBest store size score =
        let scores = loadScores store
        let bestScore = scores |> Map.tryFind size |> Option.defaultValue 0

        if score > bestScore then
            let updatedScores = scores |> Map.add size score
            saveScores store updatedScores
            score
        else
            bestScore
