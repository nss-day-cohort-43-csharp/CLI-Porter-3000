using System;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public SearchManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) Search Blogs");
            Console.WriteLine(" 2) Search Authors");
            Console.WriteLine(" 3) Search Posts");
            Console.WriteLine(" 4) Search All");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SearchBlogs();
                    return this;
                case "2":
                    SearchAuthors();
                    return this;
                case "3":
                    SearchPosts();
                    return this;
                case "4":
                    SearchAll();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void SearchAuthors()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Author> results = _tagRepository.SearchAuthors(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        public void SearchBlogs()
        {
            //pick a tag
            Console.Write("Tag> ");
            //reading user input
            string tagName = Console.ReadLine();
            //results = tagRepository.SearchBlogs
            SearchResults<Blog> results = _tagRepository.SearchBlogs(tagName);
            // if not tag is found then user see no results on tag name else they see the blog that is connected to the tag
            if(results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }   
        }

        public void SearchPosts()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();
            SearchResults<Post> results = _tagRepository.SearchPosts(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        public void SearchAll()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Author> resultsAuthor = _tagRepository.SearchAuthors(tagName);

            if (resultsAuthor.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                resultsAuthor.Display();
            }

            
            SearchResults<Blog> resultsBlog = _tagRepository.SearchBlogs(tagName);
            // if not tag is found then user see no results on tag name else they see the blog that is connected to the tag
            if (resultsBlog.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                resultsBlog.Display();
            }

            SearchResults<Post> resultsPost = _tagRepository.SearchPosts(tagName);

            if (resultsPost.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                resultsPost.Display();
            }

        }
    }
}