﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MSPLabWork.Models
{
    public class MovieInfo
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public string Year { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }
        
        public string Production { get; set; }

        public string Released { get; set; }

        public string Runtime { get; set; }

        public string Awards { get; set; }

        public string imdbRating { get; set; }

        public string Plot { get; set; }

        public string Poster { get; set; }

        [PrimaryKey]
        public string ImdbID { get; set; }
    }
}
