﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Masuit.Tools.Mime;

namespace Masuit.Tools.Files.FileDetector.Detectors;

[FormatCategory(FormatCategory.Document)]
internal class MicrosoftExcelXLSDetector : AbstractCompoundFileDetailDetector
{
    public override IEnumerable<string> Chunks
    {
        get
        {
            yield return "Workbook";
        }
    }

    public override string Extension => "xls";

    public override string MimeType => new MimeMapper().GetMimeFromExtension("." + Extension);

    public override List<FormatCategory> FormatCategories => GetType().GetCustomAttributes<FormatCategoryAttribute>().Select(a => a.Category).ToList();

    protected override bool IsValidChunk(string chunkName, byte[] chunkData)
    {
        return chunkName == "Workbook";
    }

    public override string ToString() => "Microsoft Office Excel Document(XLS) Detector";
}
