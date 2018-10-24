using System;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DUCK.Utils.Tests
{
	[TestFixture]
	public class TransformSnapshotTests
	{
		private Transform transformA;
		private Transform transformB;

		[SetUp]
		public void SetUp()
		{
			transformA = new GameObject().transform;
			transformA.position = new Vector3(1, 2, 3);
			transformA.rotation = Quaternion.Euler(50, 100, 120);

			transformB = new GameObject().transform;
			transformB.position = new Vector3(-3, -22, -1);
			transformB.rotation = Quaternion.Euler(310, 24, 81);

			transformB.SetParent(transformA);
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(transformA.gameObject);
		}

		[Test]
		public void ExpectConstructorNotToThrowWithValidArgs()
		{
			Assert.DoesNotThrow(() => { new TransformSnapshot(transformA); });
		}

		[Test]
		public void ExpectConstructorToThrowWithNullArgs()
		{
			Assert.Throws<ArgumentNullException>(() => { new TransformSnapshot(null); });
		}

		[Test]
		public void ExpectAllPropertiesToBeSetOnSnapshotFromTransform()
		{
			var snapshot = new TransformSnapshot(transformA);

			Assert.AreEqual(transformA.forward, snapshot.Forward);
			Assert.AreEqual(transformA.localEulerAngles, snapshot.LocalEulerAngles);
			Assert.AreEqual(transformA.localPosition, snapshot.LocalPosition);
			Assert.AreEqual(transformA.localRotation, snapshot.LocalRotation);
			Assert.AreEqual(transformA.localScale, snapshot.LocalScale);
			Assert.AreEqual(transformA.parent, snapshot.Parent);
			Assert.AreEqual(transformA.right, snapshot.Right);
			Assert.AreEqual(transformA.up, snapshot.Up);
			Assert.AreEqual(transformA.eulerAngles, snapshot.WorldEulerAngles);
			Assert.AreEqual(transformA.position, snapshot.WorldPosition);
			Assert.AreEqual(transformA.rotation, snapshot.WorldRotation);
			Assert.AreEqual(transformA.lossyScale, snapshot.WorldScale);

			Assert.AreEqual(transformA, snapshot.Transform);
		}

		[Test]
		public void ExpectApplyToToThrowForNullTransforms()
		{
			var snapshot = new TransformSnapshot(transformA);

			Assert.Throws<ArgumentNullException>(() =>
			{
				snapshot.ApplyTo(null);
			});
		}

		[Test]
		public void ExpectApplyToToCopyAllProprtiesToTheTargetTransform()
		{
			var snapshot = new TransformSnapshot(transformA);
			snapshot.ApplyTo(transformB);

			Assert.AreEqual(transformB.forward, snapshot.Forward);
			Assert.AreEqual(transformB.localEulerAngles, snapshot.LocalEulerAngles);
			Assert.AreEqual(transformB.localPosition, snapshot.LocalPosition);
			Assert.AreEqual(transformB.localRotation, snapshot.LocalRotation);
			Assert.AreEqual(transformB.localScale, snapshot.LocalScale);
			Assert.AreEqual(transformB.parent, snapshot.Parent);
			Assert.AreEqual(transformB.right, snapshot.Right);
			Assert.AreEqual(transformB.up, snapshot.Up);
			Assert.AreEqual(transformB.eulerAngles, snapshot.WorldEulerAngles);
			Assert.AreEqual(transformB.position, snapshot.WorldPosition);
			Assert.AreEqual(transformB.rotation, snapshot.WorldRotation);
			Assert.AreEqual(transformB.lossyScale, snapshot.WorldScale);
		}

		[Test]
		public void ExpectApplyToRevertTheOriginalTransformToTheSnapshot()
		{
			var snapshot = new TransformSnapshot(transformA);

			transformA.localPosition += Vector3.one;
			transformA.eulerAngles += Vector3.one;
			transformA.SetParent(transformB);

			snapshot.Apply();

			Assert.AreEqual(transformA.forward, snapshot.Forward);
			Assert.AreEqual(transformA.localEulerAngles, snapshot.LocalEulerAngles);
			Assert.AreEqual(transformA.localPosition, snapshot.LocalPosition);
			Assert.AreEqual(transformA.localRotation, snapshot.LocalRotation);
			Assert.AreEqual(transformA.localScale, snapshot.LocalScale);
			Assert.AreEqual(transformA.parent, snapshot.Parent);
			Assert.AreEqual(transformA.right, snapshot.Right);
			Assert.AreEqual(transformA.up, snapshot.Up);
			Assert.AreEqual(transformA.eulerAngles, snapshot.WorldEulerAngles);
			Assert.AreEqual(transformA.position, snapshot.WorldPosition);
			Assert.AreEqual(transformA.rotation, snapshot.WorldRotation);
			Assert.AreEqual(transformA.lossyScale, snapshot.WorldScale);
		}
	}
}