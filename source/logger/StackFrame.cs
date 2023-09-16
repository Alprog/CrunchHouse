
using System;

public struct StackFrame
{
    public string FileName;
    public string ClassName;
    public string MethodName;
    public int Line;

    public StackFrame(string fileName, string className, string methodName, int line)
    {
        this.FileName = fileName;
        this.ClassName = className;
        this.MethodName = methodName;
        this.Line = line;
    }

    public override string ToString()
    {
        return String.Format("{0}.{1}() ({2}:{3})", ClassName, MethodName, FileName, Line);
    }
}