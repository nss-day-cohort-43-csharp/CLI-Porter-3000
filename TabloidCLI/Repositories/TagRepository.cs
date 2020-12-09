using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    public class TagRepository : DatabaseConnector, IRepository<Tag>
    {
        public TagRepository(string connectionString) : base(connectionString) { }

        public List<Tag> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Name FROM Tag";
                    List<Tag> tags = new List<Tag>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tag tag = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                        tags.Add(tag);
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        public Tag Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Tag (Name)
                                    VALUES (@Name)";
                    cmd.Parameters.AddWithValue("@Name", tag.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Tag
                                        SET Name = @editedName
                                        WHERE id = @id";

                    cmd.Parameters.AddWithValue("@editedName", tag.Name);
                    cmd.Parameters.AddWithValue("@id", tag.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            { 
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM AuthorTag WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("@TagId", id);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BlogTag WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("@TagId", id);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostTag WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("@TagId", id);
                    cmd.ExecuteNonQuery();
                }
                
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Tag WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public SearchResults<Author> SearchAuthors(string tagName)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT a.id,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio
                                          FROM Author a
                                               LEFT JOIN AuthorTag at on a.Id = at.AuthorId
                                               LEFT JOIN Tag t on t.Id = at.TagId
                                         WHERE t.Name LIKE @name";
                    cmd.Parameters.AddWithValue("@name", $"%{tagName}%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    SearchResults<Author> results = new SearchResults<Author>();
                    while (reader.Read())
                    {
                        Author author = new Author()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Bio = reader.GetString(reader.GetOrdinal("Bio")),
                        };
                        results.Add(author);
                    }

                    reader.Close();

                    return results;
                }
            }
        }

        public SearchResults<Blog> SearchBlogs(string tagName)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Selecting the blog and join the blogtag and tag. Where tag.name = name
                    cmd.CommandText = @"SELECT blog.Id,
                                               blog.Title,
                                               blog.Url
                                          FROM Blog blog
                                               LEFT JOIN BlogTag blogTag on blog.Id = blogTag.BlogId
                                               LEFT JOIN Tag tag on tag.Id = blogTag.TagId
                                         WHERE tag.Name LIKE @name";
                    cmd.Parameters.AddWithValue("@name", $"%{tagName}%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    //creating a list for the results
                    SearchResults<Blog> results = new SearchResults<Blog>();
                    while (reader.Read())
                    {
                        //getting the blogs 
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")), 
                       };
                        //adding the blogs
                        results.Add(blog);
                    }

                    reader.Close();
                    //return the results
                    return results;
                }
            }
        }


        public SearchResults<Post> SearchPosts(string tagName)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                   
                    cmd.CommandText = @"SELECT post.Id,
                                               post.Title,
                                               post.Url,
                                               post.publishDateTime,
                                               post.AuthorId,
                                               post.BlogId
                                          FROM Post Post
                                               LEFT JOIN Author on post.AuthorId = Author.Id
                                               LEFT  JOIN Blog on post.BlogId = Blog.Id
                                               LEFT JOIN PostTag postTag on post.Id = postTag.PostId
                                               LEFT JOIN Tag tag on tag.Id = PostTag.TagId
                                         WHERE tag.Name LIKE @name";
                    cmd.Parameters.AddWithValue("@name", $"%{tagName}%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    SearchResults<Post> results = new SearchResults<Post>();
                    while (reader.Read())
                    {
                        Post post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),
                            PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                        };
                            Author author = new Author()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("AuthorId"))
                            };
                            Blog blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId"))
                            };
                        results.Add(post);
                    };

                    reader.Close();
                    return results;

                }
                 
                }
            }
        }

    }

