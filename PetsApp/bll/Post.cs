using System;
using System.Collections.Generic;

namespace PetsApp.bll
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int Reward { get; set; }
        public Pet Pet { get; set; }

        private Dictionary<int, string> types = new Dictionary<int, string>()
        {
            { 1, "Petsitting"},
            { 2, "Lost"},
            { 3, "Founded"},
            { 4, "Shelter"}
        };


    }
}