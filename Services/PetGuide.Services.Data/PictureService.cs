namespace PetGuide.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PetGuide.Data.Common.Repositories;
    using PetGuide.Data.Models;
    using PetGuide.Web.ViewModels.Pictures;

    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;

    public class PictureService : IPictureService
    {
        private const int ThumbnailWidth = 300;
        private const int FullscreenWidth = 1000;

        private readonly IDeletableEntityRepository<Picture> picturesRepository;
        private readonly IDeletableEntityRepository<Pet> petsRepository;
        private readonly IDeletableEntityRepository<PetEvent> petEventsRepository;
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IRepository<Shelter> sheltersRepository;

        public PictureService(
            IDeletableEntityRepository<Picture> picturesRepository,
            IDeletableEntityRepository<Pet> petsRepository,
            IDeletableEntityRepository<PetEvent> petEventsRepository,
            IDeletableEntityRepository<Post> postsRepository,
            IRepository<Shelter> sheltersRepository)
        {
            this.picturesRepository = picturesRepository;
            this.petsRepository = petsRepository;
            this.petEventsRepository = petEventsRepository;
            this.postsRepository = postsRepository;
            this.sheltersRepository = sheltersRepository;
        }

        // Upload Picture
        public async Task Upload(IEnumerable<PictureInputModel> pictures, string userId, string id)
        {
            var dict = new ConcurrentDictionary<Guid, Picture>();

            var totalPictures = await this
               .picturesRepository
               .AllAsNoTrackingWithDeleted()
               .CountAsync();

            var tasks = pictures
                .Select(p => Task.Run(async () =>
                {
                    try
                    {
                        using var pictureResult = await Image.LoadAsync(p.Content);

                        var pictureId = Guid.NewGuid();
                        var path = $"/images/{totalPictures % 1000}/";
                        var name = $"{pictureId}.jpg";

                        var storagePath = Path.Combine(
                            Directory.GetCurrentDirectory(), $"wwwroot{path}".Replace("/", "\\"));

                        if (!Directory.Exists(storagePath))
                        {
                            Directory.CreateDirectory(storagePath);
                        }

                        await this.SavePicture(pictureResult, $"Original_{name}", storagePath, pictureResult.Width);
                        await this.SavePicture(pictureResult, $"Fullscreen_{name}", storagePath, FullscreenWidth);
                        await this.SavePicture(pictureResult, $"Thumbnail_{name}", storagePath, ThumbnailWidth);

                        dict.GetOrAdd(pictureId, this.CreatePicture(pictureId, path, userId, id));
                    }
                    catch
                    {
                        // Log information.
                    }
                }))
                .ToList();

            await Task.WhenAll(tasks);

            foreach (var picture in dict)
            {
                await this.picturesRepository.AddAsync(picture.Value);
            }

            await this.picturesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var picture = this.picturesRepository.All().FirstOrDefault(x => x.Id == id);
            this.picturesRepository.Delete(picture);
            await this.picturesRepository.SaveChangesAsync();
        }

        // Get Upload Pictures
        public UploadPicturesInputModel GetUpload(string id)
        {
            var pictures = this.GetPicturesToShow(id);

            var viewModel = new UploadPicturesInputModel
            {
                Id = id,
                PicturesToShow = pictures,
            };

            return viewModel;
        }

        public IEnumerable<PictureViewModel> GetPicturesToShow(string id)
        {
            return this.picturesRepository
                .All()
                .Where(x => x.PetId == id || x.PetEventId == id || x.ShelterId == id || x.PostId == id)
                .Select(x => new PictureViewModel
                {
                    Id = x.Id,
                    Name = x.Folder + "/Fullscreen_" + x.Id + ".jpg",
                })
                .ToList();
        }

        public Gallery GetGallery(string userId)
        {
            var pictures = this.picturesRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new PictureViewModel
                {
                    Name = x.Folder + "/Fullscreen_" + x.Id + ".jpg",
                })
                .ToList();

            var viewModel = new Gallery
            {
                PicturesToShow = pictures,
            };

            return viewModel;
        }

        public IEnumerable<PictureViewModel> GetPetsPictures(string id)
        {
            return this.picturesRepository
               .All()
               .Where(x => x.PetId == id)
               .Select(x => new PictureViewModel
               {
                   Id = x.Id,
                   Name = x.Folder + "/Thumbnail_" + x.Id + ".jpg",
               })
               .ToList();
        }

        public IEnumerable<PictureViewModel> GetEventsPictures(string id)
        {
            return this.picturesRepository
                .All()
                .Where(x => x.PetEventId == id)
                .Select(x => new PictureViewModel
                {
                    Id = x.Id,
                    Name = x.Folder + "/Thumbnail_" + x.Id + ".jpg",
                })
                .ToList();
        }

        public IEnumerable<PictureViewModel> GetSheltersPictures(string id)
        {
            return this.picturesRepository
                .All()
                .Where(x => x.ShelterId == id)
                .Select(x => new PictureViewModel
                {
                    Id = x.Id,
                    Name = x.Folder + "/Thumbnail_" + x.Id + ".jpg",
                })
                .ToList();
        }

        private async Task SavePicture(Image image, string name, string path, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }

            image
                .Mutate(i => i
                    .Resize(new Size(width, height)));

            image.Metadata.ExifProfile = null;

            await image.SaveAsJpegAsync($"{path}/{name}", new JpegEncoder
            {
                Quality = 75,
            });
        }

        private Picture CreatePicture(Guid pictureId, string folder, string userId, string id)
        {
            var petId = this.petsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id)?.Id;
            var eventId = this.petEventsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id)?.Id;
            var shelterId = this.sheltersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id)?.Id;
            var postId = this.postsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id)?.Id;

            return new Picture
            {
                Id = pictureId.ToString(),
                Folder = folder,
                UserId = userId,
                PetId = petId,
                PetEventId = eventId,
                ShelterId = shelterId,
                PostId = postId,
            };
        }
    }
}
