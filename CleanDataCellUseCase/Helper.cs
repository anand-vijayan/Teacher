using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CleanDataCellUseCase
{
    public class Helper
    {
        private DataTable dataTable;
        private List<PayRecord> PayRecords { get; set; }

        public Helper()
        {
            CreateDataTable();
            PopulateDataTable();
            ConvertToList();
        }

        private void CreateDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(System.Int32));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Amount", typeof(System.Double));
            dataTable.Columns.Add("PayDate", typeof(System.DateTime));
        }

        private void PopulateDataTable()
        {
            if(dataTable != null)
            {
                dataTable.Rows.Add(1, "Anand Vijayan", 1200.50D, DateTime.Now.AddDays(-5));
                dataTable.Rows.Add(2, "Arun Kumar", 1000.00D, DateTime.Now.AddDays(-4));
                dataTable.Rows.Add(3, "Bill Jobs", 1000.50D, DateTime.Now.AddDays(-3));
                dataTable.Rows.Add(4, "Steve Gates", 2340.50D, DateTime.Now.AddDays(-5));
            }
        }

        private T ConvertDataCell<T>(object itemCell, T defaultValue) where T : IConvertible
        {
            T returnObj = default(T);
            string defaultConversion = string.Empty;
            Type type = typeof(T);

            if(itemCell != null)
            {
                defaultConversion = itemCell.ToString();
            }
            
            if (!string.IsNullOrEmpty(defaultConversion))
            {
                try
                {
                    returnObj = (T)Convert.ChangeType(defaultConversion, typeof(T));
                }
                catch (Exception)
                {
                    //Nothing to do, returnObj will have default value;
                }
            }

            return (returnObj == null) ? defaultValue : returnObj;
        }

        private void ConvertToList()
        {
            if(dataTable != null && dataTable.Rows.Count > 0)
            {
                if (PayRecords == null) PayRecords = new List<PayRecord>();

                PayRecords = dataTable.AsEnumerable()
                    .Select(dt => new PayRecord()
                    {
                        Id = ConvertDataCell(dt["Id"], Int32.MinValue),
                        Name = ConvertDataCell(dt["Name"],string.Empty).Trim(),
                        Amount = ConvertDataCell<double>(dt["Amount"], 0),
                        PayDate = ConvertDataCell(dt["PayDate"], DateTime.MinValue)
                    }).ToList();
            }
        }

        public void DisplayValues()
        {
            if(PayRecords != null && PayRecords.Count > 0)
            {
                Console.WriteLine("Id\tName\t\tAmount\tPayDate");
                foreach (var payRecord in PayRecords)
                {
                    Console.WriteLine($"{payRecord.Id}\t{payRecord.Name}\t{payRecord.Amount}\t{payRecord.PayDate}");
                }
            }
        }
    }
}
