using System;
using System.IO;
using System.Text;

namespace PoESplit.ClientParser
{
    class LogReader : IDisposable
    {
        public static string kConfigFile =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PoeSplit",
                "config.txt");

        private readonly MainWindow fMainWindow;
        private FileStream fFileStream;
        private long fPosition;

        private Decoder fDecoder = Encoding.UTF8.GetDecoder();
        private LineBuilder fLineBuilder = new LineBuilder();

        public LogReader(MainWindow mainWindow)
        {
            fMainWindow = mainWindow;
        }

        public void TryLoadingAppDataConfig()
        {
            if (File.Exists(kConfigFile))
            {
                TryOpenClient(File.ReadAllText(kConfigFile));
            }
        }

        public bool IsOpen
        {
            get
            {
                return fFileStream != null;
            }
        }

        public void TryOpenClient(string path)
        {
            try
            {
                fFileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                fPosition = fFileStream.Length;
                fFileStream.Position = fPosition;
                fDecoder = Encoding.UTF8.GetDecoder();
                fLineBuilder = new LineBuilder();
                fMainWindow.fDebugWindow.LogMessage("Connected to client log");

                Directory.CreateDirectory(Path.GetDirectoryName(kConfigFile));
                File.WriteAllText(kConfigFile, path);

                fMainWindow.fDebugWindow.LogMessage("Wrote client log location to %APPDATA%\\PoESplit\\config.txt");
            }
            catch(Exception e)
            {
                fMainWindow.fDebugWindow.LogMessage(e.Message);
                fFileStream = null;
                fDecoder = null;
                fLineBuilder = null;
            }
        }

        public void CloseClient()
        {
            fFileStream = null;
            fDecoder = null;
            fLineBuilder = null;
        }

        public string TryReadLine()
        {
            return fLineBuilder.TryReadLine();
        }

        public bool TryCheckForChanges()
        {          
            long newLength = fFileStream.Length;
            if (newLength != fPosition)
            {
                byte[] data = new byte[newLength - fPosition];
                fFileStream.Read(data, 0, data.Length);

                char[] chars = new char[fDecoder.GetCharCount(data, 0, data.Length, false)];
                fDecoder.GetChars(data, 0, data.Length, chars, 0, false);
                
                for (int i = 0; i < chars.Length; ++i)
                {
                    fLineBuilder.AddCharacter(chars[i]);
                }

                fPosition = newLength;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (fFileStream != null)
            {
                fFileStream.Dispose();
            }
        }
    }
}