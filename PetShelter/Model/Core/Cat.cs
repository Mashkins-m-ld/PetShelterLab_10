using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Cat : Pet
    {
        //добавить два поля

        //порода
        private string _breed;
        //окрас 
        private string _color;
        //история жизни до приюта 
        private string _lifeStory;

        //приучен ли к лотку 
        private bool _toilet;

        //добавить к ним свойтсва 
        public string Breed => _breed;
        public string Color => _color;
        public string LifeStory => _lifeStory;

        public bool Toilet => _toilet;

        public override string Type => "Cat";

        //чтобы путанницы не было
        //конструктор, отправить в базовый
        //public Cat(string name, double age, double weight, string breed, string color, string lifeStory,
        //    bool toilet) : base(name, age, weight)
        //{
        //    _breed = breed;
        //    _color = color;
        //    _lifeStory = lifeStory;

        //    _toilet = toilet;
        //}

        //конструктор без атрибутов дяля xml - сериализации 
        //public Cat() { }


    }
}
