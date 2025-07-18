using Unity.Jobs;
using Unity.XR.CoreUtils;
using Unity.XR.CoreUtils.Bindings.Variables;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Jobs;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Theme;

namespace XR.AffordanceSystem
{
    [AddComponentMenu("Affordance System/Receiver/Primitives/Bool Affordance Receiver", 13)]
    public class BoolAffordanceReceiver : BaseAsyncAffordanceStateReceiver<bool>
    {
        [SerializeField]
        [Tooltip("Bool Affordance Theme datum property used to map affordance state to a bool affordance value. Can store an asset or a serialized value.")]
        BoolAffordanceThemeDatumProperty m_AffordanceThemeDatum;

        /// <summary>
        /// Affordance theme datum property used as a template for creating the runtime copy used during initialization.
        /// </summary>
        /// <remarks>
        /// This value can be bypassed by directly setting the <see cref="BaseAffordanceStateReceiver{T}.affordanceTheme"/> at runtime.
        /// </remarks>
        /// <seealso cref="defaultAffordanceTheme"/>
        public BoolAffordanceThemeDatumProperty affordanceThemeDatum
        {
            get => m_AffordanceThemeDatum;
            set => m_AffordanceThemeDatum = value;
        }

        [SerializeField]
        [Tooltip("The event that is called when the current affordance value is updated.")]
        BoolUnityEvent m_ValueUpdated;

        /// <summary>
        /// The event that is called when the current affordance value is updated.
        /// </summary>
        public BoolUnityEvent valueUpdated
        {
            get => m_ValueUpdated;
            set => m_ValueUpdated = value;
        }

        /// <inheritdoc/>
        protected override BaseAffordanceTheme<bool> defaultAffordanceTheme => m_AffordanceThemeDatum != null ? m_AffordanceThemeDatum.Value : null;

        /// <inheritdoc/>
        protected override BindableVariable<bool> affordanceValue { get; } = new BindableVariable<bool>();

        /// <inheritdoc/>
        protected override JobHandle ScheduleTweenJob(ref TweenJobData<bool> jobData)
        {
            var job = new BoolTweenJob { jobData = jobData };
            return job.Schedule();
        }

        /// <inheritdoc/>
        protected override BaseAffordanceTheme<bool> GenerateNewAffordanceThemeInstance()
        {
            return new BoolAffordanceTheme();
        }

        /// <inheritdoc/>
        protected override void OnAffordanceValueUpdated(bool newValue)
        {
            m_ValueUpdated?.Invoke(newValue);
        }
    }
}