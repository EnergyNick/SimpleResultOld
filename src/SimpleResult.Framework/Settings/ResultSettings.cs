using System;

namespace SimpleResult.Settings
{
    /// <summary>
    /// Settings for general Result behavior in certain scenarios
    /// </summary>
    public static class ResultSettings
    {
        /// <summary>
        /// Current parameters used in Results
        /// </summary>
        public static ResultParameters Parameters { get; private set; }
        
        static ResultSettings()
        {
            Parameters = GetDefaultParameters();
        }
        
        /// <summary>
        /// Thread-safe parameters modification for results
        /// </summary>
        public static void SetupParameters(Func<ResultParameters, ResultParameters> settingsChangeFunction)
        {
            lock (Parameters)
            {
                Parameters = settingsChangeFunction(Parameters);
            }
        }
        
        public static ResultParameters GetDefaultParameters() => new ResultParameters()
        {
            DefaultTryCatchHandler = ex => new ExceptionalError(ex)
        };
    }
}