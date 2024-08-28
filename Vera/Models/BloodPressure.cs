using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vera.Models {
    public class BloodPressure {
        public int id { get; set; }
        public DateTime datetimebloodpressure { get; set; }
        public int sys { get; set; }
        public int dia { get; set; }
        public int pulse { get; set; }
        public string? comment { get; set; }
        public string? UserId { get; set; }
    }
}
