﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipStatusCode.cs
// romak_000, 2015-03-19 15:49

namespace Spreadbot.Core.Connectors.Ebay.Mip.Operations.StatusCode
{
    public enum MipStatusCode
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFail,
        SendZippedFeedFolderSuccess,
        SendZippedFeedFolderFail,
        SendZippedFeedSuccess,
        SendZippedFeedFail,
        ZipFeedSuccess,
        ZipFeedFail,
        FindRequestSuccess,
        FindRequestFail,
        FindRemoteFileSuccess,
        FindRemoteFileFail,
        GetRequestStatusSuccess,
        GetRequestStatusFail
    }
}