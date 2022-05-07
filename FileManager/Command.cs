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
            }
            catch (Exception e)
            {                
                Error = true;
                Log.Write(e);
            }
            try
            {
                foreach (var dir in Dir.GetDirectories())
                {
                    Delete(dir);
                }
                Dir.Delete();
            }
            catch (Exception ex)
            {
                Error = true;
                Log.Write(ex);
            }
        }
        public static void Delete(FileInfo File)
        {
            try
            {
                PrepareToDelete(File);
                File.Delete();
            }
            catch (Exception e)
            {
                Error = true;
                Log.Write(e);
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
            }
            catch (Exception e)
            {
                Error = true;
                Log.Write(e);
            }
        }
        private static void PrepareToDelete(FileInfo File)
        {
            try
            {
                File.Attributes = FileAttributes.Normal;
            }
            catch (Exception ex)
            {
                Error = true;
                Log.Write(ex);
            }
        }

        #endregion

        #region Copy

        public static async Task CopyAsync (DirectoryInfo From, DirectoryInfo To)
        {
            if (!To.Exists)
            {
                To.Create ();
            }

            try
            {
                foreach (DirectoryInfo directoryInfo in From.GetDirectories())
                {
                    var to = new DirectoryInfo(Path.Combine(To.FullName, directoryInfo.Name));

                    if (!to.Exists)
                    {
                        to.Create();
                    }

                    await CopyAsync(directoryInfo, to);
                    to.Refresh();
                    to.Attributes = From.Attributes;
                }
            }
            catch (Exception e)
            {
                Error = true;
                Log.Write(e);
            }

            try
            {
                foreach (FileInfo file in From.GetFiles())
                {
                    await (CopyAsync(file, To));
                }
            }
            catch (Exception ex)
            {
                Error = true;
                Log.Write(ex);
            }
            
        }
        public static async Task CopyAsync (FileInfo File, DirectoryInfo To)
        {
            var to = new FileInfo(Path.Combine(To.FullName, File.Name));

            if (to.Exists) 
            {
                to.Attributes = FileAttributes.Normal;
            }
            try
            {
                using (FileStream sourseStream = File.OpenRead())
                {
                    using (FileStream destinationStream = to.Create())
                    {
                        await sourseStream.CopyToAsync(destinationStream);
                        destinationStream.Flush(true);
                    }
                }
                to.Attributes = File.Attributes;

            }
            catch (Exception e)
            {
                Error = true;
                Log.Write(e);
            }
        }

        #endregion

    }
}
