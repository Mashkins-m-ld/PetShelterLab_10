using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IFilter
    {
        //метод для фильтрации по виду животных
        Pet[] Filter(Type animalType);


    }
}
