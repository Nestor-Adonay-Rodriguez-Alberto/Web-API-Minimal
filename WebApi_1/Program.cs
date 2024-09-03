var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// Lista De Objetos:
List<Client> clients = new List<Client>();


// ***********  CREACION  DE  LA  API   ***********
// ************************************************

// GET : Obtener Todos Los Registros:
app.MapGet("/clients", () => 
{
    return clients; 
});


// GET : Obtener Un Registro Con Ese ID:
app.MapGet("/clients/{id}", (int id) => 
{ 
    Client Objeto_Obtenido = clients.FirstOrDefault(x=> x.Id == id);
    return Objeto_Obtenido;
});


// POST : Recibe Un Objeto Y Lo Guarda En La Lista:
app.MapPost("/clients", (Client client) =>
{
    clients.Add(client);
    return Results.Ok();
});


// PUT : Obtiene un Objeto con ese ID:
app.MapPut("/clients/{id}", (int id,Client client) =>
{
    //Buscamos:
    Client Objeto_Obtenido = clients.FirstOrDefault(x=> x.Id == id);

    if(Objeto_Obtenido!=null)
    {
        //Actualizamos Datos:
        Objeto_Obtenido.Name = client.Name;
        Objeto_Obtenido.LastName = client.LastName;

        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});


// DELETE : Obtiene un Objeto con ese ID Y Lo Elimina De La Lista:
app.MapDelete("/clients/{id}", (int id) =>
{
    //Buscamos:
    Client Objeto_Obtenido = clients.FirstOrDefault(x => x.Id == id);

    if (Objeto_Obtenido != null)
    {
        //Eliminamos:
        clients.Remove(Objeto_Obtenido);

        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});


// CORREMOS LA APLICACION
app.Run();



// CLASE:
internal class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}

