namespace BlazorApp.Dtos
{
    public class FiltersDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public FiltersDto()
        {
            Page = 1;
            PageSize = 10;
        }
    }
}
