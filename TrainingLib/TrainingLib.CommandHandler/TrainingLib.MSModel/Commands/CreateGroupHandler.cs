using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.SDK.Diagnostics.Common;
using Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.DataModel;

namespace Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class CreateGroupHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private CreateGroup.Response CreateGroupHandler(CreateGroup command)
        {
            ITracer tracer = platform.Tracer;

            try
            {
                var group = platform.Query<ITeamGroup>().FirstOrDefault(p => p.Name == command.Name);
                if (group != null)
                {
                    return new CreateGroup.Response { Error = new ExecutionError(-1010, "不允许重复创建") };
                }

                var entity = platform.Create<ITeamGroup>();
                entity.Name = command.Name;
                entity.Description = command.Description;
                platform.Submit(entity);

                return new CreateGroup.Response { Id = entity.Id };

            }
            catch (Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
                return new CreateGroup.Response { Error = new ExecutionError(-1010, ex.Message) };
            }
        }
    }
}
