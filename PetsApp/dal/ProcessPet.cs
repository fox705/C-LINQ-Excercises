using PetsApp.bll;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace PetsApp.dal
{
    public class ProcessPet
    {

        public static List<Pet> ProcessPets(string path)
        {
            var query = File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');

                    return new Pet
                    {
                        PetId = int.Parse(columns[0]),
                        PostId = int.Parse(columns[1]),
                        Name = columns[2],
                        Race = columns[3],
                        Type = columns[4],
                        Sex = columns[5],
                        Age = int.Parse(columns[6]),
                        Size = int.Parse(columns[7])

                    };

                });

            return query.ToList();
        }

        

    }
}