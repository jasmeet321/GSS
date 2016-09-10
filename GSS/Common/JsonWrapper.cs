using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSS.Common
{
    public class JsonWrapper
    {
        #region Constructors
        /// <summary>
        /// Wrapper to return server response
        /// </summary>
        /// <param name="IsSuccess"> if the operation performed is successfull or not </param>
        /// <param name="PostMessage"> msg to be sent to client side </param>
        public JsonWrapper(Boolean IsSuccess, String PostMessage)
        {
            this.IsSuccess = IsSuccess;
            this.PostMessage = PostMessage;
            this.PostData = "";
            this.ErrorType = "";
            this.MessageType = "";
            this.SuccessUrl = "";
        }
        public JsonWrapper(Boolean IsSuccess, String PostMessage, String MessageType, String SuccessUrl)
        {
            this.IsSuccess = IsSuccess;
            this.PostMessage = PostMessage;
            this.MessageType = MessageType;
            this.SuccessUrl = SuccessUrl;
            this.PostData = "";
            this.ErrorType = "";
        }
        /// <summary>
        /// Wrapper to return server response
        /// </summary>
        /// <param name="IsSuccess"> if the operation performed is successfull or not </param>
        /// <param name="PostMessage"> msg to be sent to client side </param>
        ///  /// <param name="PostData"> Data to be sent to client side </param>
        public JsonWrapper(Boolean IsSuccess, String PostMessage, String PostData)
        {
            this.IsSuccess = IsSuccess;
            this.PostMessage = PostMessage;
            this.PostData = PostData;
            this.ErrorType = "";
            this.MessageType = "";
            this.SuccessUrl = "";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Tells whether the operation has been successful or not.
        /// </summary>
        public Boolean IsSuccess
        { get; set; }

        /// <summary>
        /// This gives the post message sent by the mehtod.
        /// </summary>
        public string PostMessage
        { get; set; }

        /// <summary>
        /// This gives any post data server wants to send to client.
        /// </summary>
        public string PostData
        { get; set; }


        /// <summary>
        /// This gives any post data server wants to send to client.
        /// </summary>
        public string ErrorType
        { get; set; }

        /// <summary>
        /// This gives any post data server wants to send to client.
        /// </summary>
        public string MessageType
        { get; set; }

        /// <summary>
        /// This gives any post data server wants to send to client.
        /// </summary>
        public string SuccessUrl
        { get; set; }
        #endregion
    }

    public enum ErrorType
    {
        ServerError,
        ImageDimensionError,
        FileTypeError
    }

    public enum HolidayType
    {
        Normal = 1,
        Special = 2,
        HolidayInWeekends = 3
    }
}