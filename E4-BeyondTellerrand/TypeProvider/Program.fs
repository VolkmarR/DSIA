open FSharp.Data

// Definiere den neuen Typ csvTypeProvider mithilfe der CSV Datei
type csvTypeProvider = CsvProvider<"data.csv", HasHeaders=true>

[<EntryPoint>]
let main argv =
    
    // Typ initialisieren
    let csv = csvTypeProvider.GetSample()

    // für alle Zeilen
    let avgNote = csv.Rows 
                  // die Spalte Note extrahieren          
                  |> Seq.map(fun row -> row.Note)
                  // den Durchschnitt bilden
                  |> Seq.averageBy (fun note -> float note)

    printfn "Durchschnittsnote: %f" avgNote

    0 