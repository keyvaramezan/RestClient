namespace RestClient.Models
{
    public class SearchRequestDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SearchText { get; set; }
        public string Sort { get; set; }
        public SearchRequestDto()
        {
            PageSize = 10;
            PageIndex = 1;
            SearchText = string.Empty;
            Sort = string.Empty;
        }
    }
}
