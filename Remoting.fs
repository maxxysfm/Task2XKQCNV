namespace Task2XKQCNV

open WebSharper

[<JavaScript>]
module Dto =
    type Book =
        {
            Title: string
            Author: string

            // Lehet tároljuk a dátumot, de csak az év fog számítani a számolásnál
            Published: System.DateTime
        }

module Database =
    
    open FSharp.Data.Sql
    open Dto
    type DB = SqlDataProvider<
                  ConnectionString = "Server=localhost;Database=Task2;Trusted_Connection=True;",
                  DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER,
                  UseOptionTypes = true>

    // Az órán említett példa alapján
    let AllBooks () =
        let ctx = DB.GetDataContext()
        query {
            for book in ctx.Dbo.Books do
            select book
        }
        |> Seq.map (fun book ->
            {
                Title = book.Title.Value
                Author = book.Author.Value
                Published = book.Published.Value
            }
        )
        |> Seq.toList
    
    // Filterezés
    let FilterBooks (title: string,author: string,published: System.DateTime) =
        let ctx = DB.GetDataContext()
        
        let resultList =
            
            // Jó, biztos van valami megoldás rá hogy egy IF statementet belerakjunk, eredetileg
            // Az lett volna az ötletem, hogy a query-be megvizsgálom van-e title, vagy author
            // Date-t meg amúgy is filtereznék, de nem úgy jött össze ahogy számítottam rá
            query {
                for book in ctx.Dbo.Books do
                select book
            }
            |> Seq.map (fun book ->
                {
                    Title = book.Title.Value
                    Author = book.Author.Value
                    Published = book.Published.Value
                }
            )

        // Ezért így oldottam meg hogy egy funkcióba lehessen a filterezést lebonyolítani
        if(title <> "") then
            resultList
            
            // "része-e" a bevitt érték string?
            |> Seq.filter(fun x -> x.Title.Contains(title))

            // Csak az éveket nézzük meg, tudom az egész dátumot tároljuk, csak az évet nézzük viszont
            |> Seq.filter(fun x -> x.Published.Year = published.Year)
            |> Seq.toList
        
        elif(author <> "") then
            resultList
            |> Seq.filter(fun x -> x.Author.Contains(author))
            |> Seq.filter(fun x -> x.Published.Year = published.Year)
            |> Seq.toList
        
        elif(title <> "" && author <> "") then
            resultList
            |> Seq.filter(fun x -> x.Author.Contains(author))
            |> Seq.filter(fun x -> x.Title.Contains(title))
            |> Seq.filter(fun x -> x.Published.Year = published.Year)
            |> Seq.toList
        
        else
            resultList
            |> Seq.filter(fun x -> x.Published.Year = published.Year)
            |> Seq.toList

    // Új rekord hozzáadása az adatbázishoz
    let AddBook(title: string,author: string,published: System.DateTime) =

        let ctx = DB.GetDataContext()
        
        // Könyv rekord létrehozása
        let Book = ctx.Dbo.Books
        
        // Feltöltés
        // https://fsprojects.github.io/SQLProvider/core/crud.html
        let row = Book.Create()
        
        // Ahoz hogy olyan fajta típus legyen, bele kell rakni egy Some()-ba
        row.Author <- Some(author)
        row.Title <- Some(title)
        row.Published <- Some(published)
        ctx.SubmitUpdates()

module Server =
      
    [<Rpc>]
    let AddBook(title: string,author: string,published: System.DateTime) =
            Database.AddBook (title,author,published)

    [<Rpc>]
    let AllBooks() =
        async {
            return Database.AllBooks()
        }    
        
    [<Rpc>]
    let FilterBooks(title: string,author: string,published: System.DateTime) =
        async {
            return Database.FilterBooks(title,author,published)
        }
