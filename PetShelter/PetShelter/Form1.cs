using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Model.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace PetShelter
{
    public partial class Form1 : Form
    {

        //поля
        private Model.Data.JsonPetShelterSerializer _serializer;
        //нужно хранить date и numbers в файле 
        private string _fileDateNumberName;
        private string _previousFormatFilePath;

        public Form1() //конструктор 
        {
            InitializeComponent();
            PetManager.Initialize();
            _serializer = new Model.Data.JsonPetShelterSerializer();
            _previousFormatFilePath = Path.Combine(PetManager.FolderPath, "previous_format.txt");
            _fileDateNumberName = "DateAndNumber";

        }
        //методы

        private void Form1_Load(object sender, EventArgs e)
        {
            //XmlPetShelterSerializer serializer = new XmlPetShelterSerializer();
            //serializer.SerializeListOfObjects<Shelter>(PetManager.FolderPath, "exampleXml", PetManager.Shelters);
            ////Заполняем первый выпадающий список
            comboBox1.Items.Add("Все питомцы");
            comboBox1.Items.Add("Кошки");
            comboBox1.Items.Add("Собаки");
            comboBox1.Items.Add("Кролики");


            //Указываем первый элемент по умолчанию
            comboBox1.SelectedIndex = 0;


            //Заполняем второй выпадающий список
            comboBox2.Items.Add("Все приюты");
            comboBox2.Items.Add("Братья наши меньшие");
            comboBox2.Items.Add("Поставьте фулл балл пожалуйста");
            comboBox2.Items.Add("Доброе сердце");

            //Указываем первый элемент по умолчанию
            comboBox2.SelectedIndex = 0;

            // Инициализация ComboBox для выбора формата

            comboBoxFormat.Items.Add("JSON");
            comboBoxFormat.Items.Add("XML");
            comboBoxFormat.SelectedIndex = 0; // По умолчанию выбран JSON
                                              // Загружаем предыдущий формат
            string previousFormat = LoadPreviousFormat();

            // Устанавливаем выбранный формат в ComboBox
            comboBoxFormat.SelectedItem = previousFormat;

            // Подписываемся на событие изменения формата
            comboBoxFormat.SelectedIndexChanged += comboBoxFormat_SelectedIndexChanged;
            // Добавляем ComboBox на форму
            this.Controls.Add(comboBoxFormat);

        }
        private void SavePreviousFormat(string format)
        {
            File.WriteAllText(_previousFormatFilePath, format);
        }
        private string LoadPreviousFormat()
        {
            if (File.Exists(_previousFormatFilePath))
            {
                return File.ReadAllText(_previousFormatFilePath);
            }
            return "JSON"; // По умолчанию возвращаем "JSON"
        }
        private void CopyDataToNewFormat(string newFormat)
        {
            // Получаем список всех файлов отчетов в папке
            string[] files = Directory.GetFiles(PetManager.FolderPath, "Подборка_№*");

            foreach (string file in files)
            {
                try
                {
                    // Определяем текущий формат файла
                    string currentFormat = Path.GetExtension(file).Replace(".", "");

                    // Если файл не относится к текущему/новому формату, пропускаем его
                    if (currentFormat != "json" && currentFormat != "xml")
                        continue;

                    // Десериализуем данные из текущего формата
                    List<Pet> pets;
                    if (currentFormat == "json")
                    {
                        pets = _serializer.DeserializeListOfObjects<Pet>(PetManager.FolderPath, "shelters", Path.GetFileNameWithoutExtension(file));
                    }
                    else if (currentFormat == "xml")
                    {
                        var xmlSerializer = new XmlPetShelterSerializer();
                        pets = xmlSerializer.DeserializeListOfObjects<Pet>(PetManager.FolderPath, "shelters", Path.GetFileNameWithoutExtension(file));
                    }
                    else
                    {
                        continue;
                    }

                    // Сериализуем данные в новый формат
                    string newFileName = Path.GetFileNameWithoutExtension(file);
                    if (newFormat == "JSON")
                    {
                        _serializer.SerializeListOfObjects(PetManager.FolderPath, newFileName, pets);
                    }
                    else if (newFormat == "XML")
                    {
                        var xmlSerializer = new XmlPetShelterSerializer();
                        xmlSerializer.SerializeListOfObjects(PetManager.FolderPath, newFileName, pets);
                    }

                    // Удаляем старый файл (если нужно)
                    //if (currentFormat != newFormat.ToLower())
                    //{
                    //    File.Delete(file);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при копировании файла {file}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем новый выбранный формат
            string newFormat = comboBoxFormat.SelectedItem.ToString();

            // Загружаем предыдущий формат из файла
            string previousFormat = LoadPreviousFormat();

            // Проверка, изменился ли формат
            if (newFormat != previousFormat)
            {
                // Формат сменился
                MessageBox.Show($"Формат сменился с {previousFormat} на {newFormat}", "Смена формата", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Копируем данные из старого формата в новый
                CopyDataToNewFormat(newFormat);

                // Сохраняем новый формат как предыдущий
                SavePreviousFormat(newFormat);
            }
        }

        private void SaveData(List<Pet> pets)
        {
            string format = comboBoxFormat.SelectedItem.ToString();
            // date и numbers получить и поработать с ними
            string[] dateAndNumber = _serializer.DeserializeDateAndNumber(PetManager.FolderPath, _fileDateNumberName);

            if (DateTime.Now.ToString("dd.MM.yyyy") != dateAndNumber[0]) //новый день
            {
                dateAndNumber[0] = DateTime.Now.ToString("dd.MM.yyyy");
                dateAndNumber[1] = "0";
                //MessageBox.Show("Сменили дату");
            }
            int.TryParse(dateAndNumber[1], out int number);
            number++;
            //MessageBox.Show(number.ToString());
            dateAndNumber[1] = number.ToString();

            //записать новые значения 
            _serializer.SerializeDateAndNumber(PetManager.FolderPath, _fileDateNumberName, dateAndNumber);

            string fileName = $"Подборка_№{dateAndNumber[1]}_от_{dateAndNumber[0]}";

            // Сохраняем данные в выбранном формате
            if (format == "JSON")
            {
                _serializer.SerializeListOfObjects(PetManager.FolderPath, fileName, pets);
            }
            else if (format == "XML")
            {
                var xmlSerializer = new XmlPetShelterSerializer();
                xmlSerializer.SerializeListOfObjects(PetManager.FolderPath, fileName, pets);
            }
        }


        //обработчик кнопки
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Получаем выбранные параметры фильтрации
            //тип животного
            Type petType = typeof(Pet);
            if (comboBox1.SelectedIndex == 1)
            {
                petType = typeof(Cat);

            }
            if (comboBox1.SelectedIndex == 2)
            {
                petType = typeof(Dog);

            }
            if (comboBox1.SelectedIndex == 3)
            {
                petType = typeof(Rabbit);

            }


            //имя приюта 
            string shelterName = comboBox2.SelectedIndex > 0
            ? comboBox2.SelectedItem.ToString()
            : null;

            //чекбокс (если поставлена галочка, то у приюта ЕСТЬ открытая территория
            //=> там могут селиться любые жиовтные,
            //если галочки нет, то у приюта НЕТ открытой территории
            //=> могут селиться животные только БЕЗ клаустрофобии)
            bool onlyWithOpenSpace = false;
            foreach (Control control in this.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Text == "Только приюты с открытым участком")
                {
                    onlyWithOpenSpace = checkBox.Checked;
                    break;
                }
            }
            //false - без клаустрофобии 
            //true - любые


            //2.фильтруем питомцев
            List<Pet> pets = new List<Pet>();

            //MessageBox.Show($"Загружено {PetManager.Shelters[0].Name}");

            if (shelterName == null)
            {
                foreach (Shelter shelter in PetManager.Shelters)
                {
                    if (onlyWithOpenSpace == true && (shelter.OpenSpace == 0)) continue;

                    Pet[] resultFilter = shelter.Filter(petType);

                    foreach (Pet pet in resultFilter) pets.Add(pet);

                }
            }
            else
            {
                foreach (Shelter shelter in PetManager.Shelters)
                {
                    if (onlyWithOpenSpace == true && (shelter.OpenSpace == 0)) continue;
                    if (shelter.Name == shelterName)
                    {
                        Pet[] resultFilter = shelter.Filter(petType);

                        foreach (Pet pet in resultFilter) pets.Add(pet);
                    }
                }
            }

            //сохранить данные
            SaveData(pets);

            //3.Показываем результаты
            if (pets.Any() || shelterName != null)
            {
                if (comboBox1.SelectedIndex == 0) // Если выбраны "Все питомцы"
                {
                    // Группируем питомцев по типу
                    var cats = pets.Where(p => p is Cat).Cast<Cat>().ToList();
                    var dogs = pets.Where(p => p is Dog).Cast<Dog>().ToList();
                    var rabbits = pets.Where(p => p is Rabbit).Cast<Rabbit>().ToList();
                    //MessageBox.Show($"{cats.Count + dogs.Count + rabbits.Count} {PetManager.Pets.Count}");
                    var resultForm = new PetTableForm(cats, dogs, rabbits, shelterName);
                    resultForm.Show();


                }
                else // Если выбран конкретный тип
                {

                    if (petType == typeof(Cat))
                    {
                        var cats = pets.Where(p => p is Cat).Cast<Cat>().ToList();
                        var resultForm = new PetTableForm(cats, comboBox1.SelectedItem.ToString(), shelterName);
                        resultForm.Show();

                    }
                    else if (petType == typeof(Dog))
                    {
                        var dogs = pets.Where(p => p is Dog).Cast<Dog>().ToList();
                        var resultForm = new PetTableForm(dogs, comboBox1.SelectedItem.ToString(), shelterName);
                        resultForm.Show();

                    }
                    else
                    {
                        var rabbits = pets.Where(p => p is Rabbit).Cast<Rabbit>().ToList();
                        var resultForm = new PetTableForm(rabbits, comboBox1.SelectedItem.ToString(), shelterName);
                        resultForm.Show();

                    }

                }
            }
            else
            {
                MessageBox.Show("Животные по заданным критериям не найдены");
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
