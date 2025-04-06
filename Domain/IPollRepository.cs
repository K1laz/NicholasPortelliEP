using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IPollRepository
    {
        void CreatePoll(Poll poll);
        IEnumerable<Poll> GetPolls();
        void Vote(Poll poll);
    }
}
