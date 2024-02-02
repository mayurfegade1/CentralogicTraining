using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using vscs2.DTO;
using vscs2.Entities;

namespace vscs2.CosmosDBServices
{
    public class CosmosService:ICosmosService
    {

        public Container container;
        public CosmosService() {
        
                 container = GetContainer();
                              }
        public async Task<CompanyUsers> CompanyUserRegistration(CompanyUsers comapnyuser)
        {

            return await container.CreateItemAsync(comapnyuser);

        }
        public async Task<List<Request>> SeeAllPendingrequests()
        {
            var requeslist = container.GetItemLinqQueryable<Request>(true).Where(q => q.DocumentType == "request" && q.Archieved == false && q.Active == true&&q.Version==1).AsEnumerable().ToList();
            
            return requeslist;
        }
        public async Task<List<Request>> SeeAllApproverequests()
        {
            var requeslist = container.GetItemLinqQueryable<Request>(true).Where(q => q.DocumentType == "request" && q.Archieved == false && q.Active == true && q.Version == 2 && q.Action==true).AsEnumerable().ToList();

            return requeslist;
        }

        public async Task<List<Request>> SeeAllRejectedrequests()
        {
            var requeslist = container.GetItemLinqQueryable<Request>(true).Where(q => q.DocumentType == "request" && q.Archieved == false && q.Active == true && q.Version == 2 && q.Action == false).AsEnumerable().ToList();

            return requeslist;
        }

        public async Task<CompanyUsers> LoginManager(string Email, string Password)
        {
            var manager = container.GetItemLinqQueryable<CompanyUsers>(true).Where(s => s.Email == Email.ToLower() && s.Password == Password && s.DocumentType == "manager" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();

            return manager;
        }

        public async Task<CompanyUsers> LoginOfficeUser(string Email, string Password)
        {

            var officeuser = container.GetItemLinqQueryable<CompanyUsers>(true).Where(s => s.Email == Email.ToLower() && s.Password == Password && s.DocumentType == "office" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();


            return officeuser;
        }

        public async Task<CompanyUsers> LoginSecurityUser(string Email, string Password)
        {
            var securityuser = container.GetItemLinqQueryable<CompanyUsers>(true).Where(s => s.Email == Email.ToLower() && s.Password == Password && s.DocumentType == "security" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();

            return securityuser; 
        }

        public async Task<Request> SearchVisitorByMono(int Search)
        {
            var visitor = container.GetItemLinqQueryable<Request>(true).Where(s => s.MoNo == Search && s.DocumentType == "request" && s.Archieved == false && s.Active == true && s.Action==true && s.Version==2).AsEnumerable().FirstOrDefault();

            return visitor;
        }
        public async Task<Visitor> VisitorRegistration(Visitor visitor)
        {
            return  await container.CreateItemAsync(visitor);
        }

        public async Task<Visitor> VisitorLogin(string Email, string Password)
        {
            var securityuser = container.GetItemLinqQueryable<Visitor>(true).Where(s => s.Email == Email.ToLower() && s.Password == Password && s.DocumentType == "visitor" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();

            return securityuser;
        }
        public async Task<Visitor> GetVisitorByEmail(string visitorEmails)
        {
            var visitor1 = container.GetItemLinqQueryable<Visitor>(true).Where(s => s.Email == visitorEmails && s.DocumentType == "visitor" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();

            return visitor1;
        }
        public async Task<CompanyUsers> GetOfficeUserByUID(string officerUid)
        {
            var office = container.GetItemLinqQueryable<CompanyUsers>(true).Where(s => s.UId == officerUid && s.DocumentType == "CompanyUsers" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();
            return office;
        }

        public async Task<Request> AddRequest(Request visitor)
        {
            Request response = await container.CreateItemAsync(visitor);
            return response;
        }
        public async Task<Visitor> GetVisitorByUId(string Uid)  
        {
            var existingcu = container.GetItemLinqQueryable<Visitor>(true).Where(q => q.UId == Uid && q.DocumentType == "visitor" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            if (existingcu != null)
            {
                return existingcu;
            }
            else return null;
        }

        public async Task<Visitor> Replacevisitor(Visitor visitor, string id)
        {
            var response = await container.ReplaceItemAsync(visitor, id);
            return response;

        }
        public async Task<Request> ReplaceRequest(Request request, string id) 
        {
            var response = await container.ReplaceItemAsync(request, id);
            return response;

        }

        public async Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            var response = await container.CreateItemAsync(visitor);
            return response;
        }
      public async  Task<CompanyUsers> GetManagerByEmail(string manageremail)
        {

            var managerdetails = container.GetItemLinqQueryable<CompanyUsers>(true).Where(q => q.Email == manageremail && q.DocumentType == "manager" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return managerdetails;
        }
        public async Task<Request> GetRequestByTrackingId(string trackingid)
        {

            var existingrequest = container.GetItemLinqQueryable<Request>(true).Where(q => q.TrackingId == trackingid && q.DocumentType == "request" && q.Archieved == false && q.Active == true && q.Version == 1).AsEnumerable().FirstOrDefault();
            return existingrequest;
        }
      public async  Task<CompanyUsers> GetCompanyUsers(string uid, string checkusertype)
        {
            var existingcu = container.GetItemLinqQueryable<CompanyUsers>(true).Where(q => q.UId == uid && q.DocumentType == checkusertype && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            return existingcu;

        }
        public async Task<CompanyUsers> ReplaceComapnyUser(CompanyUsers companyuser, string id)
        {
            var response = await container.ReplaceItemAsync(companyuser, id);
            return response;

        }


        public async Task<bool> SendMail(string subject, string Body, string visitorEmail, string officerEmail)
        {
            try
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

            return true;
        }


        private Container GetContainer()
        {
            string? URI = Environment.GetEnvironmentVariable("cosmos-uri");
            string? PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
            string? DatabaseName = Environment.GetEnvironmentVariable("database-name");
            string? ContainerName = Environment.GetEnvironmentVariable("container-name");
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database db = cosmosClient.GetDatabase(DatabaseName);
            Container container = db.GetContainer(ContainerName);
            return container;
        }
    }
}
