using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Data;
using Model;
using Model.Core;

namespace PetShelter
{
    public partial class PetTableForm : Form
    {
        //поля
        private DataGridView dataGridViewCats;
        private DataGridView dataGridViewDogs;
        private DataGridView dataGridViewRabbits;
        private DataGridView dataGridView;
        private JsonPetShelterSerializer _jsonPetShelterSerializer;
        private XmlPetShelterSerializer _xmlPetShelterSerializer;
        // Поле для отслеживания типа таблицы
        private string _tableType; // "All", "Cat", "Dog", "Rabbit"

        //конструктор 

        public PetTableForm(List<Cat> cats, List<Dog> dogs, List<Rabbit> rabbits, string shelterName)
        {
            this.Text = "Все питомцы";
            this.Size = new Size(1000, 600);
            this._tableType = "All";

            //MessageBox.Show("all");

            //MessageBox.Show($"{cats.Count}");

            // Создаем таблицу для кошек
            dataGridViewCats = new DataGridView
            {
                Dock = DockStyle.Top,
                AutoGenerateColumns = true,
                DataSource = cats,
                ReadOnly = true,
                Height = 200
            };

            // Создаем таблицу для собак
            dataGridViewDogs = new DataGridView
            {
                Dock = DockStyle.Top,
                AutoGenerateColumns = true,
                DataSource = dogs,
                ReadOnly = true,
                Height = 200
            };

            // Создаем таблицу для кроликов
            dataGridViewRabbits = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = rabbits,
                ReadOnly = true,
                Height = 200
            };

            // Добавляем таблицы на форму
            this.Controls.Add(dataGridViewRabbits);
            this.Controls.Add(dataGridViewDogs);
            this.Controls.Add(dataGridViewCats);

            if (shelterName != null)
            {
                //чекбокс для удаления
                AddCheckboxColumn(dataGridViewCats);
                AddCheckboxColumn(dataGridViewDogs);
                AddCheckboxColumn(dataGridViewRabbits);

                //кнопки
                // Кнопка "Добавить"
                var btnAdd = new Button
                {
                    Text = "Добавить",
                    Dock = DockStyle.Bottom
                };
                btnAdd.Click += (sender, e) => BtnAdd_Click(sender, e, shelterName);

                // Кнопка "Удалить"
                var btnRemove = new Button
                {
                    Text = "Удалить",
                    Dock = DockStyle.Bottom
                };
                btnRemove.Click += (sender, e) => BtnRemove_Click(sender, e, shelterName);
                // Добавляем кнопки на форму
                this.Controls.Add(btnAdd);
                this.Controls.Add(btnRemove);
            }

        }

        public PetTableForm(List<Cat> pets, string title, string shelterName)
        {
            this.Text = title;
            this.Size = new Size(800, 600);
            this._tableType = "Cat";
            //MessageBox.Show("cats");

            // Создаем таблицу
            this.dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = pets,
                ReadOnly = true
            };

            this.Controls.Add(dataGridView);

