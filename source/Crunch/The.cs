
namespace Crunch
{
    public static class The
    {
        public static Application Application => Application.Instance;
        public static WindowManager WindowManager => WindowManager.Instance;

        public static DarkCrystal.CommandLine.CommandLine CommandLine = new DarkCrystal.CommandLine.CommandLine(new DefaultGlobalResolver());
        public static Logger Logger => Logger.Instance;
    }
}