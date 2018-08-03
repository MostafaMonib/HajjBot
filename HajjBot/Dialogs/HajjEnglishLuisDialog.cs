namespace HajjBot.Dialogs {     using System;     using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;     using System.Threading.Tasks;     using System.Web;     using Microsoft.Bot.Builder.Dialogs;     using Microsoft.Bot.Builder.FormFlow;     using Microsoft.Bot.Builder.Luis;     using Microsoft.Bot.Builder.Luis.Models;     using Microsoft.Bot.Connector;
    using HajjBot.Forms;
    using HajjBot.Helper;
    using Newtonsoft.Json;

    [LuisModel("91094df2-65ad-41db-a09b-6c9a05d1fdc4", "57b03824355c428ba5232efdfdfe0c52", LuisApiVersion.V2)]
    [Serializable]     public class HajjEnglishLuisDialog : LuisDialog<object>     {
        //public HajjEnglishLuisDialog() : base(new LuisService(new LuisModelAttribute(
        //    ConfigurationManager.AppSettings["LuisAppId"],
        //    ConfigurationManager.AppSettings["LuisAPIKey"],
        //    domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        //{
        //}

        [LuisIntent("")]         public async Task None(IDialogContext context, LuisResult result)         {             await context.PostAsync("I can't understand!?, Please repeat what you said?");             context.Wait(MessageReceived);         }

        [LuisIntent("EmergencyAmbulance")]         public async Task EmergencyAmbulanceIntent(IDialogContext context, LuisResult result)         {
            var form = new FormDialog<AmbulanceForm>(new AmbulanceForm(), AmbulanceForm.BuildForm, FormOptions.PromptInStart, null);
            context.Call(form, LastDialog);
         }

        [LuisIntent("EmergencyHelp")]         public async Task EmergencyHelpIntent(IDialogContext context, LuisResult result)         {

            var form = new FormDialog<HelpForm>(new HelpForm(), HelpForm.BuildForm, FormOptions.PromptInStart, null);
            context.Call(form, ChoosUserInput);
        }

        private async Task LastDialog(IDialogContext context, IAwaitable<object> result)
        {
            string fname = string.Empty;

            try
            {
                var message = await result as Activity;
                string data = message.ChannelData.ToString();
                TelegramData myClass = JsonConvert.DeserializeObject<TelegramData>(data);
                fname = myClass.message.from.first_name;
            }
            catch (Exception ex)
            {
            }

            await context.PostAsync($"Thanks! for using Hajj Bot!... {fname}");
        }

        #region Test
        private async Task ChoosUserInput(IDialogContext context, IAwaitable<HelpForm> result)
        {
            var dialog = await result as HelpForm;

            if (dialog?.Help != null)
            {
                if (dialog.Help == EmergencyHelpTypes.Ambulance)
                {
                    var form = new FormDialog<AmbulanceForm>(new AmbulanceForm(), AmbulanceForm.BuildForm, FormOptions.PromptInStart, null);
                    context.Call(form, LastDialog);

                }
                if (dialog.Help == EmergencyHelpTypes.Fire)
                {
                    var form = new FormDialog<FireForm1>(new FireForm1(), FireForm1.BuildForm, FormOptions.PromptInStart, null);
                    context.Call(form, ToFireForm2Async);

                }
                else if (dialog.Help == EmergencyHelpTypes.Police)
                {
                    var form = new FormDialog<PoliceForm1>(new PoliceForm1(), PoliceForm1.BuildForm, FormOptions.PromptInStart, null);
                    context.Call(form, LastDialog);

                }
                else if (dialog.Help == EmergencyHelpTypes.Other)
                {
                    var form = new FormDialog<EmergencyOtherForm>(new EmergencyOtherForm(), EmergencyOtherForm.BuildForm, FormOptions.PromptInStart, null);
                    context.Call(form, LastDialog);

                }

            }
        }

        private async Task ToFireForm2Async(IDialogContext context, IAwaitable<FireForm1> result)
        {
            var d = await result as FireForm1;

            if (d?.ConfirmInjuries == ConfirmationEN.Yes)
            {
                var form = new FormDialog<FireForm2>(new FireForm2(), FireForm2.BuildForm, FormOptions.PromptInStart, null);
                context.Call(form, ToFireForm3Async);
            }
            else
            {
                await new HajjHelper().Reset(activity: Common.CommonConversation.CurrentActivity);
            }
           
        }

        private async Task ToFireForm3Async(IDialogContext context, IAwaitable<FireForm2> result)
        {
            var d = await result as FireForm2;

            if (d?.Confirm == ConfirmationEN.Yes)
            {
                var form = new FormDialog<FireForm3>(new FireForm3(), FireForm3.BuildForm, FormOptions.PromptInStart, null);
                context.Call(form, LastDialog);
            }
            else
            {
                await new HajjHelper().Reset(activity: Common.CommonConversation.CurrentActivity);
            }
        }

        #endregion

        [LuisIntent("EmergencyFire")]         public async Task EmergencyFireIntent(IDialogContext context, LuisResult result)         {
            var form = new FormDialog<FireForm1>(new FireForm1(), FireForm1.BuildForm, FormOptions.PromptInStart, null);
            context.Call(form, ToFireForm2Async);
        }           [LuisIntent("EmergencyOther")]         public async Task EmergencyOtherIntent(IDialogContext context, LuisResult result)         {
            var form = new FormDialog<EmergencyOtherForm>(new EmergencyOtherForm(), EmergencyOtherForm.BuildForm, FormOptions.PromptInStart, null);
            context.Call(form, LastDialog);
        }           [LuisIntent("EmergencyPolice")]         public async Task EmergencyPoliceIntent(IDialogContext context, LuisResult result)         {
            var form = new FormDialog<PoliceForm1>(new PoliceForm1(), PoliceForm1.BuildForm, FormOptions.PromptInStart, null);
            context.Call(form, LastDialog);
        }           [LuisIntent("Greeting")]         public async Task GreetingIntent(IDialogContext context, LuisResult result)         {             await context.PostAsync("You are in the right place. Hajj Bot will help you in your spiritual journey? ");              context.Wait(MessageReceived);         }        } }