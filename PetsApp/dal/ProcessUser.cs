using PetsApp.bll;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PetsApp.dal
{
    public class ProcessUser
    {



        public static List<User> ProcessUsers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');

                        return new User
                        {
                            UserId = int.Parse(columns[0]),
                            Name = columns[1],
                            Password = columns[2],
                            Adress = columns[3],
                            Age = int.Parse(columns[4]),
                            RewardPoints = int.Parse(columns[5])



                        };


                    });

            return query.ToList();
        }
    }
}