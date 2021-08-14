using System;
using System.Data;
using System.IO;

namespace Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter camera name");
            
            string cameraName=Console.ReadLine();
            Console.WriteLine();    
            ConvertCSVtoDataTable(cameraName);
        }

        public static void ConvertCSVtoDataTable(string cameraName)
        {
            string strFilePath = "d://cameras-defb.csv";
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(';');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                //string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                string[] rows = sr.ReadLine().Split(";");

                DataRow dr = dt.NewRow();
                try
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                }

            }
            //var resultRange = dt.AsEnumerable()
            //          .Where((row, index) => index >= 2 && 5 >= index)
            //          .CopyToDataTable();
            var resultRange = dt.AsEnumerable()
                      .CopyToDataTable();
            //this should return 100  
            //var value = "Neude ";
            var result = resultRange.Select("Camera like '%" + cameraName + "%'");
            string detaild = string.Empty;
            //object detail = result.SelectMany(x => x.ItemArray).ToList();
            for (var i = 0; i < result.Length; i++)
            {
                detaild = "";
                for (var j = 0; j < result[i].ItemArray.Length; j++)
                {
                    detaild += result[i].ItemArray[j].ToString() + "  |";                 
                }
                Console.WriteLine(detaild);
            }         
        }
    }
}
