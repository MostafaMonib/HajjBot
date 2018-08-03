using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Microsoft.Bot.Connector;
using HajjBot.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HajjBot.Forms
{
    public enum ConfirmationEN
    {
        Yes,
        No,
    }

    public enum PaymentTypeEN
    {
        Visa,
        Cash
    };


    [Serializable]
    public class HajjChecker_en
    {
        [Prompt("What is your Name? {||}")]
        public string Name { get; set; }

        [Prompt("Is this your first Hajj? {||}")]
        public ConfirmationEN? HajjBefore { get; set; }

        [Prompt("Did you know the conditions to perform Hajj? {||}")]
        public ConfirmationEN? HajjConditions;

        [Prompt("Are you an Adult? {||}")]
        public ConfirmationEN? AreYouAdult { get; set; }

        [Prompt("What is your budget? {||}")]
        [Numeric(1, 10000000)]
        public Int32 Budget { get; set; }

        [Prompt("Do you have any serious health conditions? {||}")]
        public ConfirmationEN? HealthProblems { get; set; }

        [Prompt("Please, select your payment method? {||}")]
        public PaymentTypeEN? PaymentTypeEN;

        [Prompt("Did you want to confirm and countinue? {||}")]
        public ConfirmationEN? ConfirmationEN;

        public static IForm<HajjChecker_en> BuildForm()
        {
            var newForm = new FormBuilder<HajjChecker_en>()
                    //.Message("Alsallam Alykom, You are in the right place. Hajj Bot will help you in your spiritual journey?")
                    //.Field(nameof(HajjBefore))
                    .Field(nameof(HajjConditions), validate: ValidateHajjConditionAsync)
                    .OnCompletion(async (context, state) =>
                    {
                        Common.CommonConversation.CurruntDialogContext = context;

                        await context.PostAsync($@"Thank you, and we wish you a good Hajja and a great effort! {state.Name}");
                    })
                    .Message("")
                    .Build();

            return newForm;
        }

        private static async Task<ValidateResult> ValidateHajjConditionAsync(HajjChecker_en state, object response)
        {
            var result = new ValidateResult();
            var confirm = Enum.GetName(typeof(ConfirmationEN), (ConfirmationEN)response);
            if (confirm == "Yes")
            {
                result.IsValid = true;
                result.Value = response;
                result.Feedback = "Are you Adult?";
            }
            else
            {
                result.IsValid = false;
                result.Value = false;
                //await new HajjHelper().Reset(null);
                result.Feedback = "Thanks, you cannot complete this process";
                
            }
            return await Task.FromResult(result);
            
        }
        
    }



}