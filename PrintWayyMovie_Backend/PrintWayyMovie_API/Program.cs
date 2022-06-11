using PrintWayyMovie_API;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SqliteConnectionString") ?? "Data Source=PrintWayy_Movie.db";

builder.Services.AddSqlite<DataContext>(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

StartDatabase startDatabase = new StartDatabase();
await startDatabase.VerificaExistenciaDatabase(app.Services, app.Logger, connectionString);

// Rotas para chamada das APIs
#region Rotas Gerenciamento de Usu�rios

// Rota Get todos Usu�rios
app.MapGet("/usuarios", async (DataContext db) =>
    await db.Usuario.ToListAsync()
);

// Rota Post Usu�rio
app.MapPost("/create-usuario", async (Usuario dadosUsuario, DataContext db) =>
{
    if (dadosUsuario != null)
    {
        db.Usuario.Add(dadosUsuario);
        await db.SaveChangesAsync();

        return Results.Created($"/create-usuario/{dadosUsuario.Id}", dadosUsuario);
    }
    else
    {
        return Results.BadRequest("Requisi��o inv�lida!");
    }
});

#endregion

#region Gerencimento de Salas

// rota Get todas Salas
app.MapGet("/salas", async (DataContext db) =>
    await db.Sala.ToListAsync()
);

// rota Get uma Sala por Id
app.MapGet("/salas/{id}", async (int id, DataContext db) =>
    await db.Sala.FindAsync(id)
        is Sala sala
            ? Results.Ok(sala)
            : Results.NotFound()
);

// rota Post sala
app.MapPost("/create-sala", async (Sala dadosSala, DataContext db) =>
{
    if (dadosSala != null)
    {
        db.Sala.Add(dadosSala);
        await db.SaveChangesAsync();

        return Results.Created($"/create-sala/{dadosSala.Id}", dadosSala);
    }
    else
    {
        return Results.BadRequest("Requisi��o inv�lida!");
    }
});

#endregion

#region Gerenciamento de Filmes

// rota Get todos Filmes
app.MapGet("/filmes", async (DataContext db) =>
    await db.Filme.ToListAsync()
);

// rota Get um Filme por Id
    app.MapGet("/filmes/{id}", async (int id, DataContext db) =>
    await db.Filme.FindAsync(id)
        is Filme filme
            ? Results.Ok(filme)
            : Results.NotFound()
);

// rota Post Filme
app.MapPost("/create-filme", async (Filme dadosFilme, DataContext db) =>
{
    if (dadosFilme.Cl_Imagem == null)
    {
        return Results.BadRequest("Campo Imagem � obrigat�rio");
    }
    if (dadosFilme.Cl_Titulo == null)
    {
        return Results.BadRequest("Campo T�tulo � obrigat�rio");
    }
    if (dadosFilme.Cl_Descricao == null)
    {
        return Results.BadRequest("Campo Descri��o � obrigat�rio");
    }
    if (dadosFilme.Cl_Duracao == null)
    {
        return Results.BadRequest("Campo Dura��o � obrigat�rio");
    }
    var listFilmes = await db.Filme.ToListAsync();
    foreach (var x in listFilmes)
    {
        if(x.Cl_Titulo == dadosFilme.Cl_Titulo)
        {
            return Results.BadRequest("T�tulo j� existente no banco de dados");
        }
    }
    db.Filme.Add(dadosFilme);
    await db.SaveChangesAsync();

    return Results.Created($"/create-filme/{dadosFilme.Id}", dadosFilme);
    
});

// rota Put Filme
app.MapPut("/update-filme/{id}", async (int id, Filme dadosFilme, DataContext db) =>
{
    var filme = await db.Filme.FindAsync(id);
    if (filme is null) return Results.NotFound();

    if (dadosFilme.Cl_Imagem == null)
    {
        return Results.BadRequest("Campo Imagem � obrigat�rio");
    }
    if (dadosFilme.Cl_Titulo == null)
    {
        return Results.BadRequest("Campo T�tulo � obrigat�rio");
    }
    if (dadosFilme.Cl_Descricao == null)
    {
        return Results.BadRequest("Campo Descri��o � obrigat�rio");
    }
    if (dadosFilme.Cl_Duracao == null)
    {
        return Results.BadRequest("Campo Dura��o � obrigat�rio");
    }

    filme.Cl_Imagem = dadosFilme.Cl_Imagem;
    filme.Cl_Titulo = dadosFilme.Cl_Titulo;
    filme.Cl_Descricao = dadosFilme.Cl_Descricao;
    filme.Cl_Duracao = dadosFilme.Cl_Duracao;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// rota Delete Filme
app.MapDelete("/delete-filme/{id}", async (int id, DataContext db) =>
{
    if (await db.Filme.FindAsync(id) is Filme dadosFilme)
    {
        var listSessoes = await db.Sessao.ToListAsync();
        foreach (var x in listSessoes)
        {
            if (x.Fk_IdFilme == id)
            {
                return Results.BadRequest("O filme selecionado possui uma sess�o vinculada");
            }
        }
        db.Filme.Remove(dadosFilme);
        await db.SaveChangesAsync();
        return Results.Ok(dadosFilme);
    }
    return Results.NotFound();
});

#endregion

#region Gerenciamento de Sess�es

// rota Get todas Sess�es
app.MapGet("/sessoes", async (DataContext db) =>
    await db.Sessao.ToListAsync()
);

// rota Post Sess�o
app.MapPost("/create-sessao", async (Sessao dadosSessao, DataContext db) =>
{
    if (dadosSessao.Cl_Data == null)
    {
        return Results.BadRequest("Campo data � obrigat�rio");
    }
    if (dadosSessao.Cl_HoraInicio == null)
    {
        return Results.BadRequest("Campo Hr. In�cio � obrigat�rio");
    }
    if (dadosSessao.Cl_Valor == 0)
    {
        return Results.BadRequest("Campo Valor � obrigat�rio");
    }

    var filme = await db.Filme.FindAsync(dadosSessao.Fk_IdFilme);
    var sala = await db.Sala.FindAsync(dadosSessao.Fk_IdSala);

    // Bloco que calcula a Hr. Fim da sess�o
    TimeSpan duracaoFilme = TimeSpan.FromMinutes(Convert.ToInt32(filme.Cl_Duracao));
    TimeSpan horainicioObj = TimeSpan.Parse(dadosSessao.Cl_HoraInicio);
    TimeSpan resultadoHoraFim = horainicioObj.Add(duracaoFilme);
    string campoHoraFim = resultadoHoraFim.ToString();
    dadosSessao.Cl_HoraFim = campoHoraFim;

    // Bloco que verifica se a sala est� ocupada
    var todasSessoes = await db.Sessao.ToListAsync();
    foreach (var x in todasSessoes)
    {
        if (dadosSessao.Fk_IdSala == x.Fk_IdSala &&
            dadosSessao.Cl_Data == x.Cl_Data &&
            Convert.ToDateTime(dadosSessao.Cl_HoraInicio) <= Convert.ToDateTime(x.Cl_HoraFim) &&
            Convert.ToDateTime(campoHoraFim) >= Convert.ToDateTime(x.Cl_HoraInicio))
        {
            return Results.BadRequest("A sala selecionada j� possui sess�o nesse mesmo hor�rio");
        }
    }

    dadosSessao.NomeFilme = filme.Cl_Titulo;
    dadosSessao.NomeSala = sala.Cl_Nome;

    db.Sessao.Add(dadosSessao);
    await db.SaveChangesAsync();

    return Results.Created($"/create-sessao/{dadosSessao.Id}", dadosSessao);
    
});

// rota Delete Sess�o
app.MapDelete("/delete-sessao/{id}", async (int id, DataContext db) =>
{
    if (await db.Sessao.FindAsync(id) is Sessao dadosSessao)
    {
        int diferencaDeDias = Convert.ToInt32(Convert.ToDateTime(dadosSessao.Cl_Data).Subtract(DateTime.Today).TotalDays);
        if (diferencaDeDias < 10)
        {
            return Results.BadRequest("Faltam " + diferencaDeDias + " dias para esta sess�o, n�o � poss�vel remover sess�es que faltem menos de 10 dias");
        }
        db.Sessao.Remove(dadosSessao);
        await db.SaveChangesAsync();
        return Results.Ok(dadosSessao);
    }
    return Results.NotFound();
});

#endregion

app.MapSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");

app.Run();
