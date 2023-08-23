using FignumCodingTest.Errors;

namespace FignumCodingTest.Models
{
    public class BaseResponse
    {
        public int Status { get; set; }
        public Error Errors { get; set; }

        public BaseResponse()
        {
            Errors = new Error();
        }
    }
}
