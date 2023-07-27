using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;

namespace _0.Framework.Application.Sms
{
    public class SmsService : ISmsService
    {
        public async Task Send(string number, string message)
        {
            var sms = new SmsIr("Hkg9TfUUeaPRdrQgAa40pvxJKMHOXxfa1S9td6KFsEhDwxFfIq0EHBAiKbZCPdmf");
            var result = await sms.BulkSendAsync(30007732901291, message, new string[] { number });
            if (result.Status != 1)
            {
                await sms.BulkSendAsync(30007732901291, message, new string[] { number });
            }
        }

        public async Task SendConfirmOrder(string number, string name, string issueTrackingNo, string orderId)
        {
            var sms = new SmsIr("Hkg9TfUUeaPRdrQgAa40pvxJKMHOXxfa1S9td6KFsEhDwxFfIq0EHBAiKbZCPdmf");
            var result = await sms.VerifySendAsync(
                number,
                146636,
                new VerifySendParameter[]
                {
                    new VerifySendParameter("DATE",DateTime.Now.ToFarsi() ),
                    new VerifySendParameter("NAME",name ),
                    new VerifySendParameter("ORDERID",orderId ),
                    new VerifySendParameter("ISSUETRACKINGNO",issueTrackingNo )
                });

            if (result.Status != 1)
            {
                await sms.VerifySendAsync(
                    number,
                    146636,
                    new VerifySendParameter[]
                    {
                        new VerifySendParameter("DATE",DateTime.Now.ToFarsi() ),
                        new VerifySendParameter("NAME",name ),
                        new VerifySendParameter("ORDERID",orderId ),
                        new VerifySendParameter("ISSUETRACKINGNO",issueTrackingNo )
                    });
            }
        }
    }
}