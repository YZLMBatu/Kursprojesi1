using Eticaret.Core.Entities;

namespace Kursprojesi.WebUI.Models
{
    public class HomePageViewModel
    {
        public List<Slide>? Slides { get; set; }
        public List<Product>? Products { get; set; }
        public List<News>? News { get; set; }
    }
}
