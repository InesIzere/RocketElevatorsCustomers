using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Rest_API
{
    public partial class Leads
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string Department { get; set; }
        public string Message { get; set; }
        public byte[] Attachment { get; set; }
        public DateTime? contact_request_date  { get; set; }

    }
}
