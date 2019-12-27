using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อ Agency ภาษาไทย (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อ Agency อังกฤษ (EN)
        /// </summary>
        public string NameEN { get; set; }

        public static AgentDropdownDTO CreateFromModel(Agent model)
        {
            if (model != null)
            {
                var result = new AgentDropdownDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
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
