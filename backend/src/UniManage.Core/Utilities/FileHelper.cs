using System.Text;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper functions for file operations and validations
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Allowed image file extensions
        /// </summary>
        public static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        /// <summary>
        /// Allowed document file extensions
        /// </summary>
        public static readonly string[] DocumentExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };

        /// <summary>
        /// Allowed video file extensions
        /// </summary>
        public static readonly string[] VideoExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".webm" };

        /// <summary>
        /// Allowed audio file extensions
        /// </summary>
        public static readonly string[] AudioExtensions = { ".mp3", ".wav", ".flac", ".aac", ".ogg" };

        /// <summary>
        /// Maximum file size constants
        /// </summary>
        public static class MaxFileSizes
        {
            public const long Image = 5 * 1024 * 1024; // 5MB
            public const long Document = 10 * 1024 * 1024; // 10MB
            public const long Video = 100 * 1024 * 1024; // 100MB
            public const long Audio = 20 * 1024 * 1024; // 20MB
            public const long General = 50 * 1024 * 1024; // 50MB
        }

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
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

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
        /// Checks if file exists
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>True if file exists</returns>
        public static bool FileExists(string filePath)
        {
            return !string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath);
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