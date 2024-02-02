using Microsoft.Azure.Cosmos;
using System.Net.Mail;
using System.Net;
using vscs2.CosmosDBServices;
using vscs2.Entities;
using vscs2.Interfaces;

namespace vscs2.Services
{
    public class OfficeService:IOfficeService
    {
        public ICosmosService _cosmosService;
        public OfficeService(ICosmosService cosmosService) { 
        _cosmosService = cosmosService;
        
        
        }  

        public async Task<CompanyUsers> LoginOfficeUser(string Email, string Password)
        {
            return await _cosmosService.LoginOfficeUser(Email, Password);
        }
        public async Task<List<Request>> SeeAllPendingrequests()
        {

            return await _cosmosService.SeeAllPendingrequests();
        }
        public async Task<List<Request>> SeeAllApproverequests()
        {
            return await _cosmosService.SeeAllApproverequests();
        }
        public async Task<List<Request>> SeeAllRejectedrequests()
        {
            return await _cosmosService.SeeAllRejectedrequests();
        }

        public async Task<Request> ApproveRejectRequest(string Trackingid, bool action_true_or_false)
        {
            var existingrequest = await _cosmosService.GetRequestByTrackingId(Trackingid);
            if (existingrequest != null)
            {

                existingrequest.Archieved = true;
                existingrequest.Active = false;
                await _cosmosService.ReplaceRequest(existingrequest, existingrequest.UId);

                existingrequest.Id = Guid.NewGuid().ToString();
                existingrequest.UId = existingrequest.UId;
                existingrequest.UpdatedBy = existingrequest.officerEmail;
                existingrequest.UpdatedByName = "mayur";
                existingrequest.UpdatedOn = DateTime.Now;
                existingrequest.Version = existingrequest.Version + 1;
                existingrequest.Active = true;
                existingrequest.Archieved = false;

                existingrequest.Name = existingrequest.Name;
                existingrequest.MoNo = existingrequest.MoNo;
                existingrequest.Email = existingrequest.Email.ToLower();
                existingrequest.officerEmail = existingrequest.Email.ToLower();
                existingrequest.Description = existingrequest.Description;
                existingrequest.Subject = existingrequest.Subject;
                existingrequest.Action = action_true_or_false;

                existingrequest.TrackingId = existingrequest.TrackingId;

                existingrequest = await _cosmosService.AddRequest(existingrequest);



                // email 
                string VEmail = existingrequest.Email;
                string oEmail = existingrequest.officerEmail;
                string body = existingrequest.Name + " " + existingrequest.Email;
                string subject;
                if (action_true_or_false == true)
                {
                    subject = "Request Approved";
                }
                else
                {
                    subject = "request Reject";
                }

                await _cosmosService.SendMail(body, subject, VEmail, oEmail); // call method from cosmosseervice

                // send mail code

                /*  try
                  {
                      // Replace these values with your actual SMTP server and credentials
                      string smtpServer = "smtp.gmail.com";
                      int smtpPort = 587; // Typically 587 or 25
                      string smtpUsername = "mfegade295@gmail.com";
                      string smtpPassword = "fuat zkve ucjd thcv";
                      // string smtpPassword = "zsji tzyg kohl neqt";
                      // Create a new SMTP client
                      using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                      {
                          smtpClient.UseDefaultCredentials = false;
                          smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                          smtpClient.EnableSsl = true; // Enable SSL if required

                          // Create a new email message
                          MailMessage mailMessage = new MailMessage();
                          mailMessage.From = new MailAddress(oEmail);
                          mailMessage.To.Add(VEmail);
                          mailMessage.To.Add("mayurfegade20001@gmail.com");// securitty guard 
                          mailMessage.Subject = "Request action";
                          mailMessage.Body = body;

                          // Send the email
                          smtpClient.Send(mailMessage);

                          // Console.WriteLine("Email sent successfully!");
                      }
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine(ex.ToString());
                  }
  */
                return existingrequest;
            }
            else return null;
        }
    }
}
