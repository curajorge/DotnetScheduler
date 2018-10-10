using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class Lead : IEntityBase
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal WeeklyCost { get; set; }
       

    }
}
