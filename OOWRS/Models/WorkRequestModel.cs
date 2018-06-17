using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOWRS.Models
{
    public class WorkRequestModel
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}