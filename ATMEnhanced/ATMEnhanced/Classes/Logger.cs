using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class Logger : ILogger
    {
        /// <summary>
        /// Logs the time of the occurence and the involved tags.
        /// Saves file in ATM\ATMUnitTest\bin\Debug
        /// </summary>
        /// <param name="occurenceTime">DateTime parameter of time of occurence</param>
        /// <param name="involvedTags">List of the involved tracks by tag name</param>
        public void LogMessage( List<Conflict> involvedTags)
        {
            //Find the path of the project
            string ProjectDirectory = System.AppContext.BaseDirectory;

            //string timeStamp = DateTime.Now.ToLongTimeString();

            //Combine the path and .txt file 
            string fullPath = Path.Combine(ProjectDirectory, "SeparationLog.txt");

            var occurenceTime = DateTime.Now;
            
                //Pass the filepath and filename to the StreamWriter Constructor
                using (StreamWriter writeText = new StreamWriter(fullPath, append: true))
                {
                    foreach (var tag in involvedTags)
                    {
                    writeText.WriteLine($"Time of occurence:  " +
                                            $"{occurenceTime.Year}/" +
                                            $"{occurenceTime.Month}/" +
                                            $"{occurenceTime.Day} " +
                                            $"{occurenceTime.Hour}:" +
                                            $"{occurenceTime.Minute}:" +
                                            $"{occurenceTime.Second}." +
                                            $"{occurenceTime.Millisecond}");

                        writeText.WriteLine($"Track involved - Tag: {tag.Tag1}");
                        writeText.WriteLine($"Track involved - Tag: {tag.Tag2}");

                        writeText.WriteLine();
                    }
                }
            
        }

        public void SeparationLogDataHandler(object sender, SeparationLogEventArgs e)
        {
            LogMessage(e.ConflictList);
        }

    }
}
