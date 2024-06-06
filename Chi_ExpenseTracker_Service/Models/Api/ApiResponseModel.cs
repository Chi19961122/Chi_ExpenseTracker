using Chi_ExpenseTracker_Service.Models.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Models.Api
{
    /// <summary>
    /// Api統一回傳Model
    /// </summary>
    public class ApiResponseModel
    {
        /// <summary>
        /// 回應代碼
        /// </summary>
        public string? Code { set; get; }

        /// <summary>
        /// 代碼訊息
        /// </summary>
        public string? Msg { set; get; }

        /// <summary>
        /// 回應資料
        /// </summary>
        public object? Data { set; get; }

        /// <summary>
        /// 回傳
        /// </summary>
        /// <param name="code">api回傳代碼</param>
        public void ApiResult(ApiCodeEnum code = ApiCodeEnum.Processing, string? msg = null)
        {
            switch (code)
            {
                case ApiCodeEnum.Processing:
                    Code = "100";
                    Msg = msg ?? "Processing";
                    return;
                case ApiCodeEnum.Success:
                    Code = "200";
                    Msg = msg ?? "Success";
                    return;
                case ApiCodeEnum.RequestOverLimit:
                    Code = "400";
                    Msg = msg ?? "Information cannot exceed Limit";
                    return;
                case ApiCodeEnum.InputError:
                    Code = "400-001";
                    Msg = msg ?? "Input Error";
                    return;
                case ApiCodeEnum.DuplicatedData:
                    Code = "400-002";
                    Msg = msg ?? "Duplicated Order";
                    return;
                case ApiCodeEnum.OrderNotExists:
                    Code = "400-003";
                    Msg = msg ?? "Order not exists";
                    return;
                case ApiCodeEnum.OrderCanNotBeCanceled:
                    Code = "400-004";
                    Msg = msg ?? "Order can not be canceled";
                    return;
                case ApiCodeEnum.OrderAlreadyCanceled:
                    Code = "400-005";
                    Msg = msg ?? "Order already canceled";
                    return;
                case ApiCodeEnum.InsertError:
                    Code = "409-001";
                    Msg = msg ?? "Insert Error";
                    return;
                case ApiCodeEnum.SystemError:
                    Code = "999";
                    Msg = msg ?? "System Error";
                    return;
            }
        }
    }
}
