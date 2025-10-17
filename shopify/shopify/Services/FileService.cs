using System;
using System.Collections.Generic;
using System.IO;

namespace shopify.Services
{ 
        public static class FileService
        {
            // Read lines from file (returns empty list agar file missing raha to )
            public static List<string> ReadFromFile(string path)
            {
                try
                {
                List<string> data = new List<string>();

                            
                if (!File.Exists(path))
                {
                    return data;  
                }

          
                string[] lines = File.ReadAllLines(path); 

            
                data = new List<string>(lines);

                return data;
            }
                catch (IOException ex)
                {
                    Console.WriteLine($"File read error ({path}): {ex.Message}");
                    return new List<string>();
                }
            }


        public static void SaveToFile(string path, List<string> lines)
        {
            try
            {
                // folder check
                var dir = Path.GetDirectoryName(path);
                if (dir != "" && !Directory.Exists(dir))        
                {
                    Directory.CreateDirectory(dir);             
                }

                // write lines to file
                File.WriteAllLines(path, lines);                // from here it is saving the data 
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File write error ({path}): {ex.Message}");
            }
        }

    }

}
