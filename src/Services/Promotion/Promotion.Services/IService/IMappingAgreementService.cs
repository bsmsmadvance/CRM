using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;

namespace Promotion.Services
{
    public interface IMappingAgreementService
    {
        Task<List<MappingAgreementDTO>> GetMappingAgreementsDataFromExcelAsync(FileDTO input);
        Task<List<MappingAgreementDTO>> ConfirmImportMappingAgreementsAsync(List<MappingAgreementDTO> inputs);
        Task<FileDTO> ExportMappingAgreementsAsync();
    }
}
