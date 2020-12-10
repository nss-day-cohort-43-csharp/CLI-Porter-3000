using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    class JournalEntryRepository : DatabaseConnector, IRepository<JournalEntry>
    {

        public JournalEntryRepository(string connectionString) : base(connectionString) { }

        public List<JournalEntry> GetAll()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, Title, Content, CreateDateTime
                                        FROM Journal";

                    List<JournalEntry> journalEntries = new List<JournalEntry>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        JournalEntry entry = new JournalEntry()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };

                        journalEntries.Add(entry);
                    }

                    reader.Close();

                    return journalEntries;
                }
            }
        }

        public JournalEntry Get(int id)
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"SELECT Id, Title, Content, CreateDateTime,
                                        FROM Journal";
                    cmd.Parameters.AddWithValue("@id", id);

                    JournalEntry entry = null;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        if (entry == null)
                        {

                            entry = new JournalEntry()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            };
                        }
                    }

                    reader.Close();

                    return entry;
                }
            }
        }

        public void Insert(JournalEntry entry)
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                        VALUES (@Title, @Content, @CreateDateTime)";
                    cmd.Parameters.AddWithValue("@Title", entry.Title);
                    cmd.Parameters.AddWithValue("@Content", entry.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", entry.CreateDateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(JournalEntry entry)
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"UPDATE Journal 
                                        SET Title = @editedTitle, Content = @editedContent
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@editedTitle", entry.Title);
                    cmd.Parameters.AddWithValue("@editedContent", entry.Content);
                    cmd.Parameters.AddWithValue("@id", entry.Id);

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
                    cmd.CommandText = @"DELETE FROM Journal WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}