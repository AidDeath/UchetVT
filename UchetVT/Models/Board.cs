using System.ComponentModel;

namespace UchetVT
{
    public class Board : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Motherboard { get; set; }
        public int? YearOut { get; set; }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                if (YearOut > 2050 || YearOut < 1900)
                {
                    result = "NOOOO!!!!!";
                }

                HasError = !string.IsNullOrEmpty(result);

                return result;
            }
        }


        public bool HasError { get; set; }

        public string Error { get; }
    }
}
