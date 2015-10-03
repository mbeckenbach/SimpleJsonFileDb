using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SimpleJsonFileDb
{
    /// <summary>
    /// Handles system IO and cache operations for each json file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class JsonFileHandler<T> where T : class
    {
        public JsonFileHandler(string dataDirectory)
        {
            // Build filepaths
            this.DataFileBaseDirectory = Path.Combine(AppBaseDirectory, dataDirectory);
            this.FileName = $"{typeof(T).FullName}.json";
            this.FilePath = Path.Combine(DataFileBaseDirectory, FileName);

            // Ensure file and directory existance
            CreateFileIfNotExists();
        }
        
        static string AppBaseDirectory = "";
        string DataFileBaseDirectory { get; set; }
        string FilePath { get; set; }
        string FileName { get; set; }

        List<T> _data;
        public List<T> Data
        {
            get
            {
                return ReadFromCacheOrLocalFile();
            }
        }

        string CacheKey
        {
            get
            {
                return $"fileDb_{this.FileName}";
            }
        }

        /// <summary>
        /// Ensures, that directory and file exist
        /// </summary>
        void CreateFileIfNotExists()
        {
            // Ensure data directory exists
            if (!Directory.Exists(this.DataFileBaseDirectory))
                Directory.CreateDirectory(this.DataFileBaseDirectory);

            // Ensure data file exists and contains an empty json array
            if (!File.Exists(this.FilePath))
            {
                using (StreamWriter sw = File.CreateText(this.FilePath))
                {
                    sw.WriteLine("[]");
                }
            }
        }

        List<T> Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<List<T>>(data);
        }

        List<T> ReadLocalFile()
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(this.FilePath)))
            {
                var fileContent = sr.ReadToEnd();
                return Deserialize(fileContent);
            }
        }

        List<T> ReadFromCacheOrLocalFile()
        {
            var data = this._data;
            if (data == null)
            {
                data = ReadLocalFile();
                _data = data;
            }
            return data;
        }

        /// <summary>
        /// Saves changes to the local data file
        /// </summary>
        public void SaveChanges()
        {
            // update file
            var fi = new FileInfo(this.FilePath);
            using (var txtWriter = new StreamWriter(fi.Open(FileMode.Truncate)))
            {
                txtWriter.Write(JsonConvert.SerializeObject(this.Data));
            }

            // update cache
            _data = this.Data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this.Data);
        }
    }
}
