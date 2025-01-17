using ArsHistoriaAPI.Models;
using System.Xml.Linq;

namespace ArsHistoriaAPI.Services.Interfaces
{
    public interface IStyleService
    {
        List<Style> GetStyles();
        Style? GetStyleById(int id);
        Style? GetStyleByName(string name);
        Style CreateStyle(Style style);
        Style? UpdateStyle(Style style);
        void DeleteStyle(Style style);
    }
}
