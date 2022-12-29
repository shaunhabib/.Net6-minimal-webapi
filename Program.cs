using Microsoft.EntityFrameworkCore;
using Minimal_webapi;

var builder = WebApplication.CreateBuilder(args); //main line 1

#region Db connection
builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection") , new MySqlServerVersion(new Version())));
#endregion

var app = builder.Build(); //main line 2


#region Minimal api with Database
app.MapGet("/GetAll", (AppDbContext Db)=>
{
    return Results.Ok(Db.Students.ToList());
});

app.MapPost("/Create", (Student std, AppDbContext Db)=>
{
    var result = Db.Students.Add(std);
    Db.SaveChanges();
    if (result.Entity.Id <= 0)
    {
        return Results.BadRequest("Failed to save");
    }
    return Results.Ok("Successfully saved");
});

app.MapPut("/Update", (Student std, AppDbContext Db, int id)=>
{
    var student = Db.Students.FirstOrDefault(x => x.Id == id);
    if (student == null)
    {
        return Results.NotFound();
    }
    student.Name = std.Name;
    student.Roll = std.Roll;
    student.RegistrationNo = std.RegistrationNo;
    student.PhoneNumber = std.PhoneNumber;
    var rs = Db.SaveChanges();
    if (rs > 0)
    {
        return Results.Ok("Successfully updated");
    }
    return Results.BadRequest("Failed to update");
});

app.MapDelete("/Delete", (int id, AppDbContext Db)=>
{
    var student = Db.Students.FirstOrDefault(x => x.Id == id);
    if (student == null)
    {
        return Results.NotFound();
    }
    Db.Students.Remove(student);
    Db.SaveChanges();
    return Results.Ok("Successfully deleted");
});
#endregion


#region Minimal api without Database
app.MapGet("/GetAll2", ()=>
{
    return Results.Ok("Successfully retrieve all");
});

app.MapPost("/Create2", (string name)=>
{
    return Results.Ok($"Name {name} is updated");
});

app.MapPut("/Update2", (string name)=>
{
    return Results.Ok($"Name {name} is updated");
});

app.MapDelete("/Delete2", ()=>
{
    return Results.Ok("Hello from delete");
});
#endregion

app.Run(); //main line 3
