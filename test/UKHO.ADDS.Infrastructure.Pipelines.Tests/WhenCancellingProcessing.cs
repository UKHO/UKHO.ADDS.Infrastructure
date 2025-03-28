﻿using FluentAssertions;
using UKHO.ADDS.Infrastructure.Pipelines.Nodes;
using Xunit;

namespace UKHO.ADDS.Infrastructure.Pipelines.Tests
{
    public class WhenCancellingProcessing
    {
        [Fact]
        public async Task When_Cancelling_Pipeline_At_First_Node_Then_Status_Is_NotRun()
        {
            PipelineNode<TestObjectA> pipelineNode = new PipelineNode<TestObjectA>();

            var testNode1 = new SimpleTestNodeA1(true, false, true);
            var testNode2 = new SimpleTestNodeA2();

            pipelineNode.AddChild(testNode1);
            pipelineNode.AddChild(testNode2);

            var testObject = new TestObjectA();

            var result = await pipelineNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.NotRun);
            pipelineNode.Status.Should().Be(NodeRunStatus.Completed);

            testNode1.Status.Should().Be(NodeRunStatus.NotRun);
            testNode2.Status.Should().Be(NodeRunStatus.NotRun);
        }

        [Fact]
        public async Task When_Cancelling_Pipeline_At_Later_Node_Then_Status_Is_Success()
        {
            PipelineNode<TestObjectA> pipelineNode = new PipelineNode<TestObjectA>();

            var testNode1 = new SimpleTestNodeA1();
            var testNode2 = new SimpleTestNodeA1(true, false, true);
            var testNode3 = new SimpleTestNodeA2();

            pipelineNode.AddChild(testNode1);
            pipelineNode.AddChild(testNode2);
            pipelineNode.AddChild(testNode3);

            var testObject = new TestObjectA();

            var result = await pipelineNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Succeeded);
            pipelineNode.Status.Should().Be(NodeRunStatus.Completed);

            testNode1.Status.Should().Be(NodeRunStatus.Completed);
            testNode2.Status.Should().Be(NodeRunStatus.NotRun);
            testNode3.Status.Should().Be(NodeRunStatus.NotRun);
        }

        [Fact]
        public async Task Parent_Pipeline_Cancels_Execution_When_Child_Pipeline_Node_Cancelled()
        {
            var testNode1 = new SimpleTestNodeA1();
            var testNode2 = new SimpleTestNodeA1(true, false, true);
            var testNode3 = new SimpleTestNodeA2();

            PipelineNode<TestObjectA> innerPipelineNode = new PipelineNode<TestObjectA>();
            innerPipelineNode.AddChild(testNode1);
            innerPipelineNode.AddChild(testNode2);

            PipelineNode<TestObjectA> pipelineNode = new PipelineNode<TestObjectA>();
            pipelineNode.AddChild(innerPipelineNode);
            pipelineNode.AddChild(testNode3);

            var testObject = new TestObjectA();

            var result = await pipelineNode.ExecuteAsync(testObject);

            result.Status.Should().Be(NodeResultStatus.Succeeded);

            innerPipelineNode.Status.Should().Be(NodeRunStatus.Completed);
            testNode1.Status.Should().Be(NodeRunStatus.Completed);
            testNode2.Status.Should().Be(NodeRunStatus.NotRun);
            pipelineNode.Status.Should().Be(NodeRunStatus.Completed);
            testNode3.Status.Should().Be(NodeRunStatus.NotRun);
        }
    }
}
