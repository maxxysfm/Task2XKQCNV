namespace Task2XKQCNV

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html
open Remoting

[<JavaScript>]
module Client =

    let timeNow = System.DateTime.Now.Year
    open Dto

    let Main () =

        // Ebben a változóban tároljuk hogy bevan-e kapcsolva a filter vagy nem
        let rvInput = Var.Create false

        // Felvitel
        let rvInputTitle = Var.Create ""
        let rvInputAuthor = Var.Create ""
        let rvInputYear = Var.Create (string timeNow)
        
        // Az értékek amikkel a filter dolgozik / szövegdobozok, ha üres a Title vagy Author, akkor azokat az oszlopokat nem filterezi
        // Dátum alapján mindig filterezünk ha bevan kapcsolva, lásd több -> Remoting.fs 60-ik sor
        let rvInputTitleFilter = Var.Create ""
        let rvInputAuthorFilter = Var.Create ""
        let rvInputYearFilter = Var.Create (string timeNow)

        // Én teljes mértékben tisztába vagyok azzal, hogy meglehet ezt oldani listázással is, de őszintén
        // Rááldoztam körülbelül 6 órát (nem viccelek), de nem találtam rá konkrétan olyan példát, ami az async módszerrel működne
        // Bár tudom biztos meglehet oldani, viszont sajnos nem sikerült, megpróbáltam
        // Ezért a string-es megoldásnál kötöttem ki, és próbáltam arra is megoldást keresni, hogy esetleg a textView értéke alapján 
        // HTML-be konvertáljam át a string-et, 

        // https://stackoverflow.com/questions/2116162/how-to-display-html-in-textview
        // Ez viszont csak Android-os cuccokon működik? Mindenesetre ja... nem szép de legalább kirajzolódik az összes rekord az adatbázisból mert van egy 
        // Kézzelfogható példa.
        
        // Mellesleg, láttam a gitteres példát ListModel-re, amit alaposan átvizsgáltam, viszont kliens oldalra nemtudtam átemelni a szerver oldali adatokat
        // Mivel nem értettem meg jól ezt az async dolgot, csak ennek a példa alapján tudtam dolgozni
        let submit = Submitter.CreateOption rvInput.View
        let vReversed =
            submit.View.MapAsync(function
                | None -> async { return "" }
                | Some input ->
                    async {
                        
                        // Filter bevan-e kapcsolva vagy nem?
                        if(input = false) then
                            let F (book: Dto.Book) =
                                
                                // (: VOLT EGY ILYEN ATTEMPT IS
                                //sprintf "ul [] [li [] [%s] li [] [%s] li [] [%s]]" book.Title book.Author (string book.Published.Year)    
                                sprintf "%s %s %s" book.Title book.Author (string book.Published.Year)    
                            let! res = Server.AllBooks()
                            let res = res |> List.fold (fun acc book -> acc + F book + ";; \n") ""
                            return res
                        else
                            let F (book: Dto.Book) =
                                sprintf "%s %s %s" book.Title book.Author (string book.Published.Year)    
                            let! res = Server.FilterBooks(rvInputTitleFilter.Value,rvInputAuthorFilter.Value,(System.DateTime.Parse(rvInputYearFilter.Value)))
                            let res = res |> List.fold (fun acc book -> acc + F book + ";; \n") ""
                            return res
                    }
            )

        div [] [
            
            // Új érték felvétele
            table [
            attr.``border`` "2"
            attr.``style`` "margin: auto;
            width: 80%;
            padding: 10px;"
            ] [
                let style = attr.``style`` "padding-top: 10px;padding-right: 10px;padding-bottom: 10px;padding-left: 10px;"
                th[style] [
                    h2 [] [text "New book!"]
                    table [] [
                        tr [][
                            let displayText =  "Author's name: "
                            th [] [
                                h3 [] [text displayText]
                                ]                    
                            th [] [
                                Doc.Input [attr.``placeholder`` displayText] rvInputTitle
                                ]
                        ]
                        tr [][
                            let displayText =  "Title: "
                            th [] [
                                h3 [] [text displayText]
                                ]                    
                            th [] [
                                Doc.Input [attr.``placeholder`` displayText] rvInputAuthor
                                ]
                        ]
                        tr [][
                            let displayText =  "Year published: "
                            th [] [
                                h3 [] [text displayText]
                                ]                    
                            th [] [
                                Doc.Input [
                                attr.``type`` "number"
                                attr.``placeholder`` displayText
                                attr.``min`` "1900"
                                attr.``max`` (string timeNow)
                                    ] rvInputYear
                                ]
                        ]
                    ]       
            
                    // Hozzáadás gomb megnézi a textboxok értékeit
                    Doc.Button "Add" [] (fun _ ->
                    if(rvInputTitle.Value = "" || rvInputAuthor.Value = "") then
                        JS.Alert("You must enter a value in every option!");
                    else
                        let theYear = System.DateTime.Parse(rvInputYear.Value)
                        Server.AddBook(rvInputTitle.Value,rvInputAuthor.Value,theYear)
                        JS.Alert("New book added!");
                    )
                ]
                th[style] [
                
                    // Filterezés
                    h2 [] [text "Filter by:"]
                    table [] [
                        tr [][
                            let displayText =  "Part of author's name: "
                            th [] [
                                h3 [] [text displayText]
                                ]                    
                            th [] [
                                Doc.Input [attr.``placeholder`` displayText] rvInputAuthorFilter
                                ]
                        ]
                        tr [][
                            let displayText =  "Part of the title: "
                            th [] [
                                h3 [] [text displayText]
                                ]                    
                            th [] [
                                Doc.Input [attr.``placeholder`` displayText] rvInputTitleFilter
                                ]
                        ]
                        tr [][
                            let displayText =  "Year published: "
                            th [] [
                                h3 [] [text displayText]
                                ]                    
                            th [] [
                                Doc.Input [
                                attr.``type`` "number"
                                attr.``placeholder`` displayText
                                attr.``min`` "1900"
                                attr.``max`` (string timeNow)
                                    ] rvInputYearFilter
                                ]
                        ]
                    ]
                    
                    // Tároljuk milyen státusza lehet a filternek
                    let textDisabled = "Disabled"
                    let textEnabled = "Enabled"
                    
                    // Vörös szín https://www.google.com/search?q=color+picker
                    let colDisabled = "background:#ff9696"
                    let colEnabled = "background:#96ff98"

                    // Milyen ID alapján hivatkozunk a gombra később
                    let btnFilterID = "filterbutton"

                    // Kezdő értéke a gombnak.
                    Doc.Button textDisabled [
                    attr.``id`` btnFilterID
                    attr.``style`` colDisabled
                    ] (fun _ ->
                        
                        //JS.Alert("Filtering!");
                        rvInput.Value <- not rvInput.Value
                        if not rvInput.Value then
                            JS.Document.GetElementById(btnFilterID).InnerHTML <- textDisabled
                            JS.Document.GetElementById(btnFilterID).SetAttribute("style",colDisabled)
                        else
                            JS.Document.GetElementById(btnFilterID).InnerHTML <- textEnabled
                            JS.Document.GetElementById(btnFilterID).SetAttribute("style",colEnabled)
                    )
                ]
            ]
            
            // Tábla kirajzolás
            hr [] []
            h3 [] [text "List of books! (Make sure to Refresh!)"]
            Doc.Button "Refresh" [] submit.Trigger
 
            div [attr.``class`` "jumbotron"] [b [] [
            textView vReversed
            ]]
        ]




