using System;
using PetsApp.dal;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PetsApp.bll
{
    public class PostManager
    {
        private List<User> users;
        private List<Post> posts;
        private List<Pet> pets;
        public List<Post> list { get; set; }


        public PostManager()
        {
            users = ProcessUser.ProcessUsers("users.csv");
            pets = ProcessPet.ProcessPets("pets.csv");
            posts = ProcessPost.ProcessPosts("post.csv");
        }


        public List<Post> showListOfPost(string type)
        {
            list = posts.Where(p => p.Type.Equals(type) && p.Active == true).OrderByDescending(p => p.Duration).ToList();

            return list;
        }

        public void addNewPost(string title, string description, int duration, string type, int reward, int userID)
        {
            posts.Add(new Post
            {
                Active = true,
                Title = title,
                Description = description,
                StarTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(duration),
                Type = type,
                Reward = reward,
                UserId = userID,
            });
        }
        
        public Post showPost(string title)
        {
            Post post = (Post) list.Where(p => p.Title.Equals(title)).Select(p => p);

            return post;
        }

        public void acceptPost(int PostId)
        {
            var post = posts.Join(users, p => p.UserId, u => u.UserId, (p, u) => new
            {
                user = u,
                post = p,
            });

            foreach (var p in post)
            {
                p.post.Active = false;
                p.user.RewardPoints = +p.post.Reward;
            }

        }


        //var query = users.Join(posts,
        //        u => u.UserId,
        //        p => p.UserId,
        //        (u, p) => new
        //        {
        //            p.PostId,
        //            u.Name,
        //            u.Age,
        //            p.Title,
        //            p.Description,
        //            p.EndTime,
        //            p.Reward

        //        })
        //    .OrderBy(p => p.PostId)
        //    .ThenBy(p => p.EndTime);


        //var usersGroups = users.GroupJoin(posts, u => u.UserId, p => p.UserId,
        //    (u, p) => new
        //    {
        //        Users = u,
        //        Post = p,
        //        //output: (USER + LIST<POST>)
        //    });

        //var group = posts.GroupBy(p => p.Type.ToUpper())
        //        .OrderByDescending(g => g.Key);

        //    foreach private IEnumerable<object> posts;

        //(var result in group)
        //{
        //    foreach (var post in result.Take(2))
        //    {
        //        Console.WriteLine($"\t{post.Title}  :  {post.Description}");
        //    }

        //    foreach (var post in query)
        //    {
        //        Console.WriteLine($" {post.Title} : {post.Description} : {post.Name}");
        //    }



    }
}



