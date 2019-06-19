namespace DDCore
{
    public class DatabaseOptions
    {
        /// <summary>
        /// Connection String to the Sql Database to use by <seealso cref="IRepository{T}"/> implementations
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// True if you want Work to Commit by Default
        /// Normally False, meaning you must call <seealso cref="UnitOfWork.Commit"/> manually
        /// </summary>
        public bool AutoCommitWork { get; set; }
    }
}
