﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFindRemoteFileResult.cs
// Roman, 2015-04-10 1:29 PM

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}