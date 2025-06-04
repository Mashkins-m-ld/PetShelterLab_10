using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    public abstract partial class Pet //ну логично его сделать абстрактным 
    {
        //поля

        //кличка 
        private string _name;
        //возраст 
        private double _age;
        //вес 
        private double _weight;

        //ссылка на текущий приют 
        private Shelter _shelter;

        //свойства 
        public string Name => _name;
        public double Age => _age;
        public double Weight => _weight;

        public string Shelter
        {
            get
            {
                if (_shelter == null) return null;
                return _shelter.Name;
            }
        }

        public abstract string Type { get; }

        //конструктор 
        public Pet(string name, double age, double weight)
        {
            //проверка 

            _name = name;
            _age = age;
            _weight = weight;


        }

        //методы 
        //установить приют для питомца
        public void SetShelter(Shelter shelter)
        {
            if (_shelter != null || shelter==null) return; //уже находится в приюте

            _shelter = shelter;
        }

        public void RemoveShelter()
        {
            if (_shelter == null ) //нечего удалять
            {
                _shelter=null;
            }

        }

    }
}
