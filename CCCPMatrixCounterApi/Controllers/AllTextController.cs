using System;
using Microsoft.AspNetCore.Mvc;

namespace CCCPMatrixCounterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllTextController
    {
        private readonly SerialWriter Writer;

        public AllTextController(SerialWriter writer)
        {
            Writer = writer;
        }

        [HttpPost]
        public AllTextReturnData TextUpdate(AllTextInputData input)
        {
            try
            {
                if(input.Text.Length > 32) return new AllTextReturnData { Success = false, Message = "Text too long" };
                Writer.Write(input.Text);

                return new AllTextReturnData { Success = true, Message = "OK" };
            }
            catch (Exception)
            {
                return new AllTextReturnData { Success = false, Message = "Error" };
            }
        }
    }

    public class AllTextInputData
    {
        public string Text { get; set; }
    }

    public class AllTextReturnData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
