using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SqlHistoryViewer
{
    public class NgFileMaker
    {
        public void RenameFolder(DirectoryInfo originPath, DirectoryInfo destinationPath)
        {
            if (Directory.Exists(originPath.FullName))
            {
                Directory.Move(originPath.FullName, destinationPath.FullName);
                Console.WriteLine($@"folder name {originPath.Name} changed to {destinationPath.Name}");
            }
            else
            {
                Console.WriteLine($@"cannot rename. Source path {originPath.FullName} does not exist");
            }
        }

        public void RenameFile(string originPath, string destinationPath)
        {
            var source = new FileInfo(originPath);
            var file = new FileInfo(destinationPath);
            if (File.Exists(originPath))
            {
                File.Move(originPath, destinationPath);
                Console.WriteLine($@"folder name {source.Name} rename to {file.Name}");
            }
            else
            {
                Console.WriteLine($@"cannot rename file. Source path {originPath} does not exist");
            }
        }

        public void DeleteFolder(DirectoryInfo folder)
        {
            if (Directory.Exists(folder.FullName))
            {
                UpdateFileAttributes(folder);
                folder.Delete(true);
                Console.WriteLine($@"{folder.FullName} has deleted");
            }

        }

        private void UpdateFileAttributes(DirectoryInfo dInfo)
        {
            // Set Directory attribute
            dInfo.Attributes = FileAttributes.Normal;

            // get list of all files in the directory and clear 
            // the Read-Only flag

            foreach (FileInfo file in dInfo.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
            }

            // recurse all of the subdirectories
            foreach (DirectoryInfo subDir in dInfo.GetDirectories())
            {
                UpdateFileAttributes(subDir);
            }
        }

        public void CreateFileText(DirectoryInfo folder, string value, string name, string ext)
        {
            string file = name + "." + ext;
            using (StreamWriter sw = File.CreateText(folder.FullName + @"\" + file))
            {
                sw.WriteLine(value);
            }

            Console.WriteLine(file + " has created");
        }

        public void DeleteFile(FileInfo file)
        {
            file.Delete();
            Console.WriteLine($@"File {file.Name} has deleted");

        }

        public void CopyFile(FileInfo from, FileInfo to, bool validateTime = true)
        {
            if (Exists(to))
            {
                if (validateTime)
                {
                    if (from.LastWriteTimeUtc < to.LastWriteTimeUtc)
                    {
                        throw new Exception($"files at the destination have a newer date. File : {from.Name}");
                    }
                }

            }

            from.CopyTo(to.FullName, true);
            Console.WriteLine($"{from.Name} has copied");
        }

        public void CreateFolder(DirectoryInfo path)
        {
            path.Create();
            Console.WriteLine($"{path.Name} has created");
        }

        public void CreateZip(DirectoryInfo sourcefolder, FileInfo destFilezip, String tempDirectory = @"C:\TempGin", List<string> exludeFileExtension = null)
        {
            if (destFilezip.Exists)
            {
                destFilezip.Delete();
            }

            if (!Directory.Exists(tempDirectory))
            {
                CreateFolder(new DirectoryInfo(tempDirectory));
            }

            var tempZipDir = new DirectoryInfo(tempDirectory);
            var fileListToZip = sourcefolder.GetFiles();

            foreach (var file in fileListToZip)
            {
                if (exludeFileExtension != null)
                {
                    var exit = false;
                    foreach (var extension in exludeFileExtension)
                    {
                        if (extension.ToLower() == file.Extension.ToLower())
                        {
                            exit = true;
                        }
                    }

                    if (exit)
                    {
                        continue;
                    }
                }

                CopyFile(file, new FileInfo(tempZipDir.FullName + @"\" + file.Name), false);
            }

            DeleteFile(new FileInfo(destFilezip.FullName + @"\bin.zip"));
            ZipFile.CreateFromDirectory(tempZipDir.FullName, destFilezip.FullName + @"\bin.zip");
            DeleteFolder(tempZipDir);
            Console.WriteLine($"{destFilezip.FullName} was created");
        }

        public void ExtractZip(DirectoryInfo folderExtract, FileInfo filezip)
        {

            ZipFile.ExtractToDirectory(filezip.FullName, folderExtract.FullName);
            Console.WriteLine($"{filezip.FullName} was extracted");
        }

        public T ReadConfig<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var fileConfig = new StreamReader(path))
            {
                var result = (T)serializer.Deserialize(fileConfig);
                return result;
            }
        }

        public void WriteConfig<T>(T config, string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var fileConfig = new StreamWriter(path))
            {
                serializer.Serialize(fileConfig, config);
            }
            Console.WriteLine($"{path} Created ");
        }

        public bool Exists(FileInfo fileInfo)
        {
            return fileInfo.Exists;
        }

        public List<FileInfo> GetFiles(DirectoryInfo path)
        {
            return path.GetFiles().ToList();
        }

        public List<FileInfo> GetFiles(DirectoryInfo path, string searchPattern)
        {
            return path.GetFiles(searchPattern).ToList();
        }

        public List<DirectoryInfo> GetFolders2(DirectoryInfo path)
        {
            return path.GetDirectories().ToList().OrderBy(x => int.Parse(x.Name.Split('.')[0])).ToList();
        }

        public void RunFile(String path, String argument = "", bool waitExit = true)
        {
            var process = Process.Start(path, argument);
            if (waitExit)
            {
                process.WaitForExit();
            }

            process.ErrorDataReceived += Process_ErrorDataReceived;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new Exception(e.Data);
        }

        public string ReadFromFile(string filePath)
        {
            var file = new FileInfo(filePath);
            var str = file.OpenText();
            var result = str.ReadToEnd();
            str.Close();
            //Console.WriteLine($@"read value from {file.Name}");

            return result;
        }

        public void WriteToFile(string filePath, string value)
        {
            var fileInfo = new FileInfo(filePath);
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(value);
            }
            Console.WriteLine($@"file from {fileInfo.Name} successfully replaced");
        }

        public void CopyFolder(string SourcePath, string DestinationPath)
        {
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, ".", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }

        public string EditFileCSV(string filePath)
        {
            string tempPath = @"tempFile.csv";
            bool isDone = false;
            string fileContent = File.ReadAllText(filePath);

            while (isDone == false)
            {
                if (fileContent.Contains(";;"))
                {
                    fileContent = fileContent.Replace(";;", ";\0;");
                }
                if (fileContent.Contains($";{Environment.NewLine}"))
                {
                    fileContent = fileContent.Replace($";{Environment.NewLine}", $";\0{Environment.NewLine}");
                }
                else
                {
                    isDone = true;
                }
            }

            File.WriteAllText(tempPath, fileContent);

            return tempPath;
        }

        public void CreateFileMerge(DirectoryInfo path, List<string> merge)
        {
            using (StreamWriter writer = File.CreateText($"{ path }" + @"\setup.txt"))
            {
                foreach (var v in merge)
                {
                    writer.WriteLine(v);
                }
            }
        }


    }
}
