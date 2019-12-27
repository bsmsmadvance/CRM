﻿using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class BankDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// รหัสธนาคาร
        /// </summary>
        public string BankNo { get; set; }
        /// <summary>
        /// ชื่อธนาคารภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อธนาคารภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ชื่อย่อ
        /// </summary>
        public string Alias { get; set; }

        public static BankDropdownDTO CreateFromModel(Bank model)
        {
            if (model != null)
            {
                var result = new BankDropdownDTO()
                {
                    Id = model.ID,
                    BankNo = model.BankNo,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Alias = model.Alias

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
