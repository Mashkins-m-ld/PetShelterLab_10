using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Model
{
    public partial class Rabbit : Pet
    {
        //конструктор 
        public Rabbit(string name, double age, double weight, string breed, string color, string lifeStory,
            bool claustrophobia = false) : base(name, age, weight, claustrophobia)
        {
            _breed = breed;
            _color = color;
            _lifeStory = lifeStory;
           
        }
    }
}
