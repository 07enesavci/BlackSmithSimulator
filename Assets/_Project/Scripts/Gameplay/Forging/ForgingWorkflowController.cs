using UnityEngine;

namespace BlacksmithSimulator.Gameplay.Forging
{
    public sealed class ForgingWorkflowController : MonoBehaviour
    {
        public enum ForgingStep
        {
            LoadCoal,
            RaiseHeat,
            HeatIngot,
            HammerOnAnvil,
            Quench,
            SharpenAndAssemble,
            Completed
        }

        [SerializeField] private ForgingStep currentStep = ForgingStep.LoadCoal;

        public ForgingStep CurrentStep => currentStep;

        public void AdvanceStep()
        {
            if (currentStep == ForgingStep.Completed)
            {
                return;
            }

            currentStep++;
        }
    }
}
