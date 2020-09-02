using System;
using System.Collections.Generic;
using System.Text;

namespace AglDeveloperTest.InputModel
{
    // It's not necessarily a good choice to model Gender as an enum, since new string values could appear in the JSON without notice.
    // However, if you have gaurantees about the set of permitted values, then it can help with the correctness and readability of your code.
    public enum Gender { Male, Female }

    public class Owner
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }

    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class People
    {
        public ICollection<Owner> Owners { get; set; }
    }
}
