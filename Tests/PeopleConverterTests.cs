using System.Collections.Generic;
using System.Linq;
using AglDeveloperTest.InputModel;
using NUnit.Framework;

namespace AglDeveloperTest.Tests
{
    public class PeopleConverterTests
    {
        private PeopleConverter _pc = new PeopleConverter();

        [Test]
        public void NullPeople_ReturnsNull()
        {
            Assert.IsNull(_pc.Convert(null));
        }

        [Test]
        public void NoPeople_ReturnsEmptyGendersCollection()
        {
            var model = _pc.Convert(new People());
            AssertEmptyGendersCollection(model);
        }

        private static void AssertEmptyGendersCollection(OutputModel.Model model)
        {
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Genders);
            Assert.AreEqual(0, model.Genders.Count);
        }

        [Test]
        public void NoOwners_ReturnsEmptyGendersCollection()
        {
            var model = _pc.Convert(new People { Owners = new Owner[] { } });
            AssertEmptyGendersCollection(model);
        }

        [Test]
        public void OneOwnerNoPets_ReturnsGenderWithNoPets()
        {
            var input = new People { Owners = new[] { new Owner { Gender = Gender.Female } } };
            var model = _pc.Convert(input);
            Assert.AreEqual(1, model.Genders.Count);
            var gender = model.Genders.First();
            Assert.AreEqual("Female", gender.Gender);
            Assert.AreEqual(0, gender.CatNames.Count);
        }

        // There are more potential edge test cases (null properties etc.), but to save time I will not add everything possible now.
        // I would always add specific test cases to cover the code path if an exception occurred during developer testing, or if a bug was reported during testing.

        [Test]
        [TestCase("cat", 1, Description = "Lower case cat type")]
        [TestCase("Cat", 1, Description = "Upper case cat type")]
        [TestCase("cat", 3, Description = "Multiple lower case cats")]
        [TestCase("Cat", 3, Description = "Multiple upper case cats")]
        public void TwoOwnersOneWithPets_ReturnsOnlyCats(string catType, int catCount)
        {
            var pets = new List<Pet>
            {
                new Pet { Name = "Archie", Type = "Dog" },
                new Pet { Name = "Reggie", Type = "Lizard" }
            };
            var cats = Enumerable.Range(1, catCount).Select(i => new Pet { Name = $"Simon #{i}", Type = catType });
            pets.AddRange(cats);
            var input = new People
            {
                Owners = MakeOwnerList(pets)
            };

            var model = _pc.Convert(input);

            Assert.AreEqual(2, model.Genders.Count);
            var male = model.Genders.Single(g => g.Gender == "Male");
            Assert.AreEqual(catCount, male.CatNames.Count);
        }

        [Test]
        public void OwnersPets_ReturnCorrectNames()
        {
            const string cat1 = "Moggsy";
            const string cat2 = "Furball";
            var pets = new List<Pet>
            {
                new Pet { Name = cat1, Type = "Cat" },
                new Pet { Name = "Reggie", Type = "Lizard" },
                new Pet { Name = cat2, Type = "Cat" }
            };
            var input = new People
            {
                Owners = MakeOwnerList(pets)
            };

            var model = _pc.Convert(input);

            var male = model.Genders.Single(g => g.Gender == "Male");
            CollectionAssert.Contains(male.CatNames, cat1);
            CollectionAssert.Contains(male.CatNames, cat2);
        }

        private static List<Owner> MakeOwnerList(List<Pet> philsPets)
        {
            return new List<Owner>
            {
                new Owner
                {
                    Name = "Phillippa",
                    Gender = Gender.Female,
                    Age = 50
                },
                new Owner
                {
                    Name = "Phil",
                    Gender = Gender.Male,
                    Age = 40,
                    Pets = philsPets
                }
            };
        }

        [Test]
        public void MultipleOfSameGender_ReturnsCombinedGender()
        {
            var input = new People
            {
                Owners = new []
                {
                    new Owner { Gender = Gender.Female, Pets = new [] { new Pet { Name = "cat 1", Type = "cat" } } },
                    new Owner { Gender = Gender.Female, Pets = new [] { new Pet { Name = "cat 2", Type = "cat" } } },
                    new Owner { Gender = Gender.Female, Pets = new [] { new Pet { Name = "cat 3", Type = "cat" } } }
                }
            };

            var model = _pc.Convert(input);

            Assert.AreEqual(1, model.Genders.Count);
            var gender = model.Genders.First();
            Assert.AreEqual("Female", gender.Gender);
            Assert.AreEqual(3, gender.CatNames.Count);
        }

        [Test]
        public void CatNames_ReturnedInAlphabeticalOrder()
        {
            var input = new People
            {
                Owners = new []
                {
                    new Owner { Gender = Gender.Female, Pets = new [] { new Pet { Name = "b", Type = "cat" } } },
                    new Owner { Gender = Gender.Female, Pets = new [] { new Pet { Name = "d", Type = "cat" }, new Pet { Name = "a", Type = "cat" } } },
                    new Owner { Gender = Gender.Female, Pets = new [] { new Pet { Name = "c", Type = "cat" } } }
                }
            };

            var model = _pc.Convert(input);

            var female = model.Genders.First();
            CollectionAssert.IsOrdered(female.CatNames);
        }
    }
}
