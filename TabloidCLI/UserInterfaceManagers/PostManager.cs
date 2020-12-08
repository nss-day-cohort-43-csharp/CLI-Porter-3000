using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    GetAll();
                    return this;
                case "2":
                    Insert();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }
        private void GetAll()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private void Insert()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();

            Console.Write("Publication Date: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            AuthorRepository authorRepo = new AuthorRepository(_connectionString);
            List<Author> authors = authorRepo.GetAll();
            while (true)
            {
                Console.WriteLine("Chose an Author");
                for (int index = 0; index < authors.Count; index++)
                {
                    Author author = authors[index];
                    Console.WriteLine($"{index + 1})  {author.FirstName}");

                }
                Console.Write(": ");
                string answer = Console.ReadLine();
                try
                {
                    int choice = int.Parse(answer);
                    post.Author = authors[choice - 1];
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid Selection");
                }
            }

            BlogRepository blogRepo = new BlogRepository(_connectionString);
            List<Blog> blogs = blogRepo.GetAll();

            while (true)
            {
                Console.WriteLine("Chose a Blog");
                for (int index = 0; index < authors.Count; index++)
                {
                    Blog blog = blogs[index];
                    Console.WriteLine($"{index + 1})  {blog.Title}");

                }
                Console.Write(": ");
                string answer = Console.ReadLine();
                try
                {
                    int choice = int.Parse(answer);
                    post.Blog = blogs[choice - 1];
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid Selection");
                }
            }

            _postRepository.Insert(post);
        }

    }
}

