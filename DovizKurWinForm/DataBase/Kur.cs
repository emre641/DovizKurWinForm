using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DovizKurWinForm.DataBase
{
    public class Kur
    {
        [Key]
        public int Id { get; set; }
        public string ad { get; set; }
        public float alis { get; set; }
        public float satis { get; set; }
        
        
    }
}
