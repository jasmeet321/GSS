using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSS.Models.Models
{
    public class MembersToCommitteeModel
    {
        public int Id { get; set; }
        public long MemberId { get; set; }
        public long CommitteeId { get; set; }

        public CommitteeModel Committee { get; set; }
        public CommitteMemberModel CommitteeMember { get; set; }
    }
}
