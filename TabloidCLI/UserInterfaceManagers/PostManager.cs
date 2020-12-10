using System;
using System.Collections.Generic;
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
            Console.WriteLine(" 5) Post Detail");
            Console.WriteLine(" 6) Clear Console"); 
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


                case "3":
                    Edit();
                    return this;


                case "4":
                    Delete();
                    return this;


                case "5":

                    Post post = Choose();


                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }


                case "6":
                    Console.Clear();
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

            while(true)
            {
                try
                {
                    Console.Write("Publication Date: ");
                    post.PublishDateTime = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("That's an incorrect date format. Please enter the date in this format:  MM/DD/YYYY");
                }
            }

            AuthorRepository authorRepo = new AuthorRepository(_connectionString);


            List<Author> authors = authorRepo.GetAll();


            while (true)
            {

                Console.WriteLine("Choose an Author");


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


        private void Edit()
        {

            Post postToEdit = Choose("Which post would you like to edit?");


            if (postToEdit == null)
            {
                return;
            }
            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();


            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New Url (blank to leave unchanged: ");
            string url = Console.ReadLine();


            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }


            while (true)
            {
                Console.Write("New Publication Date (blank to leave unchanged: ");


                try
                {
                    string publishDate = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(publishDate))
                    {
                        break;
                    }


                    DateTime updatedDate = Convert.ToDateTime(publishDate);


                    if (updatedDate < DateTime.Now)
                    {
                        postToEdit.PublishDateTime = updatedDate;
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Date");
                }
            }


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


                if (string.IsNullOrWhiteSpace(answer))
                {
                    break;
                }


                try
                {
                    int choice = int.Parse(answer);
                    postToEdit.Author = authors[choice - 1];
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


                if (string.IsNullOrWhiteSpace(answer))
                {
                    break;
                }


                try
                {
                    int choice = int.Parse(answer);
                    postToEdit.Blog = blogs[choice - 1];
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid Selection");
                }
            }


            if (postToEdit != null)
            {
                _postRepository.Update(postToEdit);
            }
        }


        private void Delete()
        {

            Post postToDelete = Choose("Which post would you like to remove?");


            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }


        private Post Choose(string prompt = null)
        {

            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }
            Console.WriteLine(prompt);


            List<Post> posts = _postRepository.GetAll();


            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();


            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
    }
}