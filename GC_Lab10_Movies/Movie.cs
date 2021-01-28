using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Lab10_Movies
{
    public partial class Movie
    {
        private string _title;
        private string _catergory;

        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string Catergory
        {
            get
            {
                return _catergory;
            }
        }

        public Movie(string Title, string Catergory)
        {
            _title = Title;
            _catergory = Catergory;
        }
    }
}
