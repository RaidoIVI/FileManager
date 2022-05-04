using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class Command

    {
        public static bool Error { get; private set; }
        public static List<Task> InProgress { get; private set; }
        

        #region Init

        public static void ExecutionCommand (Commands Command, FileSystemInfo From, FileSystemInfo To)

        {
            switch ( Command )
            {
                case Commands.DirectoryList :
                    DirectoryList(From as DirectoryInfo);
                    return;
                case Commands.Copy:
                    Copy(From, To);
                    return;
                //case Commands.Delete:
                //    Delete(From);
                //    return;
            }
        }
        #endregion

        #region List

        private static List<FileSystemInfo> DirectoryList (DirectoryInfo Dir, int Deep = -1)
        {
            try
            {
                var dirList = new List<FileSystemInfo>();
                if (Deep == 0)
                {
                    foreach (DirectoryInfo dir in Dir.GetDirectories ())
                    {
                        dirList.Add(dir);
                    }
                }
                else
                {
                    Deep--;
                    foreach (DirectoryInfo dir in Dir.GetDirectories())
                    {
                        dirList.AddRange(DirectoryList(dir, Deep));
                    }
                }
                return dirList;
            }
            catch (Exception ex)
            {
                Error = true;
                Log.Write(ex.Message);
                return null;
            }
        }
        private static List<FileInfo> FileList (DirectoryInfo Dir, int Deep = 0)  // по умолчанию только содержимое текущего каталога
        {
            var fileList = new List<FileInfo>();
            try
            {
                if (Deep == 0)
                {                                                                           // содержимое текущего каталога
                    foreach (FileInfo file in Dir.GetFiles())
                    {
                        fileList.Add(file);
                    }
                }
                else                                                                        // все файли из текущей и вложенных каталогов
                {
                    foreach (FileInfo file in Dir.GetFiles())
                    {
                        fileList.Add(file);
                    }
                    foreach (DirectoryInfo dir in Dir.GetDirectories())
                    {
                        fileList.AddRange(FileList(dir, -1));
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
                Error = true;
            }
            return fileList;
        }

        #endregion

        #region Delete



        //private void Delete(FileSystemInfo ObjToDelete)
        //{
        //    try
        //    {
        //        if ((ObjToDelete.Attributes & FileAttributes.Directory) == FileAttributes.Directory) // это каталог
        //        {
        //            Delete(ObjToDelete as DirectoryInfo); // приводим его к каталогу и удаляем
        //        }
        //        else
        //        {
        //            Delete(ObjToDelete as FileInfo); // иначе это файл и удаляем его
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error = true;
        //        Log.Write(ex.Message); // Скидиваем ошибку в лог
        //    }
        //}
        //private void Delete(DirectoryInfo Dir)
        //{
        //    PrepareToDelete(Dir);
        //    try
        //    {
        //        foreach (var file in Dir.GetFiles())
        //        {
        //            file.Delete();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Error = true;
        //        Log.Write(e.Message);
        //    }
        //    try
        //    {
        //        foreach (var dir in Dir.GetDirectories())
        //        {
        //            Delete(dir);
        //        }
        //        Dir.Delete();
        //    }
        //    catch (Exception ex)
        //    {
        //        Error = true;
        //        Log.Write(ex.Message);
        //    }
        //}
        //private void Delete(FileInfo File)
        //{
        //    try
        //    {
        //        PrepareToDelete(File);
        //        File.Delete();
        //    }
        //    catch (Exception e)
        //    {
        //        Error = true;
        //        Log.Write(e.Message);
        //    }
        //}
        //private void PrepareToDelete(DirectoryInfo Dir)
        //{
        //    try
        //    {
        //        foreach (var dir in Dir.GetDirectories())
        //        {
        //            dir.Attributes = FileAttributes.Normal;
        //            PrepareToDelete(dir);
        //        }
        //        foreach (var file in Dir.GetFiles())
        //        {
        //            file.Attributes = FileAttributes.Normal;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Error = true;
        //        Log.Write(e.Message);
        //    }
        //}
        //private void PrepareToDelete(FileInfo File)
        //{
        //    try
        //    {
        //        File.Attributes = FileAttributes.Normal;
        //    }
        //    catch (Exception ex)
        //    {
        //        Error = true;
        //        Log.Write(ex.Message);
        //    }
        //}

        #endregion

        #region Copy

        private static async void Copy (FileSystemInfo From, FileSystemInfo To)
        {
            var copyList = FileList(From as DirectoryInfo,-1);
            var copyDir = DirectoryList(From as DirectoryInfo, -1);
            try
            {
                foreach (DirectoryInfo dir in copyDir)
                {
                    var pathTo = new StringBuilder(dir.FullName);
                    pathTo.Remove(0, From.FullName.Length);
                    pathTo.Insert(0, To.FullName);
                    var to = new DirectoryInfo(pathTo.ToString());
                    to.Create();
                }
                foreach (FileInfo file in copyList)
                {
                    var pathTo = new StringBuilder(file.FullName);
                    pathTo.Remove(0, From.FullName.Length);
                    pathTo.Insert(0, To.FullName);
                    var to = new FileInfo(pathTo.ToString());
                    await CopyAsync(file, to);
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
                Error = true;
            }
        }

        //public static async Task CopyAsync(DirectoryInfo From, DirectoryInfo To)
        //{
        //    if (!To.Exists)
        //    {
        //        To.Create();
        //    }

        //    try
        //    {
        //        foreach (DirectoryInfo directoryInfo in From.GetDirectories())
        //        {
        //            var to = new DirectoryInfo(Path.Combine(To.FullName, directoryInfo.Name));

        //            if (!to.Exists)
        //            {
        //                to.Create();
        //            }

        //            await CopyAsync(directoryInfo, to);
        //            to.Refresh();
        //            to.Attributes = From.Attributes;
        //        }
        //        Error = false;
        //    }
        //    catch (Exception e)
        //    {
        //        Message = e.Message;
        //        Error = true;
        //        Log.Write(Message);
        //    }

        //    try
        //    {
        //        foreach (FileInfo file in From.GetFiles())
        //        {
        //            await CopyAsync(file, To);
        //        }
        //        Error = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //        Error = true;
        //        Log.Write(Message);
        //    }
        //}
        private static async Task CopyAsync(FileInfo File, FileInfo To)
        {
            // var to = new FileInfo(Path.Combine(To.FullName, File.Name));

            if (To.Exists)
            {
                To.Attributes = FileAttributes.Normal;
            }
            try
            {
                using (FileStream sourseStream = File.OpenRead())
                {
                    using (FileStream destinationStream = To.Create())
                    {
                        await sourseStream.CopyToAsync(destinationStream);
                        destinationStream.Flush(true);
                    }
                }
                To.Attributes = File.Attributes;
                

            }
            catch (Exception e)
            {
                Error = true;
                Log.Write(e.Message);
            }
        }

        #endregion

    }
}
