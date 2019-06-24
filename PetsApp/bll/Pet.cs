using System.Collections.Generic;

namespace PetsApp.bll
{
    public class Pet
    {
        public int PetId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Type { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public int Size { get; set; }


        private Dictionary<int, string> types = new Dictionary<int, string>()
        {
            { 1, "Dog"},
            { 2, "Cat"}
        };

        private Dictionary<int, string> sextypes = new Dictionary<int, string>()
        {
            { 1, "male"},
            { 2, "female"}
        };

    }
}