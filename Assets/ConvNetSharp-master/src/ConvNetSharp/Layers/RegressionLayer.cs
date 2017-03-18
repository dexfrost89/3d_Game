using System;
using UnityEngine;
using System.Runtime.Serialization;

namespace ConvNetSharp.Layers
{
    /// <summary>
    ///     implements an L2 regression cost layer,
    ///     so penalizes \sum_i(||x_i - y_i||^2), where x is its input
    ///     and y is the user-provided array of "correct" values.
    /// </summary>
    [DataContract]
    public class RegressionLayer : LayerBase, ILastLayer
    {
        public RegressionLayer(int neuronCount)
        {
            this.NeuronCount = neuronCount;
        }

        [DataMember]
        public int NeuronCount { get; private set; }

        public double Backward(double y)
        {
            // compute and accumulate gradient wrt weights and bias of this layer
            var x = this.InputActivation;
            x.WeightGradients = new double[x.Weights.Length]; // zero out the gradient of input Vol
            var loss = 0.0;

            // lets hope that only one number is being regressed
            var dy = x.Weights[0] - y;
            x.WeightGradients[0] = dy;
            loss += 0.5 * dy * dy;

            return loss;
        }

        public double Backward(double[] y)
        {
            // compute and accumulate gradient wrt weights and bias of this layer
            var x = this.InputActivation;
            x.WeightGradients = new double[x.Weights.Length]; // zero out the gradient of input Vol
            var loss = 0.0;

            for (var i = 0; i < this.OutputDepth; i++)
            {
                var dy = x.Weights[i] - y[i];
                x.WeightGradients[i] = dy;
                loss += 0.5 * dy * dy;
            }

            return loss;
        }

        public double Backward(ystr y)
        {
            var x = this.InputActivation;
            x.WeightGradients = new double[x.Weights.Length];
            var loss = 0.0;
            var i = y.dim;
            var yi = y.val;
            var dy = x.Weights[i] - yi;
            x.WeightGradients[i] = dy;
            loss += 0.5 * dy * dy;

            return loss;
        }
 
        public override Volume Forward(Volume input, bool isTraining = false)
        {
            this.InputActivation = input;
            this.OutputActivation = input;
            return input; // identity function
        }

        public override void Backward()
        {
            throw new NotImplementedException();
        }

        public override void Init(int inputWidth, int inputHeight, int inputDepth)
        {
            base.Init(inputWidth, inputHeight, inputDepth);

            var inputCount = inputWidth * inputHeight * inputDepth;
            this.OutputDepth = inputCount;
            this.OutputWidth = 1;
            this.OutputHeight = 1;
        }

        public override void Save(string name)
        {
            PlayerPrefs.SetString(name + ".WasSaved", "t");

            PlayerPrefs.SetInt(name + ".NeuronCount", this.NeuronCount);

            PlayerPrefs.SetInt(name + ".OutputDepth", this.OutputDepth);

            PlayerPrefs.SetInt(name + ".OutputHeight", this.OutputHeight);

            PlayerPrefs.SetInt(name + ".OutputWidth", this.OutputWidth);
        }

        public override bool Load(string name)
        {
            if(PlayerPrefs.HasKey(name + ".WasSaved"))
            {
                if(PlayerPrefs.GetString(name + ".WasSaved") == "t")
                {
                    this.NeuronCount = PlayerPrefs.GetInt(name + ".NeuronCount");

                    this.OutputDepth = PlayerPrefs.GetInt(name + ".OutputDepth");

                    this.OutputHeight = PlayerPrefs.GetInt(name + ".OutputHeight");

                    this.OutputWidth = PlayerPrefs.GetInt(name + ".OutputWidth");

                    return true;
                }
            }
            return false;
        }
    }
}