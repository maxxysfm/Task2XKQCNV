(function()
{
 "use strict";
 var Global,Task2XKQCNV,Dto,Book,Client,SC$1,WebSharper,UI,Var$1,Submitter,View,Concurrency,IntelliFactory,Runtime,Utils,Unchecked,Remoting,AjaxRemotingProvider,Seq,DateUtil,Doc,AttrProxy,List,Date;
 Global=self;
 Task2XKQCNV=Global.Task2XKQCNV=Global.Task2XKQCNV||{};
 Dto=Task2XKQCNV.Dto=Task2XKQCNV.Dto||{};
 Book=Dto.Book=Dto.Book||{};
 Client=Task2XKQCNV.Client=Task2XKQCNV.Client||{};
 SC$1=Global.StartupCode$Task2XKQCNV$Client=Global.StartupCode$Task2XKQCNV$Client||{};
 WebSharper=Global.WebSharper;
 UI=WebSharper&&WebSharper.UI;
 Var$1=UI&&UI.Var$1;
 Submitter=UI&&UI.Submitter;
 View=UI&&UI.View;
 Concurrency=WebSharper&&WebSharper.Concurrency;
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 Utils=WebSharper&&WebSharper.Utils;
 Unchecked=WebSharper&&WebSharper.Unchecked;
 Remoting=WebSharper&&WebSharper.Remoting;
 AjaxRemotingProvider=Remoting&&Remoting.AjaxRemotingProvider;
 Seq=WebSharper&&WebSharper.Seq;
 DateUtil=WebSharper&&WebSharper.DateUtil;
 Doc=UI&&UI.Doc;
 AttrProxy=UI&&UI.AttrProxy;
 List=WebSharper&&WebSharper.List;
 Date=Global.Date;
 Book.New=function(Title,Author,Published)
 {
  return{
   Title:Title,
   Author:Author,
   Published:Published
  };
 };
 Client.Main=function()
 {
  var rvInput,rvInputTitle,rvInputAuthor,rvInputYear,rvInputTitleFilter,rvInputAuthorFilter,rvInputYearFilter,submit,vReversed;
  rvInput=Var$1.Create$1(false);
  rvInputTitle=Var$1.Create$1("");
  rvInputAuthor=Var$1.Create$1("");
  rvInputYear=Var$1.Create$1(Global.String(Client.timeNow()));
  rvInputTitleFilter=Var$1.Create$1("");
  rvInputAuthorFilter=Var$1.Create$1("");
  rvInputYearFilter=Var$1.Create$1(Global.String(Client.timeNow()));
  submit=Submitter.CreateOption(rvInput.get_View());
  vReversed=View.MapAsync(function(a)
  {
   var input,b,b$1;
   return a!=null&&a.$==1?(input=a.$0,(b=null,Concurrency.Delay(function()
   {
    function F(book)
    {
     return((((Runtime.Curried(function($1,$2,$3,$4)
     {
      return $1(Utils.toSafe($2)+" "+Utils.toSafe($3)+" "+Utils.toSafe($4));
     },4))(Global.id))(book.Title))(book.Author))(Global.String((new Global.Date(book.Published)).getFullYear()));
    }
    function F$1(book)
    {
     return((((Runtime.Curried(function($1,$2,$3,$4)
     {
      return $1(Utils.toSafe($2)+" "+Utils.toSafe($3)+" "+Utils.toSafe($4));
     },4))(Global.id))(book.Title))(book.Author))(Global.String((new Global.Date(book.Published)).getFullYear()));
    }
    return Unchecked.Equals(input,false)?Concurrency.Bind((new AjaxRemotingProvider.New()).Async("Task2XKQCNV:Task2XKQCNV.Server.AllBooks:705854993",[]),function(a$1)
    {
     return Concurrency.Return(Seq.fold(function(acc,book)
     {
      return acc+F(book)+";; \n";
     },"",a$1));
    }):Concurrency.Bind((new AjaxRemotingProvider.New()).Async("Task2XKQCNV:Task2XKQCNV.Server.FilterBooks:710066497",[rvInputTitleFilter.Get(),rvInputAuthorFilter.Get(),DateUtil.Parse(rvInputYearFilter.Get())]),function(a$1)
    {
     return Concurrency.Return(Seq.fold(function(acc,book)
     {
      return acc+F$1(book)+";; \n";
     },"",a$1));
    });
   }))):(b$1=null,Concurrency.Delay(function()
   {
    return Concurrency.Return("");
   }));
  },submit.view);
  return Doc.Element("div",[],[Doc.Element("table",[AttrProxy.Create("border","2"),AttrProxy.Create("style","margin: auto;\n            width: 80%;\n            padding: 10px;")],List.ofSeq(Seq.delay(function()
  {
   var style;
   style=AttrProxy.Create("style","padding-top: 10px;padding-right: 10px;padding-bottom: 10px;padding-left: 10px;");
   return Seq.append([Doc.Element("th",[style],[Doc.Element("h2",[],[Doc.TextNode("New book!")]),Doc.Element("table",[],[Doc.Element("tr",[],List.ofSeq(Seq.delay(function()
   {
    var displayText;
    displayText="Author's name: ";
    return Seq.append([Doc.Element("th",[],[Doc.Element("h3",[],[Doc.TextNode(displayText)])])],Seq.delay(function()
    {
     return[Doc.Element("th",[],[Doc.Input([AttrProxy.Create("placeholder",displayText)],rvInputTitle)])];
    }));
   }))),Doc.Element("tr",[],List.ofSeq(Seq.delay(function()
   {
    var displayText;
    displayText="Title: ";
    return Seq.append([Doc.Element("th",[],[Doc.Element("h3",[],[Doc.TextNode(displayText)])])],Seq.delay(function()
    {
     return[Doc.Element("th",[],[Doc.Input([AttrProxy.Create("placeholder",displayText)],rvInputAuthor)])];
    }));
   }))),Doc.Element("tr",[],List.ofSeq(Seq.delay(function()
   {
    var displayText;
    displayText="Year published: ";
    return Seq.append([Doc.Element("th",[],[Doc.Element("h3",[],[Doc.TextNode(displayText)])])],Seq.delay(function()
    {
     return[Doc.Element("th",[],[Doc.Input([AttrProxy.Create("type","number"),AttrProxy.Create("placeholder",displayText),AttrProxy.Create("min","1900"),AttrProxy.Create("max",Global.String(Client.timeNow()))],rvInputYear)])];
    }));
   })))]),Doc.Button("Add",[],function()
   {
    var theYear;
    if(rvInputTitle.Get()===""||rvInputAuthor.Get()==="")
     Global.alert("You must enter a value in every option!");
    else
     {
      theYear=DateUtil.Parse(rvInputYear.Get());
      (new AjaxRemotingProvider.New()).Send("Task2XKQCNV:Task2XKQCNV.Server.AddBook:4243286",[rvInputTitle.Get(),rvInputAuthor.Get(),theYear]);
      Global.alert("New book added!");
     }
   })])],Seq.delay(function()
   {
    return[Doc.Element("th",[style],List.ofSeq(Seq.delay(function()
    {
     return Seq.append([Doc.Element("h2",[],[Doc.TextNode("Filter by:")])],Seq.delay(function()
     {
      return Seq.append([Doc.Element("table",[],[Doc.Element("tr",[],List.ofSeq(Seq.delay(function()
      {
       var displayText;
       displayText="Part of author's name: ";
       return Seq.append([Doc.Element("th",[],[Doc.Element("h3",[],[Doc.TextNode(displayText)])])],Seq.delay(function()
       {
        return[Doc.Element("th",[],[Doc.Input([AttrProxy.Create("placeholder",displayText)],rvInputAuthorFilter)])];
       }));
      }))),Doc.Element("tr",[],List.ofSeq(Seq.delay(function()
      {
       var displayText;
       displayText="Part of the title: ";
       return Seq.append([Doc.Element("th",[],[Doc.Element("h3",[],[Doc.TextNode(displayText)])])],Seq.delay(function()
       {
        return[Doc.Element("th",[],[Doc.Input([AttrProxy.Create("placeholder",displayText)],rvInputTitleFilter)])];
       }));
      }))),Doc.Element("tr",[],List.ofSeq(Seq.delay(function()
      {
       var displayText;
       displayText="Year published: ";
       return Seq.append([Doc.Element("th",[],[Doc.Element("h3",[],[Doc.TextNode(displayText)])])],Seq.delay(function()
       {
        return[Doc.Element("th",[],[Doc.Input([AttrProxy.Create("type","number"),AttrProxy.Create("placeholder",displayText),AttrProxy.Create("min","1900"),AttrProxy.Create("max",Global.String(Client.timeNow()))],rvInputYearFilter)])];
       }));
      })))])],Seq.delay(function()
      {
       var textDisabled,colDisabled,btnFilterID;
       textDisabled="Disabled";
       colDisabled="background:#ff9696";
       btnFilterID="filterbutton";
       return[Doc.Button(textDisabled,[AttrProxy.Create("id",btnFilterID),AttrProxy.Create("style",colDisabled)],function()
       {
        rvInput.Set(!rvInput.Get());
        !rvInput.Get()?(self.document.getElementById(btnFilterID).innerHTML=textDisabled,self.document.getElementById(btnFilterID).setAttribute("style",colDisabled)):(self.document.getElementById(btnFilterID).innerHTML="Enabled",self.document.getElementById(btnFilterID).setAttribute("style","background:#96ff98"));
       })];
      }));
     }));
    })))];
   }));
  }))),Doc.Element("hr",[],[]),Doc.Element("h3",[],[Doc.TextNode("List of books! (Make sure to Refresh!)")]),Doc.Button("Refresh",[],function()
  {
   submit.Trigger();
  }),Doc.Element("div",[AttrProxy.Create("class","jumbotron")],[Doc.Element("b",[],[Doc.TextView(vReversed)])])]);
 };
 Client.timeNow=function()
 {
  SC$1.$cctor();
  return SC$1.timeNow;
 };
 SC$1.$cctor=function()
 {
  var c;
  SC$1.$cctor=Global.ignore;
  SC$1.timeNow=(c=Date.now(),(new Date(c)).getFullYear());
 };
}());
