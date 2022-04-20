using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class Command

    {
        public static bool Error { get; private set; }
        public static string Message { get; private set; }

        #region Delete

        public static void Delete(DirectoryInfo Dir)
        {
            PrepareToDelete(Dir);
            try
            {
                foreach (var file in Dir.GetFiles())
                {
                    file.Delete();
                }
                Error = false;
            }
            catch (Exception e)
            {
                Message = e.Message;
                Error = true;
                Log.Write(Message);
            }
            try
            {
                foreach (var dir in Dir.GetDirectories())
                {
                    Delete(dir);
                }
                Dir.Delete();
                Error = false;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Error = true;
                Log.Write(Message);
            }
        }
        public static void Delete(FileInfo File)
        {
            try
            {
                PrepareToDelete(File);
                File.Delete();
                Error = false;
            }
            catch (Exception e)
            {
                Message = e.Message;
                Error = true;
                Log.Write(Message);
            }
        }
        private static void PrepareToDelete(DirectoryInfo Dir)
        {
            try
            {
                foreach (var dir in Dir.GetDirectories())
                {
                    dir.Attributes = FileAttributes.Normal;
                    PrepareToDelete(dir);
                }
                foreach (var file in Dir.GetFiles())
                {
                    file.Attributes = FileAttributes.Normal;
                }
                Error = false;
            }
            catch (Exception e)
            {
                Message = e.Message;
                Error = true;
                Log.Write(Message);
            }
        }
        private static void PrepareToDelete(FileInfo File)
        {
            try
            {
                File.Attributes = FileAttributes.Normal;
                Error = false;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Error = true;
                Log.Write(Message);
            }
        }

        #endregion

        #region Copy
        public static void Copy (DirectoryInfo From, DirectoryInfo To)
        {
            if (!To.Exists)
            {
                To.Create ();
            }
            foreach (FileInfo file in From.GetFiles())
            {
                file.CopyTo (Path.Combine(To.FullName, file.Name),true);
            }

            foreach (DirectoryInfo directoryInfo in From.GetDirectories())
            {
                var to = new DirectoryInfo (Path.Combine(To.FullName, directoryInfo.Name));
                if (!to.Exists) 
                {
                    to.Create ();
                }
                Copy (directoryInfo,to);
            }
        }
        public static void Copy (FileInfo File, DirectoryInfo To)
        {
            File.CopyTo(Path.Combine(To.FullName, File.Name),true);
        }

        #endregion
    }
}
