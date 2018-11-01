using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileParsingEngine
{
    public class LogEventArgs: EventArgs
    {
        public string LogMessage { get; set; }
        public Exception LogException { get; set; }
        public TraceLevel LogTraceLevel { get; set; }

        public LogEventArgs(Exception ex)
        {
            this.LogException = ex;
            this.LogMessage = ex.Message;
            this.LogTraceLevel = TraceLevel.Error;
        }

        public LogEventArgs(String message)
        {
            this.LogMessage = message;
            this.LogTraceLevel = TraceLevel.Info;
        }
        public LogEventArgs(String message, TraceLevel lvl)
        {
            this.LogMessage = message;
            this.LogTraceLevel = lvl;
        }
    }
}
