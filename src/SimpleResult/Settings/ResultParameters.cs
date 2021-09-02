using System;

namespace SimpleResult.Settings
{
    public class ResultParameters
    {
        public Func<Exception, Error> DefaultTryCatchHandler { get; set; }
    }
}