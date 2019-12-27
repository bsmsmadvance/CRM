using System;
namespace Identity.Params.Outputs
{
    public class UserSyncResponse
    {
        public int Updated { get; set; }
        public int Created { get; set; }
        public int Deleted { get; set; }
    }
}
