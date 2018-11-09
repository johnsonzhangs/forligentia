using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Demo.Wage.API.Controllers
{
    [Route("wage/[controller]")]
    public class DeductedTaxController : Controller
    {
        public const string SESSION_LATEST_AREAKEY = "SESSION_LATEST_AREAKEY";
        // GET: api/<controller>
        [HttpGet]
        public ApiReturn Get()
        {
            return new ApiReturn()
            {
                Code = ApiReturnCodes.GrossWageError
                ,
                Message = APIMessages.MESSAGE_GrossWage_Error_Empty

            };
        }

        [HttpGet("{GrossWage}")]
        public ApiReturn Get(double GrossWage)
        {
            string areaKey = "";
            return Get(GrossWage, areaKey);
        }

        [HttpGet("{GrossWage}/{AreaKey}")]
        public ApiReturn Get(double GrossWage, string AreaKey)
        {
            ApiReturn rtn = null;

            //Validate the input value of GrossWage 
            if (GrossWage <= 0)
            {
                rtn = new ApiReturn()
                {
                    Code = ApiReturnCodes.GrossWageError
                    ,
                    Message = APIMessages.MESSAGE_GrossWage_Error_Invalid
                    ,
                    Data = GrossWage
                };

            }
            else
            {

                //Validate the input value of AreaKey 
                if (string.IsNullOrEmpty(AreaKey))
                {
                    if (HttpContext.Session.IsAvailable)
                    {
                        AreaKey = HttpContext.Session.GetString(SESSION_LATEST_AREAKEY);
                    }
                }


                if (!string.IsNullOrEmpty(AreaKey))
                {
                    try
                    {
                        //Find / create area instance, and do calculation.
                        IArea area = AreaFactory.Instance.Create(AreaKey);

                        double deductedWage = area.Calculation.Cal(GrossWage);

                        Dictionary<string, object> values = new Dictionary<string, object>();
                        values["DeductedWage"] = deductedWage;
                        values["AreaKey"] = AreaKey;
                        values["AreaName"] = area.Name;
                        values["Time"] = DateTime.Now;

                        rtn = new ApiReturn()
                        {
                            Code = ApiReturnCodes.Success
                    ,
                            Message = APIMessages.MESSAGE_SUCCESS
                    ,
                            Data = values
                        };

                        //Put AreaKey into session
                        HttpContext.Session.SetString(SESSION_LATEST_AREAKEY, AreaKey);

                    }
                    catch (AreaNotExistException notExistException)
                    {
                        rtn = new ApiReturn()
                        {
                            Code = ApiReturnCodes.AreaKeyNotExist
                    ,
                            Message = string.Format(APIMessages.MESSAGE_AreaKey_Error_NotExist, AreaKey)
                    ,
                            Data = AreaKey
                        };
                    }
                    catch (CalculationException calEx)
                    {
                        rtn = new ApiReturn()
                        {
                            Code = ApiReturnCodes.CalculationError
                    ,
                            Message = APIMessages.MESSAGE_Calculation_Error
                    ,
                            Data = AreaKey
                        };
                    }
                    catch (Exception ex)
                    {
                        rtn = new ApiReturn()
                        {
                            Code = ApiReturnCodes.OtherError
                    ,
                            Message = ex.Message
                    ,
                            Data = AreaKey
                        };
                    }

                }
                else
                {
                    rtn = new ApiReturn()
                    {
                        Code = ApiReturnCodes.AreaKeyNotExist
                    ,
                        Message = APIMessages.MESSAGE_AreaKey_Error_Empty
                    ,
                        Data = GrossWage
                    };
                }
            }


            return rtn;
        }

    }

}
