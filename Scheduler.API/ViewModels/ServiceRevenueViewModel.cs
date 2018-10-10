using Scheduler.API.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.ViewModels
{
    public class ServiceRevenueViewModel //: IValidatableObject
    {
        public string WorkType { get; set; }
        public decimal RevenueTotal { get; set; }
        public decimal Total { get; set; }

    }
}
