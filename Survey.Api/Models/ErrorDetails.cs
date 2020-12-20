using Newtonsoft.Json;

namespace Survey.Api.Models
{
    /// <summary>
    /// Global response handle class
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Status code of error
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Details of errors
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Serialized error object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
