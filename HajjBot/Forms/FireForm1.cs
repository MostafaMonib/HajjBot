using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajjBot.Forms
{
    [Serializable]
    public class FireForm1
    {
        [Prompt("How severe is the situation? {||}")]
        public AmbulanceTypes? AmbulanceTypes { get; set; }

        [Prompt("are there injuries? {||}")]
        public ConfirmationEN? ConfirmInjuries { get; set; }

        public static IForm<FireForm1> BuildForm()
        {
            var newForm = new FormBuilder<FireForm1>()
                .Field(nameof(ConfirmInjuries))    
                .Message("We wish you a good Hajja and a great effort! 😊")
                    .Build();

            return newForm;
        }

    }
}