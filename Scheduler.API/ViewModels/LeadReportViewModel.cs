using Scheduler.API.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.ViewModels
{
    public class LeadReportViewModel //: IValidatableObject
    {       
        public string Lead { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal NetProfit { get; set; }
        public decimal CallsReceived { get; set; }
        public decimal TotalAppt { get; set; }
        public decimal BookedPercent { get; set; }
        public decimal CostCall { get; set; }
        public decimal CostLead { get; set; }
        public decimal LeadCost  { get; set; } 

    }
}
