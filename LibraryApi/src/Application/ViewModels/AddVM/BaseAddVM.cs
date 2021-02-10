using System;

namespace Application.ViewModels.AddVM
{
    public abstract class BaseAddVM
    {
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
