using System;
using System.Collections;
using SI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace SpaceInvadersTest
{
    public class WaitTaskTest
    {
        [UnityTest]
        public IEnumerator While_WhileWait5Second_Wait5Seconds()
        {
            // Arrange
            bool wait = true;

            bool waitEnded = false;
            bool waitEndedEarlyOn = false;

            // Act
            WaitWhileAsync(() => waitEnded = true);

            if (waitEnded)
                waitEndedEarlyOn = true;

            yield return new WaitForSeconds(5);

            if (waitEnded)
                waitEndedEarlyOn = true;

            wait = false;

            yield return null;

            // Assert
            Assert.IsTrue(waitEnded);
            Assert.IsFalse(waitEndedEarlyOn);

            yield return null;

            async void WaitWhileAsync(Action onWaitEnded)
            {
                await WaitTask.While(() => wait);
                onWaitEnded?.Invoke();
            }
        }
    }
}
