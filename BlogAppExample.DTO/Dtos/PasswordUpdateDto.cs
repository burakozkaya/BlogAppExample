using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppExample.DTO.Dtos
{
    public class PasswordUpdateDto
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string id { get; set; }
    }
}
