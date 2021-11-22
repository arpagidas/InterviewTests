using BlazorApp.Commons;
using System;
using System.Collections.Generic;

namespace BlazorApp.Dtos
{
    public class PagedCustomerDtos
    {
        public Metadata Metadata { get; set; }
        public List<CustomerListDto> Customers { get; set; }

        public PagedCustomerDtos()
        {
            Metadata = new Metadata
            {
                CurrentPage = 0,
                PageSize = 20,
                TotalCount = 0,
                TotalPages = 1
            };
            Customers = new List<CustomerListDto>();
        }

        public void UpdateMetadata(int count, int pageNumber, int pageSize)
        {
            Metadata.TotalCount = count;
            Metadata.CurrentPage = pageNumber;
            Metadata.PageSize = pageSize;
            Metadata.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
