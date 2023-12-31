﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xphyrus.MessageBus;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Models.ResReq;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRunController : ControllerBase
    {
        private readonly IJudgeService _judgeService;
    
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IAssesmentService _assesmentService;
        protected ResponseDto _responseDto;
        public TestRunController(IJudgeService judgeService,  IConfiguration configuration, IAuthService authService, IAssesmentService assesmentService)
        {
            _judgeService = judgeService;
           
            _configuration = configuration;
            _responseDto = new ResponseDto();
            _authService = authService;
            _assesmentService = assesmentService;

        }

        [HttpPost("Run")]
        public async Task<ActionResult<ResponseDto>> RunCode([FromBody] TestRunDto testRunDto)
        {
            //var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            //var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if(email == null || id == null)
            //{   
            //    _responseDto.IsSuccess = false;
            //    _responseDto.Message = "invalid token";
            //    return _responseDto;
            //}

            SubmissionRequest submissionRequest = new SubmissionRequest(testRunDto);
            TokenResponse res = await _judgeService.SubmitPost(submissionRequest);
            Thread.Sleep(1000);
            SubmissionStatusResponse statusResponse = await _judgeService.GetResponse(res);

            _responseDto.Result = statusResponse;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }
    }
}
