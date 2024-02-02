using Microsoft.AspNetCore.Mvc;
using vscs2.DTO;
using vscs2.Entities;

namespace vscs2.Interfaces
{
    public interface IVisitorService
    {

         Task<Visitor> VisitorRegistration(Visitor visitor);

        Task<Visitor> VisitorLogin(string Email, string Password);
        Task<Visitor> GetVisitorByEmail(string visitorEmails);
        Task<CompanyUsers> GetOfficeUserByUID(string officerUid);

        //Task<Visitor>sendRequest(string officerUid, string visitorEmails, string subject, string Description);
        Task<Request> sendRequest(string officerUid, string visitorEmails, string subject, string Description);
        Task<Visitor> GetVisitorByUId(string Uid);
        Task<Visitor> UpdateVisitor(VisitorModel visitorModel);
        Task<Visitor> DeleteVisitor(string uId);
    }
}
