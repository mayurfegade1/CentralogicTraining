using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;
using vscs2.DTO;
using vscs2.Entities;
using Container = Microsoft.Azure.Cosmos.Container;
using vscs2.Interfaces;

namespace vscs.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public Container container;
        public IManagerService _managerService;
        public IMapper _mapper;
        public ManagerController(IManagerService managerService, IMapper mapper)
        {
            container = GetContainer();
            _managerService = managerService;
            _mapper = mapper;
        }
       
        //LoginManager
        [HttpGet]
        public async Task<IActionResult> LoginManager(string Email, string Password)
        {
            try
            {

 
                var manager=await _managerService.LoginManager(Email, Password);

                if (manager == null)
                {
                    return NotFound("wrong email or password");
                }

                var response = new { manager.Name };

                

                return Ok($" Login Successfully{response}");


            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        //AddcompanyUsers  Office/security/manager
        [HttpPost]
        public async Task<IActionResult> CompanyUserRegistration(CompanyUsersModel comapnyusermodel)
        {
            try
            {
               // string avs = checkusertypes.ToString();
                string checkusertype= comapnyusermodel.UserType.ToLower();
                if (checkusertype != "office" && checkusertype != "security" && checkusertype != "manager")
                {
                    return Ok("wrong usertype enter \n usertype should be :- office or security or manager");
                }
               else {
                    //stp 1 convert CompanyUsersModel to CompanyUsers
                    CompanyUsers comapnyuser = new CompanyUsers();
                    //manual mapping 
                    comapnyuser.Name = comapnyusermodel.Name.ToLower();
                    comapnyuser.MoNo = comapnyusermodel.MoNo;
                    comapnyuser.UserType = comapnyusermodel.UserType.ToLower();
                    comapnyuser.Email = comapnyusermodel.Email.ToLower();
                    comapnyuser.Password = comapnyusermodel.Password.ToLower();


                    comapnyuser.DocumentType = comapnyuser.UserType.ToLower();
                   
                  CompanyUsers response=  await _managerService.CompanyUserRegistration(comapnyuser);


                    //auto mapping
                    var model = _mapper.Map<CompanyUsersModel>(response);


                    return Ok(model);
              }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        //see all pendingrequests
        [HttpGet]
        public async Task<IActionResult> SeeAllPendingrequests()
        {
            try
            {
                //stp 1 get all requests
             
                var requeslist= await _managerService.SeeAllPendingrequests();
                if (requeslist == null)
                {
                    return NotFound("there is no requests");
                }
                //stp 2 mapping all data
                List<RequestModel> visitorModelist = new List<RequestModel>();
                foreach (var request in requeslist)
                {
                       //  Auto Mapping
                    var model = _mapper.Map<RequestModel>(request);
                    visitorModelist.Add(model);
                }
                return Ok(visitorModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        // all Approve requests
        [HttpGet]
        public async Task<IActionResult> SeeAllApproverequests()
        {
            try
            {
             
                var requeslist = await _managerService.SeeAllApproverequests();
                if (requeslist == null)
                {
                    return NotFound("there is no requests");
                }
                //stp 2 mapping all data
                List<RequestModel> visitorModelist = new List<RequestModel>();
                foreach (var request in requeslist)
                {
                    //  Auto Mapping
                    var model = _mapper.Map<RequestModel>(request);
                    visitorModelist.Add(model);
                }
                return Ok(visitorModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        // all Approve requests
        [HttpGet]
        public async Task<IActionResult> SeeAllRejectedrequests()
        {
            try
            {
               
                var requeslist = await _managerService.SeeAllRejectedrequests();

                if (requeslist == null)
                {
                    return NotFound("there is no requests");
                }
                //stp 2 mapping all data
                List<RequestModel> visitorModelist = new List<RequestModel>();
                foreach (var request in requeslist)
                {
                    //  Auto Mapping
                    var model = _mapper.Map<RequestModel>(request);
                    visitorModelist.Add(model);
                }
                
                
                return Ok(visitorModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        //Approve or reject request
        [HttpPut]
        public async Task<IActionResult> ApproveRejectRequest(string manageremail,string Trackingid, bool Aprrove_Reject)
        {
            try
            {

              
                Request existingrequest = await _managerService.ApproveRejectRequest(manageremail, Trackingid, Aprrove_Reject);
                //auto mapping
                if (existingrequest != null)
                {
                    string body;
                    if (Aprrove_Reject == true)
                    {
                        body = "Request Approved";
                    }
                    else
                    {
                        body = "request Reject";
                    }
                    var model = _mapper.Map<RequestModel>(existingrequest);


                  

                    return Ok($" {body} Successfully for Tracking id={existingrequest.TrackingId}");
                }
                else return Ok("please check values again");
}
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        //update office user/security user/ manager
        [HttpPut]
        public async Task<IActionResult> UpdateCompanyUsers(CompanyUsersModel companyuserModel)
        {
            
            try
            {
                string checkusertype = companyuserModel.UserType.ToLower();
                if (checkusertype != "office" && checkusertype != "security" && checkusertype != "manager")
                {
                    return Ok("wrong usertype enter \n usertype should be :- office or security or manager");
                }
                else
                {
                   
                    CompanyUsers existingcu = await _managerService.UpdateCompanyUsers(companyuserModel);
                    if (existingcu != null)
                    {


                        existingcu.Name = companyuserModel.Name;
                        existingcu.MoNo = companyuserModel.MoNo;
                        existingcu.UserType = companyuserModel.UserType.ToLower();
                        existingcu.Email = companyuserModel.Email.ToLower();
                        existingcu.Password = companyuserModel.Password;
                        existingcu.DocumentType = companyuserModel.UserType.ToLower();

                        await _managerService.UpdateUsers(existingcu);
                        //  existingcu = await container.CreateItemAsync(existingcu);

                        //auto mapping
                        var model = _mapper.Map<CompanyUsersModel>(existingcu);



                        return Ok(model);
                    }
                    else return Ok("please check enter values again!!");
                    }
                
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        // delete office user/security user/ manager
        [HttpDelete]
        public async Task<IActionResult> DeleteCompanyUsers(string CompannyUserUId, string Usertype)
        {
             CompanyUsers duser = await _managerService.DeleteCompanyUsers(CompannyUserUId, Usertype);

            if (duser != null)
            {
    
                return Ok($"{duser.UserType} delete success");
            }
           
            else
            {
                return NotFound($"Please Enter Correct {duser.UserType} UID");
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
