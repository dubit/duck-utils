using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace DUCK.Utils.Tests
{
	[TestFixture]
	public class TransformExtensionsTests
	{
		private Transform transform;
		private readonly List<Transform> children = new List<Transform>();

		[SetUp]
		public void SetUp()
		{
			transform = new GameObject().transform;
			children.Clear();

			for (var i = 0; i < 4; i++)
			{
				var child = new GameObject().transform;
				child.SetParent(transform);
				children.Add(child);
			}

			transform.GetChild(1).gameObject.SetActive(false);
		}

		[TearDown]
		public void TearDown()
		{
			Object.DestroyImmediate(transform.gameObject);
		}

		[Test]
		public void ExpectForEachToCallTheFunctionForEveryChild()
		{
			var childrenToCall = children.ToList();
			transform.ForEach(child =>
			{
				childrenToCall.Remove(child);
			});

			Assert.IsEmpty(childrenToCall);
		}

		[Test]
		public void ExpectResetToResetWorldPositionRotationAndScale()
		{
			transform.position = new Vector3(1, 2, 3);
			transform.Reset();

			Assert.AreEqual(Vector3.zero, transform.position);
			Assert.AreEqual(Quaternion.identity, transform.rotation);
			Assert.AreEqual(Vector3.one, transform.localScale);
		}

		[Test]
		public void ExpectResetToResetLocalPositionRotationAndScale()
		{
			transform.position = new Vector3(1, 2, 3);
			transform.Reset(true);

			Assert.AreEqual(Vector3.zero, transform.localPosition);
			Assert.AreEqual(Quaternion.identity, transform.localRotation);
			Assert.AreEqual(Vector3.one, transform.localScale);
		}
	}
}