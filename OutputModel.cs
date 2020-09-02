using System.Collections.Generic;

namespace AglDeveloperTest.OutputModel
{
    public class GenderAndCats
    {
        public string Gender { get; set; }
        public ICollection<string> CatNames { get; set; }
    }

    public class Model
    {
        public ICollection<GenderAndCats> Genders {get;set;}
    }
}
