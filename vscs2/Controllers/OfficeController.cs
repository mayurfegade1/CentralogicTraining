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

namespace vscs2.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {

        public Container container;
        public IMapper _mapper;
        public IOfficeService _officeservice;
        public OfficeController(IOfficeService officeservice,IMapper mapper)
        {
            _officeservice=officeservice;
            container = GetContainer();
            _mapper = mapper;
        }
        //Login officer
        [HttpGet]
        public async Task<IActionResult> LoginOfficeUser(string Email, string Password)
        {
            try
            {


              
                var officeuser= await _officeservice.LoginOfficeUser(Email, Password);  
                if (officeuser == null)
                {
                    return NotFound("wrong email or password");
                }

                var response = new {officeuser.Name  };


                return Ok($"{officeuser.Name} Login Successfully{response}");


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
               
                var requeslist = await _officeservice.SeeAllPendingrequests();
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

                var requeslist = await _officeservice.SeeAllApproverequests();
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

                var requeslist = await _officeservice.SeeAllRejectedrequests();

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


        // update request status
        [HttpPut]
        public async Task<IActionResult> ApproveRejectRequest(string Trackingid ,bool action_true_or_false)
        {
            try
            {
                
                Request existingrequest = await _officeservice.ApproveRejectRequest(Trackingid, action_true_or_false);
                if (existingrequest == null)
                {
                    return Ok("request not found or appropriate action is already taken by officer or manager");
                }
                else { 
                //auto mapping
                var model = _mapper.Map<RequestModel>(existingrequest);

                string body;
                if (action_true_or_false == true)
                {
                    body = "Request Approved";
                }
                else
                {
                    body = "request Reject";
                }
               
                return Ok($" {body} Successfully for Tracking id={existingrequest.TrackingId}");
            }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
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
