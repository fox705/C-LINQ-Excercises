using System;
using PetsApp.bll;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PetsApp.dal
{
    public static class ProcessPost
    {
        public static List<Post> ProcessPosts(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');

                        return new Post
                        {
                            PostId = int.Parse(columns[0]),
                            UserId = int.Parse(columns[1]),
                            Title = columns[2],
                            Description = columns[3],
                            Type = columns[4],
                            StarTime = DateTime.Parse(columns[5]),
                            EndTime = DateTime.Parse(columns[6]),
                            Reward = int.Parse(columns[7]),
                            Active = bool.Parse(columns[8]),
                            Duration = int.Parse(columns[9])


                        };


                    });

            return query.ToList();
        }
    }
}