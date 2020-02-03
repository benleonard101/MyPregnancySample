﻿namespace MyPregnancy.Application
{
    public class QueryableArgsBase : IPaginationInfo
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
