using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISBN.Web.Code
{
    public static class StringExtensions
    {
        public static void PadRight(this String s, int length, string paddingChar) {
            while (s.Length < length)
            {
                s += paddingChar;
            }            
        }
    }
}