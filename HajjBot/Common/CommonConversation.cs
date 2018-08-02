using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HajjBot.Common
{
    public static class CommonConversation
    {
        public static ConnectorClient Connector { get; set; }
        public static Activity CurrentActivity { get; set; }

        public static IDialogContext CurruntDialogContext { get; set; }

        #region RoomTypes
        public const string Singe = "http://static.lodgeic.com/res/images/thew14umb/rooms/single/single-room-1.jpg";
        public const string Double = "https://www.claytonhotelbelfast.com/wp-content/uploads/Standard-Family-bedroom-Resized1.jpg";
        public const string Twin = "https://www.arlington.ie/upload/slide_images/twin-room-2.jpg";
        public const string Duplex = "https://www.akkaalinda.com/userfiles/WebPageFile/c463Alinda-Room-Dublex-02.jpg";
        public const string Suite = "http://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/70/2016/11/22095538/suite-novotel-bangkok-ploenchit-sukhumvit-1-724x357.jpeg";
        #endregion
    }

}