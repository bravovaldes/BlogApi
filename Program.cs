using BlogApi.Data;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Ajouter la connexion PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(); // ← active le service CORS
var app = builder.Build();
app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());



app.MapGet("/posts", async (AppDbContext db) =>
{
    var posts = await db.BlogPosts.OrderByDescending(p => p.Id).ToListAsync();
    return Results.Ok(posts);
});

app.MapPost("/posts", async (AppDbContext db, BlogPost post) =>
{
    post.Date = DateTime.Now.ToString("dd/MM/yyyy");
    post.Views = new Random().Next(10, 200);
    post.ReadTime = $"{2 + post.Content.Length / 150} min";

    db.BlogPosts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/posts/{post.Id}", post);
});

app.MapPut("/posts/{id}/like", async (AppDbContext db, int id) =>
{
    var post = await db.BlogPosts.FindAsync(id);
    if (post == null) return Results.NotFound();

    post.Likes++;
    await db.SaveChangesAsync();

    return Results.Ok(post);
});

app.MapPut("/posts/{id}/dislike", async (AppDbContext db, int id) =>
{
    var post = await db.BlogPosts.FindAsync(id);
    if (post == null) return Results.NotFound();

    post.Dislikes++;
    await db.SaveChangesAsync();

    return Results.Ok(post);
});


app.Run();
