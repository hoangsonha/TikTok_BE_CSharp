namespace TikTokAPI.Response
{
    public class ObjectPageList : ObjectResponse
    {
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

    }
}
