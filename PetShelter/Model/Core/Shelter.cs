using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Shelter : ICountable, IFilter
    {
        //поля
        private string _name; //название
        private int _capacity;//вместимость 
        private double _openSpace; //открытая территория 

        private Pet[] _pets;//будем хранить всех животных

        //свойства 
        public string Name => _name;
        public int Capacity => _capacity;
        public double OpenSpace => _openSpace;

        public Pet[] Pets => _pets;

        //конструктор 
        public Shelter(string name, int capacity, double openSpace)
        {
            //проверка на имя и вместимость 
            if (name == null || capacity <= 0 || openSpace < 0) return;

            _name = name;
            _capacity = capacity;
            _openSpace = openSpace;

            _pets = new Pet[0];
        }

        //public Shelter() { }

        //методы, реализация интерфейсов 
        public int Count()
        {
            if (_pets == null) return 0;

            return _pets.Length;
        }

        public int Count(Type animalType)
        {
            if (_pets == null) return 0;

            if (animalType == typeof(Pet)) return Count(); //если все животные

            //дается какой-то анимал тайп в виде typeof(...)
            int count = 0;
            for (int i = 0; i < _pets.Length; i++)
            {
                if (_pets[i].GetType() == animalType) //_pets[i] в любом случае какое-то конкретное животное
                                                      //здесь только наследники учитываются
                {
                    count++;
                }
            }
            return count;
        }

        public int Percentage(Type animalType)
        {
            if (_pets == null || _pets.Length == 0) return 0;

            double answer = (double)Count(animalType) / Count();
            answer = Math.Round(answer, 2) * 100;
            return (int)answer;
        }

        public Pet[] Filter(Type animalType)
        {
            //MessageBox.Show($"_pets.Length={_pets.Length}");
            if (_pets == null || _pets.Length == 0) return null;

            if (animalType == typeof(Pet))
            {
                Pet[] pets = new Pet[_pets.Length];
                Array.Copy(_pets, pets, _pets.Length);
                return pets;
            }

            Pet[] answer = new Pet[0];
            for (int i = 0; i < _pets.Length; i++)
            {
                if (_pets[i].GetType() == animalType) //_pets[i] в любом случае какое-то конкретное животное
                //здесь только наследники учитываются
                {
                    Array.Resize(ref answer, answer.Length + 1);
                    answer[answer.Length - 1] = _pets[i];
                }
            }
            return answer;
        }

        

    }

}


    
