using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Rest_API
{
    public partial class Interventions
    {
        public long Id { get; set; }
        public Nullable<long> AuthorId { get; set; }
        public long CustomerId { get; set; }
        public long BuildingId { get; set; }
        public long? BatteryId { get; set; }
        public Nullable<long> ColumnId { get; set; }
        public Nullable<long> ElevatorId { get; set; }
        public Nullable<long> EmployeeId { get; set; }
         public Nullable<DateTime> StartDate { get; set; }
         public Nullable<DateTime> EndDate { get; set; }
        public string Result { get; set; }
        public string Report { get; set; }
        public string Status { get; set; }

        public virtual Batteries Battery { get; set; }
        public virtual Buildings Building { get; set; }
        public virtual Columns Column { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Elevators Elevator { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
