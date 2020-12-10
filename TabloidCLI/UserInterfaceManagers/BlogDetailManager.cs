using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{


    internal class BlogDetailManager : IUserInterfaceManager
    {

        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private TagRepository _tagRepository;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private int _blogId;


        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogId = blogId;
        }


        public IUserInterfaceManager Execute()
        {

            Blog blog = _blogRepository.Get(_blogId);


            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine("1) View");
            Console.WriteLine("2) Add Tag");
            Console.WriteLine("3) Remove Tag");
            Console.WriteLine("4) View Blog's Posts");
            Console.WriteLine("5) Clear Console");
            Console.WriteLine("0) Go Back");
            Console.WriteLine(">");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    View();
                    return this;


                case "2":
                    AddTag();
                    return this;


                case "3":
                    RemoveTag();
                    return this;


                case "4":
                    ViewBlogsPosts();
                    return this;


                case "5":
                    Console.Clear();
                    return this;


                case "0":
                    return _parentUI;


                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }


        private void View()
        {
            // getting the blog by id
            Blog blog = _blogRepository.Get(_blogId);

            //displaying the title, url, and tag
            Console.WriteLine($"Title: {blog.Title}");
            Console.WriteLine($"Url: {blog.Url}");
            Console.WriteLine("Tags:");

            //looping throught to get all tag associated with this blog
            foreach (Tag tag in blog.Tags)
            {
                Console.WriteLine(" " + tag);
            }
            Console.WriteLine();
        }


        private void AddTag()
        {
            //getting the blog by id
            Blog blog = _blogRepository.Get(_blogId);

            //asking which tag they want to add
            Console.WriteLine($"Which tag would you like to add to {blog.Title}?");

            //getting a list of all the tags and iterate through the list of tags
            List<Tag> tags = _tagRepository.GetAll();


            for (int i = 0; i < tags.Count; i++)
            {
                //tag is equal to the index number 
                Tag tag = tags[i];
                //displaying the tag 
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write(">");
            string input = Console.ReadLine();


            // inserting the tag into the blog, If they don't pick a tag then we say invalid selection and no tag is added and it takes you back to the blog detial menu
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _blogRepository.InsertTag(blog, tag);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid SELECTION. Won't add any tags.");
            }
        }


        public void RemoveTag()
        {
            // getting the blog by id
            Blog blog = _blogRepository.Get(_blogId);

            //ask the user which blog tag they want to remove
            Console.WriteLine($"Which tag would you like to remove from {blog.Title}?");

            //getting the list of tags an itrating through the list
            List<Tag> tags = blog.Tags;


            for (int i = 0; i < tags.Count; i++)
            {
                //tag equal the tag index
                Tag tag = tags[i];
                //displaying the tags that can be deleted
                Console.WriteLine($"{i + 1} {tag.Name}");
            }
            Console.WriteLine(">");
            string input = Console.ReadLine();

            //if the user inputs a number then the tag is deleted if not its invaild and the user is taken back to the detail blog menu
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _blogRepository.DeleteTag(blog.Id, tag.Id);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection. Won't be remove any tags");
            }
        }


        public void ViewBlogsPosts()
        {

            List<Post> posts = _postRepository.GetByBlog(_blogId);


            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
            Console.WriteLine();
        }
    }
}