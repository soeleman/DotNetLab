using System;

namespace Soeleman.Libs.Model
{
    public class SayModel
    {
        public SayModel()
        {
            this.Id       = Guid.NewGuid();
            this.DateTime = DateTime.UtcDateTime;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public DateTimeOffset DateTime { get; }
    }
}