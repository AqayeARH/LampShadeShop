namespace CommentManagement.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public long OwnerRecordId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string OwnerRecordName { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
    }
}
