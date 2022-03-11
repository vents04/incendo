using System;
using System.Diagnostics;

namespace Data.Models
{
    public class Log
    {
        public static void Exception(Exception exception)
        {
            Debug.WriteLine(exception.ToString());
        }
    }
}