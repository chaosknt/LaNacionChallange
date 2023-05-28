# LaNacionChallange

<p>
 Content

<ol>
  <li>System Requirements</li>
  <li>Project Configuration</li>
  <li>AppSetting.json</li>
  <li>Project testing</li>
<ol>
 </p>

## System Requirements {#system-requirements}

### Purpose

As part of La Nacion hiring process, we ask interviewees to complete a small project to help
assess skill, experience, ingenuity, problem-solving and knowledge.

### Scope

Develop a RESTful API that would allow a web or mobile front-end to:

<ol>
  <li>Create a contact record</li>
  <li>Retrieve a contact record</li>
  <li>Update a contact record</li>
  <li>Delete a contact record</li>
  <li>Search for a record by email or phone number</li>
  <li>Retrieve all records from the same state or city</li>
</ol>

The contact record should represent the following information: name, company, profile
image, email, birthdate, phone number (work, personal) and address.

  <ol>
    <li>Use al least one design pattern.</li>
    <li>Apply solid principles</li>
    <li>Also please provide a unit test for at least one of the endpoints you create.</li>
  </ol>
  
## Project Configuration
  
  <p>
   There is two availables database ready to be used in the proyect, <b>SQL Server</b> and <b>Database in Memoryr</b>,  the last one is the defult database provider.
  
  If you want to use <b>SQL Server</b> like your database provider follow this steps:
  
  * In the Startup.cs, uncomment the 34 and 35 lines <br>

      ```
       //services.AddDbContext<LaNacionContext>(options => options
            //  .UseSqlServer(Configuration.GetConnectionString("LaNacionDbConn")));
     ```
* Comment lines 37 to 30

 ```
       services.AddDbContext<LaNacionContext>(options => options
                .UseInMemoryDatabase(databaseName: "LaNacionInMemory")
                );
```
* Uncomment the line 75
```
//context.Database.Migrate();
```

* Setup the connectionString in the `appsettings.json` <br>
  `"LaNacionDbConn": "Server=localhost;Initial Catalog=LaNacion;User ID=sa;Password=Password.;MultipleActiveResultSets=True"`
  </p>
<p>

## AppSetting.json

Take into consideration the following configuration before run the proyect:

* Serilog path configuration
```
"Serilog": {
    "WriteTo": [
      {
        "Args": {
          "path": "C:\\LaNacion\\Api\\logs\\log.txt",
        }
      }
    ]
  },
```

* ProfileImage path configuration

```
 "ProfileImage": {
    "ImagesOutputPath": "C:\\LaNacion\\Api\\images\\",
     }
```

</p> 

## Project testing

<p>
You can test the project endPoint by using the Swagger Page, the default url is <a href='https://localhost:44316/swagger/index.html'>https://localhost:44316/swagger/index.html</a>.
Or you can navigate to serverUrl/swagger
</p>
  
<p>
For testing purpose, the project has configurated a Seed, you can see available contacts here: https://github.com/chaosknt/LaNacionChallange/blob/master/LaNacion.Data/ContactsFakeData.cs <br>
  
 If you want to check the available Enum values you can use the `/api/v1/Contact/enum`, it will return all enum values availables.
</p>
  
 

