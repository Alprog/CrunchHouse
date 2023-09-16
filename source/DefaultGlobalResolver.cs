
public class DefaultGlobalResolver : DarkCrystal.CommandLine.GlobalObjectResolver
{
    public DefaultGlobalResolver()
    {
        AddClass("The", typeof(The));
        AddClass("Logger", typeof(Logger));
    }
}