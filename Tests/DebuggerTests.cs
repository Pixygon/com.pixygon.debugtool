using NUnit.Framework;
using Pixygon.DebugTool;
using UnityEngine;

namespace Pixygon.DebugTool.Tests {
    public class DebuggerTests {
        [Test]
        public void NewTestScriptSimplePasses() {
            Log.DebugMessage(DebugGroup.Addressable, "This is a test");
            Log.DebugMessage(DebugGroup.Tablet, "This is a test");
            Log.DebugMessage(DebugGroup.Application, "This is a test");
            Log.DebugMessage(DebugGroup.Avatar, "This is a test");
            Log.DebugMessage(DebugGroup.Actor, "This is a test");
            Log.DebugMessage(DebugGroup.Nft, "This is a test");
            Log.DebugMessage(DebugGroup.Property, "This is a test");
            Log.DebugMessage(DebugGroup.Camera, "This is a test");
            Log.DebugMessage(DebugGroup.PixygonApi, "This is a test");
            Log.DebugMessage(DebugGroup.PixygonMicro, "This is a test");
            var p = Resources.Load<DebugProfiles>("DebugProfiles");
            Assert.That(p != null);
            Assert.That(p._addressable != null);
            Assert.That(p._tablet != null);
            Assert.That(p._application != null);
            Assert.That(p._avatar != null);
            Assert.That(p._actor != null);
            Assert.That(p._nft != null);
            Assert.That(p._property != null);
            Assert.That(p._camera != null);
            Assert.That(p._pixygonApi != null);
            Assert.That(p._pixygonMicro != null);
        }
    }
}