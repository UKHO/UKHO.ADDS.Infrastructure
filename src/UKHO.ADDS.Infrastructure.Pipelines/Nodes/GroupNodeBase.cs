﻿using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace UKHO.ADDS.Infrastructure.Pipelines.Nodes
{
    /// <summary>
    ///     Class from which to derive custom group nodes.
    ///     This node runs all children potentially simultaneously using Async's WhenAll.
    ///     This is a good choice for multiple i/o operations.  The node will not complete until all children complete.
    /// </summary>
    public abstract class GroupNodeBase<T> : MultiNode<T>, IGroupNodeBase<T>
    {
        /// <summary>
        ///     Executes child nodes of the current node.
        /// </summary>
        /// <param name="context">Current ExecutionContext.</param>
        /// <returns>NodeResultStatus representing the current node result.</returns>
        protected override async Task<NodeResultStatus> ExecuteChildrenAsync(IExecutionContext<T> context)
        {
            var aggregateTask = Task.WhenAll(Children.Select(x => x.ExecuteAsync(context)));
            NodeResult[] results;

            try
            {
                Log.Debug("Running all child nodes asynchronously.");
                results = await aggregateTask.ConfigureAwait(false);
            }
            catch
            {
                if (aggregateTask.Exception != null)
                {
                    throw aggregateTask.Exception;
                }

                throw;
            }

            return results.AggregateNodeResults(GetEffectiveOptions(context.GlobalOptions));
        }
    }
}
