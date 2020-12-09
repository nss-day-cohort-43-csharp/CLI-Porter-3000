using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
   internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _postId;
        private string tag;

        public PostDetailManager (IUserInterfaceManager parentUI, string connectionString,  int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postId = postId;
            
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine("1) View");
            Console.WriteLine("2) Add Tag");
            Console.WriteLine("3) Remove Tag");
            Console.WriteLine("4) Note Management");
            Console.WriteLine("0) Go Back");

            Console.WriteLine(">");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
<<<<<<< HEAD
                //case "2":
                //    AddTag();
                //    return this;
                //case "3":
                //    RemoveTag();
                //    return this;
=======
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
>>>>>>> 6cafcdcd217b6be959896d7abea2350816ea80aa
                //case "4":
                //    NoteManagement();
                //return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

<<<<<<< HEAD
=======
        private void AddTag()
        {
          
            Post post = _postRepository.Get(_postId);
           
            Console.WriteLine($"Which tag would you like to add to {post.Title}?");
            
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write(">");

            string input = Console.ReadLine();         
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.InsertTag(post, tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid SELECTION. Won't add any tags.");
            }
        }

>>>>>>> 6cafcdcd217b6be959896d7abea2350816ea80aa
        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Url: {post.Url}");
            Console.WriteLine($"PublishDateTime: {post.PublishDateTime}");
            Console.WriteLine("Tags: ");
<<<<<<< HEAD
            foreach (Tag tag in post.Tags)
=======
            foreach (Tag tag in post.tags)
>>>>>>> 6cafcdcd217b6be959896d7abea2350816ea80aa
            {
                Console.WriteLine(" " + tag);
            }
            Console.WriteLine();
        }
<<<<<<< HEAD
=======
        private void RemoveTag()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Which tag would you like to remove from {post.Title}?");
            List<Tag> tags = post.tags;
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.DeleteTag(post.Id, tag.Id);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection. Won't remove any tags.");
            }
        }
>>>>>>> 6cafcdcd217b6be959896d7abea2350816ea80aa
    }    
}
