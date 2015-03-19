﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// IChannelTask.cs
// romak_000, 2015-03-19 13:43

using Spreadbot.Core.Common.Channel.Operations.Args;
using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Common.Channel.Operations.Tasks
{
    public interface IChannelTask : ITask
    {
        IChannel ChannelRef { get; }
        ChannelMethod ChannelMethod { get; }
        IChannelTaskArgs GetChannelArgs();
    }
}