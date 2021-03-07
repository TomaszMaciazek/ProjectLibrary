namespace Application.Args
{
    public class BookFilterArgs : BasePaginationArgs
    {
        public string FilterTitleString { get; set; }
        public string [] Authors { get; set; }
        public string[] Categories { get; set; }
        public string[] Publishers { get; set; }
        public bool OnlyAvailable { get; set; }
    }
}
