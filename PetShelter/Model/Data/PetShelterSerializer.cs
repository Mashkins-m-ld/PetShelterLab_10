using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public abstract class PetShelterSerializer
    {
        //свойства 
        public abstract string Extention { get; }

        //методы
        public abstract void SerializeListOfObjects<T>(string folderPath, string fileName, List<T> list) where T: class;

        public abstract List<T> DeserializeListOfObjects<T>(string folderPath, string fileSheletersName, string filePetsName) where T: class;

        public abstract void SerializeOneObject<T>(string folderPath, string fileName, T obj);
        //public abstract T DeserializeOneObject<T>(string folderPath, string fileName);
    }
}
