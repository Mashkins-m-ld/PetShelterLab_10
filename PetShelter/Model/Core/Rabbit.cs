using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Rabbit : Pet
    {
        //добавить два поля

        //порода
        private string _breed;
        //окрас 
        private string _color;
        //история жизни до приюта 
        private string _lifeStory;

        //добавить к ним свойтсва 
        public string Breed => _breed;
        public string Color => _color;
        public string LifeStory => _lifeStory;

        public override string Type => "Rabbit";

        //конструктор, отправить в базовый
        //public Rabbit(string name, double age, double weight, string breed, string color, string lifeStory) : base(name, age, weight)
        //{
        //    _breed = breed;
        //    _color = color;
        //    _lifeStory = lifeStory;
        //}

        //public Rabbit() { }
    }
}
