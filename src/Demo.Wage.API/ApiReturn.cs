using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Wage.API
{
    public class ApiReturn
    {
        public ApiReturnCodes Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public enum ApiReturnCodes
    {
        Success = 0
        , GrossWageError = 101
        , AreaKeyNotExist = 102
        , CalculationError = 103
        , OtherError = 201
    }

    public class APIMessages
    {
        public const string MESSAGE_GrossWage_Error_Empty = "Please input GrossWage value for calculation.";
        public const string MESSAGE_GrossWage_Error_Invalid = "GrossWage value is invalid!";
        public const string MESSAGE_SUCCESS = "Calculation Success!";
        public const string MESSAGE_AreaKey_Error_Empty = "AreaKey is empty!";
        public const string MESSAGE_AreaKey_Error_NotExist = "Area [{0}] doesn't exist.";
        public const string MESSAGE_Calculation_Error = "Calculation process error!";
    }
}
