using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.Core
{
    [XmlInclude(typeof(CatWrapper))]
    [XmlInclude(typeof(DogWrapper))]
    [XmlInclude(typeof(RabbitWrapper))]
    public abstract class PetWrapper
    {
        // Публичные свойства с геттерами и сеттерами
        public string Name { get; set; }
        public double Age { get; set; }
        public double Weight { get; set; } // Новое свойство

        public abstract string Type { get; set; }

        public string Shelter { get; set; }

        public bool Claustrophobia { get; set; }

        // Пустой конструктор для XmlSerializer
        public PetWrapper() { }

        // Конструктор для создания обертки из оригинального объекта
        public PetWrapper(Pet pet)
        {
            Name = pet.Name;
            Age = pet.Age;
            Weight = pet.Weight; // Копируем значение нового свойства

            Shelter = pet.Shelter;
            Claustrophobia = pet.Claustrophobia;

        }

        public class CatWrapper : PetWrapper
        {
            // Публичные свойства с геттерами и сеттерами

            public string Breed { get; set; }
            public string Color { get; set; }
            public string LifeStory { get; set; }
            public bool Toilet { get; set; }


            public override string Type { get; set; }



            // Пустой конструктор для XML-сериализации
            public CatWrapper() { }

            // Конструктор для создания обертки из оригинального объекта Cat
            public CatWrapper(Cat cat)
            {
                Name = cat.Name;
                Age = cat.Age;
                Weight = cat.Weight;
                Breed = cat.Breed;
                Color = cat.Color;
                LifeStory = cat.LifeStory;
                Toilet = cat.Toilet;
                Shelter = cat.Shelter;
                Type = cat.Type;
                Claustrophobia = cat.Claustrophobia;
            }

            // Метод для преобразования обертки обратно в оригинальный объект Cat
            public Cat ToCat()
            {
                return new Cat(Name, Age, Weight, Breed, Color, LifeStory, Toilet, Claustrophobia);
            }
        }

        public class DogWrapper : PetWrapper
        {
            // Публичные свойства с геттерами и сеттерами

            public string Breed { get; set; }
            public string Color { get; set; }
            public string LifeStory { get; set; }
            public string Commands { get; set; }

            public override string Type { get; set; }

            // Пустой конструктор для XML-сериализации
            public DogWrapper() { }

            // Конструктор для создания обертки из оригинального объекта Dog
            public DogWrapper(Dog dog)
            {
                Name = dog.Name;
                Age = dog.Age;
                Weight = dog.Weight;
                Breed = dog.Breed;
                Color = dog.Color;
                LifeStory = dog.LifeStory;
                Commands = dog.Commands;
                Shelter = dog.Shelter;
                Type = dog.Type;
                Claustrophobia = dog.Claustrophobia;
            }

            // Метод для преобразования обертки обратно в оригинальный объект Dog
            public Dog ToDog()
            {
                return new Dog(Name, Age, Weight, Breed, Color, LifeStory, Commands, Claustrophobia);
            }
        }
        public class RabbitWrapper : PetWrapper
        {
            // Публичные свойства с геттерами и сеттерами

            public string Breed { get; set; }
            public string Color { get; set; }
            public string LifeStory { get; set; }

            public override string Type { get; set; }

            // Пустой конструктор для XML-сериализации
            public RabbitWrapper() { }

            // Конструктор для создания обертки из оригинального объекта 
            public RabbitWrapper(Rabbit rabbit)
            {
                Name = rabbit.Name;
                Age = rabbit.Age;
                Weight = rabbit.Weight;
                Breed = rabbit.Breed;
                Color = rabbit.Color;
                LifeStory = rabbit.LifeStory;
                Shelter = rabbit.Shelter;
                Type = rabbit.Type;
                Claustrophobia = rabbit.Claustrophobia;
            }

            // Метод для преобразования обертки обратно в оригинальный объект 
            public Rabbit toRabbit()
            {
                return new Rabbit(Name, Age, Weight, Breed, Color, LifeStory, Claustrophobia);
            }
        }

        public class ShelterWrapper
        {
            // Свойства для обертки
            //[XmlElement("Name")]
            public string Name { get; set; }

            //[XmlElement("Capacity")]
            public int Capacity { get; set; }

            //[XmlElement("OpenSpace")]
            public double OpenSpace { get; set; }


            // Конструктор по умолчанию для сериализации
            public ShelterWrapper() { }

            // Конструктор для создания обертки из объекта Shelter
            public ShelterWrapper(Shelter shelter)
            {

                Name = shelter.Name;
                Capacity = shelter.Capacity;
                OpenSpace = shelter.OpenSpace;

            }
            // Метод для преобразования обертки обратно в объект Shelter
            //public Shelter ToShelter()
            //{
            //    return new Shelter{Name, Capacity, OpenSpace};
                   
            //}

        }
    }
}
