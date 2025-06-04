using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Dog : Pet
    {
        //добавить два поля

        //порода
        private string _breed;
        //окрас 
        private string _color;
        //история жизни до приюта 
        private string _lifeStory;

        //команды которые знает
        private string _commands;

        //добавить к ним свойтсва 
        public string Breed => _breed;
        public string Color => _color;
        public string LifeStory => _lifeStory;

        public override string Type => "Dog";

        public string Commands => _commands;


        //конструктор, отправить в базовый
        //public Dog(string name, double age, double weight, string breed, string color, string lifeStory,
        //    string commands) : base(name, age, weight)
        //{
        //    _breed = breed;
        //    _color = color;
        //    _lifeStory = lifeStory;

        //    _commands = commands;

        //}

        //public Dog() { }
    }
}
