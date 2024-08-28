using System;
using Microsoft.AspNetCore.Identity;

namespace Vera.Models {
    public class User {
        public int id { get; set; }
        public string? firstname { get; set;}
        public string? lastname { get; set; }
        public DateTime birthdate { get; set; }
        public string? isapproved { get; set; }
        public string? role { get; set; }
        public string? email { get; set; }
        public string? emailconfirmed { get; set;}
    }
}
