using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using ConvNetSharp.Layers;
using ConvNetSharp;
using ConvNetSharp.Training;

public class from_field_to_text : MonoBehaviour
{

    //public int shtsht;

    public class tdtrainer_options {
        public double learning_rate = 0;
        public double momentum = 0;
        public int batch_size = 0;
        public double l2_decay = 0;

        public void Save(string name)
        {
            PlayerPrefs.SetString(name + ".WasSaved", "t");

            PlayerPrefs.SetInt(name + ".batch_size", this.batch_size);

            PlayerPrefs.SetString(name + ".learning_rate", this.learning_rate.ToString());

            PlayerPrefs.SetString(name + ".momentum", this.momentum.ToString());

            PlayerPrefs.SetString(name + ".l2_decay", this.l2_decay.ToString());

        }

        public bool Load(string name)
        {
            if(PlayerPrefs.HasKey(name + ".WasSaved"))
            {
                if(PlayerPrefs.GetString(name + ".WasSaved") == "t")
                {
                    batch_size = PlayerPrefs.GetInt(name + ".batch_size");

                    learning_rate = Convert.ToDouble(PlayerPrefs.GetString(name + ".learning_rate"));

                    momentum = Convert.ToDouble(PlayerPrefs.GetString(name + ".momentum"));

                    l2_decay = Convert.ToDouble(PlayerPrefs.GetString(name + ".l2_decay"));
                }
            }

            return false;
        }

        public void DeleteSave(string name)
        {
            PlayerPrefs.SetString(name + ".WasSaved", "f");
        }
    }

    public class Layer
    {
        public string type = "";
        public int out_sx = 0, out_sy = 0, out_depth = 0;
        public string activation = "";
        public int num_neurons = 0;
        public Layer(string type, int out_sx, int out_sy, int out_depth)
        {
            this.type = type;
            this.out_sx = out_sx;
            this.out_sy = out_sy;
            this.out_depth = out_depth;
        }

        public Layer(string type, int num_neurons, string activation)
        {
            this.type = type;
            this.num_neurons = num_neurons;
            this.activation = activation;
        }

        public Layer(string type, int num_neurons)
        {
            this.type = type;
            this.num_neurons = num_neurons;
        }
    }


    public class opt
    {
        public Layer[] layer_defs;
        public int temporal_window, experience_size, start_learn_threshold;
        public double gamma;
        public int learning_steps_total, learning_steps_burnin;
        public double epsilon_min, epsilon_test_time;
        public int[] hidden_layer_sizes;
        public tdtrainer_options tdtrainer_options;
    }


    public class deepqlearn
    {



        /*
            public void Save(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "t");
            }

            public bool Load(string name)
            {
                if(PlayerPrefs.HasKey(name + ".WasSaved"))
                {
                    if(PlayerPrefs.GetString(name + ".WasSaved") == "t")
                    {
                        return true;
                    }
                }
                return false;
            }

            public void DeleteSaves(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "f");
            }
            */


        // An agent is in state0 and does action0
        // environment then assigns reward0 and provides new state, state1
        // Experience nodes store all this information, which is used in the
        // Q-learning update step


        public class Experience
        {
            public double[] state0 = new double[0], state1 = new double[0];
            public int action0 = 0;
            public double reward0 = 0;
            public Experience(double[] state0, int action0, double reward0, double[] state1)
            {
                this.state0 = state0;
                this.state1 = state1;
                this.action0 = action0;
                this.reward0 = reward0;
            }
            public Experience()
            {

            }

            public void Save(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "t");
                PlayerPrefs.SetInt(name + ".action0", this.action0);
                PlayerPrefs.SetInt(name + ".state0.Length", this.state0.Length);
                for(int i = 0; i < this.state0.Length; i++)
                {
                    PlayerPrefs.SetString(name + ".state0[" + i.ToString() + "]", state0[i].ToString());
                }
                PlayerPrefs.SetInt(name + ".state1.Length", this.state1.Length);
                for (int i = 0; i < this.state1.Length; i++)
                {
                    PlayerPrefs.SetString(name + ".state1[" + i.ToString() + "]", state1[i].ToString());
                }
                PlayerPrefs.SetString(name + ".reward0", this.reward0.ToString());
            }

