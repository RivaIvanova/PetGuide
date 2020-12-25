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

        public PictureService(IDeletableEntityRepository<Picture> picturesRepository)
        {
            this.picturesRepository = picturesRepository;
        }

        public async Task Upload(IEnumerable<PictureInputModel> pictures, string userId = null, string petId = null, string eventId = null, string shelterId = null, string postId = null)
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

                        var id = Guid.NewGuid();
                        var path = $"/images/{totalPictures % 1000}/";
                        var name = $"{id}.jpg";

                        var storagePath = Path.Combine(
                            Directory.GetCurrentDirectory(), $"wwwroot{path}".Replace("/", "\\"));

                        if (!Directory.Exists(storagePath))
                        {
                            Directory.CreateDirectory(storagePath);
                        }

                        await this.SavePicture(pictureResult, $"Original_{name}", storagePath, pictureResult.Width);
                        await this.SavePicture(pictureResult, $"Fullscreen_{name}", storagePath, FullscreenWidth);
                        await this.SavePicture(pictureResult, $"Thumbnail_{name}", storagePath, ThumbnailWidth);

                        var picture = this.CreatePicture(id, path, userId, petId, eventId, shelterId, postId);

                        dict.GetOrAdd(id, picture);
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

        public Task<List<string>> GetAllPictures()
            => this.picturesRepository
            .AllAsNoTracking()
            .Select(x => x.Folder + "/Thumbnail_" + x.Id + ".jpg")
            .ToListAsync();

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

        private Picture CreatePicture(Guid id, string folder, string userId, string petId, string eventId, string shelterId, string postId)
        {
            var picture = new Picture
            {
                Id = id.ToString(),
                Folder = folder,
                UserId = userId,
                PetId = petId,
                PetEventId = eventId,
                ShelterId = shelterId,
                PostId = postId,
            };

            return picture;
        }
    }
}
