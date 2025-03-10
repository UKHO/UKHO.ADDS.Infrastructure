﻿using FluentAssertions;
using UKHO.ADDS.Infrastructure.Pipelines.Contexts;
using UKHO.ADDS.Infrastructure.Pipelines.Nodes;
using Xunit;

namespace UKHO.ADDS.Infrastructure.Pipelines.Tests
{
    public class WhenRunningGroupNode
    {
        [Fact]
        public async Task Successful_Group_Run_Status_Is_Succeeded()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA>();

            groupNode.AddChild(new SimpleTestNodeA1());
            groupNode.AddChild(new SimpleTestNodeA2());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Group_Run_With_Failure_Returns_Failed_Status()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA>();

            groupNode.AddChild(new SimpleTestNodeA1());
            groupNode.AddChild(new FailingTestNodeA());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Failed);
            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Group_Run_With_Failure_And_ContinueOnError_Returns_SucceededWithErrors_Status()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA> { LocalOptions = new ExecutionOptions { ContinueOnFailure = true } };

            groupNode.AddChild(new SimpleTestNodeA1());
            groupNode.AddChild(new FailingTestNodeA());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.SucceededWithErrors);
            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Group_Run_With_Fault_Returns_Failed_Status()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA>();

            groupNode.AddChild(new SimpleTestNodeA1());
            groupNode.AddChild(new FaultingTestNodeA());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Failed);
            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Group_Run_With_Fault_And_ContinueOnError_Returns_SucceededWithErrors_Status()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA> { LocalOptions = new ExecutionOptions { ContinueOnFailure = true } };

            groupNode.AddChild(new SimpleTestNodeA1());
            groupNode.AddChild(new FaultingTestNodeA());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.SucceededWithErrors);
            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Failed_Group_Run_Returns_Failed_Status()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA>();

            groupNode.AddChild(new FailingTestNodeA());
            groupNode.AddChild(new FailingTestNodeA());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Failed);
            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Faulted_Group_Run_Returns_Failed_Status()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA>();

            groupNode.AddChild(new FaultingTestNodeA());
            groupNode.AddChild(new FaultingTestNodeA());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Failed);
            groupNode.Status.Should().Be(NodeRunStatus.Completed);
        }

        [Fact]
        public async Task Successful_Group_Result_Matches_Expectations()
        {
            GroupNode<TestObjectA> groupNode = new GroupNode<TestObjectA>();

            groupNode.AddChild(new SimpleTestNodeA1());
            groupNode.AddChild(new SimpleTestNodeA2());

            var testObject = new TestObjectA();
            var result = await groupNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Succeeded);
            testObject.TestValueString.Should().Be("Completed");
            testObject.TestValueInt.Should().Be(100);
        }
    }
}
