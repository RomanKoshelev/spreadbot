using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Response
    {
        // ===================================================================================== []
        // Public
        public Response(bool isSucces = false, StatusCode statusCode = StatusCode.Unknown, string statusDescription = "",
            params object[] args)
        {
            IsSuccess = isSucces;
            StatusCode = statusCode;
            StatusDescription = statusDescription.SafeFormat(args);
        }

        public StatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public object Result { get; set; }

        public void Check()
        {
            // Todo: USe FailedStatusDescription
            if (!IsSuccess)
                throw new Exception(
                    string.Format("StatusCode=[{0}] StatusDescription=[{1}]",
                        StatusCode,
                        StatusDescription)
                    );
        }

        public bool IsSuccess { get; private set; }
    }
}