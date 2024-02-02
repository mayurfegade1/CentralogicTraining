using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net.Mail;
using System.Net;
using vscs2.DTO;
using vscs2.Entities;
using vscs2.Interfaces;
using vscs2.Services;
using System.Diagnostics.Eventing.Reader;

namespace vscs2.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        public Container container;
        public IMapper _mapper;
        public IVisitorService _visitorService;
        public VisitorController(IMapper mapper, IVisitorService visitorService)
        {
            container = GetContainer();
            _mapper = mapper;
            _visitorService = visitorService;
        }

        //Add Visitor
        [HttpPost]
        public async Task<IActionResult> VisitorRegistration(VisitorModel visitormodel)
        {
            try
            {
                //stp 1 convert bookModel to bookentity
                Visitor visitor = new Visitor();
                //manual mapping
                visitor.Name = visitormodel.Name.ToLower();
                visitor.MoNo = visitormodel.MoNo;
                visitor.Address = visitormodel.Address.ToLower();
                visitor.Email = visitormodel.Email.ToLower();
                visitor.Password = visitormodel.Password.ToLower();
                //  visitor.Action = false;


                /*      //stp 2 Assign madetory fields
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
      */

                //stp 3 Add data to database
                //  Visitor response = await container.CreateItemAsync(visitor);
                Visitor response = await _visitorService.VisitorRegistration(visitor);
                //stp4 return model to ui(reverse mapping )(DRY)



                //auto mapping
                var model = _mapper.Map<VisitorModel>(response);


                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        //visitorLogin
        [HttpGet]
        public async Task<IActionResult> VisitorLogin(string Email, string Password)
        {
            try
            {


                /*  var securityuser = container.GetItemLinqQueryable<Visitor>(true).Where(s => s.Email == Email.ToLower() && s.Password == Password && s.DocumentType == "visitor" && s.Archieved == false && s.Active == true).AsEnumerable().FirstOrDefault();
                */

                var securityuser = await _visitorService.VisitorLogin(Email, Password);
                if (securityuser == null)
                {
                    return NotFound("wrong email or password");
                }

                var response = new { securityuser.Name };


                return Ok($"{securityuser.Name} Login Successfully{response}");


            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        //sendrequest

        [HttpPost]
        public async Task<IActionResult> sendRequest(string officerUid, string visitorEmails, string subject, string Description)
        {
            try
            {
                
                    Request response = await _visitorService.sendRequest(officerUid, visitorEmails, subject, Description);

                        
                    if (response == null)
                    {
                        return Ok("officeruid or visitor email not found");
                    }
                    
                    //auto mapping
                     var model = _mapper.Map<RequestModel>(response);
                    


                    return Ok($"request Sent Successfully!to{response.officerEmail} Your Traking Id is {response.TrackingId}");
                  
                

            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }
     // update visior details
        [HttpPut]
        public async Task<IActionResult> UpdateVisitor(VisitorModel Model)
        {
            try
            {


                   Visitor existingcu = await _visitorService.UpdateVisitor(Model);
                    if(existingcu == null)
                {
                    return Ok("visitor uid is wrong");
                }
                    //auto mapping
                    var model = _mapper.Map<Visitor>(existingcu);



                    return Ok(model);
                
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVisitor(string vsitorUId)
        {
            // var task = container.GetItemLinqQueryable<Task>(true).Where(q => q.UId == taskUId && q.DocumentType == "task" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();try{
            Visitor duser = await _visitorService.DeleteVisitor(vsitorUId);

            if (duser != null)
            {

                return Ok($"{duser.Name} delete success");
            }
            // var tasks=await _taskService.DeleteTask(taskUId);

            else
            {
                return NotFound($"Please Enter Correct {duser.Name} UID");
            }

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
