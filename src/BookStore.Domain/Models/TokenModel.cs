﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class TokenModel
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
        [Required]
        public string AccessToken { get; set; } = string.Empty;
    }
}
