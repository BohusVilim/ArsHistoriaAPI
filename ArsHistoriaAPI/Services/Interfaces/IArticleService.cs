using ArsHistoriaAPI.Models;
using System.Xml.Linq;

namespace ArsHistoriaAPI.Services.Interfaces
{
    public interface IArticleService
    {
        List<Article> GetArticles();
        Article? GetArticleById(int id);
        Article? GetArticleByTitle(string title);
        Article CreateArticle(Article article);
        Article? UpdateArticle(Article aricle);
    }
}
