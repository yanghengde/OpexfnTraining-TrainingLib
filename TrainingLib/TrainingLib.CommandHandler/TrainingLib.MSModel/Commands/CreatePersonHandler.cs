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
    public partial class CreatePersonHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private CreatePerson.Response CreatePersonHandler(CreatePerson command)
        {
            ITracer tracer = platform.Tracer;

            try
            {
                var teams = platform.Query<IPerson>().FirstOrDefault(p => p.Name == command.Name);
                if (teams != null)
                {
                    return new CreatePerson.Response { Error = new ExecutionError(-1010, "不允许重复创建") };
                }

                var entity = platform.Create<IPerson>();
                entity.Name = command.Name;
                platform.Submit(entity);

                return new CreatePerson.Response { Id = entity.Id };

            }
            catch (Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
                return new CreatePerson.Response { Error = new ExecutionError(-1010, ex.Message) };
            }
        }
    }
}
