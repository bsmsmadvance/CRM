using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentEmployeeDropdownDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static AgentEmployeeDropdownDTO CreateFromModel(AgentEmployee model)
        {
            if (model != null)
            {
                var result = new AgentEmployeeDropdownDTO()
                {
                    Id = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
