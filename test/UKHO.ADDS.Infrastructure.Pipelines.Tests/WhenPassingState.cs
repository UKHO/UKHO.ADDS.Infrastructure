﻿using FluentAssertions;
using UKHO.ADDS.Infrastructure.Pipelines.Contexts;
using UKHO.ADDS.Infrastructure.Pipelines.Nodes;
using Xunit;

namespace UKHO.ADDS.Infrastructure.Pipelines.Tests
{
    public class WhenPassingState
    {
        [Fact]
        public async Task Adding_State_To_A_Node_Is_Available_In_Following_Node()
        {
            PipelineNode<TestObjectA> pipelineNode = new PipelineNode<TestObjectA>();

            pipelineNode.AddChild(new SimpleTestNodeA1());
            pipelineNode.AddChild(new FuncNode<TestObjectA>
            {
                ExecutedFunc = ctxt =>
                {
                    ctxt.State.Foo = "Bar";
                    return Task.FromResult(NodeResultStatus.Succeeded);
                }
            });
            pipelineNode.AddChild(new FuncNode<TestObjectA>
            {
                ExecutedFunc = ctxt =>
                    ctxt.State.Foo == "Bar"
                        ? Task.FromResult(NodeResultStatus.Succeeded)
                        : Task.FromResult(NodeResultStatus.Failed)
            });

            var testObject = new TestObjectA();
            var result = await pipelineNode.ExecuteAsync(testObject);
            result.Status.Should().Be(NodeResultStatus.Succeeded);
        }

        [Fact]
        public async Task Adding_State_To_A_Node_Is_Available_In_Global_Context()
        {
            PipelineNode<TestObjectA> pipelineNode = new PipelineNode<TestObjectA>();

            pipelineNode.AddChild(new SimpleTestNodeA1());
            pipelineNode.AddChild(new FuncNode<TestObjectA>
            {
                ExecutedFunc = ctxt =>
                {
                    ctxt.State.Foo = "Bar";
                    return Task.FromResult(NodeResultStatus.Succeeded);
                }
            });

            var testObject = new TestObjectA();
            ExecutionContext<TestObjectA> context = new ExecutionContext<TestObjectA>(testObject);
            var result = await pipelineNode.ExecuteAsync(context);
            result.Status.Should().Be(NodeResultStatus.Succeeded);

            Assert.Equal("Bar", context.State.Foo);
        }

        [Fact]
        public void Accessing_Nonexistent_State_Returns_Null()
        {
            var testObject = new TestObjectA();
            ExecutionContext<TestObjectA> context = new ExecutionContext<TestObjectA>(testObject);

            object result = context.State.NonexistentProperty;

            result.Should().BeNull();
        }
    }
}
