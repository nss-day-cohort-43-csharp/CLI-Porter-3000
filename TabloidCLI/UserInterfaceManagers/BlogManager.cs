﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine("1) List Blogs");
            Console.WriteLine("2) Blog Details");
            Console.WriteLine("3) Add Blog");
            Console.WriteLine("4) Edit Blog");
            Console.WriteLine("5) Remove Blog");
            Console.WriteLine("0) Go Back");

            Console.Write(">");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Blog blog = Choose();
                    if(blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
;                    }
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
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
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }

        private Blog Choose(string prompt = null)
        {
            // if they don't enter a number it prompts the user to choose a blog
            if(prompt == null)
            {
                prompt = "Please choose a blog:";
            }
            //prompt a user to choose a blog
            Console.WriteLine(prompt);
            //gets all the blogs and iterate through the list of blogs
            List<Blog> blogs = _blogRepository.GetAll();
            for(int i = 0; i < blogs.Count; i++)
            {
                //blog is equal to the index number 
                Blog blog = blogs[i];
                //displays the blog title
                Console.WriteLine($" {i + 1} {blog.Title}");
            }
            Console.Write(">");
            string input = Console.ReadLine();
            //if they don't enter a number from the the list then it will say invalid selection and go back to blog menu
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
            {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.WriteLine("Title: ");
            blog.Title = Console.ReadLine();

            Console.WriteLine("Url: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);

        }

        private void Edit()
            {
            Blog blogToEdit = Choose("Which blog would you like to edit?");
            if(blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.WriteLine("New Url (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if(!String.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }
            _blogRepository.Update(blogToEdit);
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("Which Blog would you like to delete?");
            if(blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }
    }
}
