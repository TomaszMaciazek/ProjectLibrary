using System;

namespace Application.ViewModels.UpdateVM
{
    public abstract class BaseUpdateVM
    {
        public int Id { get; set; }
        public DateTime ModyficationDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
