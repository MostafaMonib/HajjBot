using Autofac; using BotSession; using HajjBot; using Microsoft.Bot.Builder.Dialogs; using Microsoft.Bot.Builder.Dialogs.Internals; using Microsoft.Bot.Connector; using HajjBot.Helper; using SpeechToText.Services; using System; using System.Linq; using System.Net; using System.Net.Http; using System.Threading; using System.Threading.Tasks; using System.Web.Http;  namespace HajjBot {     [BotAuthentication]     public class MessagesController : ApiController     {         /// <summary>         /// POST: api/Messages         /// Receive a message from a user and reply to it         /// </summary>         ///          public async Task<HttpResponseMessage> Post([FromBody]Activity activity)         {             Common.CommonConversation.CurrentActivity = activity;             Common.CommonConversation.Connector = new ConnectorClient(new Uri(activity.ServiceUrl));              if (activity.Type == ActivityTypes.Message)             {               
                    var oggAudioAttachment = activity.Attachments?.FirstOrDefault(a => a.ContentType.Equals("audio/ogg") || a.ContentType.Equals("application/octet-stream"));

                    if (oggAudioAttachment != null)
                    {
                        var connector = Common.CommonConversation.Connector;
                        MicrosoftCognitiveSpeechService speechService = new MicrosoftCognitiveSpeechService();

                        var stream = await new HajjHelper().GetAudioStream(connector, oggAudioAttachment);

                        string text = await Task.Factory.StartNew(
                            async () => await speechService.GetTextFromAudioAsync(stream)).Result;


                        Activity reply = activity.CreateReply(
                            $"Did you say?! ...    {text}");

                        await connector.Conversations.ReplyToActivityAsync(reply);

                        text = text.Replace(".", string.Empty).Trim();

                        activity.Text = text;
                    }

                    var dictionary = SessionTimeouter.Dictionary;

                    var key = activity.ChannelId + activity.From.Id + activity.Conversation.Id;

                    SessionTimeouter timeouter = null;

                    int millisecondsTime = 1000 * 60 * 5;

                    if (dictionary.ContainsKey(key))
                    {
                        timeouter = dictionary[key];
                        timeouter.Reset();
                    }
                    else
                    {
                        timeouter = new SessionTimeouter(key, activity, millisecondsTime);
                        timeouter.SetNewSession(millisecondsTime);
                        dictionary.Add(key, timeouter);
                    }

                    string msg = activity.Text.ToLower().Trim();
                    if (msg == "start over" || msg == "exit" || msg == "quit" || msg == "done" || msg == "start again" || msg == "restart" || msg == "leave" || msg == "reset")
                    {
                        await new HajjHelper().Reset(activity);
                    }
                    else
                    {

                        //await Conversation.SendAsync(
                        //                               activity, () => new Dialogs.HajjEnglishLuisDialog().DefaultIfException()
                        //                            );

                        await Task.Factory.StartNew(async () => await Conversation.SendAsync(activity, () => new Dialogs.HajjEnglishLuisDialog().DefaultIfException()));

                        //await Task.Factory.StartNew(async () => await Conversation.SendAsync(activity, MakeDeviceOrderDialog));


                    }                               }             else             {                 await HandleSystemMessageAsync(activity);             }               var response = Request.CreateResponse(HttpStatusCode.OK);             return response;         }          private ConversationStarter GetConversationStarter(Activity message)         {             ConversationStarter cs = new ConversationStarter
            {
                toId = message.From.Id,
                toName = message.From.Name,
                fromId = message.Recipient.Id,
                fromName = message.Recipient.Name,
                serviceUrl = message.ServiceUrl,
                channelId = message.ChannelId,
                conversationId = message.Conversation.Id
            };              return cs;         }          private static IDialog<HajjBot.Forms.HajjChecker_en> MakeDeviceOrderDialog()         {             return Chain.From(() => Microsoft.Bot.Builder.FormFlow.FormDialog.FromForm(HajjBot.Forms.HajjChecker_en.BuildForm));         }          private async Task<Activity> HandleSystemMessageAsync(Activity message)         {             if (message.Type == ActivityTypes.DeleteUserData)             {                 // Implement user deletion here                 // If we handle user deletion, return a real message             }             else if (message.Type == ActivityTypes.ConversationUpdate)             {                 if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))                 {                     //var reply = message.CreateReply("اهلا و مرحبا بك في الحج بوت");                     var reply = message.CreateReply("Welcome to Hajj bot, your companian on this blessed journey");                      ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));                      await connector.Conversations.ReplyToActivityAsync(reply);                 }             }             else if (message.Type == ActivityTypes.ContactRelationUpdate)             {                 // Handle add/remove from contact lists                 // Activity.From + Activity.Action represent what happened             }             else if (message.Type == ActivityTypes.Typing)             {                 // Handle knowing tha the user is typing             }             else if (message.Type == ActivityTypes.Ping)             {             }              return null;         }      } }