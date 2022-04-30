using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileManager
{
    public class Command

    {
        public bool Error { get; private set; }
        public readonly List<FileSystemInfo> fileList;

        #region Init

        public Command (Commands Command, FileSystemInfo From, FileSystemInfo To = null)
        {
            fileList = new List<FileSystemInfo> ();
            Error = false;

            switch ( Command )
            {
                case Commands.DirectoryList :
                    DirectoryList(From as DirectoryInfo);
                    return;
                //case Commands.Copy:
                //    Copy(From, To);
                //    return;
                case Commands.Delete:
                    Delete(From);
                    return;
            }
        }

        #endregion

        #region List

        private void DirectoryList (DirectoryInfo Dir)
        {
            try
            {
                fileList.Add(Dir);
                foreach (FileInfo File in Dir.GetFiles())
                {
                    fileList.Add(File);
                }
                foreach (DirectoryInfo dir in Dir.GetDirectories())
                {
                    DirectoryList(dir);
                }
            }
            catch (Exception ex)
            {
                Error = true;
                Log.Write(ex.Message);
            }
        }

        #endregion

        #region Delete

        private void Delete (FileSystemInfo Delete)
        {
            DirectoryList(Delete as DirectoryInfo);
            while fileList
        }

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

        //#region Copy

        //private void Copy 
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
        //public static async Task CopyAsync(FileInfo File, DirectoryInfo To)
        //{
        //    var to = new FileInfo(Path.Combine(To.FullName, File.Name));

        //    if (to.Exists)
        //    {
        //        to.Attributes = FileAttributes.Normal;
        //    }
        //    try
        //    {
        //        using (FileStream sourseStream = File.OpenRead())
        //        {
        //            using (FileStream destinationStream = to.Create())
        //            {
        //                await sourseStream.CopyToAsync(destinationStream);
        //            }
        //        }
        //        to.Attributes = File.Attributes;
        //        Error = false;
        //    }
        //    catch (Exception e)
        //    {
        //        Message = e.Message;
        //        Error = true;
        //        Log.Write(Message);
        //    }
        //}

        //#endregion

    }
}
