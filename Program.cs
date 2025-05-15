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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


app.MapGet("/posts", async (AppDbContext db) =>
{
    var posts = await db.BlogPosts.OrderByDescending(p => p.Id).ToListAsync();
    return Results.Ok(posts);
});

app.MapPost("/posts", async (AppDbContext db, BlogPost post) =>
{
    post.Date = DateTime.Now.ToString("dd/MM/yyyy");
    post.Views = 0;
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
app.MapPut("/posts/views", async (AppDbContext db) =>
{
    var posts = await db.BlogPosts.ToListAsync();

    foreach (var post in posts)
    {
        post.Views++;
    }

    await db.SaveChangesAsync();

    return Results.Ok("Vues incrémentées pour tous les articles.");
});


app.Run();
