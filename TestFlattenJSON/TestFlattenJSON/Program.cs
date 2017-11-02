using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TestFlattenJSON.Processing;

namespace TestFlattenJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var jsonData = File.ReadAllText(Path.Combine(workingDirectory, @"Resources\CFA_RFI_sample_mobile_order.json"));
            var flatJsonData = new DataProcessor().FlattenJsonData(jsonData);
            foreach(var item in flatJsonData)
            {
                Console.WriteLine($"Key: {item.Key} | Value: {item.Value.ToString()}");
            }
            Console.WriteLine("Hit enter to exit....");
            Console.ReadLine();
        }
    }
}
