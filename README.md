# CareTracker
Back End Capstone Project for Nashville Software School Cohort 22 - CareTracker is a web application that allows users to coordinate the care of dependents by tracking Doctors, Medications and Appointments


# Deployment 
1. Clone the repository to the target machine
1. Create an appsettings.json file and configure the "DefaultConnection" database connection, the example is setup for an SQLEXPRESS database called CareTrackerDB as follows:
````
    {
       "ConnectionStrings": {
       "DefaultConnection": "Server=.\\SQLEXPRESS;Database=CareTrackerDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     },
      "Logging": {
       "IncludeScopes": false,
        "LogLevel": {
         "Default": "Warning"
     }
    }
    }
````
1. from the console, type dotnet restore to grab any required dependencies
1. from the console, type dotnet ef database update to create the database for the application


