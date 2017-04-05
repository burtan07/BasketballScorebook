using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [RegularExpression("^[:alpha:0-9_]*$", ErrorMessage = "Please use letters, numbers, or underscore")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [RegularExpression("^[:alpha:0-9]*$", ErrorMessage = "Please use letters and numbers")]
        public string Password { get; set; }

        
        [MaxLength(20)]
        [MinLength(2)]
        [RegularExpression("^[:alpha:'-]*$", ErrorMessage = "Please use letters")]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [MinLength(2)]
        [RegularExpression("^[:alpha:'-]*$", ErrorMessage = "Please use letters")]
        public string LastName { get; set; }

        [MaxLength(20)]
        [MinLength(7)]
        [RegularExpression("^[:alpha:0-9@.-_]*$", ErrorMessage = "Please use letters, numbers and punctuation")]
        public string EmailAddress { get; set; }
        public string SecurityQuestion { get; set; }

        [MaxLength(20)]
        [MinLength(2)]
        [RegularExpression("^[:alpha:0-9'.-]*$", ErrorMessage = "Please use letters, numbers or punctuation")]
        public string SecurityAnswer { get; set; }
    }
}