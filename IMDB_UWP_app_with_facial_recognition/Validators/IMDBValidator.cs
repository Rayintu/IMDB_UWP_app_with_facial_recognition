using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMDB_UWP_app_with_facial_recognition.Validators
{
    public static class IMDBValidator
    {
        public static bool ValidateImdbUrl(string url)
        {
            var match = Regex.Match(url, "(?<=https:\\/\\/www\\.imdb\\.com\\/title\\/)\\w*");
            return match.Length > 0;
        }
    }
}
