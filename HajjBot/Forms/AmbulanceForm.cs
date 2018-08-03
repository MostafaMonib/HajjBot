using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajjBot.Forms
{
    public enum AmbulanceTypes
    {
        Normal,
        Severe,
        Very_Servere,
    };

    [Serializable]
    public class AmbulanceForm
    {
        [Prompt("How severe is the situation? {||}")]
        public AmbulanceTypes? AmbulanceTypes { get; set; }

        [Prompt("Number of injuries? {||}")]
        public int? Injuries { get; set; }

        [Prompt("Can you specify age? {||}")]
        [Describe("Answer with 'No' to skip this question.")]
        [Optional]
        public int? Age { get; set; }

        public static IForm<AmbulanceForm> BuildForm()
        {
            var newForm = new FormBuilder<AmbulanceForm>()
                    .Message("Your location has been determined , We are on our way to you")
                    .Build();

            return newForm;
        }

    }
}