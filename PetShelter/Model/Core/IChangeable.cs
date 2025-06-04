using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IChangeable
    {
        void Add(Pet pet);
        void Add(Pet[] pet);
        void RemovePet(Pet pet);
    }
}
