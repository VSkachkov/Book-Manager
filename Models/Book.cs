﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri Uri { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

    }
}
