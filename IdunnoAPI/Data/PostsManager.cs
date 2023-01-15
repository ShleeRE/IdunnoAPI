﻿using IdunnoAPI.Extensions;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using MySqlConnector;
using System.Reflection.Metadata.Ecma335;

namespace IdunnoAPI.Data
{
    public class PostsManager
    {
        private readonly MySqlDbContext _context;

        public PostsManager(MySqlDbContext context)
        {
            _context = context;
        }


        public async Task<int?> GetNextPostIdAsync()
        {
            int? retID = null;

            try
            {
                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();
                cmd.CommandText = $"USE idunnodb; " +
                    $"SELECT COUNT(*) " +
                    $"FROM Posts;";

                await using MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                await reader.ReadAsync();

                retID = reader.GetInt32(0);

                await _context.conn.CloseAsync();

                return retID + 1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary> postID's are signed and cannot be null -> query will be modified if this is any different
        public async Task<List<Post>> GetPostsAsync(int? postID = null) 
        {
            List<Post> posts = new List<Post>();
            Post temp = new Post();

            try
            {
                await _context.conn.OpenAsync();

                using MySqlCommand cmd = _context.conn.CreateCommand();
                cmd.CommandText = "USE idunnodb; " +
                    "SELECT PostID, UserID, date_format(PostDate, '%Y-%m-%e %H:%i') as PostDate, PostTitle, PostDescription, ImagePath " +
                    "FROM Posts";

                if(postID != null)
                {
                    cmd.CommandText += $" WHERE PostID = '{postID}'";
                }

                cmd.CommandText += ";"; 

                await using MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    temp.PostID = (int)reader[0];
                    temp.UserID = (int)reader[1];
                    temp.PostDate = reader[2].ToString();
                    temp.PostTitle = reader[3].ToString();
                    temp.PostDescription = reader[4].ToString();
                    temp.ImagePath = reader[5].ToString();

                    posts.Add(temp);

                    temp = new Post();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            return posts;
        }

        public async Task<ValidationResult> AddPostAsync(Post toBeAdded)
        {
            ValidationResult ret = new ValidationResult();

            try
            {
                int? nextPostID = await GetNextPostIdAsync();

                if(nextPostID == null)
                {
                    return ret.RetInternelServerError();
                }

                toBeAdded.PostID = (int)nextPostID;              // needed so we can deliver resource URI

                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();
                cmd.CommandText = $"USE idunnodb; " +
                    $"INSERT INTO Posts " +
                    $"VALUES ({nextPostID}, {toBeAdded.UserID}, '{toBeAdded.PostDate}', '{toBeAdded.PostTitle}', '{toBeAdded.PostDescription}', '{toBeAdded.ImagePath}');";

                if(await cmd.ExecuteNonQueryAsync() == -1)
                {
                    return ret.RetInternelServerError();
                }

                await _context.conn.CloseAsync();
            }
            catch(Exception ex)
            {
                return ret.RetInternelServerError();
            }

            return ret.FormatReturn(true, "New post successfuly added!", StatusCodes.Status201Created);
        }
        public async Task<ValidationResult> DeletePostAsync(int toBeDeleted)
        {
            ValidationResult ret = new ValidationResult();

            try
            {
                if((await GetPostsAsync(toBeDeleted)).Count == 0) // if post not found return error
                {
                    return ret.FormatReturn(false, "Post not found!", StatusCodes.Status404NotFound);
                }

                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();
                cmd.CommandText = $"USE idunnodb; " +
                    $"DELETE FROM Posts " +
                    $"WHERE PostID = '{toBeDeleted}'";

                if (await cmd.ExecuteNonQueryAsync() == -1)
                {
                    return ret.RetInternelServerError();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return ret.RetInternelServerError();
            }

            return ret.FormatReturn(true, "Post successfuly deleted!", StatusCodes.Status200OK);
        }

        public async Task<ValidationResult> UpdatePostAsync(int postID, Post post)
        {
            ValidationResult ret = new ValidationResult();

            try
            {
                if ((await GetPostsAsync(postID)).Count == 0) // if post not found return error
                {
                    return ret.FormatReturn(false, "Post not found!", StatusCodes.Status404NotFound);
                }

                await _context.conn.OpenAsync();
                using MySqlCommand cmd = _context.conn.CreateCommand();


                cmd.CommandText = $"USE idunnodb; " +
                    $"UPDATE Posts " +
                    $"SET PostTitle = '{post.PostTitle}', PostDescription = '{post.PostDescription}', ImagePath = '{post.ImagePath}'" +
                    $"WHERE PostID = '{postID}'";

                if (await cmd.ExecuteNonQueryAsync() == -1)
                {
                    return ret.RetInternelServerError();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return ret.RetInternelServerError();
            }

            return ret.FormatReturn(true, "Post've been updated!", StatusCodes.Status200OK);
        }

    }
}
