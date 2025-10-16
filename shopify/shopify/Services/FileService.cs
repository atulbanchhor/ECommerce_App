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

                            // agar file nahi milti
                if (!File.Exists(path))
                {
                    return data;   // empty list wapas bheja
                }

                // agar file milti hai
                string[] lines = File.ReadAllLines(path); // file ke saare line padh lo

                // array ko list me badal do
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
                var dir = Path.GetDirectoryName(path);
                if (dir != "" && !Directory.Exists(dir))        // Check karo ki folder ka naam khaali nahi hai aur folder abhi tak exist nahi karta
                {                    
                    Directory.CreateDirectory(dir);     // Agar folder nahi bana hai to ab bana do
                }
            }
                catch (IOException ex)
                {
                    Console.WriteLine($"File write error ({path}): {ex.Message}");
                }
            }
        }
    
}
