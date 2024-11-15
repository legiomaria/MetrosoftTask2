# Requirement: 
1. Visual Studio 2022
2. Microsoft SQL Server 
3. Stable Internet Connection(due to the cdn for UI)


# Steps to run:
1. Clone the application, from https://github.com/legiomaria/MetrosoftTask2.git.
2. Run the application.
3. The application automatically runs it migrations, create relevant tables, and seed the tables. 
4. Enjoy.

# ConnectionString:
The connectionstring is found in the appsettings.json, contained in the TodoList.Api project.
I suggest the server in the connectionstring is replaced from the dot to that on your SSMS 
eg  Server=.;Database=MetroSoftTaskDb;TrustServerCertificate=true;Integrated Security=true;Trusted_Connection=True;

changed to Server=YourServerName;Database=MetroSoftTaskDb;TrustServerCertificate=true;Integrated Security=true;Trusted_Connection=True; 


