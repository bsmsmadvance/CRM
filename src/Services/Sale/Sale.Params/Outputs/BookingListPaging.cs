using Base.DTOs.SAL;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Outputs
{
    public class BookingListPaging
    {
        public List<BookingListDTO> Bookings { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
