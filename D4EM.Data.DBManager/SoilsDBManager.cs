using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using MapWinUtility;

namespace D4EM.Data.DBManager
{
    public class SoilsDBManager
    {
        /// <summary>
        /// Database connection object.
        /// </summary>
        private DbConnection _myConnection;
        /// <summary>
        /// Database type (MySQL, SQLite).
        /// </summary>
        private string _dbType;
        /// <summary>
        /// Property DbType to get the value of _dbType.
        /// </summary>
        public string DbType
        {
            get { return _dbType; }
        }
        /// <summary>
        /// Database provider (complete string used to make connection to DLL).
        /// </summary>
        private string _provider;
        /// <summary>
        /// Property Provider to get the value of _provider.
        /// </summary>
        public string Provider
        {
            get { return _provider; }
        }
        /// <summary>
        /// Actual connection string to make connection to DBMS and a specific database
        /// by a userid/password.
        /// </summary>
        private string _connectionString;
        /// <summary>
        /// Property ConnectionString to get the value of _connectionString.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
        }
        /// <summary>
        /// Object for database provider factory.
        /// </summary>
        private DbProviderFactory _fact = null;
        /// <summary>
        /// Property Fact to get the _fact.
        /// </summary>
        public DbProviderFactory Fact
        {
            get { return _fact; }
        }
        /// <summary>
        /// Object to issue commands to the DBMS.
        /// </summary>
        private DbCommand _myCommand = null;
        /// <summary>
        /// Property MyCommand to get the _myCommand.
        /// </summary>
        public DbCommand MyCommand
        {
            get { return _myCommand; }
        }

        /// <summary>
        /// Class constructor to construct an object of type RTIUtilityDB for a specific RDBMS.
        /// </summary>
        /// <param name="dbType">Database type (MySQL, SQLite)</param>
        /// <param name="aLogger">FileLogger to write messages</param>
        public SoilsDBManager(string dbType)
        {

            // constructor, type defines provider = "MySql.Data.MySqlClient" or "System.Data.SQLite"
            switch (dbType)
            {
                case "MySQL":
                    _dbType = dbType;
                    //_provider = "MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data";
                    _provider = "MySql.Data.MySqlClient";
                    break;
                case "SQLite":
                    _dbType = dbType;
                    //_provider = "MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data";
                    _provider = "System.Data.SQLite";
                    break;
                default:
                    MapWinUtility.Logger.Dbg("DB", "ERROR: Invalid database type: " + dbType);
                    _provider = String.Empty;
                    break;  // should fall through to error out
            }
            try
            {
                try
                {
                    //Type factoryType = Type.GetType(_provider);
                    //_fact = (DbProviderFactory)Activator.CreateInstance(factoryType);
                    _fact = DbProviderFactories.GetFactory(_provider);
                }
                catch (Exception e)
                {
                    MapWinUtility.Logger.Dbg("Unable to load DLL for " + dbType + ", reason: " + e.Message);
                }
                if (_fact == null)
                {
                    MapWinUtility.Logger.Dbg("Could not instantiate factory: " + _provider);
                }
            }
            catch (DbException e)
            {
                MapWinUtility.Logger.Dbg("Error constructing DB object");
                MapWinUtility.Logger.Dbg("Provider: " + _provider);
                MapWinUtility.Logger.Dbg(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Member function to initialize a connection string from individual parameters.
        /// Connection string is built from those parameters and stored in the object.
        /// </summary>
        /// <param name="connParts">Hashtable of key/value pairs for building connection string.</param>
        /// <returns>True (success), False (failure)</returns>
        public Boolean initializeConnection(Hashtable connParts)
        {   // initialize connection for specified connection string
            // requires constructor to have already set data provider
            _connectionString = String.Empty;
            string tmpString = String.Empty;    // temporary string to hold values from Hashtable
            switch (_dbType)
            {
                case "MySQL":

                    tmpString = (string)connParts["Server"];

                    if (!String.IsNullOrEmpty(tmpString))
                    {
                        _connectionString = "Server=" + tmpString + ";";
                    }
                    tmpString = (string)connParts["Port"];

                    if (!String.IsNullOrEmpty(tmpString))
                    {
                        _connectionString += "Port=" + tmpString + ";";
                    }
                    tmpString = (string)connParts["Username"];

                    if (!String.IsNullOrEmpty(tmpString))
                    {
                        _connectionString += "Username=" + tmpString + ";";
                    }
                    tmpString = (string)connParts["Password"];

                    if (!String.IsNullOrEmpty(tmpString))
                    {
                        _connectionString += "Password=" + tmpString;
                    }
                    tmpString = (string)connParts["Database"];
                    if (!String.IsNullOrEmpty(tmpString))
                    {
                        _connectionString += ";Database=" + tmpString;
                    }

                    break;
                case "SQLite":

                    _connectionString = "Data Source=" + (string)connParts["loc"] +
                            Path.DirectorySeparatorChar + (string)connParts["Database"];

                    break;
                default:
                    MapWinUtility.Logger.Dbg("ERROR: Invalid database provider.");
                    return false;
            }
            try
            {
                _myConnection = _fact.CreateConnection();
                _myConnection.ConnectionString = _connectionString;
                _myConnection.Open();
            }
            catch (Exception e)
            {
                MapWinUtility.Logger.Dbg("Error opening connection: " + _connectionString);
                MapWinUtility.Logger.Dbg(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Member function to close an open database connection.
        /// </summary>
        public void closeConnection()
        {   // close database connection
            try
            {
                _myConnection.Close();
            }
            catch (DbException e)
            {
                MapWinUtility.Logger.Dbg("Error closing database connection.");
                MapWinUtility.Logger.Dbg(e.Message);
                throw;
            }
        }

        public void RunSQLNonQuery(string sSQL)
        {
            DbCommand cmd = _myConnection.CreateCommand();
            cmd.CommandText = sSQL;
            try
            {
                if (_myConnection.State == ConnectionState.Closed)
                {
                    _myConnection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (DbException e)
            {
                MapWinUtility.Logger.Dbg("Error executing query: " + sSQL);
                MapWinUtility.Logger.Dbg(e.Message);
            }
            finally
            {
                _myConnection.Close();
            }
        }

        public DataTable RunSQL(string sSQL)
        {
            DataTable dt = new DataTable();
            DbCommand cmd = _myConnection.CreateCommand();
            cmd.CommandText = sSQL;
            try
            {
                if (_myConnection.State == ConnectionState.Closed)
                {
                    _myConnection.Open();
                }
                DbDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (DbException e)
            {
                MapWinUtility.Logger.Dbg("Error executing query: " + sSQL);
                MapWinUtility.Logger.Dbg(e.Message);
            }
            finally
            {
                _myConnection.Close();
            }
            return dt;
        }

        
    }
}

