using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Entities
{
    public class Subject : Base<int>
    {
        public int MaxCapacity { get; set; }

    }
}
