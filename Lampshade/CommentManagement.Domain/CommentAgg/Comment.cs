using _0.Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : BaseEntity
    {
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }   // if == 1 => product, if == 2 => article
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Text { get; private set; }
        public int Status { get; private set; }
        public long ParentId { get; private set; }

        
        //The constructor is called when creating a new instance
        public Comment(long ownerRecordId, int type, string name, string email, string text, long parentId)
        {
            OwnerRecordId = ownerRecordId;
            Type = type;
            Name = name;
            Email = email;
            Text = text;
            ParentId = parentId;
        }

        public void Confirm()
        {
            Status = CommentStatuses.Confirmed;
        }

        public void Cancel()
        {
            Status = CommentStatuses.Canceled;
        }
    }
}
