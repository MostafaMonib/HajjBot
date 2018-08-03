using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HajjBot.Helper;
using Newtonsoft.Json;

namespace HajjBot.Forms
{
    public enum EmergencyHelpTypes
    {
        Police,
        Fire,
        Ambulance,
        Other
    };

    [Serializable]
    public class HelpForm
    {
        [Prompt("Please, What kind of emergency help you want? {||}")]
        public EmergencyHelpTypes? Help { get; set; }

        public static IForm<HelpForm> BuildForm()
        {
            var newForm = new FormBuilder<HelpForm>()
                    //.Message("Alsallam Alykom, You are in the right place. Hajj Bot will help you in your Hajj journey?")
                    .Field(nameof(Help))
                    .Message("")
                    .Build();

            return newForm;
        }

        private static async Task<ValidateResult> ValidateHelpAsync(HelpForm state, object response)
        {
            var result = new ValidateResult();
            var helpKind = Enum.GetName(typeof(EmergencyHelpTypes), (EmergencyHelpTypes)response);
            if (helpKind == "Police")
            {
                result.IsValid = true;
                result.Value = response;

            }
            else if (helpKind == "Ambulance")
            {
                result.IsValid = true;
                result.Value = response;

            }
            else if (helpKind == "Fire")
            {
                result.IsValid = true;
                result.Value = response;

            }
            else if (helpKind == "Other")
            {
                result.IsValid = true;
                result.Value = response;

            }
            else
            {
                result.IsValid = false;
                result.Value = false;
                //await new HajjHelper().Reset(null);
                //result.Feedback = "Thanks, you cannot complete this process";
            }

            return await Task.FromResult(result);
        }
    }
}