using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Project.Services
{
    public class ProjectImageService : IProjectImageService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public ProjectImageService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        public async Task<FileDTO> UpdateProjectLogoAsync(Guid projectID, FileDTO input)
        {
            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);

            if (input.IsTemp)
            {
                string logoName = $"{project.ID}/logo/{input.Name}";
                await FileHelper.MoveTempFileAsync(input.Name, logoName);

                project.Logo = logoName;
                DB.Entry(project).State = EntityState.Modified;
                await DB.SaveChangesAsync();

                string url = await FileHelper.GetFileUrlAsync(logoName);

                var result = new FileDTO()
                {
                    Name = logoName,
                    Url = url
                };

                return result;
            }
            else
            {
                return input;
            }

        }

        public async Task<FileDTO> GetProjectLogoAsync(Guid projectID)
        {
            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            if (!string.IsNullOrEmpty(project.Logo))
            {
                string url = await FileHelper.GetFileUrlAsync(project.Logo);

                var result = new FileDTO()
                {
                    Name = project.Logo,
                    Url = url
                };

                return result;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<List<FloorPlanImageDTO>> GetFloorPlanImagesAsync(Guid projectID, string name)
        {
            IQueryable<FloorPlanImage> query = DB.FloorPlanImages.Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }
            var floorPlanImages = await query.ToListAsync();
            var results = floorPlanImages
                    .Select(async o => await FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                    .Select(o => o.Result).ToList();
            return results;
        }

        public async Task<List<RoomPlanImageDTO>> GetRoomPlanImagesAsync(Guid projectID, string name)
        {
            IQueryable<RoomPlanImage> query = DB.RoomPlanImages.Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }
            var roomPlanImages = await query.ToListAsync();
            var results = roomPlanImages
                .Select(async o => await RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                .Select(o => o.Result).ToList();
            return results;
        }

        public async Task<List<FloorPlanImageDTO>> SaveFloorPlanImagesAsync(Guid projectID, List<FloorPlanImageDTO> inputs)
        {
            var addingList = new List<FloorPlanImage>();
            var updatingList = new List<FloorPlanImage>();
            var deletingList = new List<FloorPlanImage>();

            var allFloorPlanImages = await DB.FloorPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
            //find add and update items
            foreach (var item in inputs)
            {
                var existingItem = allFloorPlanImages.SingleOrDefault(o => o.ID == item.Id);

                if (existingItem != null)
                {
                    if (item.File.IsTemp)
                    {
                        string floorPlanName = $"{projectID}/floorPlans/{item.File.Name}";
                        await FileHelper.MoveTempFileAsync(item.File.Name, floorPlanName);
                        item.File.Name = floorPlanName;
                    }

                    item.ToModel(ref existingItem);
                    updatingList.Add(existingItem);
                }
                else
                {
                    string floorPlanName = $"{projectID}/floorPlans/{item.File.Name}";
                    await FileHelper.MoveTempFileAsync(item.File.Name, floorPlanName);
                    item.File.Name = floorPlanName;

                    existingItem = new FloorPlanImage();
                    item.ToModel(ref existingItem);
                    existingItem.ProjectID = projectID;
                    addingList.Add(existingItem);
                }
            }
            //find delete items
            foreach (var item in allFloorPlanImages)
            {
                var existingInput = inputs.SingleOrDefault(o => o.Id == item.ID);
                if (existingInput == null)
                {
                    item.IsDeleted = true;
                    deletingList.Add(item);
                }
            }

            //save to database
            DB.UpdateRange(updatingList);
            DB.UpdateRange(deletingList);
            await DB.AddRangeAsync(addingList);
            await DB.SaveChangesAsync();

            allFloorPlanImages = await DB.FloorPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
            var results = allFloorPlanImages
                .Select(async o => await FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                .Select(o => o.Result).ToList();

            return results;

        }

        public async Task<List<RoomPlanImageDTO>> SaveRoomPlanImagesAsync(Guid projectID, List<RoomPlanImageDTO> inputs)
        {
            var addingList = new List<RoomPlanImage>();
            var updatingList = new List<RoomPlanImage>();
            var deletingList = new List<RoomPlanImage>();

            var allRoomPlanImages = await DB.RoomPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
            //find add and update items
            foreach (var item in inputs)
            {
                var existingItem = allRoomPlanImages.SingleOrDefault(o => o.ID == item.Id);
                if (existingItem != null)
                {
                    if (item.File.IsTemp)
                    {
                        string roomPlanName = $"{projectID}/roomPlans/{item.File.Name}";
                        await FileHelper.MoveTempFileAsync(item.File.Name, roomPlanName);
                        item.File.Name = roomPlanName;
                    }
                    item.ToModel(ref existingItem);
                    updatingList.Add(existingItem);
                }
                else
                {
                    string roomPlanName = $"{projectID}/roomPlans/{item.File.Name}";
                    await FileHelper.MoveTempFileAsync(item.File.Name, roomPlanName);
                    item.File.Name = roomPlanName;

                    existingItem = new RoomPlanImage();
                    item.ToModel(ref existingItem);
                    existingItem.ProjectID = projectID;
                    addingList.Add(existingItem);
                }
            }
            //find delete items
            foreach (var item in allRoomPlanImages)
            {
                var existingInput = inputs.SingleOrDefault(o => o.Id == item.ID);
                if (existingInput == null)
                {
                    item.IsDeleted = true;
                    deletingList.Add(item);
                }
            }

            //save to database
            DB.UpdateRange(updatingList);
            DB.UpdateRange(deletingList);
            await DB.AddRangeAsync(addingList);
            await DB.SaveChangesAsync();

            allRoomPlanImages = await DB.RoomPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
            var results = allRoomPlanImages
                .Select(async o => await RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                .Select(o => o.Result).ToList();

            return results;

        }
    }
}
