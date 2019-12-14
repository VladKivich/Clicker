using Models;
using UnityEngine;
using UnityEngine.UI;
using BaseScripts;
using System;
using Controllers;

namespace View
{
    public class InGameUIView : MonoBehaviour
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
            progress = Core.Instance.GetProgress;
            cpu = clickerController.cpu;
            gpu = clickerController.gpu;
            cpu.OnModelUpdate += OnCPUModelUpdate;
            gpu.OnModelUpdate += OnGPUModelUpdate;
            progress.OnModelUpdate += OnAmountUpdate;
            cpu.Reset();
            gpu.Reset();
        }

        private void OnAmountUpdate()
        {
            Amount.text = String.Format($"{progress.currentAmount} $");
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