            public bool Load(string name)
            {
                if(PlayerPrefs.HasKey(name + ".WasSaved"))
                {
                    if(PlayerPrefs.GetString(name + ".WasSaved") == "t")
                    {
                        this.action0 = PlayerPrefs.GetInt(name + ".action0");
                        this.state0 = new double[PlayerPrefs.GetInt(name + ".state0.Length")];
                        for (int i = 0; i < state0.Length; i++)
                        {
                            try
                            {
                                this.state0[i] = Convert.ToDouble(PlayerPrefs.GetString(name + "state0[" + i.ToString() + "]"));
                            }
                            catch
                            {
                                this.state0[i] = 0;
                            }
                        }
                        this.state1 = new double[PlayerPrefs.GetInt(name + ".state1.Length")];
                        for (int i = 0; i < state1.Length; i++)
                        {
                            try
                            {

                                this.state1[i] = Convert.ToDouble(PlayerPrefs.GetString(name + "state1[" + i.ToString() + "]"));
                            }
                            catch
                            {
                                this.state1[i] = 0;
                            }
                        }

                        this.reward0 = Convert.ToDouble(PlayerPrefs.GetString(name + ".reward0"));

                        return true;
                    }
                }
                return false;
            }

            public void DeleteSaves(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "f");
            }
        }

        public class window
        {
            public List<double> avav;
            public double average;
            int n = 0;

            public window()
            {
                avav = new List<double>();
                average = 0;
                n = 0;
            }

            public void add(double value)
            {
                avav.Add(value);
                if(n == 0)
                {
                    n = 1;
                    average = value;
                    avav.Add(value);
                } else
                {
                    n++;
                    avav.Add(value);
                   
                }
            }

            public double get_average()
            {
                average = 0;

                for (int i = 0; i < n; i++)
                {
                    average += avav[i] / n;
                }
                return average;
            }


            public void Save(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "t");
                PlayerPrefs.SetInt(name + ".n", this.n);
                PlayerPrefs.SetInt(name + ".avav.Count", this.avav.Count);
                for(int i = 0; i < this.avav.Count; i++)
                {
                    PlayerPrefs.SetString(name + ".avav[" + i.ToString() + "]", avav[i].ToString());
                }
            }

            public bool Load(string name)
            {
                if (PlayerPrefs.HasKey(name + ".WasSaved"))
                {
                    if (PlayerPrefs.GetString(name + ".WasSaved") == "t")
                    {
                        this.n = PlayerPrefs.GetInt(name + ".n");
                        avav.Clear();
                        for(int i = 0; i < n; i++)
                        {
                            avav.Add(Convert.ToDouble(PlayerPrefs.GetString(name + ".avav[" + i.ToString() + "]")));
                        }

                        return true;
                    }
                }
                return false;
            }

            public void DeleteSaves(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "f");
            }
        }


        // A Brain object does all the magic.
        // over time it receives some inputs and some rewards
        // and its job is to set the outputs to maximize the expected reward

        public class Brain
        {


            public Net makeLayers(Layer[] layer_defs)
            {
                Net res = new ConvNetSharp.Net();
                for (int i = 0; i < layer_defs.Length; i++)
                {
                    if (layer_defs[i].type == "input")
                    {
                        res.AddLayer(new InputLayer(layer_defs[i].out_sx, layer_defs[i].out_sy, layer_defs[i].out_depth));
                    }
                    else if (layer_defs[i].type == "fc")
                    {
                        if (layer_defs[i].activation == "relu")
                        {
                            res.AddLayer(new FullyConnLayer(layer_defs[i].num_neurons, Activation.Relu));
                        }
                    }
                    else if (layer_defs[i].type == "regression")
                    {
                        res.AddLayer(new RegressionLayer(layer_defs[i].num_neurons));
                    }
                }
                return res;
            }

            public double[] state0 = new double[0], state1 = new double[0];
            public int action0;
            public double reward0;
            public Layer[] layer_defs;
            public int temporal_window, experience_size, start_learn_threshold;
            public double gamma;
            public int learning_steps_total, learning_steps_burnin;
            public double epsilon_min, epsilon_test_time;
            public int[] hidden_layer_sizes;
            public tdtrainer_options tdtrainer_options;
            public int net_inputs;
            public int num_states;
            public int num_actions;
            public int window_size;
            public double[][] state_window;
            public double[] reward_window;
            public int[] action_window;
            public double[][] net_window;
            public Net value_net;
            public SgdTrainer tdtrainer;
            public Experience[] experience;
            public int hlp_size;
            public int age, forward_passes;
            public double epsilon = 1.0;
            public double latest_reward;
            public window average_reward_window = new window();
            public window average_loss_window = new window();
            public double[] last_input_array;
            public bool learning;



            public int test;
            


            public void Save(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "t");
                this.value_net.Save(name + ".value_net");
                PlayerPrefs.SetInt(name + ".test", test);
                Debug.Log("Successful save");
                test++;

            }

            public bool Load(string name)
            {
                if (PlayerPrefs.HasKey(name + ".WasSaved"))
                {
                    if (PlayerPrefs.GetString(name + ".WasSaved") == "t")
                    {
                        if (!this.value_net.Load(name + ".value_net"))
                        {
                            return false;
                        }
                        test = PlayerPrefs.GetInt(name + ".test");
                        Debug.Log("Successful load");
                        return true;
                    }
                }
                return false;
            }

            public void DeleteSaves(string name)
            {
                PlayerPrefs.SetString(name + ".WasSaved", "f");
            }



            public Brain(int num_states, int num_actions, opt opt)
            {
                this.temporal_window = opt.temporal_window;
                this.experience_size = opt.experience_size;
                this.start_learn_threshold = opt.start_learn_threshold;
                this.gamma = opt.gamma;
                this.learning_steps_total = opt.learning_steps_total;
                this.learning_steps_burnin = opt.learning_steps_burnin;
                this.epsilon_min = opt.epsilon_min;
                this.epsilon_test_time = opt.epsilon_test_time;


                this.net_inputs = num_states * this.temporal_window + num_actions * this.temporal_window + num_states;
                this.num_states = num_states;
                this.num_actions = num_actions;
                this.window_size = Mathf.Max(this.temporal_window, 2);
                this.state_window = new double[this.window_size][];
                for(int i = 0; i < this.window_size; i++)
                {
                    this.state_window[i] = new double[0];
                }
                this.action_window = new int[this.window_size];
                this.reward_window = new double[this.window_size];
                this.net_window = new double[this.window_size][];
                for (int i = 0; i < this.window_size; i++)
                {
                    this.net_window[i] = new double[0];
                }
                layer_defs = opt.layer_defs;
                this.value_net = new Net();
                this.value_net = makeLayers(layer_defs);
                tdtrainer_options = opt.tdtrainer_options;
                this.tdtrainer = new SgdTrainer(this.value_net);
                tdtrainer.LearningRate = this.tdtrainer_options.learning_rate;
                tdtrainer.BatchSize = this.tdtrainer_options.batch_size;
                tdtrainer.L2Decay = this.tdtrainer_options.l2_decay;
                tdtrainer.Momentum = this.tdtrainer_options.momentum;
                experience = new Experience[0];
                hlp_size = 0;
                this.age = 0;
                this.forward_passes = 0;
                this.epsilon = 1.0;
                this.latest_reward = 0;
                this.last_input_array = new double[0];
                this.learning = true;
            }



            public int random_action()
            {
                return UnityEngine.Random.Range(0, this.num_actions);
            }

            public class polic
            {
                public int action;
                public double value;
            }

            public polic policy(double[] s)
            {
                polic res = new polic();
                Volume svol = new Volume(1, 1, this.net_inputs);
                svol.Weights = s;
                var action_values = this.value_net.Forward(svol);
                var maxk = 0;
                var maxval = action_values.Weights[0];
                for(int k = 1; k < this.num_actions; k++)
                {
                    if(action_values.Weights[k] > maxval)
                    {
                        maxk = k;
                        maxval = action_values.Weights[k];
                    }
                }
                res.action = maxk;
                res.value = maxval;
                return res;
            }

            public double[] concat(double[] a, double[] b)
            {
                double[] res;
                /*if(b == null)
                {
                    return a;
                }
                */
                res = new double[a.Length + b.Length];
                for(int i = 0; i < a.Length; i++)
                {
                    res[i] = a[i];
                }
                for(int i = 0; i < b.Length; i++)
                {
                    res[i + a.Length] = b[i];
                }
                return res;
                //*/return a;
            }

            public double[] getNetInput(double[] xt)
            {
                double[] w = new double[0];
                w = concat(w, xt);
                var n = this.window_size;
                for(var k = 0; k < this.temporal_window; k++)
                {
                    w = concat(w, this.state_window[n - 1 - k]);
                    var action1ofk = new double[this.num_actions];
                    for(int q = 0; q < this.num_actions; q++)
                    {
                        action1ofk[q] = 0.0;
                    }
                    action1ofk[this.action_window[n - 1 - k]] = 1.0 * this.num_states;
                    w = concat(w, action1ofk);
                }
                return w;
                //*/return xt;
            }

            public double[] shift_push(double[] a, double b)
            {
                double[] res = new double[a.Length];
                for(int i = 0; i < a.Length - 1; i++)
                {
                    res[i] = a[i + 1];
                }
                res[a.Length - 1] = b;
                return res;
            }

            public double[][] shift_push(double[][] a, double[] b)
            {
                double[][] res = new double[a.Length][];
                for (int i = 0; i < a.Length - 1; i++)
                {
                    res[i] = a[i + 1];
                }
                res[a.Length - 1] = b;
                return res;
            }

            public int[] shift_push(int[] a, int b)
            {
                int[] res = new int[a.Length];
                for (int i = 0; i < a.Length - 1; i++)
                {
                    res[i] = a[i + 1];
                }
                res[a.Length - 1] = b;
                return res;
            }

            public int forward(double[] input_array)
            {
                var net_input = this.getNetInput(input_array);
                this.forward_passes++;
                this.last_input_array = input_array;
                int action;

                if(this.forward_passes > this.temporal_window)
                {
                    net_input = this.getNetInput(input_array);
                    if(this.learning)
                    {
                        this.epsilon = Mathf.Min((float)1.0, Mathf.Max((float)this.epsilon_min, (float)(1.0 - (float)(this.age - this.learning_steps_burnin) / 
                            (float)(this.learning_steps_total - this.learning_steps_burnin))));
                    } else
                    {
                        this.epsilon = this.epsilon_test_time;
                    }

                  /*  Debug.Log(epsilon);
                    Debug.Log((float)this.epsilon_min);
                    Debug.Log((float)(1.0 - (float)(this.age - this.learning_steps_burnin) /
                            (float)(this.learning_steps_total - this.learning_steps_burnin)));*/
                    var rf = UnityEngine.Random.Range((float) 0.0, (float) 1);
                    if(rf < this.epsilon)
                    {
                        action = this.random_action();
                    } else
                    {
                        var maxact = this.policy(net_input);
                        action = maxact.action;
                    }
                } else
                {
                    net_input = new double[0];
                    action = this.random_action();
                }
                this.net_window = shift_push(this.net_window, net_input);
                this.state_window = shift_push(this.state_window, input_array);
                this.action_window = shift_push(this.action_window, action);


                return action;
               //*/ return 1;
            }



            Experience[] push(Experience[] a, Experience b)
            {
                Experience[] res = new Experience[a.Length + 1];
                for(int i = 0; i < a.Length; i++)
                {
                    res[i] = a[i];
                }
                res[a.Length] = b;
                return res;
            }

            public void backward(double reward)
            {
                this.latest_reward = reward;
                this.average_reward_window.add(reward);
                this.reward_window = shift_push(this.reward_window, reward);
                if(!this.learning)
                {
                    return;
                }

                this.age++;



                if(this.forward_passes > this.temporal_window + 1)
                {
                    var e = new Experience();
                    var n = this.window_size;
                    e.state0 = this.net_window[n - 2];
                    e.action0 = this.action_window[n - 2];
                    e.reward0 = this.reward_window[n - 2];
                    e.state1 = this.net_window[n - 1];
                    if(this.experience.Length < this.experience_size)
                    {
                        this.experience = push(this.experience, e);
                    } else
                    {
                        var ri = UnityEngine.Random.Range(0, this.experience_size);
                        this.experience[ri] = e;
                    }
                }

                


                if(this.experience.Length > this.start_learn_threshold)
                {
                    var avcost = 0.0;
                    for(var k = 0; k < this.tdtrainer.BatchSize; k++)
                    {
                        var re = UnityEngine.Random.Range(0, this.experience.Length);
                        var e = this.experience[re];
                        var x = new Volume(1, 1, this.net_inputs);
                        x.Weights = e.state0;
                        var maxact = this.policy(e.state1);
                        var r = e.reward0 + this.gamma * maxact.value;
                        var ystruct = new ystr();
                        ystruct.dim = e.action0;
                        ystruct.val = r;
                        double loss = this.tdtrainer.Train(x, ystruct);
                        avcost += loss;
                    }
                    avcost = avcost / this.tdtrainer.BatchSize;
                    this.average_loss_window.add(avcost);
                }
                visSelf("out.txt");
            }

            

            public void visSelf(string fileName)
            {
                if(!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                if(this.age == 1)
                {
                    File.WriteAllLines(fileName, new[] { ""});
                }

                File.AppendAllText(fileName, this.age.ToString() + " " + this.average_loss_window.get_average().ToString() + 
                    " " + this.average_reward_window.get_average().ToString() + " " + this.latest_reward.ToString() + "\n");
            }

            
        }





    }


    public Layer[] push(Layer[] a, Layer b)
    {
        Layer[] res = new Layer[a.Length + 1];
        for(int i = 0; i < a.Length; i++)
        {
            res[i] = a[i];
        }
        res[a.Length] = b;
        return res;
    }


    public Net my_net;
    public from_field_to_text.opt opt1 = new opt();
    public Layer[] layer_defs = new Layer[0];
    public int num_inputs = 10;
    public int num_actions = 4;
    public int temporal_window = 3;
    public int network_size;
    public deepqlearn.Brain brain;


    public GameObject input1, input2, reward1, text1;
    public double test;

    public void reward(double reward)
    {
        brain.backward(reward);
    }

    public int input(double[] inputs)
    {
        return brain.forward(inputs);
    }


    public void save()
    {
        brain.Save(AiName);
        test = brain.test;
    }

    public void load()
    {
        brain.Load(AiName);
        test = brain.test;
    }

    public void reset()
    {
        brain.DeleteSaves(AiName);
        PlayerPrefs.DeleteAll();
        init(11, 5, 3);
    }

    
    /*
    public void save()
    {
        PlayerPrefs.g
    }*/

    public string AiName;

	// Use this for initialization
	void Start () {
        init(11, 5, 3);


    }

    public void stop_learn()
    {
        brain.learning = false;
    }
    public void start_learn()
    {
        brain.learning = true;
    }

    public void init(int inps, int acs, int temp)
    {



        opt1 = new opt();
        layer_defs = new Layer[0];
        num_inputs = inps;
        num_actions = acs;
        temporal_window = temp;
        
        
        network_size = num_inputs * temporal_window + num_actions * temporal_window + num_inputs;
        layer_defs = push(layer_defs, new Layer("input", 1, 1, network_size));
        layer_defs = push(layer_defs, new Layer("fc", 50, "relu"));
        layer_defs = push(layer_defs, new Layer("fc", 50, "relu"));
        layer_defs = push(layer_defs, new Layer("regression", num_actions));
        opt1.layer_defs = layer_defs;
        opt1.temporal_window = temporal_window;
        opt1.experience_size = 3000;
        opt1.start_learn_threshold = 1000;
        opt1.gamma = 0.7;
        opt1.learning_steps_total = 20000;
        opt1.learning_steps_burnin = 3000;
        opt1.epsilon_min = 0.2;
        opt1.epsilon_test_time = 0.0;
        tdtrainer_options nev = new tdtrainer_options();
        nev.learning_rate = 0.02;
        nev.momentum = 0.0;
        nev.batch_size = 64;
        nev.l2_decay = 0.01;
        opt1.tdtrainer_options = nev;
        brain = new deepqlearn.Brain(num_inputs, num_actions, opt1);



    }

    // Update is called once per frame
    void Update () {
		
	}
}
