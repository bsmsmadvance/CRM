using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commission.Params.Inputs
{
    public class ProjectInput
    {
        public Guid? ProjectID { get; set; }

        public static ProjectInput CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectInput()
                {
                    ProjectID = model.ID
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public class ListProjectInput
    {
        public List<ProjectInput> Projects { get; set; }

    }
}
