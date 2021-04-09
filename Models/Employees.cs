using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Rest_API
{
    public partial class Employees
    {
        public Employees()
        {
            Interventions = new HashSet<Interventions>();
        }

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public long? UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Interventions> Interventions { get; set; }
    }
}
