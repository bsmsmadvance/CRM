using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Minio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.API.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private int _expireHours = 24;
        private IConfiguration Configuration { get; }

        public FilesController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        [HttpPost("Upload")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var endpoint = Configuration["Minio:Endpoint"];
            var accessKey = Configuration["Minio:AccessKey"];
            var secretKey = Configuration["Minio:SecretKey"];
            var bucketName = Configuration["Minio:DefaultBucket"];
            var withSSL = Configuration["Minio:WithSSL"];
            MinioClient minio;
            if (withSSL == "true")
            {
                minio = new MinioClient(endpoint, accessKey, secretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(endpoint, accessKey, secretKey);
            }

            bool bucketExisted = await minio.BucketExistsAsync(bucketName);
            if (!bucketExisted)
            {
                await minio.MakeBucketAsync(bucketName);
            }
            var stream = file.OpenReadStream();
            string objectName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            await minio.PutObjectAsync(bucketName, objectName, stream, file.Length, file.ContentType);
            // expire in 1 day
            var url = await minio.PresignedGetObjectAsync(bucketName, objectName, (int)TimeSpan.FromHours(_expireHours).TotalSeconds);
            var result = new FileDTO()
            {
                Name = $"{objectName}",
                Url = url,
                IsTemp = true
            };

            return Ok(result);
        }

        [HttpPost("MultipleUpload")]
        [ProducesResponseType(200, Type = typeof(List<FileDTO>))]
        public async Task<IActionResult> MultipleUploadFile(List<IFormFile> files)
        {
            var endpoint = Configuration["Minio:Endpoint"];
            var accessKey = Configuration["Minio:AccessKey"];
            var secretKey = Configuration["Minio:SecretKey"];
            var bucketName = Configuration["Minio:DefaultBucket"];
            var withSSL = Configuration["Minio:WithSSL"];
            MinioClient minio;
            if (withSSL == "true")
            {
                minio = new MinioClient(endpoint, accessKey, secretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(endpoint, accessKey, secretKey);
            }

            bool bucketExisted = await minio.BucketExistsAsync(bucketName);
            if (!bucketExisted)
            {
                await minio.MakeBucketAsync(bucketName);
            }
            var results = new List<FileDTO>();
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                string objectName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                await minio.PutObjectAsync(bucketName, objectName, stream, file.Length, file.ContentType);
                var url = await minio.PresignedGetObjectAsync(bucketName, objectName, (int)TimeSpan.FromHours(_expireHours).TotalSeconds);

                var result = new FileDTO()
                {
                    Name = $"{bucketName}/{objectName}",
                    Url = url,
                    IsTemp = true
                };

                results.Add(result);
            }


            return Ok(results);
        }
    }
}
