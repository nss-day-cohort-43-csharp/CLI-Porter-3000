using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {  
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
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
            }
        }

        private void List()
        {
            List<Tag> newTag = _tagRepository.GetAll();
            foreach (Tag tag in newTag)
            {
                Console.WriteLine($"Tag Name:  {tag.Name}");
            }
        }

        private void Add()
        {
            Console.WriteLine("New Tag");
            Tag newTag = new Tag();

            Console.Write("Tag Name:  ");
            newTag.Name = Console.ReadLine();

            _tagRepository.Insert(newTag);
        }

        private void Edit()
        {
            Tag tagToEdit = Choose("Which tag would you like to edit?");
            if(tagToEdit == null)
            {
                return;
            }

            Console.Write("New Tag Name (blank to leave unchanged: ");
            string newTag = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(newTag))
            {
                tagToEdit.Name = newTag;
            }

            _tagRepository.Update(tagToEdit);
        }

        private Tag Choose(string prompt = null)
        {
            if(prompt == null)
            {
                prompt = "Please choose a tag:  ";
            }

            Console.WriteLine(prompt);

            List <Tag> tags = _tagRepository.GetAll();

            for(int i = 0; i < tags.Count; i++)
            {
                Tag tagEdit = tags[i];
                Console.WriteLine($"{i + 1} {tagEdit.Name}");
            }
            Console.Write(">");

            string input = Console.ReadLine();

            try
            {
                int selection = int.Parse(input);
                return tags[selection - 1];
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
