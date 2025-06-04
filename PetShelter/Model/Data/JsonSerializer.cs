using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class JsonPetShelterSerializer : PetShelterSerializer
    {
        //свойтсва
        public override string Extention => "json";

        //констркутор 
        public JsonPetShelterSerializer() { }

        //метод
        public override void SerializeListOfObjects<T>(string folderPath, string fileName, List<T> list)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || list==null ) return ;

            Directory.CreateDirectory(folderPath);// проверка на папку

            string path = Path.Combine(folderPath, fileName + "." + Extention);
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public override List<T> DeserializeListOfObjects<T>(string folderPath, string fileSheltersName, string filePetsName)
        {

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileSheltersName) || string.IsNullOrEmpty(filePetsName)) return new List<T>();


            //загружаем приюты
            string sheltersPath = Path.Combine(folderPath, fileSheltersName + "." + Extention);
            if (!File.Exists(sheltersPath)) return new List<T>();
            string json = File.ReadAllText(sheltersPath);
            //List<Shelter> shelters = JsonConvert.DeserializeObject<List<Shelter>>(json);

            List<Shelter> shelters = new List<Shelter>();
            JArray jArrayShelters = JArray.Parse(json);
            foreach (var jToken in jArrayShelters)
            {
                int.TryParse(jToken["Capacity"].ToString(), out int capacity);
                double.TryParse(jToken["OpenSpace"].ToString(), out double openSpace);
                Shelter shelter = new Shelter(jToken["Name"].ToString(), capacity, openSpace);
                shelters.Add(shelter);
            }


            //загружаем питомцев
            string petsPath = Path.Combine(folderPath, filePetsName + "." + Extention);
            if (!File.Exists(petsPath)) return new List<T>();
            json = File.ReadAllText(petsPath);

            //поэтапная десериализация 
            List<Pet> pets = new List<Pet>();
            //массив объектов json
            JArray jArray = JArray.Parse(json);

            foreach (var jToken in jArray)
            {
                string type = jToken["Type"].ToString();
                Pet pet;
                if (type == "Cat")
                {
                    pet = jToken.ToObject<Cat>();

                }
                else if (type == "Dog")
                {
                    pet = jToken.ToObject<Dog>();

                }
                else
                {
                    pet = jToken.ToObject<Rabbit>();

                }
                string shelter = (jToken["Shelter"]).ToObject<string>();
                
                foreach (Shelter shelt in shelters)
                {
                    if (shelt.Name == shelter)
                    {
                        
                        shelt.Add(pet);
                        break;
                    }
                }
                pets.Add(pet);
            }

            //Debug.WriteLine($"Загружено приютов: {shelters.Count}");

            if (typeof(Pet).IsAssignableFrom(typeof(T))) // T - это Pet или его наследник
            {
                return pets.Cast<T>().ToList(); //к T привели
            }
            else
            {
                return shelters.Cast<T>().ToList(); //к T привели
            }

        }
        public void SerializeDateAndNumber(string folderPath, string fileName, string[] dateAndNumber)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || dateAndNumber==null) return ;

            Directory.CreateDirectory(folderPath);// проверка на папку

            string path = Path.Combine(folderPath, fileName + "." + Extention);
            if (!File.Exists(path)) return;

            string json = JsonConvert.SerializeObject(dateAndNumber);
            File.WriteAllText(path, json);

        }
        public string[] DeserializeDateAndNumber(string folderPath, string fileName)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) ) return null;

            Directory.CreateDirectory(folderPath);// проверка на папку

            string path = Path.Combine(folderPath, fileName + "." + Extention);

            //первыый раз вызвали файл пока такой не существует
            if (!File.Exists(path))
            {
                string jsonFirst = JsonConvert.SerializeObject(new string[] { $"{DateTime.Now.ToString("dd.MM.yyyy")}", "0" });
                File.WriteAllText(path,jsonFirst);
            }
            string json = File.ReadAllText(path);
            string[] dateAndNumber = JsonConvert.DeserializeObject<string[]>(json);
            return dateAndNumber;
        }

        public override void SerializeOneObject<T>(string folderPath, string fileName, T obj)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || obj == null) return;

            Directory.CreateDirectory(folderPath);// проверка на папку

            string path = Path.Combine(folderPath, fileName + "." + Extention);
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        //public override T DeserializeOneObject<T>(string folderPath, string fileName)
        //{
        //    if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName)) return default(T);
        //    Directory.CreateDirectory(folderPath);// проверка на папку
        //    string path = Path.Combine(folderPath, fileName + "." + Extention);

        //    T obj = JsonConvert.DeserializeObject<T>(path);
        //    return obj;

        //}
    }
}
