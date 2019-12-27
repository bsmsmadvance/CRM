using Database.Models;
using Database.Models.SAL;
using FileStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class AgreementFileDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public SAL.AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// ชื่อไฟล์
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO Files { get; set; }

        public static async Task<AgreementFileDTO> CreateFromModel(AgreementFile model)
        {
            if (model != null)
            {
                AgreementFileDTO result = new AgreementFileDTO()
                {
                    AgreementNo = model.Agreement.AgreementNo,
                    Agreement = SAL.AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    FileName = model.FileName
                    //Files = FileDTO.CreateFromFileNameAsync(model.FileName, fileHelper)
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
