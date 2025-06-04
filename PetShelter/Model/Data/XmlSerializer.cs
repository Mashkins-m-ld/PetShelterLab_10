using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using static Model.Core.PetWrapper;
using Model.Core;

namespace Model.Data
{
    public class XmlPetShelterSerializer : PetShelterSerializer
    {
        public override string Extention => "xml";

        public override void SerializeListOfObjects<T>(string folderPath, string fileName, List<T> list)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || list == null) return;

            Directory.CreateDirectory(folderPath);// проверка на папку

            string path = Path.Combine(folderPath, fileName + "." + Extention);

            //сериализация в xml
            if (typeof(Pet).IsAssignableFrom(typeof(T))) //T-Shelter или его наследник
            {
                List<PetWrapper> petWrappers = new List<PetWrapper>();
                foreach (var item in list)
                {
                    if ((item as Pet).Type == "Cat")
                    {
                        petWrappers.Add(new CatWrapper(item as Cat));
                    }
                    else if ((item as Pet).Type == "Dog")
                    {
                        petWrappers.Add(new DogWrapper(item as Dog));
                    }
                    else if ((item as Pet).Type == "Rabbit")
                    {
                        petWrappers.Add(new RabbitWrapper(item as Rabbit));
                    }
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PetWrapper>));
                using (FileStream fs = new FileStream(path, FileMode.Create)) //создаст файл
                {
                    xmlSerializer.Serialize(fs, petWrappers);
                }
            }

            else
            {
                List<ShelterWrapper> shelterWrappers = new List<ShelterWrapper>();
                foreach (var item in list)
                {
                    shelterWrappers.Add(new ShelterWrapper(item as Shelter));
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ShelterWrapper>));
                using (FileStream fs = new FileStream(path, FileMode.Create)) //создаст файл
                {
                    xmlSerializer.Serialize(fs, shelterWrappers);
                }
            }

            
        }

        

        public override List<T> DeserializeListOfObjects<T>(string folderPath, string fileSheletersName, string filePetsName)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileSheletersName) || string.IsNullOrEmpty(filePetsName)) return new List<T>();

            //загружаем приюты
            string sheltersPath = Path.Combine(folderPath, fileSheletersName + "." + Extention);
            if (!File.Exists(sheltersPath)) return new List<T>();

            List<Shelter> shelters;
            XmlSerializer xmlSerializer = new XmlSerializer (typeof(Shelter));

            using (FileStream fs = new FileStream(sheltersPath, FileMode.OpenOrCreate))
            {
                shelters =  xmlSerializer.Deserialize(fs) as List<Shelter>;
            }

            //загружаем питомцев
            string petsPath = Path.Combine(folderPath, filePetsName + "." + Extention);
            if (!File.Exists(petsPath)) return new List<T>();

            List<Pet> pets;
            xmlSerializer = new XmlSerializer(typeof(Pet));

            using (FileStream fs = new FileStream(petsPath, FileMode.OpenOrCreate))
            {
                pets = xmlSerializer.Deserialize(fs) as List<Pet>;
            }

            foreach (var pet in pets)
            {
                foreach (Shelter shelt in shelters)
                {
                    if (shelt.Name == pet.Shelter)
                    {

                        shelt.Add(pet);
                        break;
                    }
                }    
            }

            if (typeof(Pet).IsAssignableFrom(typeof(T))) // T - это Pet или его наследник
            {
                return pets.Cast<T>().ToList(); //к T привели
            }
            else
            {
                return shelters.Cast<T>().ToList(); //к T привели
            }
        }

        public override void SerializeOneObject<T>(string folderPath, string fileName, T obj)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || obj == null) return;
            Directory.CreateDirectory(folderPath);// проверка на папку
            string path = Path.Combine(folderPath, fileName + "." + Extention);

            //сериализация в xml
            if (typeof(Pet).IsAssignableFrom(typeof(T))) //T-Shelter или его наследник
            {
                PetWrapper petWrapper;

                if ((obj as Pet).Type == "Cat")
                {
                    petWrapper = new CatWrapper(obj as Cat);
                }
                else if ((obj as Pet).Type == "Dog")
                {
                    petWrapper = new DogWrapper(obj as Dog);
                }
                else 
                {
                    petWrapper = new RabbitWrapper(obj as Rabbit);
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PetWrapper));
                using (FileStream fs = new FileStream(path, FileMode.Create)) //создаст файл
                {
                    xmlSerializer.Serialize(fs, petWrapper);
                }
            }
        }

        //public override T DeserializeOneObject<T>(string folderPath, string fileName)
        //{
        //    if (typeof(Pet).IsAssignableFrom(typeof(T))) // T - это Pet или его наследник
        //    {
        //        string sheltersPath = Path.Combine(folderPath, fileSheletersName + "." + Extention);
        //        if (!File.Exists(sheltersPath)) return new List<T>();

        //        List<Shelter> shelters;
        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Shelter));

        //        using (FileStream fs = new FileStream(sheltersPath, FileMode.OpenOrCreate))
        //        {
        //            shelters = xmlSerializer.Deserialize(fs) as List<Shelter>;
        //        }
        //    }
        //    else
        //    {
        //        return shelters.Cast<T>().ToList(); //к T привели
        //    }
        //}
    }
}
