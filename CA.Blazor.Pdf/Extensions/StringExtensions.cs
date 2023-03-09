using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Blazor.Pdf.Extensions
{
    public static class StringExtensions
    {
        public static bool IsBase64String(this string str)
        {
			try
			{
				Convert.FromBase64String(str);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }

        /// <summary>
        /// Allows to convert a base64 encoded string to a stream.<br />
        /// If the string is not base64 encoded returns a empty stream
        /// </summary>
        /// <param name="base64">Base64 encoded string</param>
        /// <returns>If the string is base64 encoded return a stream with the string,<br /> otherwise returns a empty stream</returns>
        public static Stream ToStream(this string base64)
        {
            if(base64.IsBase64String())
            {
                var bytes = Convert.FromBase64String(base64);
                return new MemoryStream(bytes);
            }
            return new MemoryStream();
        }
    }
}
