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
        Ssvere,
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
        public int? Age { get; set; }

        public static IForm<AmbulanceForm> BuildForm()
        {
            var newForm = new FormBuilder<AmbulanceForm>()
                    .Message("")
                    .Build();

            return newForm;
        }

    }
}