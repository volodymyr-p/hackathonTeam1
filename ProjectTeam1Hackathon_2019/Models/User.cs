using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTeam1Hackathon_2019.Models
{
    public class User : Entity.AspNetUsers
    {
        public string Role { get; set; }
    }
}