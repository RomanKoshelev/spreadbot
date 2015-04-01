﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IChannelTask.cs
// Roman, 2015-04-01 4:58 PM

using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Tasks
{
    public interface IChannelTask : IAbstractTask
    {
        string ChannelId { get; }
        ChannelMethod ChannelMethod { get; }
    }
}