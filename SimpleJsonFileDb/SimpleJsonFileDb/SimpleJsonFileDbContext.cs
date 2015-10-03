namespace SimpleJsonFileDb
{
    public abstract class SimpleDbContext
    {
        /// <summary>
        /// This is where the json files are stored
        /// </summary>
        public string dataDirectory;

        /// <summary>
        /// Initiates the DbContext with a given data file directory
        /// </summary>
        /// <param name="dataDirectory"></param>
        public SimpleDbContext(string dataDirectory)
        {
            this.dataDirectory = dataDirectory;
            CallInitiateJsonFiles();
        }

        /// <summary>
        /// Gets called to initiate the Json files as list objects
        /// </summary>
        void CallInitiateJsonFiles()
        {
            InitiateJsonFiles();
        }

        /// <summary>
        /// Used to create a new instance of each SimpleDbSet
        /// </summary>
        public abstract void InitiateJsonFiles();
    }
}
