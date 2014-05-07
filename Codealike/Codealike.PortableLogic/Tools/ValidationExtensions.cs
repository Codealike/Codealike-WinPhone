using System.Text.RegularExpressions;

namespace Codealike.PortableLogic.Tools
{

    public static class ValidationExtensions
    {
        public static bool IsValidEmail(this string emailAddress)
        {
            if ( emailAddress.IsNullOrEmptyOrWhiteSpace() )
                return false;
            var regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                  + "@"
                                  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            return regex.IsMatch(emailAddress);
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text) || string.Empty == text;
        }

        public static bool IsNotEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text) == false && string.Empty != text;
        }
    }
}