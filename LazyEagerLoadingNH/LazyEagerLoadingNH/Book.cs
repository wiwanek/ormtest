﻿using System;


namespace FluentNHTest
{
    public class Book
    {
        public virtual Guid BookId { get; set; }
        public virtual string Title { get; set; }

        public virtual Author Author { get; set; }
    }
}
