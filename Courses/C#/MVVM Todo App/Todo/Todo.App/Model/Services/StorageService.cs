using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Toolkit;

namespace Todo.App.Model.Services
{
    public class StorageService
    {
        private const string FileName = "tododb.xml";


        public Task Save<T>(T data)
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            using (var stream = new IsolatedStorageFileStream(FileName, FileMode.Create, isolatedStorage))
            {
                serializer.WriteObject(stream, data);
            }

            return Tasks.Null;
        }

        public Task<T> Load<T>()
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            var serializer = new DataContractSerializer(typeof(T));
            T res = default(T);

            try
            {
                using (var stream = new IsolatedStorageFileStream(FileName, FileMode.Open, isolatedStorage))
                {
                    res = (T)serializer.ReadObject(stream);
                }
            } catch
            {

            }

            return Task.FromResult(res);

        }

        public StorageService()
        {

        }
    }
}
