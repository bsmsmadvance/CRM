using Base.DTOs.MST;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Params.Outputs
{
    public class TypeOfRealEstatePaging
    {
        public List<TypeOfRealEstateDTO> TypeOfRealEstates { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
