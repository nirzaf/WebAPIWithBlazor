using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.QueryFilters
{
    public class TicketQueryFilter
    {
        public int? Id { get; set; }
        public string TitleOrDescription { get; set; }        
    }
}
