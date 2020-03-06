using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.DataModel;

namespace Siemens.Mom.Presales.Training.MasterData.TrainingLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class UpdateTeamHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private UpdateTeam.Response UpdateTeamHandler(UpdateTeam command)
        {
            try
            {
                var existEntity = platform.GetEntity<ITeam>(command.Id);
                if(existEntity != null){
                    existEntity.Name = command.Team.Name;
                    existEntity.Description = command.Team.Description;
                    existEntity.Number = command.Team.Number;
                    existEntity.IsActive = command.IsActive;
                    platform.Submit(existEntity);
                }

                return new UpdateTeam.Response();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
