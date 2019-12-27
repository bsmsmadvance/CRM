using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;

namespace Project.Services
{
    public interface IProjectImageService
    {
        Task<FileDTO> UpdateProjectLogoAsync(Guid projectID, FileDTO input);
        Task<FileDTO> GetProjectLogoAsync(Guid projectID);
        Task<List<FloorPlanImageDTO>> GetFloorPlanImagesAsync(Guid projectID, string name);
        Task<List<RoomPlanImageDTO>> GetRoomPlanImagesAsync(Guid projectID, string name);
        Task<List<FloorPlanImageDTO>> SaveFloorPlanImagesAsync(Guid projectID, List<FloorPlanImageDTO> inputs);
        Task<List<RoomPlanImageDTO>> SaveRoomPlanImagesAsync(Guid projectID, List<RoomPlanImageDTO> inputs);
    }
}
