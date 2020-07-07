using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DUT4UStudentApp.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [DisplayName("Student Number")]
        public string StudentNo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Home Address")]
        public string HomeAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        [DisplayName("Mobile No.")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }

        public string ImageURL { get; set; }
    }
}