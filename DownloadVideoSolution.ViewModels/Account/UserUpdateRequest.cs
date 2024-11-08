using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSolution.ViewModels.Account
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        [Range(1,3)]
        public int? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
