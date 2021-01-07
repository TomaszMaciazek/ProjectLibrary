using System;
using System.Collections.Generic;

namespace Application.ViewModels.AddVM
{
    public class AddBookVM : BaseAddVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearOfRelease { get; set; }
        public int PublisherId { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public ICollection<int> AuthorsId { get; set; }
    }
}
