namespace TalabatPro.Api.Helpers
{
    public class Pagination<T>
    {
     

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int CountInPage {  get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data,int countInPage)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Data = data;
            CountInPage = countInPage;
        }
    }
}
