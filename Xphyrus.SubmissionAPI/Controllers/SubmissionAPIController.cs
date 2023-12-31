﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xphyrus.MessageBus;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Models.ResReq;
using Xphyrus.SubmissionAPI.RabbitMQ;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionAPIController : ControllerBase
    {   
        private readonly IJudgeService _judgeService;
        private readonly IMQSender _bus;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IAssesmentService _assesmentService;
        protected ResponseDto _responseDto;
        public SubmissionAPIController(IJudgeService judgeService, IMQSender bus, IConfiguration configuration, IAuthService authService, IAssesmentService assesmentService)
        {
            _judgeService = judgeService;
            _bus = bus;
            _configuration = configuration;
            _responseDto = new ResponseDto();
            _authService = authService;
            _assesmentService = assesmentService;
         
        }

        [HttpPut]
       
        public async Task<ActionResult<SubmissionStatusResponse>> GetASubmission(TokenResponse id)
        {
            return await _judgeService.GetResponse(id);
        }
        [HttpPost]

        public async Task<ActionResult<object>> PostSubmission([FromBody]SubmissionRequest request)
        {
            return await _judgeService.SubmitPost(request);
        }
        [Authorize]
        [HttpPost("Submit")]
        public async Task<ActionResult<ResponseDto>> Submission([FromBody] SubmissionDto submissionDto)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           // t.SubmissionRequest.StudentId = Int32.Parse(id);
          //  t.SubmissionRequest.AssesmentId = Int32.Parse(t.submission.AssignmentId);

            try
            {
                _responseDto = await _assesmentService.MarkSubmission(submissionDto);
                if(_responseDto.IsSuccess && _responseDto.Message == "")
                {
                   _bus.SendMessage(submissionDto.source_code, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
                }
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
            
        }

        [HttpPost]
        [Route("BusTest")]

        public async Task<ActionResult<bool>> BusTest([FromBody] SubmissionRequest request)
        {
            try
            {
                 _bus.SendMessage(request.source_code, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }

    
}
