﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IStoreTask.cs
// Roman, 2015-04-07 2:57 PM

using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Store.Operations.Tasks
{
    public interface IStoreTask : IAbstractTask
    {
        string StoreId { get; set; }
    }
}