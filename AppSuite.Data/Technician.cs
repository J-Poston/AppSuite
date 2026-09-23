using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppSuite.Data
{
    public class Technician
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
