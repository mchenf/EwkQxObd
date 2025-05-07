using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd
{
    public static class Prompts
    {
        public const string ArgZero = 
@" [~::WARN::~] Zero arguments provided.";

        public const string HelpInfo = 
@"usage: iqxobd <command> [<command>] [<args>]

Organization commands
    org parse <json string>         Record from fetch/XHR json
    org list --all                  Get all org records
    org get <id>                    Get an org record by account id
    org lookup <geis guid>          Lookup a record by GEIS Guid
";

    }
}
