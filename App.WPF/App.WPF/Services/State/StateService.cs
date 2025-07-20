using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Services.State
{
    public class StateService : IStateService
    {
        public StateService() {
            
        }
        public int UserId { get ; set ; }
    }
}
