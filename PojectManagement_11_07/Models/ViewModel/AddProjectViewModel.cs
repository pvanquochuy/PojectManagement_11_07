using ProjectManagement_11_07.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_11_07.Models.ViewModel
{
    public class AddProjectViewModel
    {
        public int ProjectId { get; set; }


        [Required(ErrorMessage = "Tên dự án là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên dự án không được vượt quá 100 ký tự.")]
        public string ProjectName { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả dự án không được vượt quá 255 ký tự.")]
        public string ProjectDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public string StatusProject { get; set; }

        public List<Users> Users { get; set; } = new List<Users>();

        public List<int> SelectedUserIds { get; set; } = new List<int>();
    }
}
