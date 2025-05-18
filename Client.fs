namespace Project_Omega

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.UI.Html

[<JavaScript>]
module Client =

    type Transaction = {
        amount: float
        description: string
        date: string
        category: string
        txType: string
    }

    let storageKey = "transactions"

    let getTransactions () =
        match JS.Window.LocalStorage.GetItem(storageKey) with
        | null -> []
        | json ->
            try Json.Deserialize<List<Transaction>> json
            with _ -> []

    let saveTransactions (txs: List<Transaction>) =
        JS.Window.LocalStorage.SetItem(storageKey, Json.Serialize txs)

    let exportCsv (txs: List<Transaction>) =
        let header = "Dátum,Típus,Kategória,Összeg,Leírás"
        let rows =
            txs
            |> List.map (fun t ->
                let amountStr = t.amount.ToString("F2", System.Globalization.CultureInfo("hu-HU"))
                $"{t.date},{t.txType},{t.category},{amountStr},\"{t.description}\""
            )
        let csv = String.concat "\n" (header :: rows)
        let bom = "\uFEFF" // UTF-8 BOM karakter
        let blob = new Blob([| bom + csv |], BlobPropertyBag(Type = "text/csv;charset=utf-8"))
        let url = JS.Window?URL?createObjectURL(blob)
        let a = JS.Document.CreateElement("a")
        a?href <- url
        a?download <- "riport.csv"
        (a :?> Dom.Element)?click()
        JS.Window?URL?revokeObjectURL(url)

    let renderCharts (txs: List<Transaction>) =
        let Chart = JS.Window?Chart
        let options = {| responsive = true |}

        // --- Kategória eloszlás (Pie) ---
        let byCategory =
            txs
            |> List.groupBy (fun t -> t.category)
            |> List.map (fun (k, v) -> k, List.sumBy (fun x -> x.amount) v)

        let labels = byCategory |> List.map fst |> List.toArray
        let data = byCategory |> List.map snd |> List.toArray

        let categoryData =
            {| labels = labels
               datasets = [| {| label = "Kategóriák"
                                data = data
                                backgroundColor = [|"#3B82F6"; "#EF4444"; "#10B981"; "#F59E0B"; "#6366F1"; "#E11D48";"#0D9488"; "#8B5CF6"; "#F97316"; "#22D3EE"; "#4ADE80"; "#A855F7"|] |} |] |}

        let categoryConfig =
            {| ``type`` = "pie"
               data = categoryData
               options = options |}

        let canvas = JS.Document.GetElementById("category-chart")
        JS.Inline("new $0($1, $2)", Chart, canvas, categoryConfig) |> ignore

        // --- Havi bontás (Bar) ---
        let byMonth =
            txs
            |> List.groupBy (fun t -> t.date.Substring(0, 7)) // YYYY-MM
            |> List.sortBy fst
            |> List.map (fun (month, txsInMonth) ->
                month,
                txsInMonth
                |> List.sumBy (fun tx -> if tx.txType = "Bevétel" then tx.amount else -tx.amount)
            )

        let monthLabels = byMonth |> List.map fst |> List.toArray
        let monthData = byMonth |> List.map snd |> List.toArray

        let monthlyChartData =
            {| labels = monthLabels
               datasets = [| {| label = "Havi egyenleg"
                                data = monthData
                                backgroundColor = "#3B82F6" |} |] |}

        let monthlyConfig =
            {| ``type`` = "bar"
               data = monthlyChartData
               options = options |}

        let monthlyCanvas = JS.Document.GetElementById("monthly-chart")
        JS.Inline("new $0($1, $2)", Chart, monthlyCanvas, monthlyConfig) |> ignore

    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    [<SPAEntryPoint>]
    let Main () =
        // Téma váltás
        let themeToggle = JS.Document.GetElementById("toggle-theme")
        themeToggle?onclick <- fun _ ->
            JS.Document.DocumentElement.ClassList.Toggle("dark") |> ignore

        // Űrlap elemek
        let form = JS.Document.GetElementById("transaction-form")
        form?onsubmit <- fun (e: Dom.Event) ->
            e.PreventDefault()

            let getValue (id: string) : string =
                let el = JS.Document.GetElementById(id)
                if el <> null then el?value |> string
                else ""

            let tx = {
                amount = float (getValue "amount")
                description = getValue "description"
                date = getValue "date"
                category = getValue "category"
                txType = getValue "type"
            }

            let list = getTransactions() @ [tx]
            saveTransactions list
            JS.Alert("Tranzakció mentve!")
            (form :?> Dom.Element)?reset()

            // Frissítés mentés után
            renderCharts list

        // CSV export gomb
        let exportBtn = JS.Document.GetElementById("export-csv")
        exportBtn?onclick <- fun _ -> exportCsv (getTransactions())

        // Diagramok megjelenítése betöltéskor
        renderCharts (getTransactions())
