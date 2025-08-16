using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Helper
{
    public class EncryptionSettings
    {
        public string Salt { get; set; }
        public string Password { get; set; }
    }
}
