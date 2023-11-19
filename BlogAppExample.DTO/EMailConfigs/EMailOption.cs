﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAppExample.DTO.EMailConfigs
{
    public class EMailOption
    {
        public EMailOption ServiceEmailOption { get; set; }
        public string Host { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
