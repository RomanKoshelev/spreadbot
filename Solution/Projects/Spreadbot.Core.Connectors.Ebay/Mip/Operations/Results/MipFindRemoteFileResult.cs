﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipFindRemoteFileResult.cs
// romak_000, 2015-03-19 15:49

using Crocodev.Common.Extensions;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public readonly string FileName;
        public readonly string FolderPath;

        public MipFindRemoteFileResult(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
        }

        public override string Autoinfo
        {
            get
            {
                return Template.SafeFormat("FolderPath", FolderPath) + ", " + Template.SafeFormat("FileName", FileName);
            }
        }
    }
}