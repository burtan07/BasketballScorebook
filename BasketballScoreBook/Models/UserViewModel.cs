using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballScoreBook.Models
{
    public class UserViewModel
    {
        public UserModel SingleUser { get; set; }
        public List<UserModel> Users { get; set; }
    }
}