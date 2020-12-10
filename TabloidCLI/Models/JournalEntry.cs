﻿using System;


namespace TabloidCLI.Models
{

    class JournalEntry
    {

        public int Id { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }


        public DateTime CreateDateTime { get; set; }
    }
}