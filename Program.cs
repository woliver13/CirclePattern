using System;
using System.IO;
using NLog;

namespace CirclePattern
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            try
            {
                PatternGenerator generator = new PatternGenerator();
                string outputFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"CirclePattern_{DateTime.Now:yyyyMMddHHmm}.svg");
                generator.Generate(outputFileName, 0, 0, 2.75, 0.125, 0.25);
                logger.Info("EXITED! Returning 0");
                return 0;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                logger.Error("ABORTED! Returning -99");
                return -99;
            }
        }
    }
}
