using System.Collections.Generic;
using System.Security.Policy;

namespace PetsApp.bll
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public int Age { get; set; }
        public int RewardPoints { get; set; }

        public List<Post> Posts { get; set; }
    }
}