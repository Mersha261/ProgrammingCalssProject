using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ProgramingCalssProject.Models.Utillity
{
    public class SendSms
    {
        public string SendSmsViaRayeganSms(ActivationCodeModel model)
        {
            string username = model.username;
            string password = model.password;

            var client = new HttpClient();
            var obj = new
            {
                AccessHash = model.AccessHash,
                PatternId = model.PatternId,
                token1 = model.token1,
                token2 = model.token2,
                token3 = model.token3,
                Mobile = model.Mobile,
                UserGroupID = model.UserGroupID,
                SendDateInTimeStamp = model.SendDateInTimeStamp,
            };
            var url = "https://smspanel.trez.ir/api/smsApiWithPattern/SendCode";
            string plainText = username + ":" + password;
            string token = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");
            var objectStr = JsonConvert.SerializeObject(obj);
            var content = new StringContent(objectStr, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return "Ok";
        }
    }

    public class ActivationCodeModel
    {
        public string AccessHash { get; set; }  //example 1c738e0e-4f10-4299-bdaa-1cff6eb84908
        public string PatternId { get; set; } //example ffa78efa-58ad-41e5-a37e-63eb5c76cf89 
        public string token1 { get; set; } // example علی احمدی
        public string token2 { get; set; } // example سفارش 12
        public string token3 { get; set; } // example RayganSMS
        public string Mobile { get; set; } // آرایه ای از شماره موبایل ها برای ارسال پیام
        public string UserGroupID { get; set; } // example 09116665601
        public long SendDateInTimeStamp { get; set; } //حتما عدد 1 باشد 
        public string username { get; set; }
        public string password { get; set; }


    }
}





