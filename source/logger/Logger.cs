
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Logger : Singleton<Logger>
{
    public List<LogRecord> Records { get; private set; }
    private const int StackOffset = 3;
    
    private Logger()
    {
        Records = new List<LogRecord>();
    }

    public int RecordCount => Records.Count;

    public void Debug(object @object) => Debug(@object.ToString());
    
    public void Debug(string message, params object[] args)
    {
        Add(LogTypes.Debug, message, args);
    }

    public void Print(object @object) => Print(@object.ToString());

    public void Print(string message, params object[] args)
    {
        Add(LogTypes.Information, message, args);
    }

    public void PrintTime(string message)
    {
        var dateTime = DateTime.Now;
        var text = String.Format("{0} {1} {2}ms", message, dateTime.ToLongTimeString(), dateTime.Millisecond);
        Add(LogTypes.Information, text);
    }

    public void Warning(object @object) => Warning(@object.ToString());

    public void Warning(string message, params object[] args)
    {
        Add(LogTypes.Warning, message, args);
    }

    public void Error(object @object) => Error(@object.ToString());

    public void Error(string message, params object[] args)
    {
        Add(LogTypes.Error, message, args);
    }

    public void Exception(Exception exception)
    {
        Add(LogTypes.Error, exception.ToString());
    }

    private void Add(LogTypes type, string message, params object[] args)
    {
        var text = args.Length > 0 ? String.Format(message, args) : message;

        var stack = GetStack(new StackTrace());
        var record = new LogRecord(type, text, stack);

        AddRecord(record);
    }

    private void AddRecord(LogRecord record)
    {
        Records.Add(record);
    }

    private StackFrame[] GetStack(StackTrace trace)
    {
        var count = Mathf.Max(0, trace.FrameCount - StackOffset);
        var stack = new StackFrame[count];
        for (int i = 0; i < count; i++)
        {
            var frame = new StackTrace(true).GetFrame(i + StackOffset);
            var fileName = frame.GetFileName() ?? "unknown";
            var index = fileName.LastIndexOf('\\');
            if (index > 0)
            {
                fileName = fileName.Substring(index + 1);
            }
            var className = frame.GetMethod().ReflectedType.Name;
            var methodName = frame.GetMethod().Name;
            stack[i] = new StackFrame(fileName, className, methodName, frame.GetFileLineNumber());
        }
        return stack;
    }
}
