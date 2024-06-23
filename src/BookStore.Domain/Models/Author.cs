using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Author: Entity
    {
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
