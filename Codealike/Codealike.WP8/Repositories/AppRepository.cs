namespace Codealike.WP8.Repositories
{
    using System;
    using System.IO;
    using System.Text;
    using System.IO.IsolatedStorage;

    using Newtonsoft.Json;
    
    using PortableLogic.Repositories;
    using PortableLogic.Communication.Services;

    public class AppRepository : IAppRepository
    {
        private string _fileName;

        public AppRepository()
        {
            _fileName = "credentials.dat";
            
        }

        public void SaveCredentials(Credentials credentials)
        {
            try
            {
                var serializedObject = JsonConvert.SerializeObject(credentials);
                var bytes = Encoding.UTF8.GetBytes(serializedObject);
                WriteToFile(bytes, _fileName);
            }
            catch (Exception)
            {
            }
        }

        public Credentials LoadCredentials()
        {
            try
            {
                var bytesRead = ReadFromFile(_fileName);
                var json = Encoding.UTF8.GetString(bytesRead, 0, bytesRead.Length);
                var credentials = JsonConvert.DeserializeObject<Credentials>(json);
                return credentials;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeleteCredentials()
        {
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            file.DeleteFile(_fileName);
        }

        public void WriteToFile(byte[] data, string filePath)
        {
            try
            {
                IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                var writeStream = new IsolatedStorageFileStream(filePath, FileMode.Create, FileAccess.Write, file);

                Stream writer = new StreamWriter(writeStream).BaseStream;
                writer.Write(data, 0, data.Length);
                writer.Close();
                writeStream.Close();
            }
            catch (Exception)
            {
            }
        }

        public byte[] ReadFromFile(string filePath)
        {
            try
            {
                IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                var readStream = new IsolatedStorageFileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, file);

                Stream reader = new StreamReader(readStream).BaseStream;
                var data = new byte[reader.Length];

                reader.Read(data, 0, data.Length);
                reader.Close();
                readStream.Close();

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
