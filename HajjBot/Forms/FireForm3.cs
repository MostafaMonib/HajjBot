using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajjBot.Forms
{
    [Serializable]
    public class FireForm3
    {
        [Prompt("How many injuries ?  {||}")]
        public int? NumberOfInjuries { get; set; }

        public static IForm<FireForm3> BuildForm()
        {
            var newForm = new FormBuilder<FireForm3>()
                    .Field(nameof(NumberOfInjuries))
                    .Message("We have acquired your location, help is on the way")
                    .Build();

            return newForm;
        }
    }
}