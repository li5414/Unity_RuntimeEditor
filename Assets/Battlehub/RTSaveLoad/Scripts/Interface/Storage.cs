using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTSaveLoad
{
    public class StoragePayload<T>
    {
        public T Path
        {
            get;
            private set;
        }

        public StoragePayload(T path)
        {
            Path = path;
        }
    }

    public class StoragePayload<T1, T2> : StoragePayload<T1>
    {
        public T2 Data
        {
            get;
            private set;
        }

        public StoragePayload(T1 path, T2 data) : base(path)
        {
            Data = data;
        }
    }

    public delegate void StorageEventHandler<T>(StoragePayload<T> payload);
    public delegate void StorageEventHandler<T1, T2> (StoragePayload<T1, T2> payload);

    public interface IStorage
    {
        void CheckFolderExists(string path, StorageEventHandler<string, bool> callback);
        void CheckFileExists(string path, StorageEventHandler<string, bool> callback);
        void GetFolders(string path, StorageEventHandler<string, string[]> callback, bool fullPath = true);
        void GetFiles(string path, StorageEventHandler<string, string[]> callback, bool fullPath = true);
        void SaveFile(string path, byte[] data, StorageEventHandler<string> callback);
        void SaveFiles(string[] path, byte[][] data, StorageEventHandler<string[]> callback);
        void LoadFile(string path, StorageEventHandler<string, byte[]> callback);
        void LoadFiles(string[] path, StorageEventHandler<string[], byte[][]> callback);
        void DeleteFile(string path, StorageEventHandler<string> callback);
        void DeleteFiles(string[] path, StorageEventHandler<string[]> callback);
        void CopyFile(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback);
        void CopyFiles(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback);
        void MoveFile(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback);
        void MoveFiles(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback);
        void CreateFolder(string path, StorageEventHandler<string> callback);
        void CreateFolders(string[] path, StorageEventHandler<string[]> callback);
        void DeleteFolder(string path, StorageEventHandler<string> callback);
        void DeleteFolders(string[] path, StorageEventHandler<string[]> callback);
        void CopyFolder(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback);
        void CopyFolders(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback);
        void MoveFolder(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback);
        void MoveFolders(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback);
    }

    public class FileSystemStorage : IStorage
    {
        private string m_basePath;
        public FileSystemStorage(string basePath)
        {
            Debug.Log("FileSystemStorage root: " + basePath);
            m_basePath = basePath;
        }

        private void CombineWithBasePath(ref string[] path)
        {
            for(int i = 0; i < path.Length; ++i)
            {
                path[i] = Path.Combine(m_basePath, path[i].TrimStart('/'));
            }
        }

        private void CombineWithBasePath(ref string path)
        {
            path = Path.Combine(m_basePath, path.TrimStart('/'));
        }

        public void CheckFolderExists(string path, StorageEventHandler<string, bool> callback)
        {
            CombineWithBasePath(ref path);

            bool result = Directory.Exists(path);
            if (callback != null)
            {
                callback(new StoragePayload<string, bool>(path, result));
            }
        }

        public void CheckFileExists(string path, StorageEventHandler<string, bool> callback)
        {
            CombineWithBasePath(ref path);

            bool result = File.Exists(path);
            if(callback != null)
            {
                callback(new StoragePayload<string, bool>(path, result));
            }
        }

        public void CopyFile(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);

            File.Copy(sourcePath, destinationPath);
            if(callback != null)
            {
                callback(new StoragePayload<string, string>(sourcePath, destinationPath));
            }
        }

        public void CopyFiles(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);

            for(int i = 0; i < sourcePath.Length; ++i)
            {
                File.Copy(sourcePath[i], destinationPath[i]);
            }
            
            if (callback != null)
            {
                callback(new StoragePayload<string[], string[]>(sourcePath, destinationPath));
            }
        }

        public void CopyFolder(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);

            DirectoryInfo source = new DirectoryInfo(sourcePath);
            DirectoryInfo destination = new DirectoryInfo(destinationPath);

            CopyAll(source, destination);

            if(callback != null)
            {
                callback(new StoragePayload<string, string>(sourcePath, destinationPath));
            }
        }

        public void CopyFolders(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);


            for (int i = 0; i < sourcePath.Length; ++i)
            {
                DirectoryInfo source = new DirectoryInfo(sourcePath[i]);
                DirectoryInfo destination = new DirectoryInfo(destinationPath[i]);

                CopyAll(source, destination);
            }
            if (callback != null)
            {
                callback(new StoragePayload<string[], string[]>(sourcePath, destinationPath));
            }
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo destination)
        {
            Directory.CreateDirectory(destination.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(destination.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    destination.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public void CreateFolder(string path, StorageEventHandler<string> callback)
        {
            CombineWithBasePath(ref path);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if(callback != null)
            {
                callback(new StoragePayload<string>(path));
            }
        }

        public void CreateFolders(string[] path, StorageEventHandler<string[]> callback)
        {
            CombineWithBasePath(ref path);

            for (int i = 0; i < path.Length; ++i)
            {
                Directory.CreateDirectory(path[i]);
            }

            if (callback != null)
            {
                callback(new StoragePayload<string[]>(path));
            }
        }

        public void DeleteFile(string path, StorageEventHandler<string> callback)
        {
            CombineWithBasePath(ref path);

            File.Delete(path);
            if(callback != null)
            {
                callback(new StoragePayload<string>(path));
            }
        }

        public void DeleteFiles(string[] path, StorageEventHandler<string[]> callback)
        {
            CombineWithBasePath(ref path);

            for (int i = 0; i < path.Length; ++i)
            {
                File.Delete(path[i]);
            }
            
            if (callback != null)
            {
                callback(new StoragePayload<string[]>(path));
            }
        }

        public void DeleteFolder(string path, StorageEventHandler<string> callback)
        {
            CombineWithBasePath(ref path);

            Directory.Delete(path, true);
            if (callback != null)
            {
                callback(new StoragePayload<string>(path));
            }
        }

        public void DeleteFolders(string[] path, StorageEventHandler<string[]> callback)
        {
            CombineWithBasePath(ref path);

            for (int i = 0; i < path.Length; ++i)
            {
                Directory.Delete(path[i], true);
            }

            if (callback != null)
            {
                callback(new StoragePayload<string[]>(path));
            }
        }

        public void GetFiles(string path, StorageEventHandler<string, string[]> callback, bool fullPath = true)
        {
            CombineWithBasePath(ref path);

            string[] files;
            if(fullPath)
            {
                files = Directory.GetFiles(path).Select(p => PathHelper.GetRelativePath(p, m_basePath)).ToArray();
            }
            else
            {
                files = Directory.GetFiles(path).Select(f => Path.GetFileName(f)).ToArray();
            }
            
            if (callback != null)
            {
                callback(new StoragePayload<string, string[]>(path, files));
            }
        }



        public void GetFolders(string path, StorageEventHandler<string, string[]> callback, bool fullPath = true)
        {
            CombineWithBasePath(ref path);


            string[] folders;
            if(fullPath)
            {
                folders = Directory.GetDirectories(path).Select(p => PathHelper.GetRelativePath(p, m_basePath)).ToArray();
            }
            else
            {
                folders = Directory.GetDirectories(path).Select(d => new DirectoryInfo(d).Name).ToArray();
            }
            
    
            if (callback != null)
            {
                callback(new StoragePayload<string, string[]>(path, folders));
            }
        }

        public void LoadFile(string path, StorageEventHandler<string, byte[]> callback)
        {
            CombineWithBasePath(ref path);
            byte[] data = null;
            if(File.Exists(path))
            {
                data = File.ReadAllBytes(path);
            }

            if(callback != null)
            {
                callback(new StoragePayload<string, byte[]>(path, data));
            }
        }

        public void LoadFiles(string[] path, StorageEventHandler<string[], byte[][]> callback)
        {
            CombineWithBasePath(ref path);

            byte[][] data = new byte[path.Length][];
            for (int i = 0; i < path.Length; ++i)
            {
                if(File.Exists(path[i]))
                {
                    data[i] = File.ReadAllBytes(path[i]);
                }
            }

            if (callback != null)
            {
                callback(new StoragePayload<string[], byte[][]>(path, data));
            }
        }

        public void MoveFile(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);

            File.Move(sourcePath, destinationPath);
            if(callback != null)
            {
                callback(new StoragePayload<string, string>(sourcePath, destinationPath));
            }
        }

        public void MoveFiles(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);

            for(int i = 0; i < sourcePath.Length; ++i)
            {
                File.Move(sourcePath[i], destinationPath[i]);
            }
            
            if (callback != null)
            {
                callback(new StoragePayload<string[], string[]>(sourcePath, destinationPath));
            }
        }

        public void MoveFolder(string sourcePath, string destinationPath, StorageEventHandler<string, string> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);
            
            if(sourcePath != destinationPath)
            {
                Directory.Move(sourcePath, destinationPath);
            }
            if(callback != null)
            {
                callback(new StoragePayload<string, string>(sourcePath, destinationPath));
            }
        }

        public void MoveFolders(string[] sourcePath, string[] destinationPath, StorageEventHandler<string[], string[]> callback)
        {
            CombineWithBasePath(ref sourcePath);
            CombineWithBasePath(ref destinationPath);

            for(int i = 0; i < sourcePath.Length; ++i)
            {
                Directory.Move(sourcePath[i], destinationPath[i]);
            }
            
            if (callback != null)
            {
                callback(new StoragePayload<string[], string[]>(sourcePath, destinationPath));
            }
        }

        public void SaveFile(string path, byte[] data, StorageEventHandler<string> callback)
        {
            CombineWithBasePath(ref path);

            File.WriteAllBytes(path, data);
            if(callback != null)
            {
                callback(new StoragePayload<string>(path));
            }
        }

        public void SaveFiles(string[] path, byte[][] data, StorageEventHandler<string[]> callback)
        {
            CombineWithBasePath(ref path);

            for(int i = 0; i < path.Length; ++i)
            {
                File.WriteAllBytes(path[i], data[i]);
            }
            
            if (callback != null)
            {
                callback(new StoragePayload<string[]>(path));
            }
        }      
    }

    public static class PathHelper
    {
        public static bool IsPathRooted(string path)
        {
            return Path.IsPathRooted(path);
        }

        public static string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        public static string RemoveInvalidFineNameCharacters(string name)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            for (int i = 0; i < invalidChars.Length; ++i)
            {
                name = name.Replace(invalidChars[i].ToString(), string.Empty);
            }
            return name;
        }

        public static string GetUniqueName(string desiredName, string ext, string[] existingNames)
        {
            if (existingNames == null || existingNames.Length == 0)
            {
                return desiredName;
            }

            for(int i = 0; i < existingNames.Length; ++i)
            {
                existingNames[i] = existingNames[i].ToLower();
            }

            HashSet<string> existingNamesHS = new HashSet<string>(existingNames);
            if (string.IsNullOrEmpty(ext))
            {
                if (!existingNamesHS.Contains(desiredName.ToLower()))
                {
                    return desiredName;
                }
            }
            else
            {
                if (!existingNamesHS.Contains(string.Format("{0}.{1}", desiredName.ToLower(), ext)))
                {
                    return desiredName;
                }
            }
            
            string[] parts = desiredName.Split(' ');
            string lastPart = parts[parts.Length - 1];
            int number;
            if (!int.TryParse(lastPart, out number))
            {
                number = 1;
            }
            else
            {
                desiredName = desiredName.Substring(0, desiredName.Length - lastPart.Length).TrimEnd(' ');
            }

            const int maxAttempts = 10000;
            for (int i = 0; i < maxAttempts; ++i)
            {
                string uniqueName;
                if(string.IsNullOrEmpty(ext))
                {
                    uniqueName = string.Format("{0} {1}", desiredName, number);
                }
                else
                {
                    uniqueName = string.Format("{0} {1}.{2}", desiredName, number, ext);
                }
                    
                if (!existingNamesHS.Contains(uniqueName.ToLower()))
                {
                    return uniqueName;
                }

                number++;
            }

            if(string.IsNullOrEmpty(ext))
            {
                return string.Format("{0} {1}", desiredName, Guid.NewGuid().ToString("N"));
            }
            return string.Format("{0} {1}.{2}", desiredName, Guid.NewGuid().ToString("N"), ext);
        }

        public static string GetUniqueName(string desiredName, string[] existingNames)
        {
            return GetUniqueName(desiredName, null, existingNames);
        }
    }
}

