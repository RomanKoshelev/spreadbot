﻿using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IStoreTask: ITask
    {
        IStore Store { get; set; }
    }
}