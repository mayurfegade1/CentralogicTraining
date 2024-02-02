using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net.Mail;
using System.Net;
using vscs2.CosmosDBServices;
using vscs2.DTO;
using vscs2.Entities;
using vscs2.Interfaces;
using System.ComponentModel;

namespace vscs2.Services
{
    public class ManagerSevice:IManagerService
    {
       // public Container container;
        public ICosmosService _cosmosService;
        public ManagerSevice(ICosmosService cosmosService)
        {
         //   container = GetContainer();
            _cosmosService= cosmosService;  
        }

       
        public async Task<CompanyUsers> CompanyUserRegistration(CompanyUsers comapnyuser)
        {

            //stp 2 Assign madetory fields
            comapnyuser.Id = Guid.NewGuid().ToString();
            comapnyuser.UId = comapnyuser.Id;
            
            comapnyuser.CreatedBy = "mayur UId ";
            comapnyuser.CreatedByName = "mayur";
            comapnyuser.CreatedOn = DateTime.Now;
            comapnyuser.UpdatedBy = "";
            comapnyuser.UpdatedByName = "";
            comapnyuser.UpdatedOn = DateTime.Now;
            comapnyuser.Version = 1;
            comapnyuser.Active = true;
            comapnyuser.Archieved = false;


            //stp 3 Add data to database
            CompanyUsers response = await _cosmosService.CompanyUserRegistration(comapnyuser);


            return response;
        }

        public async Task<List<Request>> SeeAllPendingrequests()
        {

            return await _cosmosService.SeeAllPendingrequests();
        }
        public async Task<List<Request>> SeeAllApproverequests(){
            return await _cosmosService.SeeAllApproverequests();
        }
        public async Task<List<Request>> SeeAllRejectedrequests()
        {
            return await _cosmosService.SeeAllRejectedrequests();
        }

        public async Task<CompanyUsers> LoginManager(string Email, string Password)
        {

            return await _cosmosService.LoginManager(Email, Password);
        }
        public async Task<Request> ApproveRejectRequest(string manageremail, string Trackingid, bool Aprrove_Reject)
        {
            var managerdetails=await _cosmosService.GetManagerByEmail(manageremail);

            var existingrequest = await _cosmosService.GetRequestByTrackingId(Trackingid);


            if (existingrequest != null && managerdetails != null) {
                existingrequest.Archieved = true;
                existingrequest.Active = false;
                await _cosmosService.ReplaceRequest(existingrequest, existingrequest.UId);
                existingrequest.Id = Guid.NewGuid().ToString();
                existingrequest.UId = existingrequest.UId;
                existingrequest.UpdatedBy = "Mayur UId";
                existingrequest.UpdatedByName = manageremail;
                existingrequest.UpdatedOn = DateTime.Now;
                existingrequest.Version = existingrequest.Version + 1;
                existingrequest.Active = true;
                existingrequest.Archieved = false;

                existingrequest.Name = existingrequest.Name;
                existingrequest.MoNo = existingrequest.MoNo;
                existingrequest.Email = existingrequest.Email.ToLower();
                existingrequest.officerEmail = existingrequest.officerEmail.ToLower();
                existingrequest.Action = Aprrove_Reject;
                existingrequest.Subject = existingrequest.Subject;
                existingrequest.Description = existingrequest.Description;
                existingrequest.TrackingId = existingrequest.TrackingId;

                 existingrequest = await _cosmosService.AddRequest(existingrequest);

                string VEmail = existingrequest.Email;
                string mEmail = managerdetails.Email;
                string body= existingrequest.Name + " " + existingrequest.Email;
                string subject;
                if (Aprrove_Reject == true)
                {
                    subject = "Request Approved";
                }
                else
                {
                    subject = "request Reject";
                }

                await _cosmosService.SendMail(body, subject, VEmail, mEmail); // call send mail method from cosmosseervice

                // send mail code
                /* try
                 {
                     // Replace these values with your actual SMTP server and credentials
                     string smtpServer = "smtp.gmail.com";
                     int smtpPort = 587; // Typically 587 or 25
                     string smtpUsername = "mfegade295@gmail.com";
                     string smtpPassword = "fuat zkve ucjd thcv";
                     //string smtpPassword = "zsji tzyg kohl neqt";

                     // Create a new SMTP client
                     using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                     {
                         smtpClient.UseDefaultCredentials = false;
                         smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                         smtpClient.EnableSsl = true; // Enable SSL if required

                         // Create a new email message
                         MailMessage mailMessage = new MailMessage();
                         mailMessage.From = new MailAddress(mEmail);
                         mailMessage.To.Add(VEmail);
                         mailMessage.To.Add("mayurfegade20001@gmail.com"); // security guard
                         mailMessage.Subject = "Request Approved";
                         mailMessage.Body = body;

                         // Send the email
                         smtpClient.Send(mailMessage);

                         // Console.WriteLine("Email sent successfully!");
                     }
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                 }*/



                return existingrequest;
            }


            else {
                return null;
                    }
        }

       public async Task<CompanyUsers> UpdateUsers(CompanyUsers comapnyuser)
        {
            return await _cosmosService.CompanyUserRegistration(comapnyuser);
        }
        public async Task<CompanyUsers> UpdateCompanyUsers(CompanyUsersModel companyuserModel)
        {

            var existingcu = await _cosmosService.GetCompanyUsers(companyuserModel.UId, companyuserModel.UserType.ToLower());
            if (existingcu != null)
            {
                existingcu.Archieved = true;

                await _cosmosService.ReplaceComapnyUser(existingcu, existingcu.Id);


                existingcu.Id = Guid.NewGuid().ToString();
                existingcu.UpdatedBy = "Mayur UId";
                existingcu.UpdatedByName = "Mayur";
                existingcu.UpdatedOn = DateTime.Now;
                existingcu.Version = existingcu.Version + 1;
                existingcu.Active = true;
                existingcu.Archieved = false;


                return existingcu;

            }
            else return null;


        }

        public async Task<CompanyUsers> DeleteCompanyUsers(string CompannyUserUId, string Usertype)
        {
          var user=  await _cosmosService.GetCompanyUsers(CompannyUserUId, Usertype);

            user.Active= false;

            await _cosmosService.ReplaceComapnyUser(user, user.Id);

            return user;

        }




    }
}
