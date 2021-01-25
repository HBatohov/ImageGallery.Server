using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace ImageGallery.DatabaseRequests.Pictures
{
    public class AddPictureValidator : AbstractValidator<AddPicture>
    {
        public AddPictureValidator(IConfiguration config)
        {
            RuleFor(x => x.FormFiles).Custom((list, context) =>
            {
                if (list.Count == 0)
                    context.AddFailure("FileListIsEmptyException", "No file data");
            });
            RuleForEach(x => x.FormFiles).Custom((file, context) =>
            {
                if (file.Length == 0)
                    context.AddFailure("FileSizeException", $"File {file.FileName} has no content.");

                var fileSizeLimit = config.GetValue<long>("FileSizeLimit");
                if (fileSizeLimit > 0 && file.Length > fileSizeLimit)
                    context.AddFailure("FileSizeException", $"The File '{file.FileName}' has size {file.Length} exceeds the maximum file size {fileSizeLimit}.");

                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var fileMimeType = MimeTypes.GetMimeType(fileBytes, file.FileName);

                if (fileMimeType == MimeTypes.DefaultMimeTipe)
                {
                    context.AddFailure("UnsupportedFileFormatException", $"File '{file.FileName}' contains an unsupported data format.");
                }
            });
        }
    }
}
