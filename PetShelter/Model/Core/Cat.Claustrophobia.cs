using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Cat : Pet
    {

        //конструктор 
        public Cat(string name, double age, double weight, string breed, string color, string lifeStory,
           bool toilet,bool claustrophobia = false) : base(name, age, weight,claustrophobia)
        {
            _breed = breed;
            _color = color;
            _lifeStory = lifeStory;
            _toilet = toilet;
        }
    }
}
