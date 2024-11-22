﻿using System.ComponentModel.DataAnnotations;

namespace FinalBlog.ViewModels.User
{
    public class UserViewModel : BaseUserData
    {
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "О себе", Prompt = "Введите данные о себе")]
        public string? About { get; set; }

        public List<DATA.Models.Role> Roles { get; set; }
    }
}