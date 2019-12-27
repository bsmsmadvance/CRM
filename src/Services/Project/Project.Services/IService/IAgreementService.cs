using Base.DTOs.PRJ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IAgreementService
    {
        Task<AgreementDTO> UpdateAgreementAsync(Guid projectID, Guid id, AgreementDTO input);
        Task<AgreementDTO> GetAgreementAsync(Guid projectID);
    }
}
