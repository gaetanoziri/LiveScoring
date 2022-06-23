using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScoring.Models
{
    public class Driver
    {
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Car { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1} {2}-{3}", Number, FirstName, LastName, Car);
        }

    }
}
