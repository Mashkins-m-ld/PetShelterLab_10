using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Shelter 
    {
        //метод
        public Pet[] Filter(Type animalType, bool claustrophobia)
        {
            if (_pets == null || _pets.Length == 0) return null;

            Pet[] answer = new Pet[0];

            if (animalType == typeof(Pet))
            {
                
                for (int i = 0; i < _pets.Length; i++)
                {
                    if (_pets[i].Claustrophobia==claustrophobia) 
                    {
                        Array.Resize(ref answer, answer.Length + 1);
                        answer[answer.Length - 1] = _pets[i];
                        
                    }
                }
                return answer;
            }

            for (int i = 0; i < _pets.Length; i++)
            {
                if (_pets[i].GetType() == animalType && _pets[i].Claustrophobia == claustrophobia) //_pets[i] в любом случае какое-то конкретное животное
                //здесь только наследники учитываются
                {
                    Array.Resize(ref answer, answer.Length + 1);
                    answer[answer.Length - 1] = _pets[i];
                }
            }
            return answer;

        }
    }

    partial class Shelter : IChangeable
    {
        public void Add(Pet pet)
        {
            if (pet == null || _pets == null) return;

            //питомец не должен находится в двух приютах одновременно
            if (pet.Shelter != null) return; //у животного уже есть приют 

            //если места в приюте уже нет  или у животного клаустрофобиа, а открытого пространства в приюте нет
            if (_pets.Length == _capacity || (_openSpace == 0 && pet.Claustrophobia == true))
            {
                return;
            }

            pet.SetShelter(this);
            Array.Resize(ref _pets, _pets.Length + 1);
            _pets[_pets.Length - 1] = pet;

        }

        public void Add(Pet[] pets)
        {
            if (pets == null || _pets == null) return;

            for (int i = 0; i < pets.Length; i++)
            {
                if (_openSpace == 0 && pets[i].Claustrophobia == true) continue; //у животного уже есть приют 
                                                                                 //или у него клаустрофобия 
                if (pets[i].Shelter != null) continue;
                //если места в приюте уже нет 
                if (_pets.Length == _capacity) return;

                pets[i].SetShelter(this);
                Array.Resize(ref _pets, _pets.Length + 1);
                _pets[_pets.Length - 1] = pets[i];

            }
        }

        
        public static Shelter operator +(Shelter shelter, Pet pet)
        {
            shelter.Add(pet);
            return shelter;
        }
        public void PerformActionOnPet(Pet pet, Action<Pet> action)
        {
            //делегат может ссылаться на метод, который принимает один параметр типа Animal и ничего не возвращает.
            action(pet);
        }

        public void RemovePet(Pet pet)
        {
            if (pet == null || _pets == null) return;
            if (pet.Shelter != _name) return;//нет такого животного в приюте

            pet.RemoveShelter();

           
            Pet[] newPets = new Pet[0];
         
            foreach (Pet _pet in _pets)
            {
                if (_pet.Name == pet.Name) continue;
                Array.Resize(ref newPets, newPets.Length + 1);
                newPets[newPets.Length - 1]= _pet;
            }
            Array.Resize(ref _pets, _pets.Length - 1);

            for (int i = 0; i < newPets.Length; i++)
            {
                _pets[i]= newPets[i];
            }
        }
    }
}
