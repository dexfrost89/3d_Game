using System.Runtime.Serialization;
using UnityEngine;

namespace ConvNetSharp.Layers
{
    [DataContract]
    public sealed class InputLayer : LayerBase
    {
        public InputLayer(int inputWidth, int inputHeight, int inputDepth)
        {
            this.Init(inputWidth, inputHeight, inputDepth);

            this.OutputWidth = inputWidth;
            this.OutputHeight = inputHeight;
            this.OutputDepth = inputDepth;
        }

        public override Volume Forward(Volume input, bool isTraining = false)
        {
            this.InputActivation = input;
            this.OutputActivation = input;
            return this.OutputActivation; // simply identity function for now
        }

        public override void Backward()
        {
        }

        public override void Save(string name)
        {
            PlayerPrefs.SetString(name + ".WasSaved", "t");

            PlayerPrefs.SetInt(name + ".OutputWidth", this.OutputWidth);

            PlayerPrefs.SetInt(name + ".OutputHeight", this.OutputHeight);

            PlayerPrefs.SetInt(name + ".OutputDepth", this.OutputDepth);
        }

        public override bool Load(string name)
        {
            if(PlayerPrefs.HasKey(name + ".WasSaved"))
            {
                if(PlayerPrefs.GetString(name + ".WasSaved") == "t")
                {

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