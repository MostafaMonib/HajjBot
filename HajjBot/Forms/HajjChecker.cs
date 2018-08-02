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
    public enum Confirmation
    {
        نعم,
        لا,
    }

    public enum PaymentType
    { فيزا, كاش };


    [Serializable]
    public class HajjChecker
    {
        [Prompt("ماهو اسمك؟ {||}")]
        public string Name { get; set; }

        [Prompt("هل حجيت من قبل؟ {||}")]
        public Confirmation? HajjBefore { get; set; }

        [Prompt("هل تعلم ماهي شروط الحج؟ {||}")]
        public Confirmation? HajjConditions;

        [Prompt("هل انت بالغ؟ {||}")]
        public Confirmation? AreYouAdult { get; set; }

        [Prompt("ماهي ميزانيتك؟ {||}")]
        [Numeric(1, 10000000)]
        public Int32 Budget { get; set; }

        [Prompt("هل تعاني من مشاكل صحية؟ {||}")]
        public Confirmation? HealthProblems { get; set; }

        [Prompt("من فضلك قم بتحديد طريقة الدفع؟ {||}")]
        public PaymentType? PaymentType;

        [Prompt("هل تريد التأكيد و الاستمرار؟ {||}")]
        public Confirmation? Confirmation;

        public static IForm<HajjChecker> BuildForm()
        {
            var newForm = new FormBuilder<HajjChecker>()
                    .Message("السلام عليكم, انت فى المكان المناسب الحج بوت سيساعدك في رحلتك الروحانية")
                    //.Confirm("Do you want to countinue booking an hotel? (Yes/No)")
                    .Field(nameof(HajjBefore))
                    //.Field(nameof(RoomType))
                    .Field(nameof(HajjConditions))
                    .Field(nameof(HajjConditions))
                    .Field(nameof(AreYouAdult))
                    .Field(nameof(Budget))
                    .Field(nameof(HealthProblems))
                    .Field(nameof(PaymentType))
                    .AddRemainingFields()
                    .Field(nameof(Confirmation), validate: ValidateConfirmation)
                    .OnCompletion(async (context, state) =>
                    {
                        Common.CommonConversation.CurruntDialogContext = context;

                        await context.PostAsync($@"تم تأكيد عمليتك شاكرين لك, ونتمنى لك حجا مبرور وسعيا مشكور! {state.HajjBefore}");
                    })
                    .Message("")
                    .Build();

            return newForm;
        }

        private static Task<ValidateResult> ValidateConfirmation(HajjChecker state, object response)
        {
            var result = new ValidateResult();
            var confirm = Enum.GetName(typeof(Confirmation), (Confirmation)response);
            // Do the checks here whether the time is available. 
            // Hard coded for demo purposes
            if (confirm == "نعم")
            {
                result.IsValid = true;
                result.Value = response;
            }
            else
            {
                result.IsValid = false;
                result.Value = response;
            }
            return Task.FromResult(result);
        }
    }



}