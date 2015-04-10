// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.imp.IAbstractTask.cs
// Roman, 2015-04-10 1:28 PM

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        private IEnumerable< IAbstractTask > _abstractSubTasks;

        public abstract TaskStatus GetStatusCode();

        public abstract IAbstractResponse AbstractResponse { get; set; }

        public virtual IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return _abstractSubTasks ?? ( _abstractSubTasks = new List< IAbstractTask >() ); }
        }
    }
}