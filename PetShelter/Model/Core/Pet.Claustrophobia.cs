using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model
{
    public abstract partial class Pet  
    {
        //поля
        private bool _claustrophobia;

        //свойтсва 
        public bool Claustrophobia => _claustrophobia;

        //конструктор 
        public Pet(string name, double age, double weight,bool claustrophobia) : this (name,age,weight)
        {
            //проверка 

            _claustrophobia = claustrophobia;
        }

        //public Pet() { }
    }
}
