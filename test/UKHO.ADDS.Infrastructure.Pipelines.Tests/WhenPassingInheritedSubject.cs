﻿using FluentAssertions;
using UKHO.ADDS.Infrastructure.Pipelines.Contexts;
using UKHO.ADDS.Infrastructure.Pipelines.Nodes;
using Xunit;

namespace UKHO.ADDS.Infrastructure.Pipelines.Tests
{
    public class WhenPassingInheritedSubject
    {
        [Fact]
        public async Task ExecutionContext_Based_On_Inherited_Type_Is_Passed_To_Execute()
        {
            var testNode = new SimpleTestNodeA1();

            var testObject = new TestObjectASub();

            ExecutionContext<TestObjectASub> context = new ExecutionContext<TestObjectASub>(testObject);

            var result = await testNode.ExecuteAsync(context);

            testNode.Status.Should().Be(NodeRunStatus.Completed);
            result.Status.Should().Be(NodeResultStatus.Succeeded);
        }

        [Fact]
        public async Task ExecutionContext_Based_On_Root_Type_Works_With_Inherited_Type()
        {
            var testNode = new SimpleTestNodeA1();

            var testObject = new TestObjectASub();

            ExecutionContext<TestObjectA> context = new ExecutionContext<TestObjectA>(testObject);

            var result = await testNode.ExecuteAsync(context);

            testNode.Status.Should().Be(NodeRunStatus.Completed);
            result.Status.Should().Be(NodeResultStatus.Succeeded);
        }

        [Fact]
        public async Task Subject_Of_Inherited_Type_Is_Passed_To_Execute()
        {
            var testNode = new SimpleTestNodeA1();

            var testObject = new TestObjectASub();

            var result = await testNode.ExecuteAsync(testObject);

            testNode.Status.Should().Be(NodeRunStatus.Completed);
            result.Status.Should().Be(NodeResultStatus.Succeeded);
        }

        [Fact]
        public async Task Subject_Of_Inherited_Type_Works_With_Func_Node()
        {
            FuncNode<TestObjectA> node = new FuncNode<TestObjectA>();

            node.ShouldExecuteFunc = context => Task.FromResult(((TestObjectA)context.Subject).TestValueInt == 0);
            node.ExecutedFunc = context =>
            {
                context.Subject.TestValueString = "Completed";
                return Task.FromResult(NodeResultStatus.Succeeded);
            };

            var testObject = new TestObjectASub();
            var result = await node.ExecuteAsync(testObject);

            node.Status.Should().Be(NodeRunStatus.Completed);
            result.Status.Should().Be(NodeResultStatus.Succeeded);
            result.GetSubjectAs<TestObjectA>().TestValueString.Should().Be("Completed");
        }
    }
}
