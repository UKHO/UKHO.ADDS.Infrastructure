﻿using System.Threading.Tasks;

namespace UKHO.ADDS.Infrastructure.Pipelines.Contexts
{
    /// <summary>
    ///     A block for creating a small bit of reusable shouldexecute logic.
    /// </summary>
    /// <typeparam name="T">Type of the node that this block corresponds to.</typeparam>
    public class ShouldExecuteBlock<T> : IShouldExecuteBlock<T>
    {
        /// <summary>
        ///     Determines if the current node should execute.
        /// </summary>
        /// <param name="context">Current ExecutionContext</param>
        /// <returns>Bool indicating if this node should run.</returns>
        public virtual Task<bool> ShouldExecuteAsync(IExecutionContext<T> context) => Task.FromResult(true);
    }
}
