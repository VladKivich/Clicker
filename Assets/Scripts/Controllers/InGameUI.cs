using Models;
using UnityEngine;
using UnityEngine.UI;
using BaseScripts;

namespace Controllers
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private Image gpuImage;
        [SerializeField] private Image cpuImage;
        [SerializeField] private Text Amount;
        [SerializeField] private Text IncomePerSecond;

        private ProgressBarCPU cpu;
        private ProgressBarGPU gpu;
        private GameProgress progress;

        public void SetModels(ClickerController clickerController)
        {
            progress = Core.GetProgress;
            cpu = clickerController.cpu;
            gpu = clickerController.gpu;
            cpu.onModelUpdate += OnCPUModelUpdate;
            gpu.onModelUpdate += OnGPUModelUpdate;
            progress.OnModelUpdate += OnAmountUpdate;
            cpu.Reset();
            gpu.Reset();
        }

        private void OnAmountUpdate()
        {
            Amount.text = progress.currentAmount + " $";
        }

        private void OnCPUModelUpdate()
        {
            cpuImage.fillAmount = cpu.GetCurrentAmount;
        }

        private void OnGPUModelUpdate()
        {
            gpuImage.fillAmount = gpu.GetCurrentAmount;
        }
    }
}
