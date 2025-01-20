using ArsHistoriaAPI.Models;
using ArsHistoriaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArsHistoriaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleService _service;

        public ArticleController(ILogger<ArticleController> logger, IArticleService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("Articles")]
        public IActionResult GetArticles()
        {
            try
            {
                var articles = _service.GetArticles();
                if (articles == null || !articles.Any())
                {
                    return NotFound("No articles found.");
                }
                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching articles.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleById(int id)
        {
            try
            {
                var articles = _service.GetArticleById(id);
                if (articles == null)
                {
                    return NotFound($"Article with ID {id} not found.");
                }
                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching article with ID {StyleId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ByTitle/{title}")]
        public IActionResult GetArticleByTitle(string title)
        {
            try
            {
                var articles = _service.GetArticleByTitle(title);
                if (articles == null)
                {
                    return NotFound($"Article with ID {title} not found.");
                }
                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching article with name {StyleTitle}.", title);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateArticle([FromBody] Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdArticle = _service.CreateArticle(article);

                if (createdArticle == null)
                {
                    return StatusCode(500, "Failed to create article.");
                }

                return CreatedAtAction(nameof(_service.GetArticleById), new { id = createdArticle.Id }, createdArticle);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while creating article.");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating article.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateArticle([FromBody] Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedArticle = _service.UpdateArticle(article);
                if (updatedArticle == null)
                {
                    return NotFound($"Article with ID {article.Id} not found.");
                }

                return Ok(updatedArticle);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error while updating article.");
                return StatusCode(500, "A database error occurred while updating the article.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating article.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                var article = _service.GetArticleById(id);
                if (article == null)
                {
                    return NotFound($"Article not found.");
                }

                _service.DeleteArticle(article);
                return Ok(new { message = $"Article {article.Title} was successfully deleted." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Article");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
