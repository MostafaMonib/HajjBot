using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajjBot.Forms
{
    [Serializable]
    public class PoliceForm1
    {
        [Prompt("Could you tell us what the problem is?   {||}")]
        public string Complaint { get; set; }

        public static IForm<PoliceForm1> BuildForm()
        {
            var newForm = new FormBuilder<PoliceForm1>()
                    .AddRemainingFields()
                    .Message("Your location has been determined , We are on our way to you")
                    .Build();

            return newForm;
        }
    }
}