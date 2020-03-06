using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.SDK.Diagnostics.Common;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.DataModel;

namespace Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class RemovePersonFromTeamHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private RemovePersonFromTeam.Response RemovePersonFromTeamHandler(RemovePersonFromTeam command)
        {

            ITracer tracer = platform.Tracer;

            try
            {
                var person = platform.Query<IPerson>().Include("Team").Single(x =>x.Name == command.PersonName);
                person.Team = null;
                platform.Submit(person);

                return new RemovePersonFromTeam.Response { };

            }
            catch (Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
                return new RemovePersonFromTeam.Response { Error = new ExecutionError(-1010, ex.Message) };
            }
        }
    }
}
