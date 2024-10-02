using EduZone.ApiResponse;
using EduZone.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using Volo.Abp.DependencyInjection;

namespace EduZone.ApiResponses
{
    public class ApiResponse : ITransientDependency, IApiResponse
    {
        private readonly IStringLocalizer<EduZoneResource> _localizer;
        public ApiResponse(IStringLocalizer<EduZoneResource> localizer)
        {
            _localizer = localizer;
        }
        public Response<List<object>> Fail(string msg)
        {
            Response<List<object>> r = new()
            {
                Status = false,
                Message = " " + msg,
                Code = 400,
                Data = new ResponseObject<List<object>> { Result = new List<object>() },
            };

            return r;
        }

        public Response<T> Fail<T>(T data, string msg)
        {
            Response<T> r = new()
            {
                Status = false,
                Message = msg,
                Code = 400,
                Data = new ResponseObject<T> { Result = data },
            };

            return r;
    }

        public Response<T> Fail<T>(T message)
        {
            Response<T> r = new()
            {
                Status = false,
                Message = " " + message,
                Code = 400
            };

            return r;
    }

        public Response<T> Fail<T>(T data, Exception e)
        {
            Response<T> r = new()
            {
                Status = false,
                Message = GetExceptionMessage(e),
                Code = 400,
                Data = new ResponseObject<T> { Result = data },
            };

            return r;
    }

        public Response<T> Success<T>(T data, Exception e)
        {
            Response<T> r = new()
            {
                Status = true,
                Message = _localizer[EduZoneConsts.SuccessMessage] + " " + GetExceptionMessage(e),
                Code = 200,
                Data = new ResponseObject<T>{ Result = data },
            };

            return r;
        }

        public Response<T> Success<T>(T data)
        {
            Response<T> r = new()
            {
                Status = true,
                Message = _localizer[EduZoneConsts.Success],
                Code = 200,
                Data = new ResponseObject<T> { Result = data },
            };

            return r;
        }

        public Response<T> Success<T>(T data, string msg)
        {
            Response<T> r = new()
            {
                Status = true,
                Message = _localizer[EduZoneConsts.SuccessMessage] + " " + msg,
                Code = 200,
                Data = new ResponseObject<T> { Result = data },
            };

            return r;
        }

        public Response<T> Unauthorized<T>(string msg)
        {
            Response<T> r = new()
            {
                Status = false,
                Message = msg,
                Code = (int)HttpStatusCode.Unauthorized,
                Data = null,
            };
            return r;
        }

        public string GetExceptionMessage(Exception e) => " " + e.Message;
    }
}
