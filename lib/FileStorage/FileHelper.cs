using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel;

namespace FileStorage
{
    public class FileHelper
    {
        private int _expireHours = 6;
        private string _minioEndpoint;
        private string _minioAccessKey;
        private string _minioSecretKey;
        private string _defaultBucket;
        private string _tempBucket;
        private bool _withSSL;

        public FileHelper(string minioEndpoint, string minioAccessKey, string minioSecretKey, string defaultBucket, string tempBucket, bool withSSL = false)
        {
            _minioEndpoint = minioEndpoint;
            _minioAccessKey = minioAccessKey;
            _minioSecretKey = minioSecretKey;
            _defaultBucket = defaultBucket;
            _tempBucket = tempBucket;
            _withSSL = withSSL;
        }

        public async Task MoveTempFileAsync(string sourceObjectName, string destObjectName)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }
            await minio.CopyObjectAsync(_tempBucket, sourceObjectName, _defaultBucket, destObjectName);
            await minio.RemoveObjectAsync(_tempBucket, sourceObjectName);
        }

        public async Task<string> GetFileUrlAsync(string name)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }
            var url = await minio.PresignedGetObjectAsync(_defaultBucket, name, (int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            return url;
        }

        public async Task<string> GetFileUrlAsync(string bucket, string name)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }
            var url = await minio.PresignedGetObjectAsync(bucket, name, (int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            return url;
        }

        public async Task<Stream> GetStreamFromUrlAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsStreamAsync();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<FileUploadResult> UploadFileFromStream(Stream fileStream, string filePath, string fileName, string contentType)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }


            bool bucketExisted = await minio.BucketExistsAsync(_defaultBucket);
            if (!bucketExisted)
            {
                await minio.MakeBucketAsync(_defaultBucket);
            }
            string objectName = $"{Guid.NewGuid().ToString()}_{fileName}";
            objectName = Path.Combine(filePath, objectName);
            objectName = objectName.Replace('\\', '/');
            await minio.PutObjectAsync(_defaultBucket, objectName, fileStream, fileStream.Length, contentType);
            // expire in 1 day
            var url = await minio.PresignedGetObjectAsync(_defaultBucket, objectName, (int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            return new FileUploadResult()
            {
                Name = objectName,
                BucketName = _defaultBucket,
                Url = url
            };
        }

        public async Task<FileUploadResult> UploadFileFromStreamWithOutGuid(Stream fileStream,string bucketName, string filePath, string fileName, string contentType)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }


            bool bucketExisted = await minio.BucketExistsAsync(bucketName);
            if (!bucketExisted)
            {
                await minio.MakeBucketAsync(bucketName);
            }
            string objectName = fileName;
            objectName = Path.Combine(filePath, objectName);
            objectName = objectName.Replace('\\', '/');
            await minio.PutObjectAsync(bucketName, objectName, fileStream, fileStream.Length, contentType);
            // expire in 1 day
            var url = await minio.PresignedGetObjectAsync(bucketName, objectName, (int)TimeSpan.FromHours(_expireHours).TotalSeconds);

            return new FileUploadResult()
            {
                Name = objectName,
                BucketName = bucketName,
                Url = url
            };
        }

        public async Task<List<string>> GetListFile(string bucketName, string prefix)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }


            bool bucketExisted = await minio.BucketExistsAsync(bucketName);
            if (bucketExisted)
            {
                List<string> bucketKeys = new List<string>();
                var observable = minio.ListObjectsAsync(bucketName, prefix);
                IDisposable subscription = observable.Subscribe(
                item => bucketKeys.Add(item.Key),
                ex => throw new Exception("Error", ex),
                () => Console.WriteLine("OnComplete: {0}"));
                observable.Wait();

                return bucketKeys;
            }
            else
            {
                return null;
            }
        }

        public async Task MoveFileAsync(string sourceBucket, string sourceObjectName, string destBucket, string destObjectName)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }
            await minio.CopyObjectAsync(sourceBucket, sourceObjectName, destBucket, destObjectName);
        }

        public async Task MoveAndRemoveFileAsync(string sourceBucket, string sourceObjectName, string destBucket, string destObjectName)
        {
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }
            await minio.CopyObjectAsync(sourceBucket, sourceObjectName, destBucket, destObjectName);
            await minio.RemoveObjectAsync(sourceBucket, sourceObjectName);
        }

        public static string GetApplicationRootPath()
        {
            var result = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            result = result.Replace("file:\\", string.Empty);
            result = result.Replace("file:", string.Empty);
            return result;
        }
        public async Task<string> DownLoadToTempFileAsync(string bucketName, string prefix, string filename)
        {
            string pathTempFile = Path.GetTempPath();
            string tempFilename = Guid.NewGuid() + "_" + filename;
            MinioClient minio;
            if (_withSSL)
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey).WithSSL();
            }
            else
            {
                minio = new MinioClient(_minioEndpoint, _minioAccessKey, _minioSecretKey);
            }
            await minio.GetObjectAsync(bucketName, prefix + filename,
                                    (stream) =>
                                    {
                                        using (Stream fs = File.OpenWrite(pathTempFile + tempFilename))
                                        {
                                            stream.CopyTo(fs);
                                        }
                                    });
            return pathTempFile + tempFilename;
        }
    }
}
