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
using Siemens.Mom.Presales.Training.Training.TrainingLibView.TNModel.DataModel.ProjectionModel;

namespace Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class AssignPersonToTeamHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private AssignPersonToTeam.Response AssignPersonToTeamHandler(AssignPersonToTeam command)
        {
            ITracer tracer = platform.Tracer;

            try
            {
                //var team2 =   platform.ProjectionQuery<IvTeam>().FirstOrDefault(x => x.Name == command.TeamName);
                //var team3 = platform.ProjectionQuery<IvTeam>().Where(x => x.Name == command.TeamName);
                var team = platform.Query<ITeam>().FirstOrDefault(x => x.Name == command.TeamName);

                if (team != null)
                {
                    foreach(string name in command.Persons)
                    {
                       var person =  platform.Query<IPerson>().FirstOrDefault(x => x.Name == name);
                        person.Team = team;
                        platform.Submit(person);
                    }
                }

                return new AssignPersonToTeam.Response { };

            }
            catch (Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
                return new AssignPersonToTeam.Response { Error = new ExecutionError(-1010, ex.Message) };
            }
        }
    }
}
