﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;

namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }
    }
}
