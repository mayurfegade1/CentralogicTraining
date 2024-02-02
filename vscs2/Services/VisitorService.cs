using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net.Mail;
using System.Net;
using vscs2.CosmosDBServices;
using vscs2.DTO;
using vscs2.Entities;
using vscs2.Interfaces;

namespace vscs2.Services
{
    public class VisitorService:IVisitorService
    {
        public ICosmosService _cosmosService;
        public VisitorService(ICosmosService cosmosService) {


            _cosmosService = cosmosService;

        }

        public async Task<Visitor> VisitorRegistration(Visitor visitor)
        {
            //stp 2 Assign madetory fields
            visitor.Id = Guid.NewGuid().ToString();
            visitor.UId = visitor.Id;
            visitor.DocumentType = "visitor";
            visitor.CreatedBy = "mayur UId ";
            visitor.CreatedByName = "mayur";
            visitor.CreatedOn = DateTime.Now;
            visitor.UpdatedBy = "";
            visitor.UpdatedByName = "";
            visitor.UpdatedOn = DateTime.Now;
            visitor.Version = 1;
            visitor.Active = true;
            visitor.Archieved = false;

           Visitor response = await _cosmosService.VisitorRegistration(visitor);

            return response;

        }
        public async Task<Visitor> VisitorLogin(string Email, string Password)
        {
          return  await _cosmosService.VisitorLogin(Email, Password);


        }

        public async Task<Visitor> GetVisitorByEmail(string visitorEmails)
        {

            return await _cosmosService.GetVisitorByEmail(visitorEmails);
        }
        public async Task<CompanyUsers> GetOfficeUserByUID(string officerUid) {
            return await _cosmosService.GetOfficeUserByUID(officerUid);


}
      public async  Task<Request> sendRequest(string officerUid, string visitorEmails, string subject, string Description)
        {
            var visitor1 = await _cosmosService.GetVisitorByEmail(visitorEmails);
            var office = await _cosmosService.GetOfficeUserByUID(officerUid);
            if (visitor1 != null && office != null)
           
            {
                //stp 1 convert bookModel to bookentity
                Request visitor = new Request();

                //manual mapping
                visitor.Name = visitor1.Name;
                visitor.MoNo = visitor1.MoNo;
                visitor.Email = visitor1.Email;

                visitor.TrackingId = Guid.NewGuid().ToString().ToLower();
                visitor.Description = Description;

                visitor.Action = false;
                visitor.officerEmail = office.Email;
                visitor.Subject = subject;

                //stp 2 Assign madetory fields
                visitor.Id = Guid.NewGuid().ToString(); ;
                visitor.UId = visitor.Id;

                visitor.CreatedBy = "mayur UId ";
                visitor.CreatedByName = "mayur";
                visitor.CreatedOn = DateTime.Now;
                visitor.UpdatedBy = "";
                visitor.DocumentType = "request";
                visitor.UpdatedByName = "";
                visitor.UpdatedOn = DateTime.Now;
                visitor.Version = visitor.Version + 1;
                visitor.Active = true;
                visitor.Archieved = false;
               Request response= await _cosmosService.AddRequest(visitor);
                string visitorEmail = visitor1.Email;
                string officerEmail = office.Email;
                string Body = Description + "Email= " + visitorEmails + "\n" + "name=" + visitor1.Name + "\n" + "mono=" + visitor1.MoNo + "\n" + "Action=" + visitor.Action;
               
                
                await _cosmosService.SendMail(subject,Body,visitorEmail,officerEmail);  // call method from cosmosseervice

                // send mail code
               /* try  
                {
                    // Replace these values with your actual SMTP server and credentials
                    string smtpServer = "smtp.gmail.com";
                    int smtpPort = 587; // Typically 587 or 25
                    string smtpUsername = "mfegade295@gmail.com";
                    // string smtpPassword = "fuat zkve ucjd thcv";
                    string smtpPassword = "zsji tzyg kohl neqt";
                    // Create a new SMTP client
                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true; // Enable SSL if required

                        // Create a new email message
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(visitorEmail);
                        mailMessage.To.Add(officerEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = Body;

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

                return response;

            }
            else
            {
                return null;
            }
           
        }

      public async  Task<Visitor> GetVisitorByUId(string Uid) {

            return await _cosmosService.GetVisitorByUId(Uid);
        
        
        }

        public async Task<Visitor> UpdateVisitor(VisitorModel Model)
        {
            var existingcu = await _cosmosService.GetVisitorByUId(Model.UId);
            if (existingcu != null)
            {

                existingcu.Archieved = true;
                await _cosmosService.Replacevisitor(existingcu, existingcu.UId);
                existingcu.Id = Guid.NewGuid().ToString();
                existingcu.UpdatedBy = "Mayur UId";
                existingcu.UpdatedByName = "Mayur";
                existingcu.UpdatedOn = DateTime.Now;
                existingcu.Version = existingcu.Version + 1;
                existingcu.Active = true;
                existingcu.Archieved = false;

                existingcu.Name = Model.Name;
                existingcu.MoNo = Model.MoNo;
                existingcu.Address = Model.Address;
                existingcu.Email = Model.Email.ToLower();
                existingcu.Password = Model.Password;

                existingcu = await _cosmosService.UpdateVisitor(existingcu);
                return existingcu;
            }

            else return null;

        }
        public async Task<Visitor> DeleteVisitor(string uId)
        {
            var user = await _cosmosService.GetVisitorByUId(uId);
            if (user != null)
            {

                user.Active = false;

                await _cosmosService.Replacevisitor(user, user.Id);

                return user;
            }
            else return null;
        }

    }
}
