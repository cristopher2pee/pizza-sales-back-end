using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Utilities
{
    public static class FileChecker
    {
        public static bool IsFileCSV(string filename)
        {
            var fileExtension = Path.GetExtension(filename);
            return fileExtension == ".csv" ? true : false;
        }
    }
}
