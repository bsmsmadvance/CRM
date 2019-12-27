using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorage;

namespace Base.DTOs
{
    public class FileDTO
    {
        /// <summary>
        /// Url ของไฟล์
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ชื่อไฟล์ (ที่เก็บบน DB)
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// ระบุว่าไฟล์อยู่ใน temp bucket
        /// </summary>
        /// <value><c>true</c> if is temp; otherwise, <c>false</c>.</value>
        public bool IsTemp { get; set; }

        public static async Task<FileDTO> CreateFromFileNameAsync(string name, FileHelper fileHelper)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var url = await fileHelper.GetFileUrlAsync(name);
                var result = new FileDTO()
                {
                    Name = name,
                    Url = url
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
