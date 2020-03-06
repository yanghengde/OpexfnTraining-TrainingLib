using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.DataModel;
using Siemens.SimaticIT.SDK.Diagnostics.Common;

namespace Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class AssignTeamToGroupHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private AssignTeamToGroup.Response AssignTeamToGroupHandler(AssignTeamToGroup command)
        {
            ITracer tracer = platform.Tracer;

            try
            {
                var group = platform.Query<ITeamGroup>().First(x => x.Name == command.GroupName);

                if (group != null)
                {
                    IList<ITeam> list = new List<ITeam>();
                    foreach (string teamname in command.Teams)
                    {
                        var team = platform.Query<ITeam>().Single(x => x.Name == teamname);
                        list.Add(team);
                    }
                    platform.AddManyToManyRelationship<ITeamGroup, ITeam>(group, x => x.Team, list);
                }

                return new AssignTeamToGroup.Response { };

            }
            catch (Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
                return new AssignTeamToGroup.Response { Error = new ExecutionError(-1010, ex.Message) };
            }
        }
    }
}
