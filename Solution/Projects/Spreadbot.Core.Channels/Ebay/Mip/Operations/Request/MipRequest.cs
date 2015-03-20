﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipRequest.cs
// romak_000, 2015-03-20 13:56

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Request
{
    public class MipRequest
    {
        public MipRequest( MipFeedHandler mipFeedHandler, Guid requestId )
        {
            MipFeedHandler = mipFeedHandler;
            Id = requestId;
        }

        public Guid Id { get; set; }
        public MipFeedHandler MipFeedHandler { get; set; }

        public static Guid GenerateId()
        {
            return Guid.NewGuid();
        }

        public static Guid GenerateTestId()
        {
            return new Guid();
        }

        public static bool VerifyRequestId( Guid requestId )
        {
            Guid guid;
            return Guid.TryParse( requestId.ToString(), out guid );
        }

        public string FileNamePrefix()
        {
            return string.Format( "{0}.{1}", MipFeedHandler.Name, Id );
        }
    }
}