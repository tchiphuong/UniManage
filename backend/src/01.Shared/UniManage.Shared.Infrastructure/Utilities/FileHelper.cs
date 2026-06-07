using System.Text;

namespace UniManage.Shared.Infrastructure.Utilities
{
    /// <summary>
    /// Helper functions for file operations and validations
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Root upload directory name (from config)
        /// </summary>
        public static string UploadPath => ConfigHelper.Configuration["FileSettings:UploadPath"] ?? "uploads";

        /// <summary>
        /// Temporary directory name (from config)
        /// </summary>
        public static string TempPath => ConfigHelper.Configuration["FileSettings:TempPath"] ?? "temp";

        /// <summary>
        /// Date format for folders (from config)
        /// </summary>
        public static string FolderDateFormat => ConfigHelper.Configuration["FileSettings:FolderDateFormat"] ?? "yyyy/MM/dd";

        /// <summary>
        /// Allowed image file extensions
        /// </summary>
        public static string[] ImageExtensions => ConfigHelper.Configuration["FileSettings:AllowedImages"]?.Split(',') ?? new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        /// <summary>
        /// Allowed document file extensions
        /// </summary>
        public static string[] DocumentExtensions => ConfigHelper.Configuration["FileSettings:AllowedDocuments"]?.Split(',') ?? new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };

        /// <summary>
        /// Allowed video file extensions
        /// </summary>
        public static string[] VideoExtensions => ConfigHelper.Configuration["FileSettings:AllowedVideos"]?.Split(',') ?? new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".webm" };

        /// <summary>
        /// Allowed audio file extensions
        /// </summary>
        public static string[] AudioExtensions => ConfigHelper.Configuration["FileSettings:AllowedAudios"]?.Split(',') ?? new[] { ".mp3", ".wav", ".flac", ".aac", ".ogg" };

        /// <summary>
        /// Maximum file size constants (in Bytes)
        /// </summary>
        public static class MaxFileSizes
        {
            public static long Image => long.Parse(ConfigHelper.Configuration["FileSettings:MaxImageSizeKB"] ?? "5120") * 1024;
            public static long Document => long.Parse(ConfigHelper.Configuration["FileSettings:MaxDocumentSizeKB"] ?? "10240") * 1024;
            public static long Video => long.Parse(ConfigHelper.Configuration["FileSettings:MaxVideoSizeKB"] ?? "102400") * 1024;
            public static long Audio => long.Parse(ConfigHelper.Configuration["FileSettings:MaxAudioSizeKB"] ?? "20480") * 1024;
            public static long General = 50 * 1024 * 1024; // 50MB
        }

        /// <summary>
        /// Maximum video duration in seconds
        /// </summary>
        public static int MaxVideoDurationSeconds => int.Parse(ConfigHelper.Configuration["FileSettings:MaxVideoDurationSeconds"] ?? "60");

        /// <summary>
        /// Validates if file extension is allowed for images
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>True if valid image extension</returns>
        public static bool IsValidImageFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return ImageExtensions.Contains(extension);
        }

        /// <summary>
        /// Validates if file extension is allowed for documents
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>True if valid document extension</returns>
        public static bool IsValidDocumentFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return DocumentExtensions.Contains(extension);
        }

        /// <summary>
        /// Validates if file extension is allowed for videos
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>True if valid video extension</returns>
        public static bool IsValidVideoFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return VideoExtensions.Contains(extension);
        }

        /// <summary>
        /// Validates if file extension is allowed for audio
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>True if valid audio extension</returns>
        public static bool IsValidAudioFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return AudioExtensions.Contains(extension);
        }

        /// <summary>
        /// Validates file size against maximum allowed
        /// </summary>
        /// <param name="fileSize">File size in bytes</param>
        /// <param name="maxSize">Maximum allowed size in bytes</param>
        /// <returns>True if file size is valid</returns>
        public static bool IsValidFileSize(long fileSize, long maxSize)
        {
            return fileSize > 0 && fileSize <= maxSize;
        }

        /// <summary>
        /// Gets maximum file size based on file type
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Maximum file size in bytes</returns>
        public static long GetMaxFileSize(string fileName)
        {
            if (IsValidImageFile(fileName))
                return MaxFileSizes.Image;

            if (IsValidDocumentFile(fileName))
                return MaxFileSizes.Document;

            if (IsValidVideoFile(fileName))
                return MaxFileSizes.Video;

            if (IsValidAudioFile(fileName))
                return MaxFileSizes.Audio;

            return MaxFileSizes.General;
        }

        /// <summary>
        /// Generates safe file name by removing invalid characters
        /// </summary>
        /// <param name="fileName">Original file name</param>
        /// <returns>Safe file name</returns>
        public static string GenerateSafeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return Guid.NewGuid().ToString();

            var invalidChars = Path.GetInvalidFileNameChars();
            var safeName = new StringBuilder();

            foreach (var c in fileName)
            {
                if (!invalidChars.Contains(c))
                {
                    safeName.Append(c);
                }
                else
                {
                    safeName.Append('_');
                }
            }

            var result = safeName.ToString().Trim();

            // Remove multiple consecutive underscores
            while (result.Contains("__"))
            {
                result = result.Replace("__", "_");
            }

            // Remove leading/trailing underscores
            result = result.Trim('_');

            // Ensure file has extension
            if (string.IsNullOrEmpty(Path.GetExtension(result)))
            {
                result += ".txt";
            }

            return string.IsNullOrEmpty(result) ? Guid.NewGuid().ToString() + ".txt" : result;
        }

        /// <summary>
        /// Generates unique file name by appending timestamp
        /// </summary>
        /// <param name="fileName">Original file name</param>
        /// <returns>Unique file name</returns>
        public static string GenerateUniqueFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return Guid.NewGuid().ToString();

            var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            var timestamp = DateTimeHelper.Now.ToString("yyyyMMdd_HHmmss");

            return $"{nameWithoutExtension}_{timestamp}{extension}";
        }

        /// <summary>
        /// Formats file size in human readable format
        /// </summary>
        /// <param name="bytes">File size in bytes</param>
        /// <returns>Formatted file size</returns>
        public static string FormatFileSize(long bytes)
        {
            if (bytes == 0)
                return "0 B";

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        /// <summary>
        /// Gets MIME type based on file extension
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>MIME type</returns>
        public static string GetMimeType(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "application/octet-stream";

            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                // Images
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",

                // Documents
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".txt" => "text/plain",

                // Videos
                ".mp4" => "video/mp4",
                ".avi" => "video/x-msvideo",
                ".mov" => "video/quicktime",
                ".wmv" => "video/x-ms-wmv",
                ".flv" => "video/x-flv",
                ".webm" => "video/webm",

                // Audio
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".flac" => "audio/flac",
                ".aac" => "audio/aac",
                ".ogg" => "audio/ogg",

                _ => "application/octet-stream"
            };
        }

        /// <summary>
        /// Validates file upload (extension and size)
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="fileSize">File size in bytes</param>
        /// <param name="allowedExtensions">Allowed extensions</param>
        /// <param name="maxSize">Maximum file size</param>
        /// <returns>Validation result with errors</returns>
        public static (bool IsValid, List<string> Errors) ValidateFileUpload(
            string fileName,
            long fileSize,
            string[] allowedExtensions,
            long maxSize)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                errors.Add("File name is required");
                return (false, errors);
            }

            // Check extension
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                errors.Add($"File extension '{extension}' is not allowed. Allowed extensions: {string.Join(", ", allowedExtensions)}");
            }

            // Check file size
            if (fileSize <= 0)
            {
                errors.Add("File size must be greater than 0");
            }
            else if (fileSize > maxSize)
            {
                errors.Add($"File size ({FormatFileSize(fileSize)}) exceeds maximum allowed size ({FormatFileSize(maxSize)})");
            }

            return (errors.Count == 0, errors);
        }

        /// <summary>
        /// Ensures directory exists, creates if not
        /// </summary>
        /// <param name="directoryPath">Directory path</param>
        /// <returns>True if directory exists or was created successfully</returns>
        public static bool EnsureDirectoryExists(string directoryPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(directoryPath))
                    return false;

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Safely deletes file if exists
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>True if deleted or didn't exist</returns>
        public static bool SafeDeleteFile(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    return true;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets file extension without dot
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Extension without dot</returns>
        public static string GetExtensionWithoutDot(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return string.IsNullOrEmpty(extension) ? string.Empty : extension.TrimStart('.');
        }

        /// <summary>
        /// Moves file from source to destination, ensures destination directory exists
        /// </summary>
        /// <param name="sourceFilePath">Source absolute path</param>
        /// <param name="destFilePath">Destination absolute path</param>
        /// <returns>True if success</returns>
        public static bool MoveFile(string sourceFilePath, string destFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sourceFilePath) || string.IsNullOrWhiteSpace(destFilePath))
                    return false;

                if (!File.Exists(sourceFilePath))
                    return false;

                var destDir = Path.GetDirectoryName(destFilePath);
                if (!string.IsNullOrEmpty(destDir))
                {
                    EnsureDirectoryExists(destDir);
                }

                if (File.Exists(destFilePath))
                {
                    File.Delete(destFilePath);
                }

                File.Move(sourceFilePath, destFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if file exists
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>True if file exists</returns>
        public static bool FileExists(string filePath)
        {
            return !string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath);
        }

        /// <summary>
        /// Validates file signature (Magic Numbers) to prevent disguised executable files
        /// </summary>
        public static bool IsValidFileSignature(Stream fileStream, string fileName)
        {
            try
            {
                using var reader = new BinaryReader(fileStream, Encoding.UTF8, true);
                var headerBytes = reader.ReadBytes(4);
                if (headerBytes.Length < 2) return false;

                var hexSignature = BitConverter.ToString(headerBytes).Replace("-", "");

                // Chặn file thực thi (EXE, DLL...) - Signature: 4D-5A (MZ)
                if (hexSignature.StartsWith("4D5A")) return false;

                var ext = Path.GetExtension(fileName).ToLowerInvariant();

                // Kiểm tra chéo với extension
                return ext switch
                {
                    ".jpg" or ".jpeg" => hexSignature.StartsWith("FFD8"),
                    ".png" => hexSignature.StartsWith("89504E47"),
                    ".gif" => hexSignature.StartsWith("47494638"),
                    ".pdf" => hexSignature.StartsWith("25504446"),
                    ".zip" or ".docx" or ".xlsx" => hexSignature.StartsWith("504B0304"), // ZIP/Office formats
                    _ => true // Các loại khác cho phép qua (hoặc bổ sung thêm tùy nhu cầu)
                };
            }
            catch
            {
                return false;
            }
            finally
            {
                fileStream.Position = 0; // Luôn reset stream để các logic sau đọc được tiếp
            }
        }

        /// <summary>
        /// Gets file information safely
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>FileInfo or null if file doesn't exist</returns>
        public static FileInfo? GetFileInfo(string filePath)
        {
            try
            {
                if (FileExists(filePath))
                {
                    return new FileInfo(filePath);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

