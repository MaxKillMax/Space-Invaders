using System;
using System.Collections;
using SpaceInvaders;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace SpaceInvadersTest
{
    public class VectorExtensionsTest
    {
        [UnityTest]
        public IEnumerator DirectionTo_FromVector2ZeroToVector2One_ReturnRightDirectionAndDistance()
        {
            // Arrange
            Vector2 firstVector = Vector2.zero;
            Vector2 secondVector = Vector2.one;

            Vector2 expectedDirection = new(0.7071f, 0.7071f);
            float expectedDistance = MathF.Sqrt(2);

            // Act
            Vector2 direction = firstVector.DirectionTo(secondVector, out float distance);

            // Assert
            Assert.IsTrue(expectedDirection == direction);
            Assert.IsTrue(expectedDistance == distance);

            yield return null;
        }

        [UnityTest]
        public IEnumerator DirectionTo_FromVector3ZeroToVector3One_ReturnRightDirectionAndDistance()
        {
            // Arrange
            Vector3 firstVector = Vector3.zero;
            Vector3 secondVector = Vector3.one;

            Vector3 expectedDirection = new(0.57735f, 0.57735f, 0.57735f);
            float expectedDistance = MathF.Sqrt(3);

            // Act
            Vector3 direction = firstVector.DirectionTo(secondVector, out float distance);

            // Assert
            Assert.IsTrue(expectedDirection == direction);
            Assert.IsTrue(expectedDistance == distance);

            yield return null;
        }
    }
}
