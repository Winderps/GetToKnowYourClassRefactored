using System;
using System.Collections.Generic;
using System.Text;

namespace GetToKnowYourClass2
{
    class StudentInfo
    {
        private string _name;
        private string _hometown;
        private string _favoriteFood;
        private string _favoriteBand;
        public string Name { get { return _name; } }
        public string Hometown { get { return _hometown; } }
        public string FavoriteFood { get { return _favoriteFood; } }
        public string FavoriteBand { get { return _favoriteBand; } }
        
        
        public StudentInfo(string name, string hometown, string food, string band)
        {
            _name = name;
            _hometown = hometown;
            _favoriteFood = food;
            _favoriteBand = band;
        }
    }
}
