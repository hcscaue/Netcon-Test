namespace NetconTest.Application.DTOs
{
    public class ErrorResponseDto
    {
        public required string Code { get; set; }
        public required string Reason { get; set; }
        public required string Message { get; set; }
        public required string Status { get; set; }
        public required string Timestamp { get; set; }
    }
}
