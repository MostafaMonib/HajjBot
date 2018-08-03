using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajjBot.Forms
{
    [Serializable]
    public class FireForm2
    {
        [Prompt("Can you tell me how  many injuries are there ?  {||}")]
        public ConfirmationEN? Confirm { get; set; }

        public static IForm<FireForm2> BuildForm()
        {
            var newForm = new FormBuilder<FireForm2>()
                    .Field(nameof(Confirm))
                    .Message("")
                    .Build();

            return newForm;
        }
    }
}