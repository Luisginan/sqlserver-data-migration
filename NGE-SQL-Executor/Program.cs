using NGE_Tool_Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace NGE_SQL_Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            NgFileMaker ngFileMaker = new NgFileMaker();
            var dbUtil = new DBUtil();
            var config = new Config();
            var styleUtil = new StyleUtil();


            styleUtil.WriteTitle("Reading Config");
            if (ngFileMaker.Exists(new System.IO.FileInfo("config.xml")))
            {
                config = ngFileMaker.ReadConfig<Config>("config.xml");
                Console.WriteLine(config.ToString());
            }
            else
            {
                ngFileMaker.WriteConfig(config, "Config.xml");
                return;
            }

            if (!ModeIsAcceptable(config.Mode))
            {
                Console.WriteLine("Please check your mode, Your mode only " + ConfigEnum.Mode.PATCH + " or " + ConfigEnum.Mode.RESTORE);
                return;
            }

            if (config.Mode == ConfigEnum.Mode.PATCH)
            {
                Running_PatchMode(ngFileMaker, dbUtil, styleUtil, config);
            }
            else
            {
                Running_RestoreMode(ngFileMaker, dbUtil, styleUtil, config);                
            }
            

            styleUtil.WriteTitle("Finish");

        }

        static private void CheckedExtensionFile(List<FileInfo> listFile)
        {
            foreach (var file in listFile)
            {
                if (file.Extension.ToLower() != ".sql")
                {
                    throw new Exception(file.Name + " : Invalid extension");
                }
                Console.WriteLine(file.FullName);
            }
        }

        static private List<FileInfo> RemoveFileAlreadyPatched(List<FileInfo> listFile, Config config)
        {
            for (int i = listFile.Count - 1; i >= 0; i--)
            {
                string deployVersion = listFile[i].Name.Split(".")[0];
                string fileName = listFile[i].Name.Replace(listFile[i].Name.Split(".")[0] + ". ", "");

                if (isFileAlreadyPatched(deployVersion, fileName, config))
                {
                    Console.WriteLine("File " + fileName + " Already patched");
                    listFile.RemoveAt(i);
                    
                }
            }

            return listFile;
        }

        static private List<Dictionary<string, string>> RemoveFileAlreadyPatched(List<Dictionary<string, string>> listFile, Config config)
        {
            for (int i = listFile.Count - 1; i >= 0; i--)
            {
                string deployVersion = listFile[i]["FileName"].Split(".")[0];
                string fileName = listFile[i]["FileName"].Replace(listFile[i]["FileName"].Split(".")[0] + ". ", "");

                if (isFileAlreadyPatched(deployVersion, fileName, config))
                {
                    Console.WriteLine("File " + fileName + " Already on Database");
                    listFile.RemoveAt(i);
                }
            }

            return listFile;
        }

        static private void Running_PatchMode(NgFileMaker ngFileMaker, DBUtil dbUtil, StyleUtil styleUtil, Config config)
        {
            styleUtil.WriteTitle("Listing SQL File");

            if (config.XMLFolderPath == "")
            {
                config.XMLFolderPath = Environment.CurrentDirectory;
            }

            var listFile = ngFileMaker.GetFiles(new System.IO.DirectoryInfo(config.ScriptFolderPath), "*.*");

            if (listFile.Count > 0)
            {
                listFile = RemoveFileAlreadyPatched(listFile, config);

                if (listFile.Count == 0)
                {
                    Console.WriteLine("All data already patched on database");
                    if (!ngFileMaker.Exists(new System.IO.FileInfo(config.XMLFolderPath + "\\" + config.XMLFileName)))
                    {
                        styleUtil.WriteTitle("XML Not Found - Create New Found");
                        listFile = ngFileMaker.GetFiles(new System.IO.DirectoryInfo(config.ScriptFolderPath), "*.*");
                        WriteXMLScriptHistoryData(listFile, config, ngFileMaker);
                    }
                    return;
                }

                CheckedExtensionFile(listFile);

                styleUtil.WriteTitle("Executing SQL File");

                dbUtil.ExecuteListSQL(config.DbServerName, config.DbName, config.DbUserName, config.DbPassword, listFile, config.CheckedOnly, onAfterExecuted);
                WriteXMLScriptHistoryData(listFile, config, ngFileMaker);
            }
            else
            {
                Console.WriteLine("No script file on directory!!!");
            }
        }

        static private void Running_RestoreMode(NgFileMaker ngFileMaker, DBUtil dbUtil, StyleUtil styleUtil, Config config)
        {
            styleUtil.WriteTitle("Listing SQL File");

            if (config.XMLFolderPath == "")
            {
                config.XMLFolderPath = Environment.CurrentDirectory;
            }

            if (!ngFileMaker.Exists(new System.IO.FileInfo(config.XMLFolderPath + "\\" + config.XMLFileName)))
            {
                Console.WriteLine("XML Not Found for restore data");
                return;
            }

            List<ScriptHistoryData> listScriptHistoryData = ngFileMaker.ReadConfig<List<ScriptHistoryData>>(config.XMLFolderPath + "//" + config.XMLFileName);

            List<Dictionary<string, string>> listData = new List<Dictionary<string, string>>();


            foreach (var scriptData in listScriptHistoryData)
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("DeployVersion", scriptData.DeployVersion);
                data.Add("FileName", scriptData.FileName);
                data.Add("QueryData", scriptData.QueryData);

                listData.Add(data);
            }

            listData = RemoveFileAlreadyPatched(listData, config);

            if (listData.Count > 0)
            {
                styleUtil.WriteTitle("Executing SQL File");

                dbUtil.ExecuteListSQL(config.DbServerName, config.DbName, config.DbUserName, config.DbPassword, listData, config.CheckedOnly, onAfterExecuted);
            }
            else
            {
                Console.WriteLine("All data already restore on database");
            }
        }

        static System.Data.SqlClient.SqlConnection getCnn(Config config)
        {
            var connetionString = $@"Data Source={config.DbServerName};Initial Catalog={config.DbName};User ID={config.DbUserName};Password={config.DbPassword}";
            return new System.Data.SqlClient.SqlConnection(connetionString);
        }

        static private bool isFileAlreadyPatched(string deployVersion, string fileName, Config config)
        {
            var cnn = getCnn(config);

            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM GIN_PATCH_HISTORY " +
                                                "WHERE FileName = @fileName and " +
                                                "DeployVersion = @deployVersion", cnn);
                cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                cmd.Parameters.Add(new SqlParameter("@deployVersion", deployVersion));
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    cnn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Get Data from GIN_PATCH_HISTORY is failed");
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        static private void WriteXMLScriptHistoryData(List<FileInfo> listFile, Config config, NgFileMaker ngFileMaker)
        {
            if (config.Dev != true)
            {
                List<ScriptHistoryData> listScriptHistorydata = new List<ScriptHistoryData>();

                foreach (var file in listFile)
                {
                    ScriptHistoryData scriptHistoryData = new ScriptHistoryData();
                    scriptHistoryData.DeployVersion = file.Name.Split(".")[0];
                    scriptHistoryData.FileName = file.Name.Replace(file.Name.Split(".")[0] + ". ", "");
                    scriptHistoryData.QueryData = File.ReadAllText(file.FullName);
                    scriptHistoryData.DateCreated = DateTime.Now;
                    listScriptHistorydata.Add(scriptHistoryData);
                }

                WriteXMLScriptHistoryData(listScriptHistorydata, config, ngFileMaker);
            }
        }

        static private void WriteXMLScriptHistoryData(List<ScriptHistoryData> currentListData, Config config, NgFileMaker ngFileMaker)
        {
            if (ngFileMaker.Exists(new System.IO.FileInfo(config.XMLFolderPath + "\\" + config.XMLFileName)))
            {
                List<ScriptHistoryData> oldListData = ngFileMaker.ReadConfig<List<ScriptHistoryData>>(config.XMLFolderPath + "\\" + config.XMLFileName);
                List<ScriptHistoryData> newListData = currentListData.Where(x => !oldListData.Any(old => x.FileName == old.FileName)).ToList();
                oldListData.AddRange(newListData);
                ngFileMaker.WriteConfig(oldListData, config.XMLFolderPath + "\\" + config.XMLFileName);
            }
            else
            {
                ngFileMaker.WriteConfig(currentListData, config.XMLFolderPath + "\\" + config.XMLFileName);
            }
        }

        static private void onAfterExecuted(SqlConnection cnn, SqlTransaction trx, Dictionary<string, string> file)
        {
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO GIN_PATCH_HISTORY (DeployVersion, FileName, QueryData, DateCreated) VALUES(@deployVersion, @fileName, @queryData, @dateCreated);", cnn, trx);
            cmd.Parameters.Add(new SqlParameter("@deployVersion", file["DeployVersion"]));
            cmd.Parameters.Add(new SqlParameter("@fileName", file["FileName"]));
            cmd.Parameters.Add(new SqlParameter("@queryData", file["QueryData"]));
            cmd.Parameters.Add(new SqlParameter("@dateCreated", DateTime.Now));
            cmd.ExecuteNonQuery();
        }

        static private bool ModeIsAcceptable(string mode)
        {
            if (mode == ConfigEnum.Mode.PATCH || mode == ConfigEnum.Mode.RESTORE)
            {
                return true;
            }
            return false;
        }
    }
}
