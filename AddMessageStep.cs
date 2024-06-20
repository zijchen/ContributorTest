using Microsoft.SqlServer.Dac.Deployment;

namespace ContributorTest
{
    [ExportDeploymentPlanModifier(typeof(AddMessageStep), "1.0.0.0")]
    public class AddMessageStep : DeploymentPlanModifier
    {
        protected override void OnExecute(DeploymentPlanContributorContext context)
        {
            DeploymentStep next = context.PlanHandle.Head;
            while (next != null)
            {
                // Add a print message step before the BeginPreDeploymentScriptStep
                if (next is BeginPostDeploymentScriptStep)
                {
                    DeploymentStep printMessage = new DeploymentScriptStep("PRINT 'Hello from custom plan modifier'");
                    AddBefore(context.PlanHandle, next, printMessage);
                    break;
                }
                next = next.Next;
            }
        }
    }
}