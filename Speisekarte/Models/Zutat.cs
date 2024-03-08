using System.ComponentModel.DataAnnotations;

namespace Speisekarte.Models
{
    public class Zutat
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Beschreibung { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Einheit { get; set; }
        public decimal Menge { get; set; }
        public Speise? Speise { get; set; }
        public int? SpeiseId { get; set; }  
        public override string ToString()
        {
            return $"{Menge} {Einheit} {Beschreibung}";
        }


    }
}
