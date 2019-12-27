using Base.DTOs.CTM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Outputs
{
    public class ContactPaging
    {
        public List<ContactListDTO> Contacts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
