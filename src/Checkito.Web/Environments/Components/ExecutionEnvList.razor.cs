using Checkito.Data.Environments.Queries;
using Checkito.Environments.Models;
using Checkito.Environments.Services;
using Checkito.Web.General.Components;

namespace Checkito.Web.Environments.Components
{
    public class ExecutionEnvListBase : ScopedComponentBase
    {
        private IExecutionEnvService _executionEnvService = default!;

        public IList<ExecutionEnv> ExecutionEnvs { get; set; } = new List<ExecutionEnv>();
        protected override void OnParametersSet()
        {
            _executionEnvService = GetService<IExecutionEnvService>();
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new GetExecutionEnvsQuery());
            ExecutionEnvs = result.ExecutionEnvs;
        }

        public async Task OnOk()
        {
            await _executionEnvService.Save(ExecutionEnvs);
        }

        public void OnRemoveExecutionEnv(ExecutionEnv executionEnv)
        {
            ExecutionEnvs.Remove(executionEnv);
        }

        public void OnSelectEnvironmentEnv(ExecutionEnv executionEnv)
        {
            var selected = ExecutionEnvs.FirstOrDefault(x => x.IsSelected);
            if (selected != null)
            {
                selected.IsSelected = false;
            }

            executionEnv.IsSelected = true;
        }

        public void OnCreateEnvironment()
        {
            var env = new ExecutionEnv { IsSelected = ExecutionEnvs.Count == 0 ? true : false };
            ExecutionEnvs.Add(env);
        }
    }
}
