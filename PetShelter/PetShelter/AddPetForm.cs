using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShelter
{
    public partial class AddPetForm : Form
    {
        // Поля для хранения данных о питомце
        private string petName { get; set; }
        private double petAge { get; set; }
        private double petWeight { get; set; }
        private bool petClaustrophobia { get; set; }
        private string petBreed { get; set; }
        private string petColor { get; set; }
        private string petLifeStory { get; set; }
        private string petCommands { get; set; }
        private bool isToiletTrained { get; set; }

        // Тип питомца (Cat, Dog, Rabbit)
        public string PetType { get; private set; }

        // Конструктор
        public AddPetForm()
        {
            InitializeComponent();
            this.Text = "Добавить питомца";
            this.Size = new Size(400, 400); // Фиксированный размер формы
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Запрет изменения размера
            this.StartPosition = FormStartPosition.CenterScreen; // Центрирование формы
            SetupUI();
        }
        // Настройка интерфейса
        private void SetupUI()
        {
            // Создаем TableLayoutPanel для упорядочивания элементов
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 10,
                AutoSize = true,
                Padding = new Padding(10),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            // Настройка столбцов
            // Добавление элементов в TableLayoutPanel
            // Поле для имени
            tableLayout.Controls.Add(new Label { Text = "Имя:", TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            var txtName = new TextBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(txtName, 1, 0);

            // Поле для возраста
            tableLayout.Controls.Add(new Label { Text = "Возраст:", TextAlign = ContentAlignment.MiddleRight }, 0, 1);
            var txtAge = new TextBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(txtAge, 1, 1);

            // Поле для веса
            tableLayout.Controls.Add(new Label { Text = "Вес:", TextAlign = ContentAlignment.MiddleRight }, 0, 2);
            var txtWeight = new TextBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(txtWeight, 1, 2);

            // Поле для клаустрофобии
            tableLayout.Controls.Add(new Label { Text = "Клаустрофобия:", TextAlign = ContentAlignment.MiddleRight }, 0, 3);
            var chkClaustrophobia = new CheckBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(chkClaustrophobia, 1, 3);

            // Поле для породы
            tableLayout.Controls.Add(new Label { Text = "Порода:", TextAlign = ContentAlignment.MiddleRight }, 0, 4);
            var txtBreed = new TextBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(txtBreed, 1, 4);

            // Поле для окраса
            tableLayout.Controls.Add(new Label { Text = "Окрас:", TextAlign = ContentAlignment.MiddleRight }, 0, 5);
            var txtColor = new TextBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(txtColor, 1, 5);

            // Поле для истории
            tableLayout.Controls.Add(new Label { Text = "История:", TextAlign = ContentAlignment.MiddleRight }, 0, 6);
            var txtLifeStory = new TextBox { Dock = DockStyle.Fill };
            tableLayout.Controls.Add(txtLifeStory, 1, 6);

            // Поле для команд (только для собак)
            var lblCommands = new Label { Text = "Команды:", TextAlign = ContentAlignment.MiddleRight, Visible = false };
            var txtCommands = new TextBox { Dock = DockStyle.Fill, Visible = false };
            tableLayout.Controls.Add(lblCommands, 0, 7);
            tableLayout.Controls.Add(txtCommands, 1, 7);

            // Поле для приучения к лотку (только для кошек)
            var lblToilet = new Label { Text = "Приучен к лотку:", TextAlign = ContentAlignment.MiddleRight, Visible = false };
            var chkToilet = new CheckBox { Dock = DockStyle.Fill, Visible = false };
            tableLayout.Controls.Add(lblToilet, 0, 7);
            tableLayout.Controls.Add(chkToilet, 1, 7);

            // Выбор типа питомца
            tableLayout.Controls.Add(new Label { Text = "Тип:", TextAlign = ContentAlignment.MiddleRight }, 0, 8);
            var cmbType = new ComboBox { Dock = DockStyle.Fill };
            cmbType.Items.AddRange(new string[] { "Кошка", "Собака", "Кролик" });
            cmbType.SelectedIndex = -1;
            tableLayout.Controls.Add(cmbType, 1, 8);



            // Обработчик выбора типа
            cmbType.SelectedIndexChanged += (s, e) =>
            {
                var selectedType = cmbType.SelectedItem.ToString();
                lblCommands.Visible = selectedType == "Собака";
                txtCommands.Visible = selectedType == "Собака";
                lblToilet.Visible = selectedType == "Кошка";
                chkToilet.Visible = selectedType == "Кошка";
            };

            // Кнопка "Добавить"
            // Кнопка "Добавить"
            var btnAdd = new Button { Text = "Добавить", Dock = DockStyle.Fill };
            tableLayout.SetColumnSpan(btnAdd, 2); // Растягиваем кнопку на оба столбца
            tableLayout.Controls.Add(btnAdd, 0, 9);

            btnAdd.Click += (s, e) =>
            {
                // Проверка, выбран ли тип животного
                if (cmbType.SelectedItem == null)
                {
                    MessageBox.Show("Тип животного обязательно нужно выбрать.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Прерываем выполнение, если тип не выбран
                }
                // Проверка введенных данных
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtWeight.Text))
                {
                    MessageBox.Show("Заполните обязательные поля имя,возраст,вес,тип");
                    return;
                }
                // Проверка корректности возраста
                if (!double.TryParse(txtAge.Text, out double age) || age <= 0 || age > 50)
                {
                    MessageBox.Show("Возраст должен быть положительным числом и не превышать 50 лет.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка корректности веса
                if (!double.TryParse(txtWeight.Text, out double weight) || weight <= 0 || weight > 100)
                {
                    MessageBox.Show("Вес должен быть положительным числом и не превышать 100 кг.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Сохранение данных
                petName = txtName.Text;
                petAge = double.Parse(txtAge.Text);
                petWeight = double.Parse(txtWeight.Text);
                petClaustrophobia = chkClaustrophobia.Checked;
                petBreed = txtBreed.Text;
                petColor = txtColor.Text;
                petLifeStory = txtLifeStory.Text;
                PetType = cmbType.SelectedItem.ToString(); // Сохраняе
                petCommands = txtCommands.Text;
                isToiletTrained = chkToilet.Checked;

                // Закрытие формы
                this.DialogResult = DialogResult.OK;
                this.Close();
            };



            // Добавление TableLayoutPanel на форму
            this.Controls.Add(tableLayout);
        }

        // Метод для создания объекта питомца
        public Pet GetPet()
        {
            switch (PetType)
            {
                case "Кошка":
                    return new Cat(petName, petAge, petWeight, petBreed, petColor, petLifeStory, isToiletTrained, petClaustrophobia);
                case "Собака":
                    return new Dog(petName, petAge, petWeight, petBreed, petColor, petLifeStory, petCommands, petClaustrophobia);
                case "Кролик":
                    return new Rabbit(petName, petAge, petWeight, petBreed, petColor, petLifeStory, petClaustrophobia);
                default:
                    throw new InvalidOperationException("Неизвестный тип питомца.");
            }
        }
    }
}
