using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    class JournalEntryManager : IUserInterfaceManager
    {
        
        private readonly IUserInterfaceManager _parentUI;
        private JournalEntryRepository _journalRepository;
        private string _connectionString;

        public JournalEntryManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalEntryRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
            {
            
            
            Console.WriteLine("\nJournal Menu");
                Console.WriteLine(" 1) List Entries");
                Console.WriteLine(" 2) Add Entry");
                Console.WriteLine(" 3) Edit Entry");
                Console.WriteLine(" 4) Remove Entry");
                Console.WriteLine(" 0) Go Back");

                Console.Write("> ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                case "1":
                    List();
                    return this;
                
                case "2":
                        Add();
                        return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                        return _parentUI;
                    default:
                        Console.WriteLine("Invalid Selection");
                        return this;


                    //case "2":
                    //    JournalEntry entry = Choose();
                    //    if (entry == null)
                    //    {
                    //        return this;
                    //    }
                    //    else
                    //    {
                    //        return new AuthorDetailManager(this, _connectionString, author.Id);
                    //    }
            }
        }

        private void List()
        {
            List<JournalEntry> entries = _journalRepository.GetAll();
            foreach (JournalEntry entry in entries)
            {
                Console.WriteLine($"\nEntry Title:  {entry.Title}\nEntry Content:  {entry.Content}\nEntry Created:  {entry.CreateDateTime} \n-----------------------");
            }
        }

        private JournalEntry Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an entry:";
            }

            Console.WriteLine(prompt);

            List<JournalEntry> entries = _journalRepository.GetAll();

            for (int i = 0; i < entries.Count; i++)
            {
                JournalEntry entry = entries[i];
                Console.WriteLine($" {i + 1}) {entry.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int selection = int.Parse(input);
                return entries[selection - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
            {
                Console.WriteLine("New Entry");
                JournalEntry entry = new JournalEntry();

                Console.Write("Entry Title:  ");
                entry.Title = Console.ReadLine();

                Console.Write("Entry Content:  ");
                entry.Content = Console.ReadLine();

                entry.CreateDateTime = DateTime.Now;

                _journalRepository.Insert(entry);
            }
        private void Remove()
        {
            JournalEntry entryToDelete = Choose("Which entry would you like to remove?");
            if (entryToDelete != null)
            {
                _journalRepository.Delete(entryToDelete.Id);
            }
        }

        private void Edit()
        {
            JournalEntry entryToEdit = Choose("Which author would you like to edit?");
            if (entryToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string editedTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(editedTitle))
            {
                entryToEdit.Title = editedTitle;
            }
            Console.Write("New Content (blank to leave unchanged: ");
            string editedContent = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(editedContent))
            {
                entryToEdit.Content = editedContent;
            }
            
            _journalRepository.Update(entryToEdit);
        }
    }
}