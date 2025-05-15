# 📰 BlogApi

> Une API minimaliste en ASP.NET 8 pour gérer des articles de blog avec PostgreSQL.  
> Inclut la création d’articles, le comptage des vues, les likes/dislikes.

---

## 2. ⚙️ Dépendances

Assurez-vous d’avoir :

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- `dotnet-ef` installé globalement :

```bash
dotnet tool install --global dotnet-ef
```

---

## 3. 🗄 Configuration base de données

Créez un fichier **`appsettings.Development.json`** (non poussé sur GitHub) :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=blogdb;Username=postgres;Password=YourPassword;SSL Mode=Disable"
  }
}
```

Puis exécutez :

```bash
dotnet ef database update
```

Cela va créer automatiquement la table `BlogPosts` dans PostgreSQL.

---

## 4. ▶️ Lancer le projet

```bash
dotnet run
```

Votre API est accessible sur :  
👉 http://localhost:5000/posts

---

## 5. 🔌 Endpoints disponibles

| Méthode | Route                   | Action                  |
|--------|--------------------------|--------------------------|
| `GET`  | `/posts`                 | Récupère tous les articles |
| `POST` | `/posts`                 | Crée un nouvel article    |
| `PUT`  | `/posts/{id}/like`       | Ajoute un like           |
| `PUT`  | `/posts/{id}/dislike`    | Ajoute un dislike        |

---

## 6. 📦 Exemple d’article (POST)

```json
{
  "title": "Titre de l'article",
  "content": "Ceci est un exemple de contenu.",
  "author": "Valdes"
}
```

---

## 7. ☁️ Déploiement (préparé)

Pour déployer, utilisez **les variables d’environnement** :

```env
ConnectionStrings__DefaultConnection=...
```

Et laissez `appsettings.json` vide ou neutre (sans mots de passe).

---

## Auteur

**Bravo Mezankou Valdes**  
🌍 [portfoliobravo.netlify.app](https://portfoliobravo.netlify.app)

---

## 🛠️ Envie de contribuer ?

Les pull requests sont bienvenues. Ce projet peut être étendu avec :
- Authentification JWT
- Pagination côté serveur
- Filtres par auteur/date
