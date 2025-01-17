using ArsHistoriaAPI.Data;
using ArsHistoriaAPI.Models;
using ArsHistoriaAPI.Services.Interfaces;
using Microsoft.Extensions.Localization;

namespace ArsHistoriaAPI.Services
{
    public class StyleService : IStyleService
    {
        private readonly ArsHistoriaDbContext _context;

        public StyleService(ArsHistoriaDbContext context)
        {
            _context = context;
        }

        public List<Style> GetStyles()
        {
            return _context.Styles.ToList();
        }

        public Style? GetStyleById(int id)
        {
            return _context.Styles.FirstOrDefault(a => a.Id == id);
        }

        public Style? GetStyleByName(string name)
        {
            return _context.Styles.FirstOrDefault(x => x.Name == name);
        }

        public Style CreateStyle(Style style)
        {
            if (_context.Styles.Any(a => a.Name == style.Name))
            {
                throw new InvalidOperationException($"A style with the name '{style.Name}' already exists.");
            }

            try
            {
                _context.Styles.Add(style);
                _context.SaveChanges();
                return style;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the style.", ex);
            }
        }

        public Style? UpdateStyle(Style style) 
        {
            try
            {
                var dbStyle = _context.Styles.FirstOrDefault(a => a.Id == style.Id);

                if (dbStyle != null)
                {
                    dbStyle.Id = style.Id;
                    dbStyle.Name = style.Name;
                    dbStyle.Period = style.Period;

                    _context.SaveChanges();

                    return dbStyle;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the style.", ex);
            }
        }

        public void DeleteStyle(Style style)
        {
            try
            {
                _context.Styles.Remove(style);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the style.", ex);
            }
        }
    }
}
