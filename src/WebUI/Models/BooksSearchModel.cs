using System.Collections.Generic;

namespace WebUI.Models
{
    public class BooksSearchModel
    {
        public IEnumerable<int> CategoriesIds { get; set; }
        public IEnumerable<int> PublishersIds { get; set; }
        public IEnumerable<int> AuthorsIds { get; set; }
        public string TitleFilterString { get; set; }
        public string OrderBy { get; set; }
        public bool OnlyAwailable { get; set; }
    }
}
