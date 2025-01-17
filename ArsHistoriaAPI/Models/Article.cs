﻿namespace ArsHistoriaAPI.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Subtitle { get; set; }
        public List<Style> Styles { get; set; } = new List<Style>();
    }
}