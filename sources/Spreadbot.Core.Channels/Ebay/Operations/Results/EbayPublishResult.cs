// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishResult.cs
// Roman, 2015-04-10 1:29 PM

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Operations.Results
{
    public class EbayPublishResult : AbstractMipResponseResult
    {
        public string MipRequestId { get; set; }
        public string MipItemId { get; set; }
    }
}