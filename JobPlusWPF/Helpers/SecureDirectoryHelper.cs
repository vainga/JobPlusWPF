using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JobPlusWPF.Helpers
{
    public class SecureDirectoryHelper
    {
        public static string CreateSecureDirectory(int userId)
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JobPlusWPF", "UserPhotos", userId.ToString());

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);

                var directoryInfo = new DirectoryInfo(directoryPath);
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                directorySecurity.SetAccessRuleProtection(true, false);
                directoryInfo.SetAccessControl(directorySecurity);
            }

            return directoryPath;
        }
    }
}