            if (shelterName != null)
            {
                AddCheckboxColumn(dataGridView);

                //кнопки
                // Кнопка "Добавить"
                var btnAdd = new Button
                {
                    Text = "Добавить",
                    Dock = DockStyle.Bottom
                };
                btnAdd.Click += (sender, e) => BtnAdd_Click(sender, e, shelterName);

                // Кнопка "Удалить"
                var btnRemove = new Button
                {
                    Text = "Удалить",
                    Dock = DockStyle.Bottom
                };
                btnRemove.Click += (sender, e) => BtnRemove_Click(sender, e, shelterName);
                // Добавляем кнопки на форму
                this.Controls.Add(btnAdd);
                this.Controls.Add(btnRemove);
            }
        }

        public PetTableForm(List<Dog> pets, string title, string shelterName)
        {
            this.Text = title;
            this.Size = new Size(800, 600);
            this._tableType = "Dog";

            //MessageBox.Show("dogs");

            // Создаем таблицу
            this.dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = pets,
                ReadOnly = true
            };

            this.Controls.Add(dataGridView);
            if (shelterName != null)
            {
                AddCheckboxColumn(dataGridView);

                //кнопки
                // Кнопка "Добавить"
                var btnAdd = new Button
                {
                    Text = "Добавить",
                    Dock = DockStyle.Bottom
                };
                btnAdd.Click += (sender, e) => BtnAdd_Click(sender, e, shelterName);

                // Кнопка "Удалить"
                var btnRemove = new Button
                {
                    Text = "Удалить",
                    Dock = DockStyle.Bottom
                };
                btnRemove.Click += (sender, e) => BtnRemove_Click(sender, e, shelterName);
                // Добавляем кнопки на форму
                this.Controls.Add(btnAdd);
                this.Controls.Add(btnRemove);
            }
        }

        public PetTableForm(List<Rabbit> pets, string title, string shelterName)
        {
            this.Text = title;
            this.Size = new Size(800, 600);
            this._tableType = "Rabbit";
            //MessageBox.Show("rabbits");

            // Создаем таблицу
            this.dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = pets,
                ReadOnly = true
            };

            this.Controls.Add(dataGridView);

            if (shelterName != null)
            {
                AddCheckboxColumn(dataGridView);

                //кнопки
                // Кнопка "Добавить"
                var btnAdd = new Button
                {
                    Text = "Добавить",
                    Dock = DockStyle.Bottom
                };
                btnAdd.Click += (sender, e) => BtnAdd_Click(sender, e, shelterName);

                // Кнопка "Удалить"
                var btnRemove = new Button
                {
                    Text = "Удалить",
                    Dock = DockStyle.Bottom
                };
                btnRemove.Click += (sender, e) => BtnRemove_Click(sender, e, shelterName);
                // Добавляем кнопки на форму
                this.Controls.Add(btnAdd);
                this.Controls.Add(btnRemove);
            }

        }

        //это метод, просто ему тут удобнее побыть
        private void AddCheckboxColumn(DataGridView dataGridView)
        {
            var checkboxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "CheckboxColumn",
                HeaderText = "Удалить",
                Width = 50,
                FalseValue = false,
                TrueValue = true,
                ReadOnly = false
            };

            dataGridView.Columns.Add(checkboxColumn);
            checkboxColumn.DisplayIndex = 0;

            // Подписываемся на событие CellClick
            dataGridView.CellClick += DataGridView_CellClick;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Проверяем, что кликнули по столбцу с чекбоксом
            {
                DataGridView dgv = (DataGridView)sender;
                bool currentValue = Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !currentValue;
                dgv.EndEdit(); // Завершаем редактирование
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e, string shelterName)
        {
            // Логика добавления питомца
            // Логика добавления питомца
            Shelter shelter = PetManager.Shelters.FirstOrDefault(s => s.Name == shelterName);

            var addForm = new AddPetForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                var newPet = addForm.GetPet(); // Используем новый метод GetPet

                if (newPet != null)
                {
                    if (string.IsNullOrEmpty(newPet.Name))
                    {
                        MessageBox.Show("Имя питомца обязательно для заполнения.");
                        return;
                    }
                    if (shelter.Pets.Any(p => p.Name == newPet.Name && p.Type == newPet.Type))
                    {
                        MessageBox.Show("Питомец с такими параметрами уже существует в приюте.");
                        return;
                    }
                    if (shelter.Pets.Length >= shelter.Capacity)
                    {
                        MessageBox.Show("Вместимость приюта превышена. Невозможно добавить нового питомца.");
                        return;
                    }
                    // Проверка на клаустрофобию и наличие открытого пространства
                    if (newPet.Claustrophobia && shelter.OpenSpace == 0)
                    {
                        MessageBox.Show("Животное с клаустрофобией нельзя добавить в приют без открытого пространства.");
                        return;
                    }
                    // Добавляем питомца в список
                    PetManager.Pets.Add(newPet);

                    //и в шелтер надо добавить 
                    shelter.PerformActionOnPet(newPet, shelter.Add);


                    //добавить файл с питомцами
                    _jsonPetShelterSerializer = new JsonPetShelterSerializer();
                    _jsonPetShelterSerializer.SerializeOneObject(PetManager.FolderPath, $"new_{newPet.Type}_{newPet.Name}", newPet);

                    _xmlPetShelterSerializer = new XmlPetShelterSerializer();
                    _xmlPetShelterSerializer.SerializeOneObject(PetManager.FolderPath, $"new_{newPet.Type}_{newPet.Name}", newPet);

                    //перезаписать файл со всеми животными и приютами
                    _jsonPetShelterSerializer.SerializeListOfObjects<Pet>(PetManager.FolderPath, "pets", PetManager.Pets);
                    _jsonPetShelterSerializer.SerializeListOfObjects<Shelter>(PetManager.FolderPath, "shelters", PetManager.Shelters);

                    // Обновляем отображение таблиц
                    if (_tableType == "All")
                    {
                        dataGridViewCats.DataSource = null;
                        List<Pet> cats = PetManager.Pets.Where(p => p is Cat && p.Shelter == shelterName).ToList();
                        List<Cat> catsList = cats.Select(p => (Cat)p).ToList();
                        dataGridViewCats.DataSource = catsList;

                        dataGridViewDogs.DataSource = null;
                        List<Pet> dogs = PetManager.Pets.Where(p => p is Dog && p.Shelter == shelterName).ToList();
                        List<Dog> dogsList = dogs.Select(p => (Dog)p).ToList();
                        dataGridViewDogs.DataSource = dogsList;

                        dataGridViewRabbits.DataSource = null;
                        List<Pet> rabbits = PetManager.Pets.Where(p => p is Rabbit && p.Shelter == shelterName).ToList();
                        List<Rabbit> rabbitsList = rabbits.Select(p => (Rabbit)p).ToList();
                        dataGridViewRabbits.DataSource = rabbitsList;
                    }
                    else
                    {
                        dataGridView.DataSource = null;
                        if (_tableType == "Cat")
                        {
                            List<Pet> cats = PetManager.Pets.Where(p => p is Cat && p.Shelter == shelterName).ToList();
                            List<Cat> catsList = cats.Select(p => (Cat)p).ToList();
                            dataGridView.DataSource = catsList;
                        }
                        else if (_tableType == "Dog")
                        {
                            List<Pet> dogs = PetManager.Pets.Where(p => p is Dog && p.Shelter == shelterName).ToList();
                            List<Dog> dogsList = dogs.Select(p => (Dog)p).ToList();
                            dataGridView.DataSource = dogsList;
                        }
                        else
                        {
                            List<Pet> rabbits = PetManager.Pets.Where(p => p is Rabbit && p.Shelter == shelterName).ToList();
                            List<Rabbit> rabbitsList = rabbits.Select(p => (Rabbit)p).ToList();
                            dataGridView.DataSource = rabbitsList;
                        }

                    }

                }
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e, string shelterName)
        {
            var shelter = PetManager.Shelters.FirstOrDefault(s => s.Name == shelterName);
            if (shelter == null)
            {
                MessageBox.Show("Приют не найден.");
                return;
            }

            // Получаем список выбранных питомцев для удаления
            List<Pet> petsToRemove = new List<Pet>();

            // В зависимости от типа таблицы проверяем соответствующий DataGridView
            if (_tableType == "All")
            {
                // Проверяем все три таблицы
                petsToRemove.AddRange(GetSelectedPets(dataGridViewCats));
                petsToRemove.AddRange(GetSelectedPets(dataGridViewDogs));
                petsToRemove.AddRange(GetSelectedPets(dataGridViewRabbits));
            }
            else
            {
                // Проверяем основную таблицу
                petsToRemove.AddRange(GetSelectedPets(dataGridView));
            }

            if (petsToRemove.Count == 0)
            {
                MessageBox.Show("Не выбраны питомцы для удаления.");
                return;
            }

            // Подтверждение удаления
            var confirmResult = MessageBox.Show($"Вы уверены, что хотите удалить {petsToRemove.Count} питомцев?",
                                               "Подтверждение удаления",
                                               MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }
            //MessageBox.Show($"For delete {petsToRemove.Count}");
            // Удаляем питомцев из всех коллекций
            foreach (var pet in petsToRemove)
            {
                // Удаляем из общего списка питомцев
                //MessageBox.Show($"{PetManager.Pets.Count}");

                List<Pet> newPets = new List<Pet>();
                PetManager.Pets.RemoveAt(PetManager.Pets.Count - 1);
                foreach (var managerPet in PetManager.Pets)
                {
                    if (managerPet.Name == pet.Name &&
                        managerPet.Type == pet.Type) continue;
                    newPets.Add(managerPet);
                }
                for (int i = 0; i < newPets.Count; i++)
                {
                    PetManager.Pets[i] = newPets[i];
                }
                //MessageBox.Show($"{PetManager.Pets.Count}");

                // Удаляем из приюта
                foreach (Shelter managerShelter in PetManager.Shelters)
                {
                    if (shelter.Name == managerShelter.Name)
                    {
                        //MessageBox.Show($"{managerShelter.Pets.Length}");
                        managerShelter.PerformActionOnPet(pet, shelter.RemovePet);
                        //MessageBox.Show($"{managerShelter.Pets.Length}");
                    }
                }

                // Удаляем файлы с данными питомца
                string petFileName = $"new_{pet.Type}_{pet.Name}";
                string jsonPath = Path.Combine(PetManager.FolderPath, $"{petFileName}.json");
                string xmlPath = Path.Combine(PetManager.FolderPath, $"{petFileName}.xml");

                if (File.Exists(jsonPath)) File.Delete(jsonPath);
                if (File.Exists(xmlPath)) File.Delete(xmlPath);
            }

            // Обновляем файлы с полными списками
            _jsonPetShelterSerializer = new JsonPetShelterSerializer();
            _xmlPetShelterSerializer = new XmlPetShelterSerializer();

            _jsonPetShelterSerializer.SerializeListOfObjects(PetManager.FolderPath, "pets", PetManager.Pets);
            _jsonPetShelterSerializer.SerializeListOfObjects(PetManager.FolderPath, "shelters", PetManager.Shelters);


            // Обновляем отображение таблиц
            if (_tableType == "All")
            {
                dataGridViewCats.DataSource = null;
                List<Pet> cats = PetManager.Pets.Where(p => p is Cat && p.Shelter == shelterName).ToList();
                List<Cat> catsList = cats.Select(p => (Cat)p).ToList();
                dataGridViewCats.DataSource = catsList;

                dataGridViewDogs.DataSource = null;
                List<Pet> dogs = PetManager.Pets.Where(p => p is Dog && p.Shelter == shelterName).ToList();
                List<Dog> dogsList = dogs.Select(p => (Dog)p).ToList();
                dataGridViewDogs.DataSource = dogsList;

                dataGridViewRabbits.DataSource = null;
                List<Pet> rabbits = PetManager.Pets.Where(p => p is Rabbit && p.Shelter == shelterName).ToList();
                List<Rabbit> rabbitsList = rabbits.Select(p => (Rabbit)p).ToList();
                dataGridViewRabbits.DataSource = rabbitsList;
            }
            else
            {
                dataGridView.DataSource = null;
                if (_tableType == "Cat")
                {
                    List<Pet> cats = PetManager.Pets.Where(p => p is Cat && p.Shelter == shelterName).ToList();
                    List<Cat> catsList = cats.Select(p => (Cat)p).ToList();
                    dataGridView.DataSource = catsList;
                }
                else if (_tableType == "Dog")
                {
                    List<Pet> dogs = PetManager.Pets.Where(p => p is Dog && p.Shelter == shelterName).ToList();
                    List<Dog> dogsList = dogs.Select(p => (Dog)p).ToList();
                    dataGridView.DataSource = dogsList;
                }
                else
                {
                    List<Pet> rabbits = PetManager.Pets.Where(p => p is Rabbit && p.Shelter == shelterName).ToList();
                    List<Rabbit> rabbitsList = rabbits.Select(p => (Rabbit)p).ToList();
                    dataGridView.DataSource = rabbitsList;
                }

            }

            MessageBox.Show($"Успешно удалено {petsToRemove.Count} питомцев.");
        }

        // Вспомогательный метод для получения выбранных питомцев из DataGridView
        private List<Pet> GetSelectedPets(DataGridView dataGridView)
        {
            List<Pet> selectedPets = new List<Pet>();

            if (dataGridView == null)
            {
                MessageBox.Show("DataGridView is null.");
                return selectedPets;
            }

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["CheckboxColumn"].Value is bool isSelected && isSelected)
                {
                    if (row.DataBoundItem is Pet pet) // Приводим к базовому типу
                    {
                        selectedPets.Add(pet);
                    }
                    else
                    {
                        MessageBox.Show("DataBoundItem is not a Pet.");
                    }
                }
            }

            if (selectedPets.Count == 0)
            {
                //MessageBox.Show("No pets selected for deletion.");
            }

            return selectedPets;
        }



    }
}
