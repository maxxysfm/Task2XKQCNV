# Project #2 - A book database
Szabó Attila - XKQCNV

Course: ***DUEN-ISR-010***

Application type:

![Application type](https://dl.dropboxusercontent.com/s/8f3u0r6y0z5cvks/devenv_grpFm7B25y.png)

# Part 1. - Setup the Project
Project file:
1. Download the latest [Microsoft Visual Studio](https://visualstudio.microsoft.com/)
2. (optionally) use [TortoiseGit](https://tortoisegit.org/) to download the repository.
Or just simply download all the files in this repo.
3. [Download WebSharper for Visual Studio](http://websharper.com/downloads) (Scroll down) 



# Part 2. - Setup SQL Server:
1. Setup [SQL Server 2019 - Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Setup and launch [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
3. Connect to your server:

![](https://dl.dropboxusercontent.com/s/7mcm2ckx3i58adq/Ssms_7lAJ8FmxML.png)

4. Use the **SQLQuery.sql** query on your **localhost** server

![](https://dl.dropboxusercontent.com/s/455gp344c5l24o2/Ssms_KyyGNxG9vr.png)![](https://dl.dropboxusercontent.com/s/dnooao0xpjve3l2/Ssms_UTqXIjHAoA.png)

5. Open the project **Task2XKQCNV.sln** with Visual Studio.
6. Make sure to verify that SQLProvider is added to your solution:

![](https://dl.dropboxusercontent.com/s/fdoy7ng9pzumw4y/devenv_5qKFiPq4vj.png)![](https://dl.dropboxusercontent.com/s/w6tbtrreh8p8uv7/devenv_N6r3C54QSo.png)

6. Build the project

![](https://dl.dropboxusercontent.com/s/v5we82rbvsv01oh/devenv_rxS1S1vlam.png)

# Usage:
![](https://dl.dropboxusercontent.com/s/97420xqu4sdldij/firefox_sdQ5WdClqw.png)

# Known bugs/errors:
1. The display of records is done via **textView**, instead of [listModel](https://developers.websharper.com/docs/v4.x/fs/ui).
2. The display of Hungarian letters (ó,á, etc) is incorrect in HTML parts. (Excluding textView)
- Possible fix that didn't work, changing from **UTF-8** to **ISO-8859-1**
