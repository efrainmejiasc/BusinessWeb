using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineNotify
    {
        bool EnviarEmail(string emailTo, string asunto, string body, string pathAdjunto);
        bool EnviarEmail(List<string> emailTo, string asunto, string body, string pathAdjunto);
    }
}
