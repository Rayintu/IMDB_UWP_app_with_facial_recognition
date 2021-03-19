using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedClassLibrary
{
    public static class Validator
    {
        public static bool CheckUrl(string url)
        {
            var match = Regex.Match(url, "(?<=https:\\/\\/www\\.imdb\\.com\\/title\\/)\\w*");
            return match.Groups.Count > 0;
        }
    }
}
