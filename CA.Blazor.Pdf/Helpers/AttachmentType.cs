using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Blazor.Pdf.Helpers
{
    internal sealed class AttachmentType : IAttachmentType
    {
        private AttachmentType(string mimeType, string friendlyName, string extension)
        {
            this.MimeType = mimeType;
            this.FriendlyName = friendlyName;
            this.Extension = extension;
        }

        public static IAttachmentType UnknownMime { get; } = new AttachmentType("application/octet-stream", "Unknown", String.Empty);
        public static IAttachmentType ImagePng { get; } = new AttachmentType("image/png", "ImagePng", ".png");
        public static IAttachmentType ImageJpeg { get; } = new AttachmentType("image/jpeg", "ImageJpeg", ".jpeg");
        public static IAttachmentType Video { get; } = new AttachmentType("video/mp4", "Video", ".mp4");
        public static IAttachmentType Pdf { get; } = new AttachmentType("application/pdf", "Pdf", ".pdf");
        public static IAttachmentType Unknown { get; } = new AttachmentType(String.Empty, "Unknown", String.Empty);

        private static readonly IDictionary<string, IAttachmentType> _mimeMap =
            new Dictionary<string, IAttachmentType>(StringComparer.OrdinalIgnoreCase)
            {
                { "IVBOR", AttachmentType.ImagePng},
                { "/9J/4", AttachmentType.ImageJpeg },
                { "AAAAF", AttachmentType.Video },
                { "JVBER", AttachmentType.Pdf }
            };

        public static IAttachmentType GetMimeType(string value)
        {
            return string.IsNullOrEmpty(value)
            ? AttachmentType.Unknown
            : (_mimeMap.TryGetValue(value.Substring(0, 5), out IAttachmentType? result) ? result : AttachmentType.Unknown);
        }

        public string MimeType { get; }

        public string FriendlyName { get; }
        public string Extension { get; }
    }
}
