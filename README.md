# Basic Visual Studio  Windows Form App I created for my client as a WMS demo

### Techstack: C#, .NET SDK
### Database: SQL Server Express Database

# Important
### Go to the App.config file to change your datasource as shown below

```
<add name="DBconn" connectionString="Data Source=Your Data Source;Initial Catalog=Your database;Integrated Security=True"
   providerName="System.Data.SqlClient" />
<add name="Demo.Properties.Settings.StockConnectionString" connectionString="Data Source=Your Data Source;Initial Catalog=Your database;Integrated Security=True"
   providerName="System.Data.SqlClient" />
```

### You need to download your crystal report from SAP online in order to use the report function and page.
Here is the link [https://origin.softwaredownloads.sap.com/public/site/index.html](https://origin.softwaredownloads.sap.com/public/site/index.html)

I downloaded and installed `CRforVS6413SP36_0-80007712.exe` for my own machine for this to work.

