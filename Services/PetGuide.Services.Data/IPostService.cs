﻿namespace PetGuide.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Posts;

    public interface IPostService
    {
        Task AddAsync(PostInputModel input, string userId);

        Task EditAsync(string id, PostInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<AllPostsViewModel> GetAll();

        PostDetailsViewModel GetPostDetails(string id);

        AllPostsByTagViewModel GetPostsByTag(int id);

        PostInputModel GetPostEdit(string id);

        Post GetPostById(string id);

        Tag GetTagById(int id);

        int GetCount();
    }
}
