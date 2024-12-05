using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatorium.Utils
{
    public class PathHelper
    {
        public static string ProjectRootPath
        {
            get
            {
                // Получаем путь к директории исполняемого файла
                string binDebugDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Поднимаемся на 3 уровня вверх (bin -> Debug -> ProjectRoot)
                string projectRoot = Path.Combine(binDebugDirectory, "..", "..");
                return projectRoot;
            }
        }

        public static void logRootPath()
        {
            Console.WriteLine(ProjectRootPath);
        }

    }
}
