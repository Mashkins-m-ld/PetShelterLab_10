using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Dog : Pet
    {
        //конструктор 
        public Dog(string name, double age, double weight, string breed, string color, string lifeStory,
            string commands, bool claustrophobia = true) : base(name, age, weight, claustrophobia)
        {
            _breed = breed;
            _color = color;
            _lifeStory = lifeStory;
            _commands= commands;
        }
    }
}
