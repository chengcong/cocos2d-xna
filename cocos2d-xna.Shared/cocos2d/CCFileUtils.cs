namespace cocos2d
{
    using cocos2d.Framework;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CCFileUtils
    {
        protected static bool s_bPopupNotify = true;

        public static int ccLoadFileIntoMemory(string filename, out char[] file)
        {
            throw new NotImplementedException("win32 only definition does not realize !");
        }

        public static string ccRemoveHDSuffixFromFile(string path)
        {
            throw new NotImplementedException("Remove hd picture !");
        }

        public static Dictionary<string, object> dictionaryWithContentsOfFile(string pFileName)
        {
            CCDictMaker maker = new CCDictMaker();
            return maker.dictionaryWithContentsOfFile(pFileName);
        }

        public static string fullPathFromRelativeFile(string pszFilename, string pszRelativeFile)
        {
            string str = pszRelativeFile.Substring(0, pszRelativeFile.LastIndexOf("/") + 1);
            int index = pszFilename.IndexOf('.');
            pszFilename = pszFilename.Substring(0, index);
            return (str + pszFilename);
        }

        public static string fullPathFromRelativePath(string pszRelativePath)
        {
            return pszRelativePath;
        }

        public static string getFileData(string pszFileName, string pszMode, ulong pSize)
        {
            return CCApplication.sharedApplication().content.Load<CCContent>(pszFileName).Content;
        }

        public static char[] getFileDataFromZip(string pszZipFilePath, string pszFileName, ulong pSize)
        {
            throw new NotImplementedException("Cannot load zip files for this method has not been realized !");
        }

        public static string getWriteablePath()
        {
            throw new NotImplementedException("win32 only definition does not realize !");
        }

        public static void setResource(string pszZipFileName)
        {
            throw new NotImplementedException("win32 only definition does not realize !");
        }

        public static void setResourcePath(string pszResourcePath)
        {
            throw new NotImplementedException("win32 only definition does not realize !");
        }

        public static bool IsPopupNotify
        {
            get
            {
                return s_bPopupNotify;
            }
            set
            {
                s_bPopupNotify = value;
            }
        }
    }
}
