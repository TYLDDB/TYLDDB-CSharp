using System.Collections.Generic;

namespace TYLDDB.Utils.Database
{
    /// <summary>
    /// Interfaces for operations on external databases.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Gets the names of all databases.<br />
        /// 获取所有数据库的名称。
        /// </summary>
        /// <param name="fileContent">Complete database file contents<br />完整数据库文件内容</param>
        /// <returns>All database names<br />所有的数据库名称</returns>
        public List<string> GetDatabaseList(string fileContent);

        /// <summary>
        /// Gets the corresponding contents of the entered database.<br />
        /// 获取输入的数据库的对应内容。
        /// </summary>
        /// <param name="content">Complete database file contents<br />完整数据库文件内容</param>
        /// <param name="databaseName">Name of the database to be read<br />需读取的数据库名称</param>
        /// <returns>Content about the database to be obtained<br />需获取的数据库的对应内容</returns>
        public string GetDatabaseContent(string content, string databaseName);
    }
}
