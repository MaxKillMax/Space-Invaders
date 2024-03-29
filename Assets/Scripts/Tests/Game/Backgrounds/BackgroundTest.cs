using System.Collections;
using SI.Backgrounds;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace SpaceInvadersTest.Backgrounds
{
    public class BackgroundTest
    {
        [UnityTest]
        public IEnumerator Background_Rotate5Seconds_RotatesOn5Degrees()
        {
            // Arrange
            SI.Inputs.Input input = new();
            Background background = new GameObject().AddComponent<Background>();

            float time = 5;
            int expectedDegrees = Mathf.RoundToInt(background.DegreesPerSecond * time);

            // Act
            background.Initialize();

            while (time > 0)
            {
                yield return new WaitForEndOfFrame();
                time -= Time.deltaTime;
                input.Update();
            }

            int degrees = Mathf.RoundToInt((360 - background.transform.rotation.eulerAngles.z) * -1);

            // Assert
            Assert.IsTrue(expectedDegrees == degrees);

            yield return null;
        }
    }
}
