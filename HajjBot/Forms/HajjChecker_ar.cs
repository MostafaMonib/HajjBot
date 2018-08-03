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
    public enum ConfirmationAR
    {
        نعم,
        لا,
    }

    public enum PaymentTypeAR
    { فيزا, كاش };


    [Serializable]
    public class HajjChecker_ar
    {
        [Prompt("ماهو اسمك؟ {||}")]
        public string Name { get; set; }

        [Prompt("هل حجيت من قبل؟ {||}")]
        public ConfirmationAR? HajjBefore { get; set; }

        [Prompt("هل تعلم ماهي شروط الحج؟ {||}")]
        public ConfirmationAR? HajjConditions;

        [Prompt("هل انت بالغ؟ {||}")]
        public ConfirmationAR? AreYouAdult { get; set; }

        [Prompt("ماهي ميزانيتك؟ {||}")]
        [Numeric(1, 10000000)]
        public Int32 Budget { get; set; }

        [Prompt("هل تعاني من مشاكل صحية؟ {||}")]
        public ConfirmationAR? HealthProblems { get; set; }

        [Prompt("من فضلك قم بتحديد طريقة الدفع؟ {||}")]
        public PaymentTypeAR? PaymentType;

        [Prompt("هل تريد التأكيد و الاستمرار؟ {||}")]
        public ConfirmationAR? Confirmation;

        public static IForm<HajjChecker_ar> BuildForm()
        {
            var newForm = new FormBuilder<HajjChecker_ar>()
                    .Message("السلام عليكم, انت فى المكان المناسب الحج بوت سيساعدك في رحلتك الروحانية")
                    //.Field(nameof(HajjBefore))
                    .Field(nameof(HajjConditions), validate: ValidateHajjConditionAsync)
                    .OnCompletion(async (context, state) =>
                    {
                        Common.CommonConversation.CurruntDialogContext = context;

                        await context.PostAsync($@"تم تأكيد عمليتك شاكرين لك, ونتمنى لك حجا مبرور وسعيا مشكور! {state.HajjBefore}");
                    })
                    .Message("")
                    .Build();

            return newForm;
        }

        private static async Task<ValidateResult> ValidateHajjConditionAsync(HajjChecker_ar state, object response)
        {
            var result = new ValidateResult();
            var confirm = Enum.GetName(typeof(ConfirmationAR), (ConfirmationAR)response);
            if (confirm == "نعم")
            {
                result.IsValid = true;
                result.Value = response;
                result.Feedback = "هل أنت مسلم وبالغ؟";
            }
            else
            {
                result.IsValid = false;
                result.Value = false;
                //await new HajjHelper().Reset(null);
                result.Feedback = "شكرا لك ولا يمكنك اكمال العملية ";
                
            }
            return await Task.FromResult(result);
            
        }
        
    }



}