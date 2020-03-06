using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.DataModel;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.SDK.Diagnostics.Common;
using Siemens.Mom.Presales.Training.Training.TrainingLibView.TNModel.DataModel.ProjectionModel;

namespace Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class CreateTeamHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private CreateTeam.Response CreateTeamHandler(CreateTeam command)
        {
            ITracer tracer = platform.Tracer;
            tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "Create Team Start...");

            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "command", command }
            };
            platform.ApplicationLog(-1010, dict);

            try
            {
                var teams = platform.Query<ITeam>().FirstOrDefault(p => p.Name == command.Team.Name);
                if(teams != null)
                {
                    return new CreateTeam.Response { Error = new ExecutionError(-1010, "不允许重复创建") };
                }

                var entity = platform.Create<ITeam>();
                entity.Name = command.Team.Name;
                entity.Description = command.Team.Description;
                entity.Number = command.Team.Number;
                entity.IsActive = command.IsActive;
                platform.Submit(entity);

                return new CreateTeam.Response { Id = entity.Id };

            }catch(Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
                return new CreateTeam.Response { Error = new ExecutionError(-1010, ex.Message) };
            }
        }
    }
}
