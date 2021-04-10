using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DovizKurWinForm.DataBase
{
    public class Context : DbContext
    {
        
        public DbSet<Kur> Kurlar { get; set; }
    }
}
