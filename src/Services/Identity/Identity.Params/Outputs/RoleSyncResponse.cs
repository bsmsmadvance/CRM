using System;
namespace Identity.Params.Outputs
{
    public class RoleSyncResponse
    {
        public int Updated { get; set; }
        public int Created { get; set; }
        public int Deleted { get; set; }
    }
}
