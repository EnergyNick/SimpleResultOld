using System;

namespace SimpleResult.Settings
{
    public partial class ResultParameters
    {
        public Func<Exception, Error> DefaultTryCatchHandler { get; set; }
    }
}