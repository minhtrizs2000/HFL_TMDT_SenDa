using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSenDa.Models
{
    public class ResetModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string Email { get; set; }

    }

}