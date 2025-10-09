using System;

namespace UniManage.Core.Models
{
    public class PagingResult
    {
        public PagingResult(int pageIndex, int pageSize, int totalRecords)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages =>
            PageSize > 0 ? (int)Math.Ceiling((double)TotalRecords / PageSize) : 0;
    }
}
