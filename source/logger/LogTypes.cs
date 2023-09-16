
public enum LogTypes
{
    None = 0,

    Debug = 1 << 0,
    Information = 1 << 1,
    Warning = 1 << 2,
    Error = 1 << 3,

    All = Debug | Information | Warning | Error
}