﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.cs
// romak_000, 2015-03-21 1:23

using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Chanel.Operations.Responses;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask : AbstractChannelTask, IProceedableTask
    {
        public MipRequestStatus MipRequestStatusCode { get; set; }
        public EbayPublishArgs Args { get; set; }

        [NotSerialize]
        public ChannelResponse< EbayPublishResult > EbayPublishResponse { get; set; }
    }
}