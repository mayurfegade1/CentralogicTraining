using Microsoft.AspNetCore.Mvc;
using vscs2.Entities;

namespace vscs2.CosmosDBServices
{
    public interface ICosmosService
    {
        Task<CompanyUsers> CompanyUserRegistration(CompanyUsers comapnyuser);
        Task<List<Request>> SeeAllPendingrequests();
        Task<List<Request>> SeeAllApproverequests();
        Task<List<Request>> SeeAllRejectedrequests();
        Task<CompanyUsers> LoginManager(string Email, string Password);
        Task<CompanyUsers> LoginOfficeUser(string Email, string Password);
        Task<CompanyUsers> LoginSecurityUser(string Email, string Password);
        Task<Request> SearchVisitorByMono(int Search);
        Task<Visitor> VisitorRegistration(Visitor visitor);
        Task<Visitor> VisitorLogin(string Email, string Password);
        Task<Visitor> GetVisitorByEmail(string visitorEmails);
        Task<CompanyUsers> GetOfficeUserByUID(string officerUid);
        Task<Request> AddRequest(Request visitor);
        Task<Visitor> GetVisitorByUId(string Uid);
        Task<Visitor> Replacevisitor(Visitor visitor, string id);
        Task<Visitor> UpdateVisitor(Visitor visitor);

        Task<CompanyUsers> GetManagerByEmail(string manageremail);
        Task<Request> GetRequestByTrackingId(string trackingid);

        Task<Request> ReplaceRequest(Request request, string id);

        Task<CompanyUsers> GetCompanyUsers(string uid, string checkusertype);
        Task<CompanyUsers> ReplaceComapnyUser(CompanyUsers companyuser, string id);
        //Task<CompanyUsers> UpdateComapanyUser(CompanyUsers companyuser);
        Task<bool> SendMail(string subject, string Body, string visitorEmail, string officerEmail);
    }
}