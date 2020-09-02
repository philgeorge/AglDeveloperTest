using AglDeveloperTest.InputModel;
using AglDeveloperTest.OutputModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AglDeveloperTest
{
    public class PeopleConverter
    {
        public Model Convert(People people)
        {
            if (people == null)
            {
                return null;
            }

            List<GenderAndCats> genders;
            if (people.Owners == null)
            {
                genders = new List<GenderAndCats>();
            }
            else
            {
                genders = people.Owners.Select(owner => new GenderAndCats
                {
                    Gender = owner.Gender.ToString(),
                    CatNames = MapPetsToCatNames(owner.Pets)
                }).ToList();
            }

            return new Model { Genders = genders };
        }

        private IList<string> MapPetsToCatNames(ICollection<Pet> pets)
        {
            if (pets == null)
            {
                return new List<string>();
            }
            return pets.Where(p=> IsCat(p.Type)).Select(p => p.Name).ToList();
        }

        private bool IsCat(string petType)
        {
            return string.Equals(petType, "cat", StringComparison.OrdinalIgnoreCase);
        }
    }
}
