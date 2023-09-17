
using System;

namespace Crunch
{
    public class LogRecord
    {
        public DateTime DateTime;
        public LogTypes Type;
        public string Message;
        public StackFrame[] Stack;

        public LogRecord(LogTypes type, string message, StackFrame[] stack)
        {
            try
            {
                this.DateTime = DateTime.Now;
            }
            catch (TimeZoneNotFoundException)
            {
                this.DateTime = DateTime.UtcNow;
            }

            this.Type = type;
            this.Message = message;
            this.Stack = stack;
        }

        public string TimeString => DateTime.ToString("T");
        public string CodeLineText => Stack == null ? String.Empty : Stack[0].ToString();

        public string GetText()
        {
            return String.Format("{0}: {1}", TimeString, Message);
        }
    }
}