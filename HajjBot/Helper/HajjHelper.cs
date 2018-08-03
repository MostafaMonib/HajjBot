using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HajjBot.Helper
{
    public class HajjHelper
    {
        public async Task<Stream> GetAudioStream(ConnectorClient connector, Attachment audioAttachment)
        {
            using (var httpClient = new HttpClient())
            {
                // The Skype attachment URLs are secured by JwtToken,
                // you should set the JwtToken of your bot as the authorization header for the GET request your bot initiates to fetch the image.
                // https://github.com/Microsoft/BotBuilder/issues/662
                var uri = new Uri(audioAttachment.ContentUrl);
                if (uri.Host.EndsWith("skype.com") && uri.Scheme == "https")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(connector));
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                }

                return await httpClient.GetStreamAsync(uri);
            }
        }
        private static async Task<string> GetTokenAsync(ConnectorClient connector)
        {
            var credentials = connector.Credentials as MicrosoftAppCredentials;
            if (credentials != null)
            {
                return await credentials.GetTokenAsync();
            }

            return null;
        }
        public async Task Reset(Activity activity)         {
            //await activity.GetStateClient().BotState
            //    .DeleteStateForUserWithHttpMessagesAsync(activity.ChannelId, activity.From.Id);

            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))             {                 var botData = scope.Resolve<IBotData>();                 await botData.LoadAsync(default(CancellationToken));                 var stack = scope.Resolve<IDialogStack>();                 stack.Reset();                 await botData.FlushAsync(default(CancellationToken));             }

            //var client = new ConnectorClient(new Uri(activity.ServiceUrl));
            //var clearMsg = activity.CreateReply();
            //clearMsg.Text = $"Reseting everything for conversation: {activity.Conversation.Id}";
            //await client.Conversations.SendToConversationAsync(clearMsg);
        } 

    }
}