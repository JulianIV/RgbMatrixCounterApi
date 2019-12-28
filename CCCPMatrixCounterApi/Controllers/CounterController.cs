using Microsoft.AspNetCore.Mvc;
using System;


namespace CCCPMatrixCounterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterController
    {
        private readonly SerialWriter Writer;

        public CounterController(SerialWriter writer)
        {
            Writer = writer;
        }

        [HttpPost]
        public ReturnData CounterUpdate(InputData input)
        {
            try
            {
                Writer.Write($"Saved: {input.Counter} coffee cups");
                
                return new ReturnData
                {
                    Success = true,
                    Message = "OK"
                };  
            }
            catch (Exception)
            {
                return new ReturnData
                {
                    Success = false,
                    Message = "Error"
                };
            }
        }
    }

    public class InputData
    {
        public int Counter { get; set; }
    }

    public class ReturnData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
