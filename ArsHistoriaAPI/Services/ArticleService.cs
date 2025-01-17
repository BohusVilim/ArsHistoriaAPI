using ArsHistoriaAPI.Data;
using ArsHistoriaAPI.Models;
using ArsHistoriaAPI.Services.Interfaces;

namespace ArsHistoriaAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ArsHistoriaDbContext _context;
        public ArticleService(ArsHistoriaDbContext context) 
        { 
            _context = context;
        }

        public List<Article> GetArticles()
        {
            return _context.Articles.ToList();
        }

        public Article? GetArticleById(int id)
        {
            return _context.Articles.FirstOrDefault(a => a.Id == id);
        }

        public Article? GetArticleByTitle(string title)
        {
            return _context.Articles.FirstOrDefault(a => a.Title == title);
        }

        public Article CreateArticle(Article article)
        {
            if (_context.Articles.Any(a => a.Title == article.Title))
            {
                throw new InvalidOperationException($"An article with the title '{article.Title}' already exists.");
            }

            if (article.Styles == null || !article.Styles.Any())
            {
                throw new InvalidOperationException("Article must have at least one style.");
            }

            var styleNames = article.Styles.Select(s => s.Name).ToList();
            var existingStyles = _context.Styles.Where(s => styleNames.Contains(s.Name)).ToList();

            article.Styles = existingStyles;

            _context.Articles.Add(article);
            _context.SaveChanges();

            return article;
        }
    }
}
