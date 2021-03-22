using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_UWP_app_with_facial_recognition.Services
{
    public static class ParserService
    {
        public static string getYearFromReleaseDate(string releaseDate)
        {
            DateTime dateTime = DateTime.Parse(releaseDate);
            return dateTime.Year.ToString();
        }
    }
}
