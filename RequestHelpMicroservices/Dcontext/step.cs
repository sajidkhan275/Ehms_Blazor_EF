namespace RequestHelpMicroservices.Dcontext
{
    public class step
    {  //dotnet add package Microsoft.EntityFrameworkCore
        //dotnet add package Microsoft.EntityFrameworkCore.SqlServer
        //dotnet add package Microsoft.EntityFrameworkCore.Tools
        //dotnet add package Microsoft.EntityFrameworkCore.Design

        //1. first create model with require key, propery 
        //2. create dbcontext with DbContextOptions option in consturction
        //3. create dbset of model
        //4. then create OnModelCreating method with ModelBuilder parameter
        //5.  then defien the propery type like lenght, reuqirment
        //6. program.cs file builder.Services.AddDbContext<EHMSDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionEf")));

        //fire some query for db chagnes and Add EF migrations->
        //Enable-Migrations
        //add Migration Name (Add-Migration InitialCreate)
        //Update-Database

        //Remove-Migration. remove last migration
        //Update-Database InitialCreate


        //Add-Migration AddRolesTable

        //oclot ->
        //create new .net core empty project
        //install
    }
}
