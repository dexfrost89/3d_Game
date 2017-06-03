using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class backward_test {

    [Test]
    public void BackwardWithRewardTest()
    {
        var component = new from_field_to_text();
        component.init(11, 5, 3);
        double[] testmas = new double[component.num_inputs];
        component.start_learn();
        for(int i = 0; i < 100; i++)
        {
            for (int j = 0; j < testmas.Length; j++)
            {
                testmas[j] = Random.Range(0, 10);
            }

        }

        for (int i = 0; i < component.temporal_window; i++)
        {
            component.input(testmas);
        }


        double[] res = component.brain.getNetInput(testmas);
    }
}
