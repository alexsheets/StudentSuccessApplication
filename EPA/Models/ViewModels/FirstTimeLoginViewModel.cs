using System.ComponentModel.DataAnnotations;

namespace EPA.Models.ViewModels
{
    public class FirstTimeLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserType { get; set; }

        // if they are a student
        public string? Concentration {  get; set; }
        public string? Class {  get; set; }

        // if they are an evaluator
        public string? BasedAtLSU { get; set; }

        public string? Clinic {  get; set; }

    }
}
