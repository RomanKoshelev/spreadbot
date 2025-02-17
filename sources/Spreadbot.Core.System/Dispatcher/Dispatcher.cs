﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.System
// Dispatcher.cs

using System;
using System.Collections.Generic;
using System.Threading;
using MoreLinq;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Amazon.Adapter;
using Spreadbot.Core.Channels.Ebay.Adapter;

namespace Spreadbot.Core.System.Dispatcher
{
    public partial class Dispatcher
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy< Dispatcher > LazyInstance =
            new Lazy< Dispatcher >( CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        private static Dispatcher CreateInstance()
        {
            return new Dispatcher();
        }

        // --------------------------------------------------------[]
        public static Dispatcher Instance
        {
            get { return LazyInstance.Value; }
        }

        // --------------------------------------------------------[]
        public Dispatcher()
        {
            RegisterChannel( EbayAdapter.Instance );
            RegisterChannel( AmazonAdapter.Instance );
        }

        // ===================================================================================== []
        // ChannelTasks
        public void RunChannelTask( IChannelTask task )
        {
            AssertChannelRegistered( task.ChannelId );
            DoRunChannelTask( task );
        }

        // --------------------------------------------------------[]
        public void RunChannelTasks( IEnumerable< IChannelTask > tasks )
        {
            tasks.ForEach( RunChannelTask );
        }

        // --------------------------------------------------------[]
        public void ProceedChannelTasks( IEnumerable< IChannelTask > tasks )
        {
            DoProceedChannelTasks( tasks );
        }
    }
}