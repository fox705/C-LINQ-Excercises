using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {

            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacture("manufacturers.csv");

            var document = new XDocument();
            var Xcars = new XElement("Cars",
                        from c in cars
                        select new XElement("Car",
                        new XAttribute("Name", c.Name),
                        new XAttribute("Combined", c.Combined),
                        new XAttribute("Manufacturer", c.Manufacturer)));





            


            document.Add(Xcars);
            document.Save("fuel.xml");

            var query = from car in cars
                        group car by car.Manufacturer.ToUpper()
                            into m
                        orderby m.Key
                        select m;

            var query2 = cars.GroupBy(c => c.Manufacturer.ToUpper())
                .OrderBy(g => g.Key);


            var query3 = from manufacturer in manufacturers
                         join car in cars on manufacturer.Name equals car.Manufacturer
                             into carGroup
                         orderby manufacturer.Name
                         select new
                         {
                             Manufacturer = manufacturer,
                             Car = carGroup
                         };

            var query4 = manufacturers.GroupJoin(cars, m => m.Name.ToUpper(), c => c.Manufacturer.ToUpper(),
                (m, c) => new
                {
                    Manufacturer = m,
                    Car = c

                }).GroupBy(m => m.Manufacturer.Headquarters);

            var query5 = from car in cars
                         group car by car.Manufacturer
                into carGroup
                         select new
                         {
                             Name = carGroup.Key,
                             Max = carGroup.Max(c => c.Combined),
                             Min = carGroup.Min(c => c.Combined),
                             Average = carGroup.Average(c => c.Combined)

                         }
                into result
                         orderby result.Max descending
                         select result;

            var query6 = cars.GroupBy(c => c.Manufacturer).Select(g => new
            {
                Name = g.Key,
                Max = g.Max(c => c.Combined),
                Min = g.Min(c => c.Combined),
                Average = g.Average(c => c.Combined)
            }).OrderByDescending(r => r.Max);

            var query7 = cars.GroupBy(c => c.Manufacturer).Select(g =>
            {
                var results = g.Aggregate(new CarStatistics(), (acc, c) => acc.Accumulate(c), acc => acc.Compute());

                return new
                {
                    Name = g.Key,
                    Max = results.Max,
                    Min = results.Min,
                    Average = results.Average
                };
            }).OrderByDescending(r => r.Max);

            foreach (var group in query7)
            {
                Console.WriteLine($"{group.Name}");
                Console.WriteLine($"{group.Max}");
                Console.WriteLine($"{group.Min}");
                Console.WriteLine($"{group.Average}");

            }

        }

        private static List<Car> ProcessFile(string path)
        {
            // var query2 = query.Where(c => c.Year > 2000).OrderByDescending(c => c.Displacement).ThenBy(c => c.Year);

            var query3 = File.ReadAllLines(path).Skip(1).ToCar();

            return query3.ToList();

        }

        private static List<Manufacturer> ProcessManufacture(string path)
        {

            var query =
                File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>
            {
                var columns = l.Split(',');

                return new Manufacturer
                {
                    Name = columns[0],
                    Headquarters = columns[1],
                    Year = int.Parse(columns[2])
                };

            });

            return query.ToList();

        }

    }

    public class CarStatistics
    {

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
    }

    public static class CarExtension
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {

            foreach (var line in source)
            {

                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])

                };

            }


        }

    }



    //var mquery = manufacturers.Select(m => m.Name).Take(10);

    //foreach (var m in manufacturers)
    //{
    //Console.WriteLine($"{m.Name} :   {m.Headquarters}   :   {m.Year}");
    //}

    //var query = from car in cars
    //where car.Manufacturer == "BMW"
    //orderby car.Manufacturer ascending, car.Year descending
    //select new
    //{
    //    car.Name,
    //    car.Manufacturer,
    //    car.Combined
    //};

    //var results = cars.Skip(1).Select(c => new { c.Manufacturer, c.Name, c.Combined }).Take(10);


    //var selectmany = cars.SelectMany(c => c.Name).OrderByDescending(c => c);

    //foreach (var character in selectmany.Take(10))
    //{
    //Console.WriteLine(character);
    //}

    //Console.WriteLine(results.ToString());
    //Console.WriteLine("________________________________");
    //foreach (var result in results)
    //{
    //    Console.WriteLine(result);

    //}
    //Console.WriteLine("________________________________");




}

