using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTSaveLoad
{
    public class ProjectPayload
    {
        public bool HasError;
    }

    public class ProjectPayload<T> : ProjectPayload
    {
        public T Data
        {
            get;
            private set;
        }

        public ProjectPayload(T data)
        {
            Data = data;
        }
    }


    public delegate void ProjectEventHandler<T>(ProjectPayload<T> payload);
    public delegate void ProjectEventHandler(ProjectPayload payload);

    public interface IProject
    {
        void LoadProject(string name, ProjectEventHandler<ProjectRoot> callback, bool metaOnly = false, params int[] exceptTypes);        
        void SaveProjectMeta(string name, ProjectMeta meta, ProjectEventHandler callback);
        void Save(ProjectItem item, bool metaOnly, ProjectEventHandler callback);
        void Save(ProjectItem[] items, bool metaOnly, ProjectEventHandler callback);
        void Load(string[] path, ProjectEventHandler<ProjectItem[]> callback, params int[] exceptTypes);
        void LoadData(ProjectItem[] items, ProjectEventHandler<ProjectItem[]> callback, params int[] exceptTypes);
        void UnloadData(ProjectItem item);
        void Delete(ProjectItem item, ProjectEventHandler callback);
        void Delete(ProjectItem[] items, ProjectEventHandler callback);
        void Move(ProjectItem item, ProjectItem parent, ProjectEventHandler callback);
        void Move(ProjectItem[] items, ProjectItem parent, ProjectEventHandler callback);
        void Rename(ProjectItem item, string name, ProjectEventHandler callback);
        void Exists(ProjectItem item, ProjectEventHandler<bool> callback);
    }

    public class Project : IProject
    {
        private IStorage m_storage = Dependencies.Storage;
        private ISerializer m_serializer = Dependencies.Serializer;

        private const string FileMetaExt = "rtmeta";
        private const string ProjectMetaExt = "rtpmeta";
        private const string ProjectDataExt = "rtpdata";

        public void Parallel(Action<ProjectEventHandler>[] actions, ProjectEventHandler callback)
        {
            if(actions == null || actions.Length == 0)
            {
                callback(new ProjectPayload());
            }

            int counter = actions.Length;
            bool hasError = false;
            foreach (Action<ProjectEventHandler> action in actions)
            {
                action(actionCallback =>
                {
                    hasError |= actionCallback.HasError;
                    counter--;
                    if (counter == 0)
                    {
                        callback(new ProjectPayload { HasError = hasError });
                    }
                });
            }
        }

        public void LoadProject(string name, ProjectEventHandler<ProjectRoot> callback, bool metaOnly = true, params int[] exceptTypes)
        {
            HashSet<int> exceptTypesHs = null;
            if (exceptTypes != null && exceptTypes.Length > 0)
            {
                exceptTypesHs = new HashSet<int>();
                for (int i = 0; i < exceptTypes.Length; ++i)
                {
                    if (!exceptTypesHs.Contains(exceptTypes[i]))
                    {
                        exceptTypesHs.Add(exceptTypes[i]);
                    }
                }
            }

            m_storage.CheckFolderExists(name, checkFolderResult =>
            {
                bool exists = checkFolderResult.Data;
                if (exists)
                {
                    
                    ProjectRoot root = new ProjectRoot();
                    m_storage.LoadFile(name + "." + ProjectMetaExt, loadMetaCallback =>
                    {
                        if(loadMetaCallback.Data != null)
                        {
                            root.Meta = m_serializer.Deserialize<ProjectMeta>(loadMetaCallback.Data);
                        }
                        else
                        {
                            root.Meta = new ProjectMeta();
                        }

                        
                        m_storage.LoadFile(name + "." + ProjectDataExt, loadDataCallback =>
                        {
                            if(loadDataCallback.Data != null)
                            {
                                root.Data = m_serializer.Deserialize<ProjectData>(loadDataCallback.Data);
                            }
                            else
                            {
                                root.Data = new ProjectData();
                            }

                            root.Item = ProjectItem.CreateFolder(name);
                            LoadFolders(root.Item, loadFoldersCallback =>
                            {
                                LoadFiles(root.Item, loadFilesCallback =>
                                {
                                    if (callback != null)
                                    {
                                        callback(new ProjectPayload<ProjectRoot>(root));
                                    }
                                }, 
                                metaOnly, exceptTypesHs);
                            });

                        });
                    });
                }
                else
                {
                    if (callback != null)
                    {
                        callback(new ProjectPayload<ProjectRoot>(null));
                    }
                }
            });
        }

        private void LoadFiles(ProjectItem item, ProjectEventHandler callback, bool metaOnly = true, HashSet<int> exceptTypesHS = null)
        {
            ProjectItem[] folders = item.Children != null ? item.Children.Where(c => c.IsFolder).ToArray() : new ProjectItem[0];
            Action<ProjectEventHandler>[] loadFilesActions = new Action<ProjectEventHandler>[folders.Length];
            for (int i = 0; i < folders.Length; ++i)
            {
                ProjectItem childItem = folders[i];
                loadFilesActions[i] = cb => LoadFiles(childItem, cb, metaOnly, exceptTypesHS);
            }

            m_storage.GetFiles(item.ToString(), getFilesCompleted =>
            {
                string[] path = getFilesCompleted.Data;

                if (path == null && path.Length > 0)
                {
                    Parallel(loadFilesActions, parallelCallback =>
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    });
                }
                else
                {
                    int metaLength;
                    if (metaOnly)
                    {
                        path = path.Where(n => n.EndsWith("." + FileMetaExt)).ToArray();
                        metaLength = path.Length;
                    }
                    else
                    {
                        path = path.Where(n => n.EndsWith("." + FileMetaExt)).ToArray();
                        metaLength = path.Length;
                        Array.Resize(ref path, metaLength + metaLength);
                        for (int i = 0; i < metaLength; ++i)
                        {
                            path[metaLength + i] = path[i].Remove(path[i].LastIndexOf("." + FileMetaExt));
                        }
                    }

                    if (metaLength > 0)
                    {
                        m_storage.LoadFiles(path, loadFilesCompleted =>
                        {
                            if (item.Children == null)
                            {
                                item.Children = new List<ProjectItem>();
                            }

                            for (int i = 0; i < metaLength; ++i)
                            {
                                ProjectItemMeta meta = null;
                                ProjectItemData data = null;

                                byte[] metaRaw = loadFilesCompleted.Data[i];
                                if (metaRaw != null)
                                {
                                    meta = m_serializer.Deserialize<ProjectItemMeta>(metaRaw);
                                }

                                if (!metaOnly)
                                {
                                    bool loadData = meta != null && (exceptTypesHS == null || !exceptTypesHS.Contains(meta.TypeCode));
                                    if (loadData)
                                    {
                                        byte[] dataRaw = loadFilesCompleted.Data[metaLength + i];
                                        if (dataRaw != null)
                                        {
                                            data = m_serializer.Deserialize<ProjectItemData>(dataRaw);
                                        }
                                    }
                                }

                                if(meta.TypeCode == ProjectItemTypes.Folder)
                                {
                                    ProjectItem folder = item.Children.Where(c => c.IsFolder && c.NameExt == meta.Name).FirstOrDefault();
                                    if(folder != null)
                                    {
                                        folder.Internal_Meta = meta;
                                    }
                                }
                                else
                                {
                                    ProjectItem childItem = new ProjectItem(meta, data);
                                    item.AddChild(childItem);
                                }
                            }

                            Parallel(loadFilesActions, parallelCallback =>
                            {
                                if (callback != null)
                                {
                                    callback(new ProjectPayload());
                                }
                            });

                        });
                    }
                    else
                    {
                        Parallel(loadFilesActions, parallelCallback =>
                        {
                            if (callback != null)
                            {
                                callback(new ProjectPayload());
                            }
                        });
                    }
                }
            });
        }

        private void LoadFolders(ProjectItem item, ProjectEventHandler callback)
        {
            m_storage.GetFolders(item.ToString(), getFoldersCompleted =>
            {
                string[] names = getFoldersCompleted.Data;

                if (names != null && names.Length > 0)
                {
                    Action<ProjectEventHandler>[] loadFoldersActions = new Action<ProjectEventHandler>[names.Length];

                    if(item.Children == null)
                    {
                        item.Children = new List<ProjectItem>(names.Length);
                    }
                    
                    for (int i = 0; i < names.Length; ++i)
                    {
                        string name = names[i];
                        ProjectItem childItem = ProjectItem.CreateFolder(name);
                        item.AddChild(childItem);
                        loadFoldersActions[i] = cb => LoadFolders(childItem, cb);
                    }

                    Parallel(loadFoldersActions, parallelCallback =>
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    });
                }
                else
                {
                    if (callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                }
            },
            false);
        }

        public void SaveProjectMeta(string name, ProjectMeta meta, ProjectEventHandler callback)
        {
            m_storage.SaveFile(name + "." + ProjectMetaExt, m_serializer.Serialize(meta), saveMetaCompleted =>
            {
                if (callback != null)
                {
                    callback(new ProjectPayload());
                }
            });
        }

        //public void SaveProjectData(string name, ProjectData data, ProjectEventHandler callback)
        //{
        //    m_storage.SaveFile(name + ".rtpdata", m_serializer.Serialize(data), saveDataCompleted =>
        //    {
        //        if (callback != null)
        //        {
        //            callback(new ProjectPayload());
        //        }
        //    });
        //}

        //public void Load(ProjectItem folder, ProjectEventHandler<ProjectItem[]> callback, params int[] exceptTypes)
        //{
        //    string folderPath = folder.ToString();
        //    m_storage.GetFiles(folderPath, getFilesResult =>
        //    {
        //        string[] path = getFilesResult.Data.Where(filePath => filePath.EndsWith("." + FileMetaExt)).ToArray();
        //        Array.Resize(ref path, path.Length + path.Length);
        //        for (int i = path.Length; i < path.Length + path.Length; ++i)
        //        {
        //            path[path.Length + i] = path[i];
        //            path[i] = path[i].Remove(path[i].LastIndexOf("." + FileMetaExt));
        //        }

        //        HashSet<int> exceptTypesHs = null;
        //        if(exceptTypes != null && exceptTypes.Length > 0)
        //        {
        //            exceptTypesHs = new HashSet<int>();
        //            for(int i = 0; i < exceptTypes.Length; ++i)
        //            {
        //                if(!exceptTypesHs.Contains(exceptTypes[i]))
        //                {
        //                    exceptTypesHs.Add(exceptTypes[i]);
        //                }
        //            }
        //        }
        //        m_storage.LoadFiles(path, loadFilesResult =>
        //        {
        //            List<ProjectItem> loadedItems = new List<ProjectItem>();
        //            for (int i = 0; i < path.Length; ++i)
        //            {
        //                byte[] dataRaw = loadFilesResult.Data[i];
        //                byte[] metaRaw = loadFilesResult.Data[path.Length + i];
        //                if (dataRaw != null && metaRaw != null)
        //                {
        //                    ProjectItemMeta meta = m_serializer.Deserialize<ProjectItemMeta>(metaRaw);
        //                    bool loadData = exceptTypesHs == null || !exceptTypesHs.Contains(meta.Type);

        //                    ProjectItemData data = loadData ? m_serializer.Deserialize<ProjectItemData>(dataRaw) : null;
        //                    loadedItems.Add(new ProjectItem(meta, data) { Parent = folder });   
        //                }
        //            }
        //            callback(new ProjectPayload<ProjectItem[]>(loadedItems.ToArray()));
        //        });
        //    });
        //}

        public void Load(string[] path, ProjectEventHandler<ProjectItem[]> callback, params int[] exceptTypes)
        {
            int pathLength = path.Length;
            Array.Resize(ref path, pathLength + pathLength);
            for (int i = 0; i < pathLength; ++i)
            {
                path[pathLength + i] = path[i] + "." + FileMetaExt;
            }

            HashSet<int> exceptTypesHs = null;
            if (exceptTypes != null && exceptTypes.Length > 0)
            {
                exceptTypesHs = new HashSet<int>();
                for (int i = 0; i < exceptTypes.Length; ++i)
                {
                    if (!exceptTypesHs.Contains(exceptTypes[i]))
                    {
                        exceptTypesHs.Add(exceptTypes[i]);
                    }
                }
            }

            m_storage.LoadFiles(path, loadFilesResult =>
            {
                List<ProjectItem> loadedItems = new List<ProjectItem>();
                for (int i = 0; i < pathLength; ++i)
                {
                    byte[] dataRaw = loadFilesResult.Data[i];
                    byte[] metaRaw = loadFilesResult.Data[pathLength + i];
                    if (dataRaw != null && metaRaw != null)
                    {
                        ProjectItemMeta meta = m_serializer.Deserialize<ProjectItemMeta>(metaRaw);
                        bool loadData = exceptTypesHs == null || !exceptTypesHs.Contains(meta.TypeCode);

                        ProjectItemData data = loadData ? m_serializer.Deserialize<ProjectItemData>(dataRaw) : null;
                        loadedItems.Add(new ProjectItem(meta, data));
                    }
                }
                callback(new ProjectPayload<ProjectItem[]>(loadedItems.ToArray()));
            });
        }

        public void LoadData(ProjectItem[] items, ProjectEventHandler<ProjectItem[]> callback, params int[] exceptTypes)
        {
            HashSet<int> exceptTypesHs = null;
            if (exceptTypes != null && exceptTypes.Length > 0)
            {
                exceptTypesHs = new HashSet<int>();
                for (int i = 0; i < exceptTypes.Length; ++i)
                {
                    if (!exceptTypesHs.Contains(exceptTypes[i]))
                    {
                        exceptTypesHs.Add(exceptTypes[i]);
                    }
                }
            }

            if(exceptTypes != null)
            {
                items = items.Where(item => !exceptTypes.Contains(item.TypeCode)).ToArray();
            }

            string[] path = items.Select(item => item.ToString()).ToArray();
            m_storage.LoadFiles(path, loadFilesResult =>
            {
                for (int i = 0; i < path.Length; ++i)
                {
                    byte[] dataRaw = loadFilesResult.Data[i];
                    if (dataRaw != null)
                    {
                        ProjectItemData data = m_serializer.Deserialize<ProjectItemData>(dataRaw);
                        items[i].Internal_Data = data;
                    }
                }
                callback(new ProjectPayload<ProjectItem[]>(items));
            });
        }

        public void Save(ProjectItem item, bool metaOnly, ProjectEventHandler callback)
        {
            Save(item, item.ToString(), metaOnly, callback);
        }

        private void Save(ProjectItem item, string path, bool metaOnly, ProjectEventHandler callback)
        {
            if (item.IsFolder)
            {
                ProjectItem[] filesAndFolders = item.FlattenHierarchy(true);
                Save(filesAndFolders, metaOnly, callback);
            }
            else
            {
                m_storage.SaveFile(path + "." + FileMetaExt, m_serializer.Serialize(item.Internal_Meta), saveMetaCompleted =>
                {
                    if (item.Internal_Data != null)
                    {
                        if (metaOnly)
                        {
                            if (callback != null)
                            {
                                callback(new ProjectPayload());
                            }
                        }
                        else
                        {
                            m_storage.SaveFile(path, m_serializer.Serialize(item.Internal_Data), saveDataCompleted =>
                            {
                                if (callback != null)
                                {
                                    callback(new ProjectPayload());
                                }
                            });
                        }
                    }
                    else
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    }
                });
            }
        }

        public void Save(ProjectItem[] items, bool metaOnly, ProjectEventHandler callback)
        {
            ProjectItem[] folders = items.Where(item => item.IsFolder).ToArray();
            string[] folderPath = folders.Select(item => item.ToString()).ToArray();

            ProjectItem[] files = items;//items.Where(item => !item.IsFolder).ToArray();

            byte[][] fileData = metaOnly ?
                files.Select(item => m_serializer.Serialize(item.Internal_Meta)).ToArray() :
                files.Where(item => item.Internal_Data != null).Select(item => m_serializer.Serialize(item.Internal_Data)).
                Union(files.Select(item => m_serializer.Serialize(item.Internal_Meta))).ToArray();

            string[] filePath = metaOnly ?
                files.Select(item => item.ToString() + "." + FileMetaExt).ToArray() :
                files.Where(item => item.Internal_Data != null).Select(item => item.ToString()).Union(files.Select(item => item.ToString() + "." + FileMetaExt)).ToArray();

            m_storage.CreateFolders(folderPath, foldersCreatedCallback =>
            {
                m_storage.SaveFiles(filePath, fileData, saveFilesCallback =>
                {
                    if(callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                });
            });
        }

        public void Delete(ProjectItem item, ProjectEventHandler callback)
        {
            if(item == null)
            {
                callback(new ProjectPayload());
                return;
            }
            string path = item.ToString();
            if (item.IsFolder)
            {
                m_storage.DeleteFolder(path, payload =>
                {
                    if(callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                });
            }
            else
            {
                string[] filePath = new[]
                {
                    path,
                    path + "." + FileMetaExt
                };
                m_storage.DeleteFiles(filePath, payload =>
                {
                    if (callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                });
            }
        }

        public void Delete(ProjectItem[] items, ProjectEventHandler callback)
        {
            items = ProjectItem.GetRootItems(items);
            string[] folderPath = items.Where(item => item.IsFolder).Select(item => item.ToString()).ToArray();
            ProjectItem[] files = items.Where(item => !item.IsFolder).ToArray();
            string[] filePath = files.Select(item => item.ToString()).Union(files.Select(item => item.ToString() + "." + FileMetaExt)).ToArray();

            GroupOperation(folderPath, filePath, m_storage.DeleteFolders, m_storage.DeleteFiles, callback);
        }

        public void Move(ProjectItem item, ProjectItem parent, ProjectEventHandler callback)
        {
            string srcPath = item.ToString();
            parent.AddChild(item);
            item.Name = ProjectItem.GetUniqueName(item.Name, item, item.Parent);
            string dstPath = item.ToString();
            Move(item, callback, srcPath, dstPath);
        }

        public void Move(ProjectItem[] items, ProjectItem parent, ProjectEventHandler callback)
        {
            items = ProjectItem.GetRootItems(items);
            string[] folderSrcPath = items.Where(item => item.IsFolder).Select(item => item.ToString()).ToArray();

            ProjectItem[] files = items.Where(item => !item.IsFolder).ToArray();
            string[] fileSrcPath = files.Select(item => item.ToString()).Union(files.Select(item => item.ToString() + "." + FileMetaExt)).ToArray();
            foreach (ProjectItem item in items)
            {
                parent.AddChild(item);
                item.Name = ProjectItem.GetUniqueName(item.Name, item, item.Parent);
            }

            string[] folderDstPath = items.Where(item => item.IsFolder).Select(item => item.ToString()).ToArray();
            string[] fileDstPath = files.Select(item => item.ToString()).Union(files.Select(item => item.ToString() + "." + FileMetaExt)).ToArray();

            GroupOperation(folderSrcPath, fileSrcPath, folderDstPath, fileDstPath,
                m_storage.MoveFolders, m_storage.MoveFiles, callback);
        }

        public void Rename(ProjectItem item, string name, ProjectEventHandler callback)
        {
            string srcPath = item.ToString();
            string srcName = item.Name;
            item.Name = ProjectItem.GetUniqueName(name, item, item.Parent);
            string dstPath = item.ToString();

            if (!item.IsFolder && !item.IsScene)
            {
                m_storage.LoadFile(srcPath, loadFilesResult =>
                {
                    byte[] dataRaw = loadFilesResult.Data;
                    if (dataRaw != null)
                    {
                        ProjectItemData data = m_serializer.Deserialize<ProjectItemData>(dataRaw);
                        item.Internal_Data = data;
                        item.Rename(name);
                    }

                    Save(item, srcPath, false, saveCompleted =>
                    {
                        UnloadData(item);
                        Move(item, callback, srcPath, dstPath);
                    });
                });
            }
            else
            {
                if (!item.IsFolder)
                {
                    Save(item, srcPath, true, saveCompleted =>
                    {
                        Move(item, callback, srcPath, dstPath);
                    });
                }
                else
                {
                    Move(item, callback, srcPath, dstPath);
                }
            }
        }

       

        private void Move(ProjectItem item, ProjectEventHandler callback, string srcPath, string dstPath)
        {
            if (item.IsFolder)
            {
                m_storage.MoveFolder(srcPath, dstPath, payload =>
                {
                    if (callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                });
            }
            else
            {

                m_storage.MoveFiles(
                    new[] { srcPath, srcPath + "." + FileMetaExt },
                    new[] { dstPath, dstPath + "." + FileMetaExt },
                    payload =>
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    });


            }
        }

        private void GroupOperation(
            string[] folderPath, 
            string[] filePath,
            string[] folderDstPath,
            string[] fileDstPath,
            Action<string[], string[], StorageEventHandler<string[], string[]>> folderOperation,
            Action<string[], string[], StorageEventHandler<string[], string[]>> fileOperation,
            ProjectEventHandler callback)
        {
            if (folderPath.Length > 0)
            {
                folderOperation(folderPath, folderDstPath, folderOperationCompleted =>
                {
                    if (filePath.Length > 0)
                    {
                        fileOperation(filePath, fileDstPath, fileOperationCompleted =>
                        {
                            if (callback != null)
                            {
                                callback(new ProjectPayload());
                            }
                        });
                    }
                    else
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    }
                });
            }
            else
            {
                if (filePath.Length > 0)
                {
                    fileOperation(filePath, fileDstPath, fileOperationCompleted =>
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    });
                }
                else
                {
                    if (callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                }
            }
        }

        private void GroupOperation(string[] folderPath, string[] filePath,
           Action<string[], StorageEventHandler<string[]>> folderOperation,
           Action<string[], StorageEventHandler<string[]>> fileOperation,
           ProjectEventHandler callback)
        {
            if (folderPath.Length > 0)
            {
                folderOperation(folderPath, folderOperationCompleted =>
                {
                    if (filePath.Length > 0)
                    {
                        fileOperation(filePath, fileOperationCompleted =>
                        {
                            if (callback != null)
                            {
                                callback(new ProjectPayload());
                            }
                        });
                    }
                    else
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    }
                });
            }
            else
            {
                if (filePath.Length > 0)
                {
                    fileOperation(filePath, fileOperationCompleted =>
                    {
                        if (callback != null)
                        {
                            callback(new ProjectPayload());
                        }
                    });
                }
                else
                {
                    if (callback != null)
                    {
                        callback(new ProjectPayload());
                    }
                }
            }
        }

        public void UnloadData(ProjectItem projectItem)
        {
            if (projectItem == null)
            {
                return;
            }

            projectItem.Internal_Data = null;
            if (projectItem.Children != null)
            {
                for (int i = 0; i < projectItem.Children.Count; ++i)
                {
                    UnloadData(projectItem.Children[i]);
                }
            }
        }

        public void Exists(ProjectItem item, ProjectEventHandler<bool> callback)
        {
            if(item.IsFolder)
            {
                m_storage.CheckFolderExists(item.ToString(), result =>
                {
                    if(callback != null)
                    {
                        callback(new ProjectPayload<bool>(result.Data));
                    }
                });
            }
            else
            {
                m_storage.CheckFileExists(item.ToString(), result =>
                {
                    if(callback != null)
                    {
                        callback(new ProjectPayload<bool>(result.Data));
                    }
                });
            }
        }

    }
}
