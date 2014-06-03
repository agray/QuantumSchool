using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace QuantumSchool.Tests {
    [System.Runtime.InteropServices.GuidAttribute("E69498D4-D596-45FA-8200-573F6FE70015")]
    public class TestDatabase {
        private const string LocalDbMaster =
            @"Data Source=(LocalDB)\v11.0;Initial Catalog=master;Integrated Security=True";
        private const string TestConnectionString =
            @"Data Source=(LocalDB)\v11.0;Initial Catalog={0};Integrated Security=True;
              MultipleActiveResultSets=True;AttachDBFilename={1}.mdf";

        private readonly string _databaseName;

        public TestDatabase(string databaseName) {
            _databaseName = databaseName;
        }

        public void CreateDatabase() {
            bool isDetached = DetachDatabase();
            if(!isDetached) return; //reuse database
            string fileName = CleanupDatabase();

            using(SqlConnection connection = new SqlConnection(LocalDbMaster)) {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format("CREATE DATABASE {0} ON (NAME = N'{0}', FILENAME = '{1}.mdf')", _databaseName, fileName);
                cmd.ExecuteNonQuery();
            }
        }

        public void InitConnectionString(string connectionStringName) {
            string connectionString = string.Format(TestConnectionString, _databaseName, DatabaseFilePath());
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetCallingAssembly().Location);
            ConnectionStringSettings settings = config.ConnectionStrings.ConnectionStrings[connectionStringName];
            if(settings == null) {
                settings = new ConnectionStringSettings(connectionStringName, connectionString, "System.Data.SqlClient");
                config.ConnectionStrings.ConnectionStrings.Add(settings);
            }
            settings.ConnectionString = connectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        private string CleanupDatabase() {
            string fileName = DatabaseFilePath();
            try {
                if(File.Exists(fileName + ".mdf")) { 
                    File.Delete(fileName + ".mdf"); 
                }
                if(File.Exists(fileName + "_log.ldf")) { 
                    File.Delete(fileName + "_log.ldf"); 
                }
            } catch {
                Console.WriteLine("Could not delete the files (open in Visual Studio?)");
            }
            return fileName;
        }
        private bool DetachDatabase() {
            using(var connection = new SqlConnection(LocalDbMaster)) {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = String.Format("exec sp_detach_db '{0}'", _databaseName);
                try {
                    cmd.ExecuteNonQuery();
                    return true;
                } catch {
                    Console.WriteLine("Could not detach");
                    return false;
                }
            }
        }
        private string DatabaseFilePath() {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _databaseName);
        }
    }
}