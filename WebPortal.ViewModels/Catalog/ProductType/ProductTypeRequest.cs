using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebPortal.Data.Enums;

namespace WebPortal.ViewModels
{
    public class ProductTypeRequest
    {
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string LanguageID { get; set; }
        [Display(Name = "Is Public?")]
        public bool IsPublic { get; set; }
        public Status Status { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text3 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text4 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text5 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text6 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text7 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text8 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text9 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text10 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text11 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text12 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text13 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text14 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text15 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text16 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text17 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text18 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text19 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text20 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc3 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc4 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc5 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc6 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc7 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc8 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc9 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Desc10 { get; set; }
        public int? WebsiteID { get; set; }
    }
}
