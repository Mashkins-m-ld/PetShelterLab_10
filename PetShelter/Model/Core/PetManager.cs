using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Model
{
    public abstract class PetManager
    {
        //поля
        private static string _folderPath;
        private static string _fileSheltersName;
        private static string _filePetsName;
        private static List<Pet> _pets;
        private static List<Shelter> _shelters;

        private static Model.Data.JsonPetShelterSerializer _jsonSerializer ;

        //свойтсва
        public static List<Pet> Pets => _pets;
        public static List<Shelter> Shelters => _shelters;

        public static string FolderPath => _folderPath;
        public static string FileSheltersName => _fileSheltersName;
        public static string FilePetsName => _filePetsName;


        //конструктор
        static PetManager()
        {
            _pets = new List<Pet>();
            _shelters = new List<Shelter>();

            _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"PetManagerFiles");
            _fileSheltersName = "shelters";
            _filePetsName = "pets";

            _jsonSerializer = new Model.Data.JsonPetShelterSerializer();

        }
    


    //методы

    public static void Initialize()
        {

            Directory.CreateDirectory(_folderPath);

            // Проверяем существование файла с приютами
            string sheltersPath = Path.Combine(_folderPath, _fileSheltersName + ".json");
            if (File.Exists(sheltersPath)) // файл существует 
            {
                // Загружаем данные из файлов
                LoadData();
            }
            else
            {
                // Создаем тестовые данные
                CreateTestData();
                // Сохраняем в JSON
                SaveData();
            }
        }

        private static void CreateTestData()
        {

            // 5 кошек
            _pets.Add(new Cat("Мурка", 2, 3.5, "Дворовая", "Серый", "Найдена на улице", true, false));
            _pets.Add(new Cat("Барсик", 3, 4.2, "Персидская", "Белый", "Из питомника", true,false));
            _pets.Add(new Cat("Васька", 1, 2.8, "Британская", "Голубой", "Брошен хозяевами", false));
            _pets.Add(new Cat("Рыжик", 4, 5.1, "Мейн-кун", "Рыжий", "Конфискован у заводчика",true,false));
            _pets.Add(new Cat("Снежок", 0.5, 1.9, "Сиамская", "Белый", "Найдена в подвале", false,false));

            // 5 собак
            _pets.Add(new Dog("Шарик", 4, 12.5, "Дворняга", "Рыжий", "Найден в парке", "Сидеть, Лежать" ,true));
            _pets.Add(new Dog("Рекс", 5, 18.2, "Овчарка", "Черный", "Конфискован у жестокого хозяина", "Сидеть, Лежать, Голос" , false));
            _pets.Add(new Dog("Лайка", 2, 8.7, "Хаски", "Серо-белый", "Передана из питомника", "Сидеть" ));
            _pets.Add(new Dog("Бобик", 3, 10.1, "Лабрадор", "Коричневый", "Брошен при переезде", "Апорт, Место" ));
            _pets.Add(new Dog("Джек", 1, 5.3, "Джек-рассел", "Бело-коричневый", "Подобран на трассе", "Дай лапу, Фас"));

            // 5 кроликов
            _pets.Add(new Rabbit("Пушистик", 1, 1.2, "Ангорский", "Белый", "Передан из зоомагазина", true));
            _pets.Add(new Rabbit("Ушастик", 2, 1.5, "Вислоухий", "Серый", "Найден в парке"));
            _pets.Add(new Rabbit("Пятнышко", 0.5, 0.9, "Карликовый", "Черно-белый", "Брошен хозяевами",false));
            _pets.Add(new Rabbit("Малыш", 1.5, 1.1, "Голландский", "Коричневый", "Передан из приюта"));
            _pets.Add(new Rabbit("Рыжик", 3, 1.8, "Фландр", "Рыжий", "Конфискован у заводчика"));

            //приюты 
            var shelter1 = new Shelter("Братья наши меньшие", 10, 0);
            var shelter2 = new Shelter("Поставьте фулл балл пожалуйста", 15, 100.0);
            var shelter3 = new Shelter("Доброе сердце", 20, 150.0);

            _shelters.Add(shelter1);
            _shelters.Add(shelter2);
            _shelters.Add(shelter3);

            //распределить животных по приютам 
            for (int i = 0; i < _pets.Count; i++)
            {
                _shelters[i % 3].Add(_pets[i]);
            }

        }

        private static void SaveData()
        {
            //сохранить приюты в файл

            _jsonSerializer.SerializeListOfObjects<Shelter>(_folderPath, _fileSheltersName, _shelters);

            //сохранить всех питомцев в один файл

            _jsonSerializer.SerializeListOfObjects<Pet>(_folderPath, _filePetsName, _pets);

        }

        private static void LoadData()
        {
            //всё почистить
            _pets.Clear();
            _shelters.Clear();

            _pets = _jsonSerializer.DeserializeListOfObjects<Pet>(_folderPath, _fileSheltersName, _filePetsName);
            _shelters = _jsonSerializer.DeserializeListOfObjects<Shelter>(_folderPath, _fileSheltersName, _filePetsName);

        }
    }
}
