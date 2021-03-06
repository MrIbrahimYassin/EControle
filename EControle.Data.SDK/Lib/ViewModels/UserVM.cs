﻿using System;
using EControle.Core.Data.Types;

namespace EControle.Data.SDK.Lib.ViewModels
{
    public class UserVM
    {
        public long Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte[] Img { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public bool SetNewPassword { get; set; }
        public bool IsDeleted { get; set; }
        public Role Role { get; set; }
    }
}