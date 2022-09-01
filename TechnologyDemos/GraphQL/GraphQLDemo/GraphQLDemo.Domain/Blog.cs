using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDemo.Domain
{
    public class Blog : DomainObject
    {
        public string Name { get; set; }
        public string Descripton { get; set; }
        public int CreatedBy { get; set; }
    }
}
