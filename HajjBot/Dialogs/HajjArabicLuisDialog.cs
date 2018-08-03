namespace HajjBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;

    [LuisModel("66bc756c-65e8-410b-9f1a-3f165c44b667", "36105a5ca2ad41f3864a6c89b579f4bc")]
    [Serializable]
    public class HajjArabicLuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("لا شيء");

            context.Wait(MessageReceived);
        }

        [LuisIntent("اسعاف")]
        public async Task cc(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("اسعاف");

            context.Wait(MessageReceived);
        }

        [LuisIntent("EmergencyAmbulance")]
        public async Task EmergencyAmbulanceIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("EmergencyAmbulance انتنت");

            context.Wait(MessageReceived);
        }


        [LuisIntent("EmergencyFire")]
        public async Task EmergencyFireIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("EmergencyFire انتنت");

            context.Wait(MessageReceived);
        }


        [LuisIntent("EmergencyOther")]
        public async Task EmergencyOtherIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("EmergencyOther انتنت");

            context.Wait(MessageReceived);
        }


        [LuisIntent("EmergencyPolice")]
        public async Task EmergencyPoliceIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("بوليس");

            context.Wait(MessageReceived);
        }


        [LuisIntent("hajj")]
        public async Task hajjIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("hajj انتنت");

            context.Wait(MessageReceived);
        }


        [LuisIntent("Greeting")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Greeting, How can I help you?");

            context.Wait(MessageReceived);
        }



    }
}