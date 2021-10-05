using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TestFlattenJSON.Processing;
using TestFlattenJSON.Models;
using TestFlattenJSON.Connectors;

namespace TestFlattenJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            #region Testing Code for JSON Flatten
            /*
            // Json testing files
            //var jsonData = File.ReadAllText(Path.Combine(workingDirectory, @"Resources\CFA_RFI_sample_mobile_order.json"));
            var jsonData = File.ReadAllText(Path.Combine(workingDirectory, @"Resources\sample.json"));

            var flatJsonData = new DataProcessor().FlattenJsonData(jsonData);
            foreach (var item in flatJsonData)
            {
                var key = item.Key;
                var value = item.Value == null ? "" : item.Value.ToString();
                Console.WriteLine($"Key: {key} | Value: {value}");
            }
            Console.WriteLine("Hit enter to exit....");
            Console.ReadLine();
            */
            #endregion // Testing Code for JSON Flatten

            var textData = File.ReadLines(Path.Combine(workingDirectory, @"Resources\transactions.txt"));
            var outputPath = Path.Combine(workingDirectory, @"Output\TransactionSample.csv");
            var jsonFlattenService = new DataProcessor();
            var csvService = new CSVConnector();
            var flatData = new List<Dictionary<string, object>>();
            var distinctKeys = new List<string>();
            var csvRows = new DataModel();
            int rowNumber = 1;
            Console.WriteLine($"processing row number {rowNumber}");
            foreach (var textLine in textData)
            {
                var row = new RowModel();
                var processedItem = jsonFlattenService.FlattenJsonData(textLine);
                flatData.Add(processedItem);
                distinctKeys.AddRange(processedItem.Keys.ToList());
                foreach(var keyValue in processedItem)
                {
                    var rowValueItem = new RowValueModel();
                    rowValueItem.ColumnName = keyValue.Key;
                    rowValueItem.ColumnValue = keyValue.Value == null ? "" : keyValue.Value.ToString();
                    row[keyValue.Key] = rowValueItem;
                }
                row.RowNumber = rowNumber;
                csvRows[rowNumber++] = row;
            }

            // more efficient ways to do this but trying to do it quickly
            // translate keys to columns
            int position = 0;
            var columns = new Dictionary<int, ColumnModel>();
            foreach(var key in distinctKeys.Distinct())
            {
                columns.Add(position++, new ColumnModel() { ColumnType = System.Data.SqlDbType.VarChar, MaxSize = 2000, Name = key, Position = position++ });
            }
            csvRows.Columns = columns;

            // Create the CSV
            csvService.Create(csvRows, outputPath);

            Console.WriteLine("Hit enter to exit....");
            Console.ReadLine();


        }
    }
}
