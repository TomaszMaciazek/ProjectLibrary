namespace Application.Args
{
    public abstract class BasePaginationArgs
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
