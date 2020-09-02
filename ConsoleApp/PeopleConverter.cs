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
                genders = people.Owners.GroupBy(o => o.Gender).Select(group => new GenderAndCats
                {
                    Gender = group.Key.ToString(),
                    CatNames = MapPetsToCatNames(group.SelectMany(o => o.Pets ?? new Pet[0])).OrderBy(c => c).ToList()
                }).ToList();
            }

            return new Model { Genders = genders };
        }

        private IEnumerable<string> MapPetsToCatNames(IEnumerable<Pet> pets)
        {
            if (pets == null)
            {
                return new List<string>();
            }
            return pets.Where(p=> IsCat(p.Type)).Select(p => p.Name);
        }

        private bool IsCat(string petType)
        {
            return string.Equals(petType, "cat", StringComparison.OrdinalIgnoreCase);
        }
    }
}
