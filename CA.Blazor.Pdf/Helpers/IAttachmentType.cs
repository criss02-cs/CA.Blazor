using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Blazor.Pdf.Helpers
{
    internal interface IAttachmentType
    {
        string MimeType { get; }
        string FriendlyName { get; }
        string Extension { get; }
    }
}
