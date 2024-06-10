using ITT.CRM.Infrastructure;
using ITT.CRM.App;
using ITT.CRM.Domain;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging();

builder.Services.AddContactService(builder.Configuration);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://localhost:7094")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");


// Initialisation de la base de données
app.MapGet("/initdb", async (DatabaseManagement dbManagement) =>
{
    await dbManagement.InitDatabase();
    return "OK";
});

// Endpoints pour les contacts
app.MapGet("/contacts", async (ContactService cService) =>
{
    try
    {
        var contacts = await cService.GetAllContactsAsync();
        return contacts != null ? Results.Ok(contacts) : Results.NotFound();
    }
    catch
    {
        return Results.Ok(null);
    }
});

app.MapGet("/contacts/{id}", async (ContactService cService, int id) =>
{
    try
    {
        var contact = await cService.GetContactByIdAsync(id);
        return contact != null ? Results.Ok(contact) : Results.NotFound();
    }
    catch
    {
        return Results.Ok(null);
    }
});

app.MapPost("/contacts", async (ContactService cService, Contact contact) =>
{
    try
    {
        var newContact = await cService.AddContactAsync(contact);
        return Results.Created($"/contacts/{newContact.Id}", newContact);
    }
    catch
    {
        return Results.Ok(null);
    }
});

app.MapPut("/contacts/{id}", async (ContactService cService, int id, Contact updatedContact) =>
{
    try
    {
        if (updatedContact.Id != id)
        {
            return Results.BadRequest("Contact ID mismatch.");
        }

        var contact = await cService.UpdateContactAsync(updatedContact);
        return contact != null ? Results.Ok(contact) : Results.NotFound();
    }
    catch
    {
        return Results.Ok(null);
    }
});

app.MapDelete("/contacts/{id}", async (ContactService cService, int id) =>
{
    try
    {
        var contact = await cService.DeleteContactAsync(id);
        return contact != null ? Results.Ok(contact) : Results.NotFound();
    }
    catch
    {
        return Results.Ok(null);
    }
});

app.MapGet("/contacts/search", async (ContactService cService, string searchTerm) =>
{
    try
    {
        var contacts = await cService.SearchContactsAsync(searchTerm);
        return contacts != null ? Results.Ok(contacts) : Results.NotFound();
    }
    catch
    {
        return Results.Ok(null);
    }
});


app.Run();


