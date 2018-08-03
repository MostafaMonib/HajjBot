using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajjBot.Forms
{
    [Serializable]
    public class EmergencyOtherForm
    {
        [Prompt("What is the emergency? {||}")]
        public string Injuries { get; set; }

        public static IForm<EmergencyOtherForm> BuildForm()
        {
            var newForm = new FormBuilder<EmergencyOtherForm>()
                    .Message("Your location has been acquired!, coucerued autherities will get in touch.")
                    .Build();

            return newForm;
        }
    }
}