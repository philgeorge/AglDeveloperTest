using System.Collections.Generic;
using System.Linq;
using AglDeveloperTest.InputModel;
using NUnit.Framework;

namespace AglDeveloperTest.Tests
{
    [TestFixture]
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
            var lastGender = model.Genders.Last();
            Assert.AreEqual("Male", lastGender.Gender);
            Assert.AreEqual(catCount, lastGender.CatNames.Count);
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

            var lastGender = model.Genders.Last();
            var catNames = lastGender.CatNames.ToList();
            Assert.AreEqual(2, catNames.Count);
            Assert.AreEqual(cat1, catNames[0]);
            Assert.AreEqual(cat2, catNames[1]);
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
    }
}
