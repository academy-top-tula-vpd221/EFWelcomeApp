using EFWelcomeApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

ConfigurationBuilder configurationBuilder = new();
configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
configurationBuilder.AddJsonFile("appsettings.json");

var config = configurationBuilder.Build();
string? connectionString = config.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);

//var optionalBuilder = new DbContextOptionsBuilder<ApplicationContext>();
//var options = optionalBuilder.UseSqlServer(connectionString).Options;

using (ApplicationContext db = new ApplicationContext(connectionString))
{
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    bool isConnect = await db.Database.CanConnectAsync();
    if (!isConnect)
    {
        Console.WriteLine("Database is not avalaible");
        throw new Exception("Database is not avalaible");
    }


    // INSERT

    Company? yandex = new() { Title = "Yandex" };
    Company? ozon = new() { Title = "Ozon" };

    Employee employee1 = new()
    { Name = "Sam", Age = 41, Company = yandex };
    Employee employee2 = new()
    { Name = "Bob", Age = 25, Company = ozon };
    Employee employee3 = new()
    { Name = "Tom", Age = 33, Company = yandex };
    Employee employee4 = new()
    { Name = "Jim", Age = 19, Company = ozon };

    //await db.Employees.AddAsync(employee1);
    //await db.Employees.AddAsync(employee2);
    await db.Employees.AddRangeAsync(
        new[] { employee1, 
            employee2, 
            employee3, 
            employee4 });

    // UPDATE

    //Employee? employee = db.Employees.FirstOrDefault(e => e.Name == "Sam");
    //if (employee is not null)
    //{
    //    employee.Name = "Tom";
    //    employee.Age = 36;
    //}

    // DELETE

    //Employee? employee = await db.Employees.FirstOrDefaultAsync(e => e.Name == "Bob");
    //if (employee is not null)
    //    db.Employees.Remove(employee);

    await db.SaveChangesAsync();

    var employees = await db.Employees.ToListAsync();
    Console.WriteLine("Employees:");
    foreach(Employee e in employees)
        Console.WriteLine($"Id: {e.Id}, Name: {e.Name}, Age: {e.Age}, Company: {e.Company?.Title}");
}


