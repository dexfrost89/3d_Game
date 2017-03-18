using System;
using System.Diagnostics;

namespace ConvNetSharp.Training
{
    public abstract class TrainerBase
    {
        protected readonly Net Net;
        protected int K; // iteration counter

        protected TrainerBase(Net net)
        {
            this.Net = net;

            this.BatchSize = 1;
        }

        public TimeSpan BackwardTime { get; private set; }

        public double CostLoss { get; private set; }

        public TimeSpan ForwardTime { get; private set; }

        public int BatchSize { get; set; }

        public virtual double Loss
        {
            get { return this.CostLoss; }
        }

        public double Train(Volume x, double y)
        {
            this.Forward(x);

            var CostLoss = this.Backward(y);

            this.TrainImplem();

            return CostLoss;
        }

        public double Train(Volume x, double[] y)
        {
            this.Forward(x);

            var CostLoss = this.Backward(y);

            this.TrainImplem();

            return CostLoss;
        }

        public double Train(Volume x, ConvNetSharp.Layers.ystr y)
        {
            this.Forward(x);

            var CostLoss = this.Backward(y);

            this.TrainImplem();

            return CostLoss;
        }

        protected abstract void TrainImplem();

        protected virtual double Backward(double y)
        {
            var chrono = Stopwatch.StartNew();
            this.CostLoss = this.Net.Backward(y);
            this.BackwardTime = chrono.Elapsed;
            return CostLoss;
        }

        protected virtual double Backward(double[] y)
        {
            var chrono = Stopwatch.StartNew();
            this.CostLoss = this.Net.Backward(y);
            this.BackwardTime = chrono.Elapsed;
            return CostLoss;
        }

        protected virtual double Backward(ConvNetSharp.Layers.ystr y)
        {
            var chrono = Stopwatch.StartNew();
            this.CostLoss = this.Net.Backward(y);
            this.BackwardTime = chrono.Elapsed;
            return CostLoss;
        }

        private void Forward(Volume x)
        {
            var chrono = Stopwatch.StartNew();
            this.Net.Forward(x, true); // also set the flag that lets the net know we're just training
            this.ForwardTime = chrono.Elapsed;
        }
    }
}