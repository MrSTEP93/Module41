﻿using System.ComponentModel.DataAnnotations;

namespace FinalBlog.ViewModels.User
{
    public class UserEditViewModel : BaseUserData
    {
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "О себе", Prompt = "Введите данные о себе")]
        public string? About { get; set; }

        public List<Data.Models.Role> Roles { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
