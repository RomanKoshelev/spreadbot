﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.System
// Dispatcher.pvt.ChannelTasks.cs

using System.Collections.Generic;
using MoreLinq;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.System.Dispatcher
{
    public partial class Dispatcher
    {
        // ===================================================================================== []
        // DoRunChannelTask
        private void DoRunChannelTask( IChannelTask task )
        {
            if( task.GetStatusCode() != TaskStatus.Todo ) {
                throw new SpreadbotTaskException( "Task was already run [{0}]", task );
            }

            switch( task.ChannelMethod ) {
                case ChannelMethod.Submit :
                    FindChannel( task.ChannelId ).RunSubmissionTask( task );
                    break;
                default :
                    throw new SpreadbotTaskException( "Unexpected task operation [{0}]", task.ChannelMethod );
            }
        }

        // ===================================================================================== []
        // DoProceedChannelTasks
        private void DoProceedChannelTasks( IEnumerable< IChannelTask > tasks )
        {
            tasks.ForEach( DoProceedChannelTask );
        }

        // --------------------------------------------------------[]
        private void DoProceedChannelTask( IChannelTask task )
        {
            if( task.GetStatusCode() != TaskStatus.InProgress ) {
                throw new SpreadbotTaskException( "Task is not In-Process [{0}]", task );
            }

            FindChannel( task.ChannelId ).ProceedTask( task );
        }
    }
}