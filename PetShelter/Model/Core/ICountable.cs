using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ICountable
    {
        //интерфейс содержащий перегруженные методы
        //интерфейс содержит только свойства, методы и там что-то сложное (полей нет)
        //не содержит реализации 

        int Count(); //число животных
        //через Type получаем метаДанные о классе
        int Count(Type animalType);//конкретного типа
        int Percentage(Type animalType); //процент от общего числа


    }
}
