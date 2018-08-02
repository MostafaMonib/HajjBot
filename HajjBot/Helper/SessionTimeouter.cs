using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace BotSession
{
    public class SessionTimeouter
    {
        public static Dictionary<string, SessionTimeouter> Dictionary
        {

            get
            {
                return _dictionary;
            }

            set
            {
                Dictionary = _dictionary;
            }

        }

        private static Dictionary<string, SessionTimeouter> _dictionary = new Dictionary<string, SessionTimeouter>();

        private System.Timers.Timer sTimer = new System.Timers.Timer();
        private Activity _activity = null;
        private string _key = null;
        private double _timeout = 0;
        public SessionTimeouter(string Key, Activity activity, double millisecondsTime = 50000)
        {

            sTimer = new System.Timers.Timer
            {
                Enabled = false
            };

            _key = Key;
            _activity = activity;
            _timeout = millisecondsTime;
        }

        public void Reset()
        {
            //this.sTimer.Enabled = false;
            //this.sTimer.Stop();

            sTimer.Interval = _timeout;  // restart the timer
        }
        private void StartTimer(double millisecondsTime)
        {
            sTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            sTimer.Interval = millisecondsTime;
            sTimer.Enabled = true;

            sTimer.Start();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Task.Run(async () =>
            {
                // Do any async anything you need here without worry
                await ResetActivity();

            }).GetAwaiter().GetResult();

            sTimer.Enabled = false;
        }

        private async Task ResetActivity()
        {
            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, _activity))
            {
                var botData = scope.Resolve<IBotData>();
                await botData.LoadAsync(default(CancellationToken));
                var stack = scope.Resolve<IDialogStack>();
                stack.Reset();
                await botData.FlushAsync(default(CancellationToken));
            }

            var client = new ConnectorClient(new Uri(_activity.ServiceUrl));
            var clearMsg = _activity.CreateReply();
            clearMsg.Text = $"Reseting everything for conversation: {_activity.Conversation.Id}";
            await client.Conversations.SendToConversationAsync(clearMsg);

            Dictionary.Remove(this._key);
        }

        public void SetNewSession(double millisecondsTime = 50000)
        {
            _timeout = millisecondsTime;

            StartTimer(millisecondsTime);
        }
    }
}