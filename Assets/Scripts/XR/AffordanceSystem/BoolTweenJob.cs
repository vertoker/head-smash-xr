using Unity.Jobs;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Jobs;

namespace XR.AffordanceSystem
{
    /// <summary>
    /// Tween job implementation for tweening Bool values. (hahaha shit code)
    /// </summary>
#if BURST_PRESENT
    [BurstCompile]
#endif
    public struct BoolTweenJob : ITweenJob<bool>
    {
        /// <inheritdoc/>
        public TweenJobData<bool> jobData { get; set; }

        /// <summary>
        /// Perform work on a worker thread.
        /// </summary>
        /// <seealso cref="IJob.Execute"/>
        public void Execute()
        {
            var stateTransitionAmount = jobData.nativeCurve.Evaluate(jobData.stateTransitionAmountFloat);
            var newTargetValue = Lerp(jobData.stateOriginValue, jobData.stateTargetValue, stateTransitionAmount);

            var outputData = jobData.outputData;
            outputData[0] = Lerp(jobData.tweenStartValue, newTargetValue, jobData.tweenAmount);
        }
        
        public bool Lerp(bool from, bool to, float t)
        {
            return t > 0.5f ? to : from;
        }
        public bool IsNearlyEqual(bool from, bool to)
        {
            return from == to;
        }
    }
}