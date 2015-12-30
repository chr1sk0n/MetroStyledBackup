using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Text;
using MetroStyledBackup.Synchronization.Contracts.Logging;

namespace MetroStyledBackup.Synchronization.Task.Logging
{
    [Export("FileLogger", typeof(ILogger))]
    public class FileLogger : ILogger
    {
        /// <summary>
        /// The logged messages.
        /// </summary>
        private readonly List<string> messages;

        public FileLogger()
        {
            this.messages = new List<string>();
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            messages.Add(DateTime.Now.ToString(CultureInfo.CurrentCulture) + "   " + message);
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <returns>The path to the logfile.</returns>
        public string WriteToFile()
        {
            string pathToLogFile = Path.Combine(Path.GetTempPath(), "MetroStyledBackup" + DateTime.Now.ToString("dd.MM.yyyy hh-mm-ss", CultureInfo.CurrentCulture) + ".log");
            try
            {
                using (var streamWriter = new StreamWriter(pathToLogFile, false, Encoding.UTF32))
                {
                    foreach (var message in messages)
                    {
                        streamWriter.WriteLine(message);
                    }
                    streamWriter.Flush();
                }
                return pathToLogFile;
            }
            catch (IOException)
            {
                this.messages.Clear();
                return null;
            }
            finally
            {
                this.messages.Clear();
            }
        }
    }
}