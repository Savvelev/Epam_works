namespace Domain.Exceptions.BadRequest
{
    public class OutputTypeReportException : BadRequestException
    {
        public OutputTypeReportException(string outputType)
        : base($"The output type was entered incorrectly. What has been entered: {outputType}.")
        {
        }

        public OutputTypeReportException() : base($"The output type was entered incorrectly.")
        {
        }
    }
}
