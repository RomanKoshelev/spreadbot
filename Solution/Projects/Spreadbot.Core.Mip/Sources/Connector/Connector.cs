﻿using System;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // SendFeed
        public static Response SendFeed(Feed feed)
        {
            var reqId = Request.GenerateRequestId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception e)
            {
                return new Response(false, StatusCode.SendFeedFail, e.Message);
            }
            return new Response(true, StatusCode.SendFeedSuccess)
            {
                RequestId = reqId,
                StatusDescription = string.Format("StatusCode=[{0}] RequestId=[{1}]", StatusCode.SendFeedSuccess, reqId)
            };
        }
    }
}